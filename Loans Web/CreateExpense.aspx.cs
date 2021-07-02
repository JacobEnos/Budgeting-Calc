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

                lblAmount.Text = "Total Amount";

                //If editing
                if (Session["EditExpense"] != null) {
                    newExpense = new Expense((Expense)Session["EditExpense"]);
                    LoadExpense(sender, e);
                    Session.Remove("EditExpense");
                    Session["ToReplace"] = newExpense.Name;
                }

                //If creating
                else {
                    newExpense = new Expense();

                    //Set Defaults
                    divDatePickers.Visible = false;

                    divStart.Visible = false;
                    cdrStart.Enabled = false;

                    divEnd.Visible = false;
                    cdrEnd.Enabled = false;
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



        private void LoadExpense(object sender, EventArgs e) {

            txtName.Text = newExpense.Name;
            txtAmount.Text = newExpense.Amount.ToString();
            btnCreateExpense.Text = "Save Changes";

            if (!newExpense.recurring) {
                txtToExpense.Text = (newExpense.ToExpense * 100).ToString();
            }
            else {
                chbRecurring.Checked = true;
                chbRecurring_CheckedChanged(sender, e);
            }


            //If expense was made before today, update it to start today
            if (newExpense.StartDate < DateTime.Today) newExpense.StartDate = DateTime.Today;

            //If scheduled
            if (DateTime.Today < newExpense.StartDate  ||  newExpense.EndDate != null) {
                divDatePickers.Visible = true;
            }

            //If Start set
            if (DateTime.Today.Date < newExpense.StartDate) {

                chbScheduled.Checked = true;
                chbScheduled_CheckedChanged(sender, e);

                //divStart.Visible = true;
                //cdrStart.Enabled = true;
                cdrStart.Text = newExpense.StartDate.ToString("yyyy-MM-dd");
            }
            else {
                divStart.Visible = false;
            }

            //If EndDate set
            if (newExpense.EndDate != null) {
                
                DateTime endDate = newExpense.EndDate ?? DateTime.MinValue;
                cdrEnd.Text =  endDate.ToString("yyyy-MM-dd");
            }
        }







        private bool ReadValues() {

            if (!ReadName() || !ReadAmount() || !ReadToExpense()) return false;
            
            //If Scheduled
            if (chbScheduled.Checked) {

                //With invalid start date
                if (!ReadStartDate()) return false;

                //And recurring, with invalid end date
                if (chbRecurring.Checked  &&  !ReadEndDate()) return false;
            }

            newExpense.recurring = chbRecurring.Checked;
            return true;
        }



        private bool ReadName() {

            if (txtName.Text != null && txtName.Text.Trim() != "") {

                switch (txtName.Text.ToLower()) {

                    case "unspent":
                        MB("Unspent is a reserved Expense name");
                        return false;

                    case "loans":
                        MB("Loans is a reserved Expense name");
                        return false;
                    default:

                        //Check if name already exists
                        Dictionary<string, object> savedData = (Dictionary<string, object>)Session["SavedSettings"];

                        if (savedData.ContainsKey("Expenses")) {

                            List<Expense> savedExpenses = (List<Expense>)savedData["Expenses"];

                            foreach (Expense e in savedExpenses) {

                                //If this Expenses already exists
                                if (txtName.Text.ToLower() == e.Name.ToLower()) {

                                    //And this Expenses is not being edited
                                    if (e.Name.ToLower() != (string)Session["ToReplace"]) {

                                        MB("You are not editing the Expense of this Name");
                                        return false;
                                    }
                                }
                            }
                        }

                        newExpense.Name = txtName.Text;
                        return true;
                }
            }

            MB("No Expense name entered");
            return false;
        }




        protected void chbScheduled_CheckedChanged(object sender, EventArgs e) {

            /* 
             *TODO: Invert if-statements, then extract commonalities of currently inner segments 
             */

            //If Recurring
            if (chbRecurring.Checked) {

                //and scheduling
                if (chbScheduled.Checked) {

                    divDatePickers.Visible = true;
                    divStart.Visible = true;
                    cdrStart.Enabled = true;
                    divEnd.Visible = true;
                    cdrEnd.Enabled = true;
                }
                //but un-scheduling
                else {

                    //Reset start/end Dates
                    newExpense.StartDate = DateTime.Today;
                    newExpense.EndDate = null;
                    //Reset inputs
                    cdrStart.Text = DateTime.Today.ToString("yyyy-MM-dd");
                    cdrEnd.Text = "";

                    divDatePickers.Visible = false;
                    divStart.Visible = false;
                    divEnd.Visible = false;
                }
            }
            //If not recurring
            else {
                //and scheduling
                if (chbScheduled.Checked) {

                    divDatePickers.Visible = true;
                    divStart.Visible = true;
                    cdrStart.Enabled = true;
                }
                //but un-scheduling
                else {

                    newExpense.StartDate = DateTime.Today;
                    newExpense.EndDate = null;
                    //Reset inputs
                    cdrStart.Text = DateTime.Today.ToString("yyyy-MM-dd");
                    cdrEnd.Text = "";

                    divDatePickers.Visible = false;
                    divEnd.Visible = false;
                }
            }
        }



        protected void chbRecurring_CheckedChanged(object sender, EventArgs e) {

            /* 
             *TODO: Invert if-statements, then extract commonalities of currently inner segments 
             */

            //If scheduled
            if (chbScheduled.Checked) {

                //and making recurring
                if (chbRecurring.Checked) {

                    //show start/end
                    lblAmount.Text = "Monthly Amount";
                    txtToExpense.Text = "As Necessary";
                    txtToExpense.Enabled = false;

                    cdrEnd.Enabled = true;
                    divEnd.Visible = true;
                }
                //but making non-recurring
                else {

                    //Hide endDate
                    cdrEnd.Enabled = false;
                    divEnd.Visible = false;
                    
                    lblAmount.Text = "Total Amount";
                    txtToExpense.Enabled = true;
                    txtToExpense.Text = "";
                }
            }
            //If unscheduled
            else {
                //and making recurring
                if (chbRecurring.Checked) {

                    divEnd.Visible = true;

                    txtToExpense.Enabled = false;
                    txtToExpense.Text = "As Necessary";
                    lblAmount.Text = "Monthly Amount";
                }
                //and making non-recurring
                else {
                    lblAmount.Text = "Total Amount";
                    txtToExpense.Enabled = true;
                    txtToExpense.Text = "";
                }
            }

            //newExpense.recurring = chbRecurring.Checked;
        }



        protected void cdrStart_SelectionChanged(object sender, EventArgs e) {

            if (DateTime.Parse(cdrStart.Text).CompareTo(DateTime.Today) < 0) {

                //Start date in the past, reject
                cdrStart.Text = DateTime.Today.ToString(); ;
                MB("Start date cannot be in the past");
            }
        }



        protected void cdrEnd_SelectionChanged(object sender, EventArgs e) {

            if (DateTime.Parse(cdrEnd.Text).CompareTo(DateTime.Today) <= 0) {

                //End date in the past, reject
                cdrStart.Text = DateTime.Today.ToString();
                MB("End date cannot be in the past");
            }

            else if (DateTime.Parse(cdrEnd.Text).CompareTo(newExpense.StartDate) < 0) {

                //End Date before Start Date, reject
                cdrEnd.Text = DateTime.Today.ToString("");
                MB("End date cannot be before Start date");
            }
        }



        private bool ReadToExpense() {

            if(chbRecurring.Checked) return true;

            if (!chbRecurring.Checked) {

                double z = -1;
                bool parseable = double.TryParse(txtToExpense.Text, out z);
                if (0 <= z && z < 100  && parseable) {
                    newExpense.ToExpense = z / 100;
                }
                else {
                    MB("Invalid ToExpense value");
                    return false;
                }
            }
            return true;
        }



        private bool ReadAmount() {

            //Validate Amount
            double t = -1;
            double.TryParse(txtAmount.Text, out t);
            if (t <= 0) {

                MB("Invalid amount!");
                return false;
            }
            newExpense.Amount = t;
            return true;
        }



        private bool ReadStartDate() {

            if (cdrStart.Text == null || cdrStart.Text == "") return true;

            //Handle empty string
            DateTime parsingHelper;
            DateTime? temp = DateTime.TryParse(cdrStart.Text, out parsingHelper)?   parsingHelper : (DateTime?)null;

            //Allow blank input
            if (temp == null) return true;

            //Start date in the past
            if (parsingHelper < DateTime.Today) {

                //Reject
                cdrStart.Text = DateTime.Today.ToString();
                MB("Start date cannot be in the past");
                return false;
            }
            else {
                newExpense.StartDate = parsingHelper;
                return true;
            }
        }



        private bool ReadEndDate() {


            //Handle empty string
            DateTime parsingHelper;
            DateTime? temp = DateTime.TryParse(cdrEnd.Text, out parsingHelper) ? parsingHelper : (DateTime?)null;

            //Allow blank input
            if (temp == null) return true;

            if (temp < DateTime.Today) {

                //End date in the past, reject
                cdrStart.Text = DateTime.Today.ToString();
                MB("End date cannot be in the past");
                return false;
            }

            else if (temp < newExpense.StartDate) {

                //End Date before Start Date, reject
                cdrEnd.Text = newExpense.StartDate.ToString("yyyy-MM-dd");
                MB("End date cannot be before Start date");
                return false;
            }

            else {

                newExpense.EndDate = DateTime.Parse(cdrEnd.Text);
                return true;
            }
        }



        public void MB(string print)    =>    MsgBox(print, this.Page, this);



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