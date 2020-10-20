namespace Spectometer.Forms
{
    partial class FSingleWaveLenght
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSingleWaveLenght));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCalc = new System.Windows.Forms.Button();
            this.lblW3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtW3 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblW2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtW2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblW1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtW1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lbl_form_text
            // 
            this._lbl_form_text.Size = new System.Drawing.Size(290, 30);
            this._lbl_form_text.Text = "Single Wavelength Monitoring";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCalc);
            this.groupBox1.Controls.Add(this.lblW3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtW3);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblW2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtW2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblW1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtW1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(30, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(387, 272);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnCalc
            // 
            this.btnCalc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnCalc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnCalc.FlatAppearance.BorderSize = 0;
            this.btnCalc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalc.ForeColor = System.Drawing.Color.White;
            this.btnCalc.Location = new System.Drawing.Point(279, 217);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(80, 36);
            this.btnCalc.TabIndex = 7;
            this.btnCalc.Text = "Calculate";
            this.btnCalc.UseVisualStyleBackColor = false;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // lblW3
            // 
            this.lblW3.AutoSize = true;
            this.lblW3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblW3.Location = new System.Drawing.Point(278, 171);
            this.lblW3.Name = "lblW3";
            this.lblW3.Size = new System.Drawing.Size(41, 20);
            this.lblW3.TabIndex = 13;
            this.lblW3.Text = "0000";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label7.Location = new System.Drawing.Point(231, 170);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "nm";
            // 
            // txtW3
            // 
            this.txtW3.Location = new System.Drawing.Point(134, 167);
            this.txtW3.Name = "txtW3";
            this.txtW3.Size = new System.Drawing.Size(91, 27);
            this.txtW3.TabIndex = 11;
            this.txtW3.TextChanged += new System.EventHandler(this.txtW3_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label8.Location = new System.Drawing.Point(15, 169);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 20);
            this.label8.TabIndex = 10;
            this.label8.Text = "Wavelength 3 :";
            // 
            // lblW2
            // 
            this.lblW2.AutoSize = true;
            this.lblW2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblW2.Location = new System.Drawing.Point(278, 105);
            this.lblW2.Name = "lblW2";
            this.lblW2.Size = new System.Drawing.Size(41, 20);
            this.lblW2.TabIndex = 9;
            this.lblW2.Text = "0000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label4.Location = new System.Drawing.Point(231, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "nm";
            // 
            // txtW2
            // 
            this.txtW2.Location = new System.Drawing.Point(134, 101);
            this.txtW2.Name = "txtW2";
            this.txtW2.Size = new System.Drawing.Size(91, 27);
            this.txtW2.TabIndex = 7;
            this.txtW2.TextChanged += new System.EventHandler(this.txtW2_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label5.Location = new System.Drawing.Point(15, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Wavelength 2 :";
            // 
            // lblW1
            // 
            this.lblW1.AutoSize = true;
            this.lblW1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblW1.Location = new System.Drawing.Point(278, 40);
            this.lblW1.Name = "lblW1";
            this.lblW1.Size = new System.Drawing.Size(41, 20);
            this.lblW1.TabIndex = 5;
            this.lblW1.Text = "0000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(231, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "nm";
            // 
            // txtW1
            // 
            this.txtW1.Location = new System.Drawing.Point(134, 36);
            this.txtW1.Name = "txtW1";
            this.txtW1.Size = new System.Drawing.Size(91, 27);
            this.txtW1.TabIndex = 3;
            this.txtW1.TextChanged += new System.EventHandler(this.txtW1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(15, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Wavelength 1 :";
            // 
            // FSingleWaveLenght
            // 
            this.AcceptButton = this.btnCalc;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 374);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FSingleWaveLenght";
            this.Text = "Single Wavelength Monitoring";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FSingleWaveLenght_FormClosing);
            this.Load += new System.EventHandler(this.FSingleWaveLenght_Load);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblW3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtW3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblW2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtW2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblW1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtW1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCalc;
    }
}