using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loans
{
    public partial class frmMain : Form
    {
        public double Salary;
        public double Loans;
        public double Interest;
        public double ToLoans;
        public double Tax;
        public List<Expense> Expenses = new List<Expense>();
        State[] States = new State[50];
        

        private void Set_Defaults()
        {
            //Set Salary
            tbrSalary.Value = 60000;
            Read_Salary();

            //Load States
            createStates();
            fillStates();

            //Set Loans
            txtLoans.Text = "40000";
            Read_Loans();
            
            //Set Interest
            txtInterest.Text = "3";
            Read_Interest();

            //Set ToLoans
            txtToLoans.Text = "40";
            Read_ToLoans();

            //Set Tax
            cmbStates.SelectedIndex = getStateIndex("MA");
            Read_Tax();

            if (Expenses == null)   MessageBox.Show("Expenses initialization failed");

            RefreshExpenses();
        }

        public frmMain()
        {
            InitializeComponent();
            Set_Defaults();
        }



        private void tbrSalary_Scroll(object sender, EventArgs e)
        {
            Read_Salary();
            printIncomeInfo();
            btnCalc_Click(sender, e);
        }



        public void Read_Salary()
        {
            Salary = tbrSalary.Value;
            txtSalary.Text = Salary.ToString("C0");
        }



        private void txtLoans_Leave(object sender, EventArgs e)
        {
            Read_Loans();
            btnCalc_Click(sender, e);
        }



        private void Read_Loans()
        {
            double save;
            if (double.TryParse(txtLoans.Text, out save))
                Loans = save;
            else{
                Loans = 0;
                MessageBox.Show("Loans could not be read.");
            }
            
            //Display Loans
            txtLoans.Text = Loans.ToString("");
        }



        private void txtInterest_Leave(object sender, EventArgs e)
        {
            Read_Interest();
            btnCalc_Click(sender, e);
        }



        private void Read_Interest()
        {
            double save;
            if (double.TryParse(txtInterest.Text, out save))
                Interest = save/100;
            else{
                Interest = 0;
                MessageBox.Show("Interest could not be read.");
            }
            txtInterest.Text = (Interest * 100).ToString();
        }



        private void txtToLoans_Leave(object sender, EventArgs e)
        {
            Read_ToLoans();
            btnCalc_Click(sender, e);
        }



        private void Read_ToLoans()
        {
            double save;
            if (double.TryParse(txtToLoans.Text, out save))
                ToLoans = save / 100;
            else{
                ToLoans = 0;
                MessageBox.Show("ToLoans could not be read.");
            }
            txtToLoans.Text = (ToLoans * 100).ToString();
        }



        private void txtTax_Leave(object sender, EventArgs e)
        {
            Read_Tax();
        }



        private void Read_Tax()
        {
            State read = new State();

            //if state was typed in
            if (cmbStates.SelectedIndex == -1){
                //search for state by name
                string search = cmbStates.Text;
                search = search.ToUpper();
                if (getState(search) != null){
                    read = getState(search);
                }
                else{
                    MessageBox.Show("State not found.\r\nUse the 2 letter abbreviation.");
                }
            }
            else{
                read = States[cmbStates.SelectedIndex];
            }
            
            
            if(read.getTax(Salary) == -1){
                Tax = 0;
            }
            else{
                Tax = read.getTax(Salary);
            }
        }



        public void ReadValues(object sender, EventArgs e)
        {
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
        private string IncrementAll(double available, DateTime today) {

            double availableLeft = available;

            foreach (Expense e in Expenses) {

                if (e.StartDate < today   &&   (e.EndDate == null || today < e.EndDate)) {

                    availableLeft -= e.IncrementMonth(available);

                    if (availableLeft < 0) return e.Name + " put you over-budget by $" + (-1 * availableLeft);
                }
            }

            return null;
        }




        //An improved version of the Calc function that performs the actual logic
        //Design simplified by psuedo-coding on whiteboard
        private void btnCalc_Click(object sender, EventArgs e) {

            ReadValues(sender, e);
            printIncomeInfo();
            
            //Track time internally
            DateTime timer = DateTime.Today;

            //Create Total Loan Cost/Time Trackers
            int monthsPaid = 0;
            double totalLoansPaid = 0;

            //Reset Expense Amounts and Times, and Date
            ZeroTimes();

            //Store function results for efficiency
            double monthlyAvailable = MonthlyIncome();
            bool useLoans = UseLoans();
            double loanPayment = CalcLoanPayment();
            txtPayment.Text = loanPayment.ToString("C");

            if (useLoans) monthlyAvailable -= loanPayment;


            //While Expenses remain, or there are outstanding Loans
            while (!CheckDone(timer)) {

                //"Progress time"
                timer.AddMonths(1);
                monthsPaid++;

                //Loan logic
                if (useLoans) {

                    if (Loans < loanPayment) {
                        loanPayment = Loans;
                    }
                    else {
                        Loans *= (1 + Interest / 12);
                    }
                    monthlyAvailable -= loanPayment;
                    Loans -= loanPayment;
                    totalLoansPaid += loanPayment;

                    
                }
                
                //Expense logic
                string result = IncrementAll(monthlyAvailable, timer);

                //Show over-budget result
                if (result != null) {
                    MessageBox.Show(result + " on " + timer.ToShortDateString());
                    OverBudget();
                }
            }


            txtTotal.Text = totalLoansPaid.ToString("C");
            txtYears.Text = "" + (monthsPaid / 12).ToString() + " / " + (monthsPaid % 12).ToString();
            
            
            RefreshExpenses();
        }



        public void OverBudget()
        {
            txtYears.Text = "Over Budget";
            txtPayment.Text = "Over Budget";
            txtTotal.Text = "Over Budget";
        }



        public double CalcLoanPayment()
        {
            if (Loans == 0){
                return 0;
            }
            return MonthlyIncome() * ToLoans;
        }        



        public bool ExpensesFinished(DateTime today){

            foreach(Expense e in Expenses){

                if (!e.recurring  &&  e.Amount > 0)
                    return false;

                if (e.recurring  &&  (e.EndDate == null  ||  today < e.EndDate))
                    return false;

                /*
                if (!e.recurring) {
                    if (e.CurrentAmount > 0) {
                        if (e.ToExpense > 0) {
                            return false;
                        }
                    }
                }*/

            }
            return true;
        }



        public void ZeroTimes()
        {
            foreach (Expense ex in Expenses){
                ex.Time = new int[2];
                ex.CurrentAmount = ex.Amount;
            }
        }
        


        private void btnAddExpense_Click(object sender, EventArgs e)
        {
            frmNewExpense newExpense = new frmNewExpense(Expenses);
            newExpense.ShowDialog();
            if (newExpense.Tag == null){
                //MessageBox.Show("No expense returned by frmNewExpense");
            }
            else if(newExpense.Tag.GetType().Name == "Expense"){
                Expenses.Add((Expense)newExpense.Tag);
            }
            else if ((int)newExpense.Tag == -1){
                //MessageBox.Show("Expense was canceled");
            }
            else{
                //MessageBox.Show("Unknown response returned by frmNewExpense");
            }

            RefreshExpenses();
            btnCalc_Click(sender, e);
        }

        

        private void PrintExpenses()
        {
            string text = "Expenses: \r\n\r\n";

            if(Expenses != null){
                foreach(Expense e in Expenses){
                    if(e != null){
                        if (e.Name != null){
                            text += e.PrintExpense(MonthlyIncome() - CalcLoanPayment());
                        }
                    }
                }
            }
            else{
                MessageBox.Show("Expenses was null in func PrintExpenses()");
            }

            txtExpenses.Text = text;
        }



        private void LoadExpenses()
        {
            lstExpenses.Items.Clear();

            if (Expenses != null){
                
                foreach(Expense e in Expenses){
                    if(e != null){
                        if (e.Name != null){
                            lstExpenses.Items.Add(e.Name);
                        }
                    }
                }
            }
            else{
                MessageBox.Show("Expenses was null in func LoadExpenses()");
            }
        }



        private void PrintTimes()
        {
            string text = "Times:\r\n\r\n";

            foreach (Expense e in Expenses){
                if(e != null){
                    if (e.Name != null){
                        if (e.recurring){
                            if(e.StartDate > DateTime.Today){
                                text += e.Name + " : " + e.Time[0].ToString() + " / " + e.Time[1].ToString() + "\r\n\r\n";
                            }
                            else{
                                text += e.Name + " : Recurring\r\n\r\n";
                            }
                        }
                        else if(e.StartDate >= DateTime.Today){
                            text += e.Name + " : " + e.Time[0].ToString() + " / " + e.Time[1].ToString() + "\r\n" +
                                "Start: " + e.StartDate.ToShortDateString() + "\r\n"; 
                                
                                if(e.StartDate < e.EndDate  &&  e.EndDate != DateTime.MaxValue){
                                    text += "End: " + e.EndDate.ToShortDateString() + "\r\n\r\n";
                                }
                                else{
                                    text += "\r\n";
                                }
                                
                        }
                        else{
                            text += e.Name + " : " + e.Time[0].ToString() + " / " + e.Time[1].ToString() + "\r\n\r\n";
                        }
                    }
                }
            }

            txtExpenseTimes.Text = text;
        }

        private void RefreshExpenses(){
            LoadExpenses();  //Fills Expense Management Window
            PrintExpenses(); //Fills Expenses Details Window
            PrintTimes();    //Fills Times Window
        }



        private double MonthlyIncome(){
            return Salary / 12 * (1-(Tax + FederalTax()));
        }



        private double FederalTax(){
            if(Salary <= 9875){
                return .1;
            }
            
            if (Salary <= 40125){
                return .12;
            }
            
            if (Salary <= 85525){
                return .22;
            }
            
            if (Salary <= 163301){
                return .24;
            }

            if (Salary <= 207350){
                return .32;
            }

            if (Salary <= 518400){
                return .35;
            }

            if (Salary > 518400 ){
                return .37;
            }

            return -1;
        }
        

        
        private void btnDeleteExpense_Click(object sender, EventArgs e)
        {
            int index = lstExpenses.SelectedIndex;

            if(index != -1){
                string name = lstExpenses.SelectedItem.ToString();
                Expenses[GetExpenseByName(name)].Name = null;
                RefreshExpenses();
            }
            else{
                MessageBox.Show("No Expense selected.");
            }

            btnCalc_Click(sender, e);
        }



        private int GetExpenseByName(string name)
        {
            int index = 0;

            foreach(Expense e in Expenses){
                if (e.Name == name){
                    return index;
                }

                index++;
            }
            return -1;
        }



        private void btnManageExpense_Click(object sender, EventArgs e)
        {
            int index = lstExpenses.SelectedIndex;
            if (index != -1){

                frmNewExpense modify = new frmNewExpense(Expenses[index], Expenses);
                modify.ShowDialog();

                if (modify.Tag.GetType().Name == "Expense"){
                    
                    Expenses[index] = (Expense)modify.Tag;
                    RefreshExpenses();
                }
                else if ((int)modify.Tag == -1){
                    //MessageBox.Show("Expense was canceled");
                }
                else{
                    //MessageBox.Show("No expense returned by frmNewExpense");
                }

                RefreshExpenses();
            }
            else{
                MessageBox.Show("No Expense selected.");
            }

            btnCalc_Click(sender, e);
        }



        //Priority Down
        private void btnPriorityDown_Click(object sender, EventArgs e)
        {
            //if no Expense is selected, exit
            if (lstExpenses.SelectedItem == null) return;
            
            string name = lstExpenses.SelectedItem.ToString();
            int index = GetExpenseByName(name);

            //if this is the last Expense, exit
            if (index == Expenses.Count - 1) return;
            
            //if there is a next Expense to swap with
            Expense temp = Expenses[index];
            Expense toSwitch = Expenses[index + 1];
            Expenses[index] = toSwitch;
            Expenses[index + 1] = temp;

            RefreshExpenses();
        }



        //Priority Up
        private void btnPriorityUp_Click(object sender, EventArgs e)
        {
            //if no Expense is selected, exit
            if (lstExpenses.SelectedItem == null) return;

            string name = lstExpenses.SelectedItem.ToString();
            int index = GetExpenseByName(name);
            
            //if it is already top priority, exit
            if(index == 0) return;
            
            Expense ex = Expenses[index];
            Expense toSwitch = Expenses[index - 1];
            Expenses[index] = toSwitch;
            Expenses[index - 1] = ex;

            RefreshExpenses();
        }



        public void createStates()
        {
            string[] stateNames = new string[] { "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY" };

            int i = 0;
            foreach (string s in stateNames){
                States[i] = new State(s);
                cmbStates.Items.Add(s);
                i++;
            }
        }



        public State getState(string name)
        {
            int i = 0;

            foreach (State st in States){
                if (st.Name == name){
                    return st;
                }
                i++;
            }
            return null;
        }



        public int getStateIndex(string name)
        {
            int i = 0;

            foreach (State st in States){
                if (st.Name == name){
                    return i;
                }
                i++;
            }
            return -1;
        }



        public void fillStates()
        {
            int index = getStateIndex("MA");

            double[] salaries = new double[] {0};
            double[] taxes = new double[] {5};
            States[index] = new State("MA", salaries, taxes);

            index = getStateIndex("NH");
            salaries = new double[] {0};
            taxes = new double[] {5};
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



        private void printIncomeInfo()
        {
            txtMonthlyIncome.Text = MonthlyIncome().ToString("C0");
            txtDisposable.Text = (MonthlyIncome() - CalcLoanPayment()).ToString("C0");
            txtStateTax.Text = (Tax * 100).ToString() + "%";
            txtFederalTax.Text = (FederalTax() * 100).ToString() + "%";
        }

        

        private void cmbStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = cmbStates.SelectedItem.ToString();
            if (getState(name).getTax(Salary) == -1){
                MessageBox.Show("State's tax rates are unknown, using 0%");
            }
            btnCalc_Click(sender, e);
        }



        private void PrintError(){
            txtYears.Text = "Over Budget";
            txtPayment.Text = "Over Budget";
            txtTotal.Text = "Over Budget";
        }

    }
}




            
            