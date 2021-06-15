using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loans_Web {
    public partial class Main : System.Web.UI.Page {


        protected void Page_Load(object sender, EventArgs e) {

            //Expense Management Window
            rptExpenses.ItemCommand += new RepeaterCommandEventHandler(rptExpenses_ItemCommand);
            rptExpenses.DataSource = Expenses;


            if (Session["SavedSettings"] != null) {
                LoadSettings();
            }


            if (!IsPostBack) {
                Set_Defaults();

                if (Session["NewExpense"] != null) {

                    string toReplaceName = (string)Session["ToReplace"];
                    Session.Remove("ToReplace");
                    Expense toAdd = (new Expense((Expense)Session["NewExpense"]));

                    //If the name already exists
                    if (this[toReplaceName] != null) {

                        this[toReplaceName] = toAdd;
                    }
                    else {

                        Expenses.Add(toAdd);
                    }

                    Session.Remove("NewExpense");
                }

                btnCalc_Click(sender, e);
            }
            else {

                //Load States
                createStates();
                fillStates();
            }
            

            rptExpenses.DataBind();
            PrintExpenses();
        }



        //Prepare a CSV string from the Expenses list
        private string ExpensesToString() {

            string test = String.Join(";", Expenses.Select(x => x.ToString()).ToArray());
            return test;
        }



        public void AllToString() {

            string master = "";

            master += Salary.ToString() + ",";
            master += Loans.ToString() + ",";
            master += Interest.ToString() + ",";
            master += ToLoans.ToString() + ",";
            master += ddlState.SelectedValue.ToString() + ",";

            master += ExpensesToString();

            //FileStream fs = File.Create("Expense Loadout", 1024, FileOptions.WriteThrough);
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



        private void ExpensesFromString(string fullString) {

            //Split expenses on ';'
            List<string> expenseStringList = fullString.Split(';').ToList();
            List<Expense> expenseList = new List<Expense>();
            
            //for each expense
            foreach (string expense in expenseStringList) {

                //Construct Expense
                Expense toAdd = new Expense();
                toAdd.FromString(expense);

                expenseList.Add(new Expense(toAdd));
            }

            Expenses.Clear();
            Expenses = new List<Expense>(expenseList);
        }



        private void AllFromString(string input) {

            string master = input;
            List<string> stuff = master.Split(',').ToList();

            Salary = double.Parse(stuff[0]);
            stuff.RemoveAt(0);

            Loans = double.Parse(stuff[0]);
            stuff.RemoveAt(0);

            Interest = double.Parse(stuff[0]);
            stuff.RemoveAt(0);

            ToLoans = double.Parse(stuff[0]);
            stuff.RemoveAt(0);

            //ddlState.SelectedValue = stuff[0];
            ddlState.SelectedValue = stuff[0];
            stuff.RemoveAt(0);

            string savedExpenses = string.Join(",", stuff.ToArray());
            if(savedExpenses != null &&  savedExpenses != "")
                ExpensesFromString(savedExpenses);
        }



        protected void Page_Unload(object sender, EventArgs e) {
            SaveSettings();
            SaveExpenses();
        }

       

        private void SaveSettings() {
            Dictionary<string, object> toSave = new Dictionary<string, object>();

            ReadValues();

            toSave.Add("Salary", Salary);
            toSave.Add("Loans", Loans);
            toSave.Add("Interest", Interest);
            toSave.Add("ToLoans", ToLoans);
            toSave.Add("Tax", Tax);
            toSave.Add("Expenses", Expenses);
            //toSave.Add("State", States);

            Session["SavedSettings"] = toSave;
        }



        private void SaveExpenses() {

            string jsonExpenses = JsonConvert.SerializeObject(Expenses);
            Session.Remove("Expenses");
            Session["Expenses"] = jsonExpenses;
        }



        private void LoadSettings() {
            Dictionary<string, object> toLoad = (Dictionary<string, object>)Session["SavedSettings"];

            Salary = (double)toLoad["Salary"];
            Loans = (double)toLoad["Loans"];
            Interest = (double)toLoad["Interest"];
            ToLoans = (double)toLoad["ToLoans"];
            Tax = (double)toLoad["Tax"];
            Expenses = (List<Expense>)toLoad["Expenses"];

            rptExpenses.DataSource = Expenses;
            rptExpenses.DataBind();
        }



        public double Salary;
        public double Loans;
        public double Interest;
        public double ToLoans;
        public double Tax;
        public List<Expense> Expenses = new List<Expense>();
        //public List<Expense.monthArgs> LoanPayments = new List<Expense.monthArgs>();
        State[] States = new State[50];


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
            Read_Salary();

            //Load States
            createStates();
            fillStates();

            //Set Loans
            txtLoans.Text = "40000";
            Read_Loans();

            //Set Interest
            txtLoanInterest.Text = "3";
            Read_Interest();

            //Set ToLoans
            txtToLoans.Text = "40";
            Read_ToLoans();

            //Set Tax
            ddlState.SelectedValue = "MA";
            Read_Tax();

            if (Expenses == null) MsgBox("Expenses initialization failed", this.Page, this);

            RefreshExpenses();
        }



        public void Read_Salary() {

            double temp = -1;
            double.TryParse(txtSalary.Text, out temp);
            if (temp > -1) {

                Salary = temp;
            }

            txtSalary.Text = Salary.ToString("0");
        }



        private void txtLoans_Leave(object sender, EventArgs e) {
            Read_Loans();
            //btnCalc_Click(sender, e);
        }



        private void Read_Loans() {
            double save;
            if (double.TryParse(txtLoans.Text, out save))
                Loans = save;
            else {
                Loans = 0;
                MsgBox("Loans could not be read.", this.Page, this);
            }

            //Display Loans
            txtLoans.Text = Loans.ToString("");
        }



        private void txtInterest_Leave(object sender, EventArgs e) {
            Read_Interest();
            //btnCalc_Click(sender, e);
        }



        private void Read_Interest() {
            double save;
            if (double.TryParse(txtLoanInterest.Text, out save))
                Interest = save / 100;
            else {
                Interest = 0;
                MsgBox("Interest could not be read.", this.Page, this);
            }
            txtLoanInterest.Text = (Interest * 100).ToString();
        }



        private void txtToLoans_Leave(object sender, EventArgs e) {
            Read_ToLoans();
            //btnCalc_Click(sender, e);
        }



        private void Read_ToLoans() {
            double save;
            if (double.TryParse(txtToLoans.Text, out save))
                ToLoans = save / 100;
            else {
                ToLoans = 0;
                MsgBox("ToLoans could not be read.", this.Page, this);
            }
            txtToLoans.Text = (ToLoans * 100).ToString();
        }



        private void txtTax_Leave(object sender, EventArgs e) {
            Read_Tax();
        }



        private void Read_Tax() {
            State read = new State();

            //if state was typed in
            if (ddlState.SelectedIndex == -1) {
                //search for state by name
                string search = ddlState.SelectedValue;
                search = search.ToUpper();
                if (getState(search) != null) {
                    read = getState(search);
                }
                else {
                    MsgBox("State not found.<br/>Use the 2 letter abbreviation.", this.Page, this);
                }
            }
            else {
                read = getState(ddlState.SelectedValue);
            }


            if (read.getTax(Salary) == -1) {
                Tax = 0;
            }
            else {
                Tax = read.getTax(Salary);
            }
        }



        public void ReadValues() {
            //Read & Display Salary
            Read_Salary();

            //Read Loans
            Read_Loans();

            //Read ToLoans
            Read_ToLoans();

            //Read Interest
            Read_Interest();

            //Read Tax
            Read_Tax();
        }



        //Determine if a loan payment should be subtracted from the salary
        private bool UseLoans() {

            if (ToLoans <= 0) {
                return false;
            }
            return true;
        }



        //Determine if there are ongoing expenses or an outstanding loan amount
        private bool CheckDone(DateTime today) {

            if (!ExpensesFinished(today)) return false;
            if (Loans <= 0) return true;
            if (!UseLoans()) return true;
            return false;
        }



        //Increments all expenses
        private double IncrementAll(double available, DateTime today) {

            double availableLeft = available;

            foreach (Expense e in Expenses) {

                if (e.StartDate.CompareTo(today) <= 0 && e.EndDate.CompareTo(today) > 0) {

                    if (e.CurrentAmount > 0) {
                        availableLeft -= e.IncrementMonth(available, today);
                        
                        if (availableLeft < 0) {

                            MsgBox(e.Name + " put you over-budget by $" + (-1 * availableLeft) + " in " + today.ToString("MMMM") + " of " + today.Year.ToString(), this.Page, this);
                            return availableLeft;
                        }
                    }
                }
            }

            return availableLeft;
        }


        private void ClearPayments() {

            foreach(Expense e in Expenses) {
                if (e.Payments.Count > 0) e.Payments = new List<Expense.monthArgs>();
            }
        }


        //An improved version of the Calc function that performs the actual logic
        //Design simplified by psuedo-coding on whiteboard
        protected void btnCalc_Click(object sender, EventArgs e) {

            ReadValues();
            //printIncomeInfo();

            
            //Log Results
            List<string> xLabels = new List<string>();
            List<Expense.monthArgs> loanPayments = new List<Expense.monthArgs>();
            List<Expense.monthArgs> unspentData = new List<Expense.monthArgs>();
            double totalLoansPaid = 0;


            //Track time internally
            DateTime timer = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            int monthsPaid = 0;
            

            //Reset Expense Amounts and Times, and Date
            ZeroTimes();
            ClearPayments();

            //Store function results for efficiency
            double monthlyAvailable = MonthlyIncome();
            bool useLoans = UseLoans();
            double loanPayment = CalcLoanPayment();



            //While Expenses remain, or there are outstanding Loans
            while (!CheckDone(timer)) {

                //"Progress time"
                try {
                    timer = timer.AddMonths(1);
                }
                catch {
                    MsgBox("You were in an infinite loop", this.Page, this);
                    return;
                }

                double leftThisMonth = monthlyAvailable;

                //Loan logic
                if (useLoans) {

                    //If Loans are not paid
                    if (1 < Loans) {

                        //If this is last payment
                        if (Loans < loanPayment) {

                            //Only pay the remainder
                            loanPayment = Loans;
                        }
                        else {
                            Loans *= (1 + Interest / 12);
                        }

                        leftThisMonth -= loanPayment;
                        Loans -= loanPayment;
                        totalLoansPaid += loanPayment;

                        //Loan Payment record
                        loanPayments.Add(new Expense.monthArgs(timer.ToString("MM/yyyy"), Math.Truncate(loanPayment)));
                        monthsPaid++;
                    }
                }

                //Expense logic
                leftThisMonth = IncrementAll(leftThisMonth, timer);

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


            //Store Loans
            StoreLoanResults(xLabels, loanPayments, unspentData);

            //Store Expenses
            SaveExpenses();
            

            //Print Results
            PrintLoansResults(loanPayment, totalLoansPaid, monthsPaid);
            RefreshExpenses();
        }


        protected void StoreLoanResults(List<string> xLabels, List<Expense.monthArgs> loanPayments, List<Expense.monthArgs> unspentData) {

            //Store Date-Labels in Order
            string xLabelData = JsonConvert.SerializeObject(xLabels);
            Session["xLabels"] = xLabelData;


            //Store LoanPayments
            if (loanPayments.Count > 0) {

                string loanPaymentJson = JsonConvert.SerializeObject(loanPayments);
                Session.Remove("LoanPayments");
                Session["LoanPayments"] = loanPaymentJson;
            }


            //Store Unspent
            string jsonData = JsonConvert.SerializeObject(unspentData);
            Session["json"] = jsonData;

        }


        protected void PrintLoansResults(double loanPayment, double totalLoansPaid, int monthsPaid) {

            txtMonthlyPayment.Text = loanPayment.ToString("C");
            txtTotalPaid.Text = totalLoansPaid.ToString("C");
            txtTimeToPay.Text = "" + (monthsPaid / 12).ToString() + " / " + (monthsPaid % 12).ToString();
            
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
                btnCalc_Click(source, e);
            }

            rptExpenses.DataBind();
        }





        public void OverBudget() {
            txtTimeToPay.Text = "Over Budget";
            txtMonthlyPayment.Text = "Over Budget";
            txtTotalPaid.Text = "Over Budget";
        }



        public double CalcLoanPayment() {
            if (Loans == 0) {
                return 0;
            }
            return MonthlyIncome() * ToLoans;
        }



        public bool ExpensesFinished(DateTime today) {

            foreach (Expense e in Expenses) {

                if (!e.recurring && e.CurrentAmount > 0)
                    return false;


                bool endDateSet = e.EndDate.Year < DateTime.MaxValue.Year;
                if (e.recurring && e.StartDate.CompareTo(today) < 0 && endDateSet)
                    return false;

            }
            return true;
        }



        public void ZeroTimes() {
            foreach (Expense ex in Expenses) {
                ex.Time = new int[2];
                ex.CurrentAmount = ex.Amount;
            }
        }



        private void btnAddExpense_Click(object sender, EventArgs e) {

            SaveSettings();
            Server.Transfer("CreateExpense.aspx");
        }



        private void PrintExpenses() {
            string text = "Expenses: <br/><br/>";

            if (Expenses != null) {
                foreach (Expense e in Expenses) {
                    if (e != null) {
                        if (e.Name != null) {
                            text += e.PrintExpense(MonthlyIncome() - CalcLoanPayment());
                        }
                    }
                }
            }
            else {
                MsgBox("Expenses was null in func PrintExpenses()", this.Page, this);
            }
        }



        private void LoadExpenses() {

            List<Expense> loadExpenses = new List<Expense>();
            rptExpenses.DataSource = loadExpenses;

            if (Expenses != null) {

                foreach (Expense e in Expenses) {
                    if (e != null) {
                        if (e.Name != null) {
                            loadExpenses.Add(e);
                        }
                    }
                }
            }
            else {
                MsgBox("Expenses was null in func LoadExpenses()", this.Page, this);
            }

            rptExpenses.DataBind();
        }



        private void PrintTimes() {
            string text = "Times:<br/><br/>";

            foreach (Expense e in Expenses) {
                if (e != null) {
                    if (e.Name != null) {
                        if (e.recurring) {
                            if (e.StartDate > DateTime.Today) {
                                text += e.Name + " : " + e.Time[0].ToString() + " / " + e.Time[1].ToString() + "<br/><br/>";
                            }
                            else {
                                text += e.Name + " : Recurring<br/><br/>";
                            }
                        }
                        else if (e.StartDate >= DateTime.Today) {
                            text += e.Name + " : " + e.Time[0].ToString() + " / " + e.Time[1].ToString() + "<br/>" +
                                "Start: " + e.StartDate.ToShortDateString() + "<br/>";

                            if (e.StartDate < e.EndDate && e.EndDate != DateTime.MaxValue) {
                                text += "End: " + e.EndDate.ToShortDateString() + "<br/>";
                            }
                            else {
                                text += "<br/>";
                            }

                        }
                        else {
                            text += e.Name + " : " + e.Time[0].ToString() + " / " + e.Time[1].ToString() + "<br/><br/>";
                        }
                    }
                }
            }

        }

        private void RefreshExpenses() {
            LoadExpenses();  //Fills Expense Management Window
            PrintExpenses(); //Fills Expenses Details Window
            PrintTimes();    //Fills Times Window
        }



        private double MonthlyIncome() {
            return Salary / 12 * (1 - (Tax + FederalTax()));
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

            RefreshExpenses();
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
            RefreshExpenses();
        }



        public void createStates() {
            string[] stateNames = new string[] { "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY" };

            int i = 0;
            foreach (string s in stateNames) {
                States[i] = new State(s);
                //cmbStates.Items.Add(s);
                i++;
            }
        }



        public State getState(string name) {
            int i = 0;

            foreach (State st in States) {
                if (st.Name == name) {
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



        public void fillStates() {
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



        private void printIncomeInfo() {

            txtStateTax.Text = (Tax * 100).ToString() + "%";
            txtFederalTax.Text = (FederalTax() * 100).ToString() + "%";
        }



        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e) {
            string name = ddlState.SelectedValue;
            if (getState(name).getTax(Salary) == -1) {
                MsgBox(ddlState.SelectedItem + "'" + "s tax rates are unknown, using 0%", this.Page, this);
            }
            btnCalc_Click(sender, e);
        }



        private void PrintError() {
            txtTimeToPay.Text = "Over Budget";
            txtMonthlyPayment.Text = "Over Budget";
            txtTotalPaid.Text = "Over Budget";
        }


        public void MsgBox(String ex, Page pg, Object obj) {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }

        protected void btnCreateExpense_Click(object sender, EventArgs e) {

            SaveSettings();
            Server.Transfer("CreateExpense.aspx");

        }

        protected void txtSalary_TextChanged(object sender, EventArgs e) {
            Read_Salary();
        }

        protected void btnSaveCSV_Click(object sender, EventArgs e) {
            AllToString();
        }

        protected void btnReadCSV_Click(object sender, EventArgs e) {

            if (uplExpenses.HasFile) {

                // get the file stream in a readable way
                StreamReader reader = new StreamReader(uplExpenses.PostedFile.InputStream);

                AllFromString(reader.ReadToEnd());

                btnCalc_Click(sender, e);
            }
        }

        protected void rptExpenseBound(object sender, RepeaterItemEventArgs e) {

            try {

                //Found objects
                System.Web.UI.HtmlControls.HtmlGenericControl divDataRow = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divExpenseData");
                
                string[] sa = e.Item.DataItem.ToString().Split(',');
                bool recurring = bool.Parse(sa[3]);
                int paymentCount = sa.Count() - 6;

                //Generate Payment div
                Label lblPayment = new Label();
                divDataRow.Controls.Add(lblPayment);

                //Generate Time div
                Label lblTime = new Label();
                divDataRow.Controls.Add(lblTime);


                if (recurring) {
                    
                    //Populate fields
                    lblPayment.Text = "<h3>Recurring</h3>";
                    lblTime.Text = "Payments: " + paymentCount;

                    //Add controls
                    divDataRow.Controls.Add(lblPayment);
                    divDataRow.Controls.Add(lblTime);
                }
                else {

                    //Generate ToExpense div
                    Label lblAddToExpense = new Label();
                    divDataRow.Controls.Add(lblAddToExpense);

                    //Populate Fields
                    double toExpense = double.Parse(sa[2]) * 100;
                    lblAddToExpense.Text = "To Expense(%): " + toExpense.ToString();
                    lblPayment.Text = "Payment: " + double.Parse(sa[6].Split(':')[1]).ToString("C0");
                    lblTime.Text = "Time(YY/MM): " + (paymentCount / 12).ToString() + '/' + (paymentCount % 12).ToString();

                    //Add Controls
                    divDataRow.Controls.Add(lblAddToExpense);
                    divDataRow.Controls.Add(lblPayment);
                    divDataRow.Controls.Add(lblTime);
                }




            }
            catch (Exception ex) {

            }
        }


    }




}