using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loans_Web {
    public partial class Main : System.Web.UI.Page {


        public double Salary;
        public List<Expense> Expenses;
        State[] States = new State[51];

        

        protected void Page_Load(object sender, EventArgs e) {

            rptExpenses.ItemCommand += new RepeaterCommandEventHandler(rptExpenses_ItemCommand);
            Expenses = new List<Expense>();
            CreateStates();


            //Fist PageLoad
            if (!IsPostBack) {

                //Setup
                LoadSettings();
                
                if (Session["NewExpense"] != null) {
                    AddExpense();
                }
            }
            //Postbacks
            else {

                LoadExpenses();
                ReadInputs();
            }

            PrintInputs();
            SaveInputs();
            CalculateExpenses();

            rptExpenses.DataSource = Expenses;
            rptExpenses.DataBind();
        }


        private double GetTax() => getState(ddlState.SelectedValue).getTax(Salary);

        //<summary> Sets screen inputs to variable values </summary>
        protected void PrintInputs() => txtSalary.Text = Salary.ToString();


        //Prepare a CSV string from the Expenses list
        private string ExpensesToString() => JsonConvert.SerializeObject(Expenses);


        //<summary> Format/Display calculated values </summary>
        protected void PrintLoansResults(double loanPayment, double totalLoansPaid, int monthsPaid) =>
            txtMonthlyPayment.Text = (loanPayment == 0) ? "N/A" : loanPayment.ToString("C0");


        protected void btnCreateExpense_Click(object sender, EventArgs e) => Server.Transfer("CreateExpense.aspx");


        protected void btnSaveCSV_Click(object sender, EventArgs e) => AllToString();


        private double MonthlyIncome() => (Salary / 12) * (1 - (GetTax() + FederalTax()));



        private void LoadExpenses() {

            if (Session["SavedSettings"] != null) {

                Dictionary<string, object> toLoad = (Dictionary<string, object>)Session["SavedSettings"];
                if (toLoad.ContainsKey("Expenses")) {
                    List<Expense> temp = (List<Expense>)toLoad["Expenses"];
                    Expenses = temp;
                }
            }
        }



        //Reads state, sets ddlState, sets Tax
        private void SetState(string stateAbrv) {

            State toSet = getState(stateAbrv.ToUpper());
            if (toSet == null) return;

            ddlState.SelectedValue = toSet.Name;
            double thisStateTax = toSet.getTax(Salary);
            if (thisStateTax < 0) {
                thisStateTax = 0;
            }
            //GetTax() = thisStateTax;
            PrintTaxInfo();
        }



        private void AddExpense() {

            if (Session["NewExpense"] != null) {

                string toReplaceName = (string)Session["ToReplace"];
                Session.Remove("ToReplace");
                Expense toAdd = (new Expense((Expense)Session["NewExpense"]));

                //If the name already exists
                if (this[toReplaceName] != null) {

                    Expenses[Expenses.IndexOf(this[toReplaceName])] = toAdd;
                }
                else {
                    Expenses.Add(toAdd);
                }

                Session.Remove("NewExpense");
            }
        }



        public void AllToString() {

            string master = "";
            master += Salary.ToString() + ",";
            master += ddlState.SelectedValue.ToString() + ",";
            master += ExpensesToString();
            
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Budget_Export.txt");
            Response.Charset = "";
            Response.ContentType = "text/plain";
            Response.Output.Write(master);
            Response.Flush();
            Response.End();
            //Response.Write(master);
        }



        private void AllFromString(string input) {

            string master = input;
            List<string> stuff = master.Split(',').ToList();
            
            Salary = double.Parse(stuff[0]);
            stuff.RemoveAt(0);
            
            ddlState.SelectedValue = stuff[0];
            stuff.RemoveAt(0);
            
            string savedExpenses = string.Join(",", stuff.ToArray());
            if (savedExpenses != null && savedExpenses != "")
                ExpensesFromString(savedExpenses);
            
            Expenses = (List<Expense>)JsonConvert.DeserializeObject<List<Expense>>(savedExpenses);

            //Display updated Salary details
            PrintInputs();
        }



        private void ExpensesFromString(string fullString) {

            List<Expense> x = (List<Expense>)JsonConvert.DeserializeObject<List<Expense>>(fullString);
            Expenses.Clear();
            Expenses = new List<Expense>(x);
        }



        //Stores Inputs
        //Stores Expenses
        //Saves Calculated JSON
        private void SaveInputsAndExpenses() {
            SaveInputs();
            SaveExpenses();
        }



        private void SaveInputs() {

            //Store Inputs
            Dictionary<string, object> toSave = new Dictionary<string, object>();
            toSave.Add("Salary", Salary);
            toSave.Add("State", ddlState.SelectedValue);
            Session["SavedSettings"] = toSave;
        }



        private void SaveExpenses() {

            //Store Inputs
            Dictionary<string, object> toSave = new Dictionary<string, object>();

            if (Session["SavedSettings"] != null) {
                toSave = (Dictionary<string, object>)Session["SavedSettings"];
            }

            //Store Expenses for reloading
            if (toSave.ContainsKey("Expenses")) {    
                toSave["Expenses"] = Expenses;
            }
            else {
                toSave.Add("Expenses", Expenses);
            }

            //Store Expenses JSON for Charting
            SaveExpenseJSON();

            Session["SavedSettings"] = toSave;
        }



        //Loads Global variables from Session["SavedSettings"]
        //Load Expenses from Session's toAdd list
        private void LoadSettings() {

            if (Session["SavedSettings"] != null) {

                Dictionary<string, object> toLoad = (Dictionary<string, object>)Session["SavedSettings"];

                Salary = (double)toLoad["Salary"];
                SetState((string)toLoad["State"]);

                //Load Expenses from Session
                if (toLoad.ContainsKey("Expenses")) {
                    Expenses = new List<Expense>((List<Expense>)toLoad["Expenses"]);
                }

                //Delete save version
                Session.Remove("SavedSettings"); 
                
            }
            else {
                Set_Defaults();
            }
        }



        private void SaveExpenseJSON() {

            string jsonExpenses = JsonConvert.SerializeObject(Expenses);
            Session.Remove("Expenses");
            Session["Expenses"] = jsonExpenses;
        }



        public Expense this[string name] {

            get {
                foreach (Expense e in this.Expenses) {

                    if (e.Name == name) {
                        return e;
                    }
                }
                return null;
            }
            set {
                int i = -1;
                foreach (Expense e in this.Expenses) {

                    if (e.Name == name) {
                        i++;
                        break;
                    }
                }

                if (i > -1) {
                    Expenses[i] = value;
                }
            }
        }



        private void Set_Defaults() {

            //Set Salary
            txtSalary.Text = "60000";
            Salary = 60000;

            //Set State/Tax
            SetState("MA");

            if (Expenses == null) MsgBox("Expenses initialization failed", this.Page, this);   
        }



        public void Read_Salary() {

            double temp = -1;
            double.TryParse(txtSalary.Text, out temp);
            if (temp > -1) {
                Salary = temp;
            }
        }

        

        public void ReadInputs() {
            //Read & Display Salary
            Read_Salary();
            
            //Set Tax based off State & Salary
            SetState(ddlState.SelectedValue);
        }
        


        //Increments all expenses
        private double IncrementAllExpenses(double available, DateTime today) {

            double availableLeft = available;

            foreach (Expense e in Expenses) {

                if (e.StartDate <= today  &&  (e.EndDate == null || today <= e.EndDate)) {

                    if (e.CurrentAmount > 0) {
                        availableLeft -= e.IncrementMonth(available, today);

                        if (availableLeft < 0) {
                            e.overBudget = true;
                            MsgBox(e.Name + " put you over-budget by $" + (-1 * availableLeft) + " in " + today.ToString("MMMM") + " of " + today.Year.ToString(), this.Page, this);
                            return availableLeft;
                        }
                    }
                }
            }

            return availableLeft;
        }




        //An improved version of the Calc function that performs the actual logic
        protected void CalculateExpenses() {

            //Reset Expense Amounts and Times, and Dates
            ZeroExpenses();

            //Log Results
            List<string> xLabels = new List<string>();
            List<Expense.monthArgs> unspentData = new List<Expense.monthArgs>();

            //Track time internally
            DateTime timer = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            //Store function results for efficiency
            double monthlyAvailable = MonthlyIncome();
            
            //While Expenses remain, or there are outstanding Loans
            while (!ExpensesFinished(timer)) {

                //"Progress time"
                try {
                    timer = timer.AddMonths(1);
                }
                catch {
                    MsgBox("Yikes, you're at the end of time...", this.Page, this);
                    OverBudget();
                    return;
                }

                double leftThisMonth = monthlyAvailable;
                

                //Expense logic
                leftThisMonth = IncrementAllExpenses(leftThisMonth, timer);


                //If Over-Budget
                if (leftThisMonth < 0) {

                    OverBudget();
                    break;
                }
                else {
                    //Log On-Budget Month
                    xLabels.Add(timer.ToString("MM/yyyy"));
                    unspentData.Add(new Expense.monthArgs(timer.ToString("MM/yyyy"), Math.Truncate(leftThisMonth)));
                }
            }

            rptExpenses.DataSource = Expenses;
            rptExpenses.DataBind();
            
            //Store Expenses
            SaveExpenses();

            //Store Unspent
            string unspentJSON = JsonConvert.SerializeObject(unspentData);
            Session["unspentData"] = unspentJSON;
        }



        private void rptExpenses_ItemCommand(object source, RepeaterCommandEventArgs e) {

            string func = e.CommandName;
            string id = e.CommandArgument.ToString();

            if (func == "Edit") {

                //SaveInputsAndExpenses();
                SaveExpenses();

                //Store this Expense in Session
                Session["EditExpense"] = this[id];

                //Send to Management page
                Server.Transfer("CreateExpense.aspx");

            }
            else if (func == "Delete") {

                //Delete Expense
                Expenses.Remove(this[id]);
                CalculateExpenses();
            }
            
            rptExpenses.DataBind();
        }



        public void OverBudget() {
            txtTimeToPay.Text = "Over Budget";
            txtMonthlyPayment.Text = "Over Budget";
            txtTotalPaid.Text = "Over Budget";
        }




        public bool ExpensesFinished(DateTime today) {

            foreach (Expense e in Expenses) {

                if (!e.recurring && e.CurrentAmount > 0)
                    return false;

                bool endDateSet = e.EndDate != null;
                if (e.recurring  &&  e.StartDate < today  &&  endDateSet  && today < e.EndDate)
                    return false;

            }
            return true;
        }



        public void ZeroExpenses() {

            if (Expenses == null) {
                MsgBox("Expenses null when Zeroed", this.Page, this);
                return;
            }
            
            foreach (Expense ex in Expenses) {

                if (ex.Payments.Count > 0) ex.Payments = new List<Expense.monthArgs>();

                ex.overBudget = false;
                ex.Time = new int[2];
                ex.CurrentAmount = ex.Amount;
            }
        }



        private double FederalTax() {
            if (Salary <= 9875) {
                return .1;
            }

            if (Salary <= 40125) {
                return .12;
            }

            if (Salary <= 85525) {
                return .22;
            }

            if (Salary <= 163301) {
                return .24;
            }

            if (Salary <= 207350) {
                return .32;
            }

            if (Salary <= 518400) {
                return .35;
            }

            if (Salary > 518400) {
                return .37;
            }

            return -1;
        }



        private int GetExpenseByName(string name) {
            int index = 0;

            foreach (Expense e in Expenses) {
                if (e.Name == name) {
                    return index;
                }

                index++;
            }
            return -1;
        }



        public static void Swap<T>(IList<T> list, int indexA, int indexB) {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }



        //Priority Down
        private void btnPriorityDown_Click(object sender, EventArgs e) {
            //if no Expense is selected, exit
            //if (lstExpenses.SelectedItem == null) return;

            //string name = lstExpenses.SelectedItem.ToString();
            //int index = GetExpenseByName(name);
            int index = -1;

            //if this is the last Expense, exit
            if (index == Expenses.Count - 1) return;

            //if there is a next Expense to swap with
            Swap(Expenses, index, index + 1);
            
        }



        //Priority Up
        private void btnPriorityUp_Click(object sender, EventArgs e) {
            //if no Expense is selected, exit
            //if (lstExpenses.SelectedItem == null) return;

            //string name = lstExpenses.SelectedItem.ToString();
            //int index = GetExpenseByName(name);
            int index = 0;

            //if it is already top priority, exit
            if (index == 0) return;

            Swap(Expenses, index, index - 1);
        }



        public void CreateStates() {
            string[] stateNames = new string[] { "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DC", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY" };

            int i = 0;
            foreach (string s in stateNames) {
                States[i] = new State(s);
                i++;
            }

            FillStates();
        }



        public void FillStates() {
            int index = getStateIndex("MA");

            double[] salaries = new double[] { 0 };
            double[] taxes = new double[] { 5 };
            States[index] = new State("MA", salaries, taxes);

            index = getStateIndex("NH");
            salaries = new double[] { 0 };
            taxes = new double[] { 5 };
            States[index] = new State("NH", salaries, taxes);

            index = getStateIndex("VT");
            salaries = new double[] { 39600, 96000, 200200 };
            taxes = new double[] { 3.35, 6.6, 7.6, 8.75 };
            States[index] = new State("VT", salaries, taxes);

            index = getStateIndex("ME");
            salaries = new double[] { 22200, 52600 };
            taxes = new double[] { 5.8, 6.75, 7.15 };
            States[index] = new State("ME", salaries, taxes);

            index = getStateIndex("RI");
            salaries = new double[] { 65250, 148350 };
            taxes = new double[] { 3.75, 4.75, 5.99 };
            States[index] = new State("RI", salaries, taxes);

            index = getStateIndex("NY");
            salaries = new double[] { 8500, 11700, 13900, 21400, 80650, 215400, 1077550 };
            taxes = new double[] { 4, 4.5, 5.25, 5.9, 6.21, 6.49, 6.85, 8.82 };
            States[index] = new State("NY", salaries, taxes);

            index = getStateIndex("PA");
            salaries = new double[] { 0 };
            taxes = new double[] { 3.7 };
            States[index] = new State("PA", salaries, taxes);

            index = getStateIndex("CT");
            salaries = new double[] { 10000, 50000, 100000, 200000, 250000, 500000 };
            taxes = new double[] { 3, 5, 5.5, 6, 6.5, 6.9, 6.99 };
            States[index] = new State("CT", salaries, taxes);

            index = getStateIndex("CA");
            salaries = new double[] { 8809, 20883, 32960, 45753, 57824, 295373, 354445, 590742, 1000000 };
            taxes = new double[] { 1, 2, 4, 6, 8, 9.3, 10.3, 11.3, 12.3, 13.3 };
            States[index] = new State("CA", salaries, taxes);

            index = getStateIndex("WA");
            salaries = new double[] { 0 };
            taxes = new double[] { 0 };
            States[index] = new State("WA", salaries, taxes);

            index = getStateIndex("TX");
            salaries = new double[] { 0 };
            taxes = new double[] { 0 };
            States[index] = new State("TX", salaries, taxes);

            index = getStateIndex("FL");
            salaries = new double[] { 0 };
            taxes = new double[] { 0 };
            States[index] = new State("FL", salaries, taxes);
        }


        //<summary> Iterates over States to find state name </summary>
        public State getState(string name) {
            int i = 0;

            foreach (State st in States) {
                if (st != null  &&  st.Name == name) {
                    return st;
                }
                i++;
            }
            return null;
        }



        public int getStateIndex(string name) {
            int i = 0;

            foreach (State st in States) {
                if (st.Name == name) {
                    return i;
                }
                i++;
            }
            return -1;
        }




        private void PrintTaxInfo() {
            
            txtStateTax.Text = (GetTax() * 100).ToString() + "%";
            txtFederalTax.Text = (FederalTax() * 100).ToString() + "%";
        }



        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e) {
            
            if (getState(ddlState.SelectedValue).getTax(Salary) == -1)
                MsgBox(ddlState.SelectedItem + "'" + "s tax rates are unknown, using 0%", this.Page, this);   
        }



        public void MsgBox(String ex, Page pg, Object obj) {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }

        
        protected void btnReadCSV_Click(object sender, EventArgs e) {

            if (uplExpenses.HasFile) {

                // get the file stream in a readable way
                StreamReader reader = new StreamReader(uplExpenses.PostedFile.InputStream);

                AllFromString(reader.ReadToEnd());
                SaveInputsAndExpenses();
                
                //Trigger postback to Calc with newly loaded data
                StringBuilder sbScript = new StringBuilder();
                sbScript.Append("<script language='JavaScript' type='text/javascript'>\n");
                sbScript.Append("<!--\n");
                sbScript.Append(this.GetPostBackEventReference(this, "PBArg") + ";\n");
                sbScript.Append("// -->\n");
                sbScript.Append("</script>\n");
                this.RegisterStartupScript("AutoPostBackScript", sbScript.ToString());
            }
        }



        //<summary> Populates Expense display in repeater </summary>
        protected void rptExpenseBound(object sender, RepeaterItemEventArgs e) {

            try {

                //Found objects
                System.Web.UI.HtmlControls.HtmlGenericControl divDataRow = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divExpenseData");

                Expense thisEx = (Expense)e.Item.DataItem;
                bool recurring = thisEx.recurring;
                bool overBudget = thisEx.overBudget;
                int paymentCount = thisEx.Payments.Count;

                //Generate Time div
                Label lblTime = new Label();
                divDataRow.Controls.Add(lblTime);


                if (recurring) {

                    //Populate fields
                    lblTime.Text = "Payments: " + paymentCount;
                }
                else {

                    //Generate ToExpense div
                    Label lblAddToExpense = new Label();
                    divDataRow.Controls.Add(lblAddToExpense);

                    //Populate Fields
                    double toExpense = thisEx.ToExpense * 100;
                    lblAddToExpense.Text = "To Expense(%): " + toExpense.ToString();
                    lblTime.Text = "Time(YY/MM): " + (paymentCount / 12).ToString() + '/' + (paymentCount % 12).ToString();

                    //Add Controls
                    divDataRow.Controls.Add(lblAddToExpense);
                }

                //If over-budget flag set
                if(overBudget) {
                    lblTime.Text = "<span class='text-danger'>OVER BUDGET</span>";
                }

                //Add controls
                divDataRow.Controls.Add(lblTime);
            }
            catch (Exception ex) {
            }
        }


    }
}