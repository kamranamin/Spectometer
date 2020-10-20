namespace Spectometer.Forms
{
    partial class FMath
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
            this.cmb1cmb1 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSeriesName = new System.Windows.Forms.TextBox();
            this.btnCalc = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lbl_form_text
            // 
            this._lbl_form_text.Size = new System.Drawing.Size(943, 30);
            this._lbl_form_text.Text = "Formula Spectrum";
            this._lbl_form_text.Click += new System.EventHandler(this._lbl_form_text_Click);
            // 
            // cmb1cmb1
            // 
            this.cmb1cmb1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb1cmb1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb1cmb1.FormattingEnabled = true;
            this.cmb1cmb1.Location = new System.Drawing.Point(133, 35);
            this.cmb1cmb1.Name = "cmb1cmb1";
            this.cmb1cmb1.Size = new System.Drawing.Size(121, 28);
            this.cmb1cmb1.TabIndex = 6;
            // 
            // comboBox1
            // 
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "+",
            "-",
            "*",
            "/"});
            this.comboBox1.Location = new System.Drawing.Point(374, 35);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(44, 28);
            this.comboBox1.TabIndex = 7;
            // 
            // comboBox2
            // 
            this.comboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(542, 35);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 28);
            this.comboBox2.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(28, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "First Series :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(427, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Second Series :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(271, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Operation :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(687, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = " Series Name :";
            // 
            // txtSeriesName
            // 
            this.txtSeriesName.Location = new System.Drawing.Point(795, 35);
            this.txtSeriesName.Name = "txtSeriesName";
            this.txtSeriesName.Size = new System.Drawing.Size(100, 27);
            this.txtSeriesName.TabIndex = 13;
            this.txtSeriesName.Text = "Formula";
            // 
            // btnCalc
            // 
            this.btnCalc.BackColor = System.Drawing.Color.Blue;
            this.btnCalc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalc.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCalc.Location = new System.Drawing.Point(918, 36);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(100, 27);
            this.btnCalc.TabIndex = 14;
            this.btnCalc.Text = "Calculate";
            this.btnCalc.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCalc.UseVisualStyleBackColor = false;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCalc);
            this.groupBox1.Controls.Add(this.cmb1cmb1);
            this.groupBox1.Controls.Add(this.txtSeriesName);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.ForeColor = System.Drawing.Color.Snow;
            this.groupBox1.Location = new System.Drawing.Point(23, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1054, 100);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Formula Spectrum";
            // 
            // FMath
            // 
            this.AcceptButton = this.btnCalc;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 177);
            this.Controls.Add(this.groupBox1);
            this.Name = "FMath";
            this.Text = "Formula Spectrum";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FMath_FormClosing);
            this.Load += new System.EventHandler(this.FMath_Load);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.ComboBox cmb1cmb1;
        public System.Windows.Forms.ComboBox comboBox2;
        public System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtSeriesName;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}