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
    public partial class frmNewExpense : Form
    {
        Expense Manage;
        List<Expense> Expenses;
        bool editing;
        
        public frmNewExpense()
        {
            InitializeComponent();
            Manage = new Expense();
        }

        public frmNewExpense(List<Expense> list)
        {
            InitializeComponent();
            Manage = new Expense();
            Expenses = list;
            editing = false;
        }

        public frmNewExpense(Expense e)
        {
            InitializeComponent();
            Manage = e;
            if (Manage == null){
                Manage = new Expense();
            }
            txtName.Text = Manage.Name;
            txtAmount.Text = Manage.Amount.ToString();
            txtPercent.Text = (Manage.ToExpense * 100).ToString();
            if (Manage.recurring) chbRecurring.Checked = true;
            editing = true;
            
        }

        public frmNewExpense(Expense e, List<Expense> list)
        {
            InitializeComponent();
            Manage = e;
            Expenses = list;
            if (Manage == null){
                Manage = new Expense();
            }
            txtName.Text = Manage.Name;
            txtAmount.Text = Manage.Amount.ToString();
            txtPercent.Text = (Manage.ToExpense * 100).ToString();
            if (Manage.recurring) chbRecurring.Checked = true;
            if (Manage.StartDate >= DateTime.Today) chbSchedule.Checked = true;
            editing = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = "";
            if (txtName.Text != ""){
                if (UniqueName(txtName.Text)){
                    name = txtName.Text;
                }
                else{
                    MessageBox.Show("Expense names must be unique");
                    return;
                }
            }
            else{
                MessageBox.Show("Expenses must be named");
                return;
            }

            //trim leading and trailing whitespace
            string readAmount = txtAmount.Text.Trim();
            
            if(readAmount.Length > 0){
                //if amount has a leading '$', remove it
                if (readAmount[0] == '$') readAmount = readAmount.Substring(1, readAmount.Length - 1);
                //if Amount contains commas, remove them
                if (readAmount.Contains(',')) readAmount = readAmount.Replace(",", "");
                //if Amount contains non-leading/trailing spaces, remove them
                if (readAmount.Contains(' ')) readAmount = readAmount.Replace(" ", "");
            }
            
            
            double cleanAmount = 0;
            if(double.TryParse(readAmount, out cleanAmount)){
            }
            
            double cleanPercent = 0;
            if (double.TryParse(txtPercent.Text, out cleanPercent)  &&  0 <= cleanPercent){

                if(cleanPercent > 100){
                    MessageBox.Show("Percentage must be less than 100");
                    return;
                }
                //cleanPercent Populated
            }

            Manage.Name = name;
            Manage.Amount = cleanAmount;
            Manage.ToExpense = cleanPercent / 100;
            if (chbRecurring.Checked) Manage.Recur();
            this.Tag = Manage;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Tag = -1;
            this.Close();
        }

        private void chbRecurring_CheckedChanged(object sender, EventArgs e)
        {
            int extension = 20;
            //If Recurring
            if (chbRecurring.Checked){
                //Set Expense.recurring to true
                Manage.recurring = true;

                //Lock toExpense
                txtPercent.ReadOnly = true;
                
                //Fill toExpense with "as neccessary"
                txtPercent.Text = "As Neccessary";

                //if Expense is also scheduled, show end dates
                if(chbSchedule.Checked == true){
                    lblScheduleEnd.Visible = true;
                    txtScheduleEnd.Visible = true;

                    //populate EndDate
                    if (Manage.StartDate < Manage.EndDate){
                        txtScheduleEnd.Text = Manage.EndDate.ToShortDateString();
                    }
                    else{
                        txtScheduleEnd.Text = "Not Set";
                    }

                    btnSchedule.Height = 39;
                    stretchFormVertical(extension);
                }
            }
            //if not recurring
            else{
                //Set Expense.recurring to false
                Manage.recurring = false;

                txtPercent.ReadOnly = false;
                txtPercent.Text = "";

                Manage.EndDate = new DateTime();

                //if Expense is scheduled, hide end dates
                if (chbSchedule.Checked == true){

                    lblScheduleEnd.Visible = false;
                    txtScheduleEnd.Visible = false;

                    btnSchedule.Height = 20;
                    stretchFormVertical(-(extension));
                }
            }
        }

        private void chbSchedule_CheckedChanged(object sender, EventArgs e)
        {
            int extension = 30;
            //if the box was just checked
            if (chbSchedule.Checked == true){
                
                //Show scheduling componenets
                lblScheduleStart.Visible = true;
                txtScheduleStart.Visible = true;
                btnSchedule.Visible = true;

                //fill txtSchedule Start
                if (Manage.StartDate >= DateTime.Now){
                    
                    //Get Expense's .startDate
                    DateTime start = Manage.StartDate;

                    //Populate Expense's .startDate
                    txtScheduleStart.Text = start.ToShortDateString();
                }
                else{
                    txtScheduleStart.Text = "Not Set";
                }

                //if Manage is recurring, show end dates
                if(Manage.recurring == true){
                    //Show EndDate components
                    lblScheduleEnd.Visible = true;
                    txtScheduleEnd.Visible = true;

                    //Increase space needed to show dates
                    extension += 20;

                    //Increase Height of btnSchedule
                    btnSchedule.Height = 39;

                    DateTime end = Manage.EndDate;
                    txtScheduleEnd.Text = end.ToShortDateString();

                    //if an end date has not been set
                    if (Manage.EndDate == null  ||  Manage.EndDate <= Manage.StartDate){
                        txtScheduleEnd.Text = "Not Set";
                    }
                    else{
                        txtScheduleEnd.Text = Manage.EndDate.ToString();
                    }
                }
                else{
                    btnSchedule.Height = 20;
                }

                stretchFormVertical(extension);
            }
            //if the box was just unchecked
            else{
                //Reset Dates
                Manage.StartDate = new DateTime();
                Manage.EndDate = new DateTime();

                //Hide scheduling fields
                btnSchedule.Visible = false;
                txtScheduleEnd.Visible = false;
                lblScheduleEnd.Visible = false;
                txtScheduleStart.Visible = false;
                lblScheduleStart.Visible = false;

                //if End dates were being shown, increase extension by 20
                if(chbRecurring.Checked == true){
                    extension += 20;
                }

                stretchFormVertical(-(extension));
            }
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            frmSchedule newSchedule = new frmSchedule(Manage);
            newSchedule.ShowDialog();

            if(newSchedule.Tag == null){
                return;
            }
            //if Tag is valid
            else if (newSchedule.Tag.GetType().Name == "Expense"){
                Expense temp = (Expense)newSchedule.Tag;

                //If StartDate is valid
                if (temp.StartDate >= DateTime.Today){

                    //Add Tag StartDate to Manage
                    txtScheduleStart.Text = temp.StartDate.ToShortDateString();
                    Manage.StartDate = temp.StartDate;

                    //If EndDate is valid
                    if (temp.EndDate > DateTime.Today){
                        
                        //Add Tag EndDate to Manage
                        txtScheduleEnd.Text = temp.EndDate.ToShortDateString();
                        Manage.EndDate = temp.EndDate;
                    }
                }
            }
            else{
                MessageBox.Show("Unknown return type");
            }
        }

        private void stretchFormVertical(int stretch)
        {
            //Shorten form
            this.Height += stretch;

            //Shift Add button up
            int y = btnAdd.Location.Y;
            int x = btnAdd.Location.X;
            btnAdd.Location = new Point(x, y + stretch);

            //Shift Cancel button up
            y = btnCancel.Location.Y;
            x = btnCancel.Location.X;
            btnCancel.Location = new Point(x, y + stretch);
        }

        private bool UniqueName(string check)
        {
            //If editing Name should not be unique
            if (editing){
                return true;
            }
            //If new Expense
            else{

                //If Expenses is valid
                if (Expenses != null){

                    //Check Expenses for the Name
                    foreach (Expense e in Expenses){

                        //If found return not unique
                        if (e.Name == check){
                            return false;
                        }
                    }
                }
                return true;
            }
        }
    }
}
