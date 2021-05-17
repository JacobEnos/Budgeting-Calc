using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loans_Web {
    public partial class CreateExpense : System.Web.UI.Page {



        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void btnCreateExpense_Click(object sender, EventArgs e) {

            //Read Inputs
            //Validate Amount
            double t = -1;
            double.TryParse(txtAmount.Text, out t);
            if (t < 0) {

                lblError.Text = "Invalid amount!";
                return;
            }


            //Create new Expense from inputs
            Expense newExpense = new Expense(txtName.Text, t);


            if (chbRecurring.Checked) {
                newExpense.recurring = true;
            }
            else {

                double z = -1;
                double.TryParse(txtToExpense.Text, out z);
                if(z >= 0)
                    newExpense.ToExpense = z/100;
            }

            if (chbScheduled.Checked) {

                //DatePickers



            }
            else {

                newExpense.StartDate = DateTime.Today;

            }

            Session["NewExpense"] = newExpense;
            Server.Transfer("Main.aspx");
        }



        protected void btnCancel_Click(object sender, EventArgs e) {
            Response.Redirect("Main.aspx");
        }
    }
}