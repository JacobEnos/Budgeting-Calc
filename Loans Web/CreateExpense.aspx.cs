using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loans_Web {
    public partial class CreateExpense : System.Web.UI.Page {


        Expense newExpense;



        protected void Page_Load(object sender, EventArgs e) {

            newExpense = new Expense();

            if (!IsPostBack) {

                //If editing
                if (Session["EditExpense"] != null) {
                    newExpense = new Expense((Expense)Session["EditExpense"]);
                    LoadExpense(newExpense);
                    Session.Remove("EditExpense");
                    Session["ToReplace"] = newExpense.Name;
                }

                //If creating
                else {
                    newExpense = new Expense();

                    //Set Defaults
                    cdrStart.Visible = false;
                    cdrEnd.Visible = false;
                    newExpense.recurring = false;
                    divDatePickers.Visible = false;
                }
            }

        }



        protected void btnCreateExpense_Click(object sender, EventArgs e) {

            //Read Inputs
            if (!ReadValues())
                return;

            Session["NewExpense"] = newExpense;
            Server.Transfer("Main.aspx");
        }



        private void LoadExpense(Expense e) {

            txtName.Text = e.Name;
            txtAmount.Text = e.Amount.ToString();

            if (!e.recurring) {
                txtToExpense.Text = (e.ToExpense * 100).ToString();
                cdrEnd.Visible = false;
            }
            else {

                try {
                    newExpense.recurring = chbRecurring.Checked;
                }
                catch {
                    //newExpense has not been instantiated yet;
                }
                chbRecurring.Checked = true;
                txtToExpense.Text = "As Necessary";
                txtToExpense.Enabled = false;
            }

            if (e.StartDate > DateTime.Today  ||  e.EndDate != DateTime.MaxValue) {

                chbScheduled.Checked = true;

                cdrStart.TodaysDate = e.StartDate;

                int z = e.EndDate.CompareTo(DateTime.MaxValue);


                if (e.EndDate.Year < 3000) {
                    cdrEnd.TodaysDate = e.EndDate;
                }
                else {
                    cdrEnd.TodaysDate = e.StartDate;
                }

                cdrStart.Visible = true;
            }
            else {
                cdrStart.Visible = false;
                cdrEnd.Visible = false;
            }

            btnCreateExpense.Text = "Save Changes";
        }







        private bool ReadValues() {

            if (!ReadName() || !ReadAmount() || !ReadToLoans()) return false;
            
            

            if (chbRecurring.Checked) {

                newExpense.recurring = true;
            }

            if (chbScheduled.Checked) {

                ReadStartDate();
                ReadEndDate();
            }

            return true;
        }


        private bool ReadName() {

            if (txtName.Text != null && txtName.Text.Trim() != "") {

                switch (txtName.Text.ToLower()) {

                    case "unspent":
                        MB("Unspent is a reserved Expense name");
                        return true;

                    case "loans":
                        MB("Loans is a reserved Expense name");
                        return true;
                    default:
                        newExpense.Name = txtName.Text;
                        return true;
                }
            }
            else {
                MB("No Expense name entered");
            }
            return false;
        }




        protected void chbScheduled_CheckedChanged(object sender, EventArgs e) {

            if (chbRecurring.Checked) {

                if (chbScheduled.Checked) {

                    cdrStart.Visible = true;
                    cdrEnd.Visible = true;
                    divDatePickers.Visible = true;
                }
                else {

                    newExpense.StartDate = DateTime.Today;
                    newExpense.EndDate = DateTime.MaxValue;

                    divDatePickers.Visible = false;
                    cdrStart.Visible = false;
                    cdrEnd.Visible = false;
                }
            }
            else {

                if (chbScheduled.Checked) {

                    cdrStart.Visible = true;
                    divDatePickers.Visible = true;
                }
                else {

                    newExpense.StartDate = DateTime.Today;
                    newExpense.EndDate = DateTime.MaxValue;

                    divDatePickers.Visible = false;
                    cdrStart.Visible = false;
                    cdrEnd.Visible = false;
                }
            }
        }



        protected void chbRecurring_CheckedChanged(object sender, EventArgs e) {

            if (chbScheduled.Checked) {

                if (chbRecurring.Checked) {

                    txtToExpense.Text = "As Necessary";
                    txtToExpense.Enabled = false;

                    cdrEnd.Enabled = true;
                    cdrEnd.Visible = true;
                }
                else {

                    txtToExpense.Enabled = true;
                    txtToExpense.Text = "";

                    cdrEnd.Enabled = false;
                    cdrEnd.Visible = false;
                }

            }
            else {


                if (chbRecurring.Checked) {

                    txtToExpense.Text = "As Necessary";
                    txtToExpense.Enabled = false;
                }
                else {

                    txtToExpense.Enabled = true;
                    txtToExpense.Text = "";
                }

            }

            newExpense.recurring = chbRecurring.Checked;
        }



        protected void cdrStart_SelectionChanged(object sender, EventArgs e) {

            if (cdrStart.SelectedDate.CompareTo(DateTime.Today) < 0) {

                //Start date in the past, reject
                cdrStart.SelectedDate = DateTime.Today;
                MB("Start date cannot be in the past");
            }
        }



        protected void cdrEnd_SelectionChanged(object sender, EventArgs e) {

            if (cdrEnd.SelectedDate.CompareTo(DateTime.Today) <= 0) {

                //End date in the past, reject
                cdrStart.SelectedDate = DateTime.Today;
                MB("End date cannot be in the past");
            }

            else if (cdrEnd.SelectedDate.CompareTo(newExpense.StartDate) < 0) {

                //End Date before Start Date, reject
                cdrEnd.SelectedDate = DateTime.Today;
                MB("End date cannot be before Start date");
            }
        }



        private bool ReadToLoans() {

            if (!newExpense.recurring) {

                double z = -1;
                double.TryParse(txtToExpense.Text, out z);
                if (0 <= z && z < 100) {
                    newExpense.ToExpense = z / 100;
                }
                else {
                    MB("Couldn't read ToExpense");
                    return false;
                }
            }
            return true;
        }



        private bool ReadAmount() {

            //Validate Amount
            double t = -1;
            double.TryParse(txtAmount.Text, out t);
            if (t < 0) {

                lblError.Text = "Invalid amount!";
                return false;
            }
            newExpense.Amount = t;
            return true;
        }



        private void ReadStartDate() {

            if (cdrStart.SelectedDate.CompareTo(DateTime.Today) < 0) {

                //Start date in the past, reject
                cdrStart.SelectedDate = DateTime.Today;
                MB("Start date cannot be in the past");
            }
            else {

                newExpense.StartDate = cdrStart.SelectedDate;
            }
        }



        private void ReadEndDate() {

            if (cdrEnd.SelectedDate.CompareTo(DateTime.Today) <= 0) {

                //End date in the past, reject
                cdrStart.SelectedDate = DateTime.Today;
                MB("End date cannot be in the past");
            }

            else if (cdrEnd.SelectedDate.CompareTo(newExpense.StartDate) < 0) {

                //End Date before Start Date, reject
                cdrEnd.SelectedDate = DateTime.Today;
                MB("End date cannot be before Start date");
            }

            else {

                newExpense.EndDate = cdrEnd.SelectedDate;
            }
        }



        public void MB(string print) {
            MsgBox(print, this.Page, this);
        }

        public void MsgBox(String ex, Page pg, Object obj) {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }



        protected void btnCancel_Click(object sender, EventArgs e) {

            Session.Remove("NewExpense");
            Response.Redirect("Main.aspx");
        }

    }
}