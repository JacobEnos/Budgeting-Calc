namespace Loans
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tbrSalary = new System.Windows.Forms.TrackBar();
            this.lblSalary = new System.Windows.Forms.Label();
            this.txtSalary = new System.Windows.Forms.TextBox();
            this.txtLoans = new System.Windows.Forms.TextBox();
            this.lblLoans = new System.Windows.Forms.Label();
            this.lblYears = new System.Windows.Forms.Label();
            this.txtYears = new System.Windows.Forms.TextBox();
            this.lblInterest = new System.Windows.Forms.Label();
            this.txtInterest = new System.Windows.Forms.TextBox();
            this.lblToLoans = new System.Windows.Forms.Label();
            this.txtToLoans = new System.Windows.Forms.TextBox();
            this.lblState = new System.Windows.Forms.Label();
            this.lblPayment = new System.Windows.Forms.Label();
            this.txtPayment = new System.Windows.Forms.TextBox();
            this.btnCalc = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.btnAddExpense = new System.Windows.Forms.Button();
            this.txtExpenses = new System.Windows.Forms.TextBox();
            this.txtExpenseTimes = new System.Windows.Forms.TextBox();
            this.lstExpenses = new System.Windows.Forms.ListBox();
            this.btnDeleteExpense = new System.Windows.Forms.Button();
            this.btnManageExpense = new System.Windows.Forms.Button();
            this.lblMonthlyIncome = new System.Windows.Forms.Label();
            this.lblDisposable = new System.Windows.Forms.Label();
            this.txtMonthlyIncome = new System.Windows.Forms.TextBox();
            this.txtDisposable = new System.Windows.Forms.TextBox();
            this.btnPriorityUp = new System.Windows.Forms.Button();
            this.btnPriorityDown = new System.Windows.Forms.Button();
            this.cmbStates = new System.Windows.Forms.ComboBox();
            this.txtStateTax = new System.Windows.Forms.TextBox();
            this.txtFederalTax = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.tbrSalary)).BeginInit();
            this.SuspendLayout();
            // 
            // tbrSalary
            // 
            resources.ApplyResources(this.tbrSalary, "tbrSalary");
            this.tbrSalary.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbrSalary.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.tbrSalary.LargeChange = 5000;
            this.tbrSalary.Maximum = 90000;
            this.tbrSalary.Minimum = 40000;
            this.tbrSalary.Name = "tbrSalary";
            this.tbrSalary.SmallChange = 1000;
            this.tbrSalary.TickFrequency = 1000;
            this.tbrSalary.Value = 60000;
            this.tbrSalary.Scroll += new System.EventHandler(this.tbrSalary_Scroll);
            this.tbrSalary.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbrSalary_KeyUp);
            this.tbrSalary.Leave += new System.EventHandler(this.tbrSalary_Leave);
            this.tbrSalary.MouseLeave += new System.EventHandler(this.tbrSalary_MouseLeave);
            // 
            // lblSalary
            // 
            resources.ApplyResources(this.lblSalary, "lblSalary");
            this.lblSalary.Name = "lblSalary";
            // 
            // txtSalary
            // 
            resources.ApplyResources(this.txtSalary, "txtSalary");
            this.txtSalary.Name = "txtSalary";
            this.txtSalary.ReadOnly = true;
            this.txtSalary.TabStop = false;
            // 
            // txtLoans
            // 
            resources.ApplyResources(this.txtLoans, "txtLoans");
            this.txtLoans.Name = "txtLoans";
            this.txtLoans.Leave += new System.EventHandler(this.txtLoans_Leave);
            // 
            // lblLoans
            // 
            resources.ApplyResources(this.lblLoans, "lblLoans");
            this.lblLoans.Name = "lblLoans";
            // 
            // lblYears
            // 
            resources.ApplyResources(this.lblYears, "lblYears");
            this.lblYears.Name = "lblYears";
            // 
            // txtYears
            // 
            resources.ApplyResources(this.txtYears, "txtYears");
            this.txtYears.Name = "txtYears";
            this.txtYears.ReadOnly = true;
            this.txtYears.TabStop = false;
            // 
            // lblInterest
            // 
            resources.ApplyResources(this.lblInterest, "lblInterest");
            this.lblInterest.Name = "lblInterest";
            // 
            // txtInterest
            // 
            resources.ApplyResources(this.txtInterest, "txtInterest");
            this.txtInterest.Name = "txtInterest";
            this.txtInterest.Leave += new System.EventHandler(this.txtInterest_Leave);
            // 
            // lblToLoans
            // 
            resources.ApplyResources(this.lblToLoans, "lblToLoans");
            this.lblToLoans.Name = "lblToLoans";
            // 
            // txtToLoans
            // 
            resources.ApplyResources(this.txtToLoans, "txtToLoans");
            this.txtToLoans.Name = "txtToLoans";
            this.txtToLoans.Leave += new System.EventHandler(this.txtToLoans_Leave);
            // 
            // lblState
            // 
            resources.ApplyResources(this.lblState, "lblState");
            this.lblState.Name = "lblState";
            // 
            // lblPayment
            // 
            resources.ApplyResources(this.lblPayment, "lblPayment");
            this.lblPayment.Name = "lblPayment";
            // 
            // txtPayment
            // 
            resources.ApplyResources(this.txtPayment, "txtPayment");
            this.txtPayment.Name = "txtPayment";
            this.txtPayment.ReadOnly = true;
            this.txtPayment.TabStop = false;
            // 
            // btnCalc
            // 
            resources.ApplyResources(this.btnCalc, "btnCalc");
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click2);
            // 
            // lblTotal
            // 
            resources.ApplyResources(this.lblTotal, "lblTotal");
            this.lblTotal.Name = "lblTotal";
            // 
            // txtTotal
            // 
            resources.ApplyResources(this.txtTotal, "txtTotal");
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.TabStop = false;
            // 
            // btnAddExpense
            // 
            resources.ApplyResources(this.btnAddExpense, "btnAddExpense");
            this.btnAddExpense.Name = "btnAddExpense";
            this.btnAddExpense.UseVisualStyleBackColor = true;
            this.btnAddExpense.Click += new System.EventHandler(this.btnAddExpense_Click);
            // 
            // txtExpenses
            // 
            resources.ApplyResources(this.txtExpenses, "txtExpenses");
            this.txtExpenses.Name = "txtExpenses";
            this.txtExpenses.ReadOnly = true;
            this.txtExpenses.TabStop = false;
            // 
            // txtExpenseTimes
            // 
            resources.ApplyResources(this.txtExpenseTimes, "txtExpenseTimes");
            this.txtExpenseTimes.Name = "txtExpenseTimes";
            this.txtExpenseTimes.ReadOnly = true;
            this.txtExpenseTimes.TabStop = false;
            // 
            // lstExpenses
            // 
            resources.ApplyResources(this.lstExpenses, "lstExpenses");
            this.lstExpenses.FormattingEnabled = true;
            this.lstExpenses.Name = "lstExpenses";
            this.lstExpenses.TabStop = false;
            // 
            // btnDeleteExpense
            // 
            resources.ApplyResources(this.btnDeleteExpense, "btnDeleteExpense");
            this.btnDeleteExpense.Name = "btnDeleteExpense";
            this.btnDeleteExpense.UseVisualStyleBackColor = true;
            this.btnDeleteExpense.Click += new System.EventHandler(this.btnDeleteExpense_Click);
            // 
            // btnManageExpense
            // 
            resources.ApplyResources(this.btnManageExpense, "btnManageExpense");
            this.btnManageExpense.Name = "btnManageExpense";
            this.btnManageExpense.UseVisualStyleBackColor = true;
            this.btnManageExpense.Click += new System.EventHandler(this.btnManageExpense_Click);
            // 
            // lblMonthlyIncome
            // 
            resources.ApplyResources(this.lblMonthlyIncome, "lblMonthlyIncome");
            this.lblMonthlyIncome.Name = "lblMonthlyIncome";
            // 
            // lblDisposable
            // 
            resources.ApplyResources(this.lblDisposable, "lblDisposable");
            this.lblDisposable.Name = "lblDisposable";
            // 
            // txtMonthlyIncome
            // 
            resources.ApplyResources(this.txtMonthlyIncome, "txtMonthlyIncome");
            this.txtMonthlyIncome.Name = "txtMonthlyIncome";
            this.txtMonthlyIncome.ReadOnly = true;
            this.txtMonthlyIncome.TabStop = false;
            // 
            // txtDisposable
            // 
            resources.ApplyResources(this.txtDisposable, "txtDisposable");
            this.txtDisposable.Name = "txtDisposable";
            this.txtDisposable.ReadOnly = true;
            this.txtDisposable.TabStop = false;
            // 
            // btnPriorityUp
            // 
            resources.ApplyResources(this.btnPriorityUp, "btnPriorityUp");
            this.btnPriorityUp.Name = "btnPriorityUp";
            this.btnPriorityUp.TabStop = false;
            this.btnPriorityUp.UseVisualStyleBackColor = true;
            this.btnPriorityUp.Click += new System.EventHandler(this.btnPriorityUp_Click);
            // 
            // btnPriorityDown
            // 
            resources.ApplyResources(this.btnPriorityDown, "btnPriorityDown");
            this.btnPriorityDown.Name = "btnPriorityDown";
            this.btnPriorityDown.TabStop = false;
            this.btnPriorityDown.UseVisualStyleBackColor = true;
            this.btnPriorityDown.Click += new System.EventHandler(this.btnPriorityDown_Click);
            // 
            // cmbStates
            // 
            resources.ApplyResources(this.cmbStates, "cmbStates");
            this.cmbStates.FormattingEnabled = true;
            this.cmbStates.Name = "cmbStates";
            this.cmbStates.SelectedIndexChanged += new System.EventHandler(this.cmbStates_SelectedIndexChanged);
            // 
            // txtStateTax
            // 
            resources.ApplyResources(this.txtStateTax, "txtStateTax");
            this.txtStateTax.Name = "txtStateTax";
            this.txtStateTax.ReadOnly = true;
            // 
            // txtFederalTax
            // 
            resources.ApplyResources(this.txtFederalTax, "txtFederalTax");
            this.txtFederalTax.Name = "txtFederalTax";
            this.txtFederalTax.ReadOnly = true;
            // 
            // frmMain
            // 
            this.AcceptButton = this.btnCalc;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.txtFederalTax);
            this.Controls.Add(this.txtStateTax);
            this.Controls.Add(this.cmbStates);
            this.Controls.Add(this.btnPriorityDown);
            this.Controls.Add(this.btnPriorityUp);
            this.Controls.Add(this.txtDisposable);
            this.Controls.Add(this.txtMonthlyIncome);
            this.Controls.Add(this.lblDisposable);
            this.Controls.Add(this.lblMonthlyIncome);
            this.Controls.Add(this.btnManageExpense);
            this.Controls.Add(this.btnDeleteExpense);
            this.Controls.Add(this.lstExpenses);
            this.Controls.Add(this.txtExpenseTimes);
            this.Controls.Add(this.txtExpenses);
            this.Controls.Add(this.btnAddExpense);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnCalc);
            this.Controls.Add(this.txtPayment);
            this.Controls.Add(this.lblPayment);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.txtToLoans);
            this.Controls.Add(this.lblToLoans);
            this.Controls.Add(this.txtInterest);
            this.Controls.Add(this.lblInterest);
            this.Controls.Add(this.txtYears);
            this.Controls.Add(this.lblYears);
            this.Controls.Add(this.lblLoans);
            this.Controls.Add(this.txtLoans);
            this.Controls.Add(this.txtSalary);
            this.Controls.Add(this.lblSalary);
            this.Controls.Add(this.tbrSalary);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            ((System.ComponentModel.ISupportInitialize)(this.tbrSalary)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar tbrSalary;
        private System.Windows.Forms.Label lblSalary;
        private System.Windows.Forms.TextBox txtSalary;
        private System.Windows.Forms.TextBox txtLoans;
        private System.Windows.Forms.Label lblLoans;
        private System.Windows.Forms.Label lblYears;
        private System.Windows.Forms.TextBox txtYears;
        private System.Windows.Forms.Label lblInterest;
        private System.Windows.Forms.TextBox txtInterest;
        private System.Windows.Forms.Label lblToLoans;
        private System.Windows.Forms.TextBox txtToLoans;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label lblPayment;
        private System.Windows.Forms.TextBox txtPayment;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Button btnAddExpense;
        private System.Windows.Forms.TextBox txtExpenses;
        private System.Windows.Forms.TextBox txtExpenseTimes;
        private System.Windows.Forms.ListBox lstExpenses;
        private System.Windows.Forms.Button btnDeleteExpense;
        private System.Windows.Forms.Button btnManageExpense;
        private System.Windows.Forms.Label lblMonthlyIncome;
        private System.Windows.Forms.Label lblDisposable;
        private System.Windows.Forms.TextBox txtMonthlyIncome;
        private System.Windows.Forms.TextBox txtDisposable;
        private System.Windows.Forms.Button btnPriorityUp;
        private System.Windows.Forms.Button btnPriorityDown;
        private System.Windows.Forms.ComboBox cmbStates;
        private System.Windows.Forms.TextBox txtStateTax;
        private System.Windows.Forms.TextBox txtFederalTax;
    }
}

