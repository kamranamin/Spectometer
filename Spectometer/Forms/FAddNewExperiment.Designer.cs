namespace Spectometer.Forms
{
    partial class FAddNewExperiment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FAddNewExperiment));
            this.btnAccept = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExperimentName = new System.Windows.Forms.TextBox();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lbl_form_text
            // 
            this._lbl_form_text.Text = "F_Base";
            // 
            // btnAccept
            // 
            this.btnAccept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnAccept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAccept.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnAccept.FlatAppearance.BorderSize = 0;
            this.btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccept.ForeColor = System.Drawing.Color.White;
            this.btnAccept.Location = new System.Drawing.Point(134, 118);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(100, 38);
            this.btnAccept.TabIndex = 7;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.txtExperimentName);
            this.panel5.Location = new System.Drawing.Point(43, 56);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(319, 43);
            this.panel5.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(11, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Experiment Name :";
            // 
            // txtExperimentName
            // 
            this.txtExperimentName.Location = new System.Drawing.Point(157, 8);
            this.txtExperimentName.Name = "txtExperimentName";
            this.txtExperimentName.Size = new System.Drawing.Size(148, 27);
            this.txtExperimentName.TabIndex = 0;
            // 
            // FAddNewExperiment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(477, 176);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.panel5);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FAddNewExperiment";
            this.Text = "Add new experiment";
            this.Controls.SetChildIndex(this.panel5, 0);
            this.Controls.SetChildIndex(this.btnAccept, 0);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtExperimentName;
    }
}
