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


        protected void Page_Load(object sender, EventArgs e) {

            rptExpenses.ItemCommand += new RepeaterCommandEventHandler(rptExpenses_ItemCommand);
            Expenses = new List<Expense>();
            
            LoadSettings(sender, e);
            SaveInputsAndExpenses();
            //CalculateExpenses();

            //rptExpenses.DataSource = Expenses;
            //rptExpenses.DataBind();
        }


        protected void Page_PreRender(object sender, EventArgs e) {
            CalculateExpenses();

            rptExpenses.DataSource = Expenses;
            rptExpenses.DataBind();
        }



        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e) {
            
            if (US51.GetState(ddlState.SelectedValue)[Salary] < 0)
                MsgBox(ddlState.SelectedItem + "'s tax rates are unknown, using 0%", this.Page, this);

            //Only update state info, not federal tax
            txtStateTax.Text = (GetTax()*100).ToString() + "%";
        }


        private double GetTax() => (US51.GetState(ddlState.SelectedValue)[Salary] < 0)? 
            0 : (US51.GetState(ddlState.SelectedValue)[Salary]);
        

        //<summary> Sets screen inputs to variable values </summary>
        protected void PrintSalary() => txtSalary.Text = Salary.ToString();


        //Prepare a CSV string from the Expenses list
        private string ExpensesToString() => JsonConvert.SerializeObject(Expenses);


        //<summary> Format/Display calculated values </summary>
        protected void PrintLoansResults(double loanPayment, double totalLoansPaid, int monthsPaid) =>
            txtMonthlyPayment.Text = (loanPayment == 0) ? "N/A" : loanPayment.ToString("C0");


        protected void btnCreateExpense_Click(object sender, EventArgs e) => Server.Transfer("CreateExpense.aspx");


        protected void btnSaveCSV_Click(object sender, EventArgs e) => AllToString();


        

        private void LoadExpenses() {

            if (Session["SavedSettings"] != null) {

                Dictionary<string, object> toLoad = (Dictionary<string, object>)Session["SavedSettings"];
                if (toLoad.ContainsKey("Expenses"))
                    Expenses = (List<Expense>)toLoad["Expenses"];
            }
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
            string[] dataToParse = input.Split(new[] {'['}, 2);
            string[] inputs = dataToParse[0].Split(',');
            string expensesToLoad =  "[" + dataToParse[1];

            Salary = double.Parse(inputs[0]);
            txtSalary.Text = Salary.ToString();
            
            ddlState.SelectedValue = inputs[1];

            //Display updated Salary details
            PrintSalary();
            PrintTaxInfo();

            Expenses = (List<Expense>)JsonConvert.DeserializeObject<List<Expense>>(expensesToLoad);
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



        //Loads Salary/State and Expenses from Session["SavedSettings"]
        private void LoadSettings(object sender, EventArgs e) {

            //Load State & Expenses
            if (Session["SavedSettings"] != null) {

                Dictionary<string, object> toLoad = (Dictionary<string, object>)Session["SavedSettings"];

                //Load Expenses from Session
                if (toLoad.ContainsKey("Expenses")) {
                    Expenses = new List<Expense>((List<Expense>)toLoad["Expenses"]);
                }

                //Only on FPL
                //Dont override user input
                if (!IsPostBack){
                    double loadedSalary = (double)toLoad["Salary"];
                    txtSalary.Text = loadedSalary.ToString();
                    Salary = loadedSalary;

                    ddlState.SelectedValue = (string)toLoad["State"];

                    //Check for a Created Expense
                    AddExpense();
                }

                //Delete save version, new version saved in SaveInputsAndExpenses();
                Session.Remove("SavedSettings");
            }
            else {
                Set_Defaults();
            }


            Read_Salary();

            //Display According Taxes
            //Needs Salary & State
            PrintTaxInfo();
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

            Salary = 60000;
            txtSalary.Text = Salary.ToString();
            ddlState.SelectedValue = "MA";
            //Might need to call ddlState_SelectedIndexChanged() or something here

            if (Expenses == null) MsgBox("Expenses initialization failed", this.Page, this);
        }



        public void Read_Salary() {

            double temp = double.TryParse(txtSalary.Text, out temp) ? temp : -1;
            if (0 < temp)
                Salary = temp; 
            else
                MsgBox("Invalid Salary input", this.Page, this);
        }

        


        //Increments all expenses
        private double IncrementAllExpenses(double available, DateTime today) {

            double availableLeft = available;

            foreach (Expense e in Expenses) {

                if (e.StartDate <= today && (e.EndDate == null || today <= e.EndDate)) {

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

            DateTime timer = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            double monthlyAvailable = (Salary / 12) * (1 - (GetTax() + FederalTax()));


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
            else if (func == "Up") {
                btnPriorityUp_Click(id);
            }
            else if (func == "Down") {
                btnPriorityDown_Click(id);
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
                if (e.recurring && e.StartDate < today && endDateSet && today < e.EndDate)
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



        public static void Swap<T>(IList<T> list, int indexA, int indexB) {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }



        //Priority Down
        private void btnPriorityDown_Click(string expenseName) {

            Expense e = this[expenseName];

            int index = Expenses.IndexOf(e);
            
            //if this is the last Expense, exit
            if (index == Expenses.Count - 1) return;

            //if there is a next Expense to swap with
            Swap(Expenses, index, index + 1);
        }



        //Priority Up
        private void btnPriorityUp_Click(string expenseName) {

            Expense e = this[expenseName];

            //if no Expense is selected, exit
            if (e == null) return;

            int index = Expenses.IndexOf(e);

            //if it is already top priority, exit
            if (index == 0) return;

            Swap(Expenses, index, index - 1);
        }


        private void PrintTaxInfo() {

            txtStateTax.Text = (GetTax() * 100).ToString() + "%";
            txtFederalTax.Text = (FederalTax() * 100).ToString() + "%";
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
                if (overBudget) {
                    lblTime.Text = "<span class='text-danger'>OVER BUDGET</span>";
                }

                //Add controls
                divDataRow.Controls.Add(lblTime);

                Label lblTotalPaid = new Label();
                lblTotalPaid.Text = "Total Paid: " + thisEx.SumPayments().ToString("C0");
                divDataRow.Controls.Add(lblTotalPaid);
                double interestRatio = thisEx.SumPayments() / thisEx.Amount;
                switch (interestRatio) {
                    case double n when interestRatio < 1.1:
                        lblTotalPaid.CssClass = "text-success";
                        break;
                    case double n when interestRatio < 1.25:
                        lblTotalPaid.ForeColor = System.Drawing.Color.Yellow;
                        break;
                     default:
                        lblTotalPaid.CssClass = "text-danger";
                        break;
                }
                } catch (Exception ex) {
            }
        }

    }
}