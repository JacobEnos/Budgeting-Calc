namespace Loans
{
    partial class frmNewExpense
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblPercent = new System.Windows.Forms.Label();
            this.txtPercent = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.chbRecurring = new System.Windows.Forms.CheckBox();
            this.chbSchedule = new System.Windows.Forms.CheckBox();
            this.lblScheduleStart = new System.Windows.Forms.Label();
            this.txtScheduleStart = new System.Windows.Forms.TextBox();
            this.txtScheduleEnd = new System.Windows.Forms.TextBox();
            this.lblScheduleEnd = new System.Windows.Forms.Label();
            this.btnSchedule = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(190, 192);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtAmount
            // 
            this.txtAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAmount.Location = new System.Drawing.Point(128, 54);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(100, 20);
            this.txtAmount.TabIndex = 1;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(49, 57);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(73, 13);
            this.lblAmount.TabIndex = 2;
            this.lblAmount.Text = "Initial Amount:";
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.Location = new System.Drawing.Point(16, 91);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(106, 13);
            this.lblPercent.TabIndex = 3;
            this.lblPercent.Text = "% Salary to Expense:";
            // 
            // txtPercent
            // 
            this.txtPercent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPercent.Location = new System.Drawing.Point(128, 88);
            this.txtPercent.Name = "txtPercent";
            this.txtPercent.Size = new System.Drawing.Size(100, 20);
            this.txtPercent.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(29, 191);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(84, 25);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 6;
            this.lblName.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(128, 22);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 20);
            this.txtName.TabIndex = 0;
            // 
            // chbRecurring
            // 
            this.chbRecurring.AutoSize = true;
            this.chbRecurring.Location = new System.Drawing.Point(32, 120);
            this.chbRecurring.Name = "chbRecurring";
            this.chbRecurring.Size = new System.Drawing.Size(196, 17);
            this.chbRecurring.TabIndex = 3;
            this.chbRecurring.Text = "Recurring ( ie. Food, Rent, Untilities)";
            this.chbRecurring.UseVisualStyleBackColor = true;
            this.chbRecurring.CheckedChanged += new System.EventHandler(this.chbRecurring_CheckedChanged);
            // 
            // chbSchedule
            // 
            this.chbSchedule.AutoSize = true;
            this.chbSchedule.Location = new System.Drawing.Point(32, 143);
            this.chbSchedule.Name = "chbSchedule";
            this.chbSchedule.Size = new System.Drawing.Size(124, 17);
            this.chbSchedule.TabIndex = 4;
            this.chbSchedule.Text = "Schedule with Dates";
            this.chbSchedule.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chbSchedule.UseVisualStyleBackColor = true;
            this.chbSchedule.CheckedChanged += new System.EventHandler(this.chbSchedule_CheckedChanged);
            // 
            // lblScheduleStart
            // 
            this.lblScheduleStart.AutoSize = true;
            this.lblScheduleStart.Location = new System.Drawing.Point(49, 177);
            this.lblScheduleStart.Name = "lblScheduleStart";
            this.lblScheduleStart.Size = new System.Drawing.Size(32, 13);
            this.lblScheduleStart.TabIndex = 10;
            this.lblScheduleStart.Text = "Start:";
            this.lblScheduleStart.Visible = false;
            // 
            // txtScheduleStart
            // 
            this.txtScheduleStart.Location = new System.Drawing.Point(87, 174);
            this.txtScheduleStart.Name = "txtScheduleStart";
            this.txtScheduleStart.ReadOnly = true;
            this.txtScheduleStart.Size = new System.Drawing.Size(100, 20);
            this.txtScheduleStart.TabIndex = 11;
            this.txtScheduleStart.TabStop = false;
            this.txtScheduleStart.Visible = false;
            // 
            // txtScheduleEnd
            // 
            this.txtScheduleEnd.Location = new System.Drawing.Point(87, 193);
            this.txtScheduleEnd.Name = "txtScheduleEnd";
            this.txtScheduleEnd.ReadOnly = true;
            this.txtScheduleEnd.Size = new System.Drawing.Size(100, 20);
            this.txtScheduleEnd.TabIndex = 12;
            this.txtScheduleEnd.TabStop = false;
            this.txtScheduleEnd.Visible = false;
            // 
            // lblScheduleEnd
            // 
            this.lblScheduleEnd.AutoSize = true;
            this.lblScheduleEnd.Location = new System.Drawing.Point(52, 196);
            this.lblScheduleEnd.Name = "lblScheduleEnd";
            this.lblScheduleEnd.Size = new System.Drawing.Size(29, 13);
            this.lblScheduleEnd.TabIndex = 13;
            this.lblScheduleEnd.Text = "End:";
            this.lblScheduleEnd.Visible = false;
            // 
            // btnSchedule
            // 
            this.btnSchedule.Location = new System.Drawing.Point(204, 174);
            this.btnSchedule.Name = "btnSchedule";
            this.btnSchedule.Size = new System.Drawing.Size(75, 39);
            this.btnSchedule.TabIndex = 5;
            this.btnSchedule.Text = "Schedule";
            this.btnSchedule.UseVisualStyleBackColor = true;
            this.btnSchedule.Visible = false;
            this.btnSchedule.Click += new System.EventHandler(this.btnSchedule_Click);
            // 
            // frmNewExpense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 238);
            this.Controls.Add(this.txtScheduleEnd);
            this.Controls.Add(this.txtScheduleStart);
            this.Controls.Add(this.chbSchedule);
            this.Controls.Add(this.chbRecurring);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtPercent);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSchedule);
            this.Controls.Add(this.lblScheduleStart);
            this.Controls.Add(this.lblScheduleEnd);
            this.Name = "frmNewExpense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Expense";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.TextBox txtPercent;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.CheckBox chbRecurring;
        private System.Windows.Forms.CheckBox chbSchedule;
        private System.Windows.Forms.Label lblScheduleStart;
        private System.Windows.Forms.TextBox txtScheduleStart;
        private System.Windows.Forms.TextBox txtScheduleEnd;
        private System.Windows.Forms.Label lblScheduleEnd;
        private System.Windows.Forms.Button btnSchedule;
    }
}