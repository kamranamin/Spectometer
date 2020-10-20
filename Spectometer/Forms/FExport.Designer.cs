namespace Spectometer.Forms
{
    partial class FExport
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
            PresentationControls.CheckBoxProperties checkBoxProperties1 = new PresentationControls.CheckBoxProperties();
            this.checkBoxComboBox1 = new PresentationControls.CheckBoxComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbpoint = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lbl_form_text
            // 
            this._lbl_form_text.Size = new System.Drawing.Size(343, 30);
            this._lbl_form_text.Text = "Export To Excel";
            // 
            // checkBoxComboBox1
            // 
            checkBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxComboBox1.CheckBoxProperties = checkBoxProperties1;
            this.checkBoxComboBox1.DisplayMemberSingleItem = "";
            this.checkBoxComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxComboBox1.FormattingEnabled = true;
            this.checkBoxComboBox1.Location = new System.Drawing.Point(133, 138);
            this.checkBoxComboBox1.Name = "checkBoxComboBox1";
            this.checkBoxComboBox1.Size = new System.Drawing.Size(305, 28);
            this.checkBoxComboBox1.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Maroon;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.FloralWhite;
            this.button1.Location = new System.Drawing.Point(133, 186);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 42);
            this.button1.TabIndex = 7;
            this.button1.Text = "Export ";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Cornsilk;
            this.label1.Location = new System.Drawing.Point(30, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Select Items :";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.ForeColor = System.Drawing.Color.Cornsilk;
            this.checkBox1.Location = new System.Drawing.Point(23, 108);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(90, 24);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "Select All";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Cornsilk;
            this.label2.Location = new System.Drawing.Point(96, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Wavelength Step :";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "3",
            "5",
            "10"});
            this.comboBox1.Location = new System.Drawing.Point(234, 24);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(45, 28);
            this.comboBox1.TabIndex = 13;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.ForeColor = System.Drawing.Color.Cornsilk;
            this.checkBox2.Location = new System.Drawing.Point(8, 26);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(82, 24);
            this.checkBox2.TabIndex = 14;
            this.checkBox2.Text = "Disable ";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbpoint);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox1.Location = new System.Drawing.Point(15, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(447, 59);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Wavelength Selection";
            // 
            // cmbpoint
            // 
            this.cmbpoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbpoint.FormattingEnabled = true;
            this.cmbpoint.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4"});
            this.cmbpoint.Location = new System.Drawing.Point(358, 22);
            this.cmbpoint.Name = "cmbpoint";
            this.cmbpoint.Size = new System.Drawing.Size(45, 28);
            this.cmbpoint.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Cornsilk;
            this.label3.Location = new System.Drawing.Point(300, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 20);
            this.label3.TabIndex = 15;
            this.label3.Text = "Point :";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // FExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 259);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBoxComboBox1);
            this.Name = "FExport";
            this.Text = "Export to Excel";
            this.Load += new System.EventHandler(this.FExport_Load);
            this.Controls.SetChildIndex(this.checkBoxComboBox1, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.checkBox1, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public PresentationControls.CheckBoxComboBox checkBoxComboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox comboBox1;
        public System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ComboBox cmbpoint;
        private System.Windows.Forms.Label label3;
    }
}