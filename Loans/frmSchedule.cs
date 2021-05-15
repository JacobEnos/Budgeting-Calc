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
    public partial class frmSchedule : Form
    {
        Expense Manage;
        public frmSchedule()
        {
            InitializeComponent();
        }

        public frmSchedule(Expense e)
        {
            InitializeComponent();
            Manage = e;
            if (Manage == null){
                Manage = new Expense();
            }
        }

        private void frmSchedule_Load(object sender, EventArgs e)
        {
            int extension = 275;
            //If Expense is not recurring, hide end dates
            if(Manage.recurring == false){
                lblEnd.Visible = false;
                cdrEnd.Visible = false;
                txtEnd.Visible = false;

                //Shift OK button
                int x = btnOK.Location.X;
                int y = btnOK.Location.Y;
                btnOK.Location = new Point(x - extension, y);

                //shrink form
                this.Width -= extension;
            }


            //If no start date
            if(Manage.StartDate < DateTime.Today){
                cdrStart.SetDate(DateTime.Today);
                txtStart.Text = "Not Set";
            }
            //else populate StartDate
            else{
                cdrStart.SetDate(Manage.StartDate);
                txtStart.Text = Manage.StartDate.ToShortDateString();
            }

            //if Manage is recurring, populate end dates
            if (Manage.recurring == true){
                if(Manage.EndDate <= Manage.StartDate){
                    cdrEnd.SelectionRange.Start = DateTime.Today.AddDays(1);
                    txtEnd.Text = "Not Required";
                }
                else{
                    cdrEnd.SetDate(Manage.EndDate);
                    txtEnd.Text = Manage.EndDate.ToShortDateString();
                }
                    
            }


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Tag = null;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //Apply Dates to Manage

            //If StartDate was not set
            if (txtStart.Text == "Not Set"){
                
                //If the Expense is assumed to start immediately
                if (txtEnd.Text != "Not Required"){
                    cdrStart.SetDate(DateTime.Today);
                    txtStart.Text = DateTime.Today.ToShortDateString();
                }
                else{
                    MessageBox.Show("Start date not set");
                    return;
                }
            }

            //if StartDate exists
            if(cdrStart.SelectionRange.Start >= DateTime.Today){
                Manage.StartDate = cdrStart.SelectionRange.Start;

                //if Manage is recurring
                if (Manage.recurring){
                    
                    //and StartDate was set
                    //but EndDate was not
                    if (txtStart.Text != "Not Set"  &&  txtEnd.Text == "Not Required"){

                        Manage.EndDate = DateTime.MaxValue;
                    }

                    //if StartDate was Set
                    //and EndDate was set
                    else if (txtStart.Text != "Not Set" && txtEnd.Text != "Not Required"){
                        
                        //If Expense begins before it ends
                        if (cdrStart.SelectionRange.Start < cdrEnd.SelectionRange.Start){

                            //and Expense does not end the same day it begins
                            if (Manage.StartDate != Manage.EndDate){

                                //Add EndDate
                                Manage.EndDate = cdrEnd.SelectionRange.Start;
                            }
                            else{
                                MessageBox.Show("Expenses cannot end the same day they start");
                                return;
                            }
                        }
                        else{
                            MessageBox.Show("Expenses must start before they end");
                            return;
                        }
                    }
                }
                //if not recurring

            }
            else{
                MessageBox.Show("Expenses cannot start in the past");
                return;
            }
            
            Tag = (Expense)Manage;
            Close();
        }

        private void cdrStart_DateChanged(object sender, DateRangeEventArgs e)
        {
        }

        private void cdrEnd_DateChanged(object sender, DateRangeEventArgs e)
        {
            
        }

        private void txtStart_Enter(object sender, EventArgs e)
        {

        }

        private void txtStart_Leave(object sender, EventArgs e)
        {
            DateTime clean = DateTime.Parse(txtStart.Text);
            if (clean == null){
                MessageBox.Show("Start Date could not be formatted");
            }
            else{
                MessageBox.Show("Date parsed");
                Manage.StartDate = clean;
                cdrStart.SetDate(Manage.StartDate);
            }
        }

        private void txtEnd_Leave(object sender, EventArgs e)
        {
            DateTime clean;
            DateTime.TryParse(txtEnd.Text, out clean);
            
            if (clean == null){
                MessageBox.Show("Start Date could not be formatted");
            }
            else{
                MessageBox.Show("Date parsed");
                Manage.EndDate = clean;
                cdrEnd.SetDate(Manage.EndDate);
            }
        }

        private void cdrStart_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtStart.Text = cdrStart.SelectionRange.Start.ToShortDateString();
        }

        private void cdrEnd_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtEnd.Text = cdrEnd.SelectionRange.Start.ToShortDateString();
        }
    }
}
