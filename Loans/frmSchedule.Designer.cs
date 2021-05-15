namespace Loans
{
    partial class frmSchedule
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
            this.cdrStart = new System.Windows.Forms.MonthCalendar();
            this.cdrEnd = new System.Windows.Forms.MonthCalendar();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblEnd = new System.Windows.Forms.Label();
            this.txtStart = new System.Windows.Forms.TextBox();
            this.txtEnd = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cdrStart
            // 
            this.cdrStart.Location = new System.Drawing.Point(18, 54);
            this.cdrStart.MaxSelectionCount = 1;
            this.cdrStart.Name = "cdrStart";
            this.cdrStart.TabIndex = 0;
            this.cdrStart.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.cdrStart_DateChanged);
            this.cdrStart.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.cdrStart_DateSelected);
            // 
            // cdrEnd
            // 
            this.cdrEnd.Location = new System.Drawing.Point(303, 54);
            this.cdrEnd.MaxSelectionCount = 1;
            this.cdrEnd.Name = "cdrEnd";
            this.cdrEnd.TabIndex = 1;
            this.cdrEnd.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.cdrEnd_DateChanged);
            this.cdrEnd.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.cdrEnd_DateSelected);
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStart.Location = new System.Drawing.Point(82, 21);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(99, 24);
            this.lblStart.TabIndex = 2;
            this.lblStart.Text = "Start Date";
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnd.Location = new System.Drawing.Point(369, 21);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(96, 24);
            this.lblEnd.TabIndex = 3;
            this.lblEnd.Text = "End Date";
            // 
            // txtStart
            // 
            this.txtStart.Location = new System.Drawing.Point(81, 228);
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(100, 20);
            this.txtStart.TabIndex = 4;
            this.txtStart.Enter += new System.EventHandler(this.txtStart_Enter);
            this.txtStart.Leave += new System.EventHandler(this.txtStart_Leave);
            // 
            // txtEnd
            // 
            this.txtEnd.Location = new System.Drawing.Point(365, 228);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new System.Drawing.Size(100, 20);
            this.txtEnd.TabIndex = 5;
            this.txtEnd.Leave += new System.EventHandler(this.txtEnd_Leave);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(455, 278);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(18, 278);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 313);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtEnd);
            this.Controls.Add(this.txtStart);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.cdrEnd);
            this.Controls.Add(this.cdrStart);
            this.Name = "frmSchedule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Schedule An Expense";
            this.Load += new System.EventHandler(this.frmSchedule_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MonthCalendar cdrStart;
        private System.Windows.Forms.MonthCalendar cdrEnd;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.TextBox txtStart;
        private System.Windows.Forms.TextBox txtEnd;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}