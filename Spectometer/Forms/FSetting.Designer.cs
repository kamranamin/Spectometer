namespace Spectometer.Forms
{
    partial class FSetting
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtpass = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _lbl_form_text
            // 
            this._lbl_form_text.Size = new System.Drawing.Size(206, 38);
            this._lbl_form_text.Text = "Setting";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.AliceBlue;
            this.label1.Location = new System.Drawing.Point(33, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Password :";
            // 
            // txtpass
            // 
            this.txtpass.Location = new System.Drawing.Point(133, 67);
            this.txtpass.Name = "txtpass";
            this.txtpass.Size = new System.Drawing.Size(162, 27);
            this.txtpass.TabIndex = 7;
            this.txtpass.UseSystemPasswordChar = true;
            this.txtpass.TextChanged += new System.EventHandler(this.txtpass_TextChanged);
            this.txtpass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpass_KeyDown);
            this.txtpass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtpass_KeyPress);
            // 
            // FSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 129);
            this.Controls.Add(this.txtpass);
            this.Controls.Add(this.label1);
            this.Name = "FSetting";
            this.Text = "Setting";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FSetting_KeyDown);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtpass, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtpass;
    }
}