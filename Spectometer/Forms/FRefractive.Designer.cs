﻿namespace Spectometer.Forms
{
    partial class FRefractive
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblt = new System.Windows.Forms.Label();
            this.txtConversion = new System.Windows.Forms.TextBox();
            this.txtAngel = new System.Windows.Forms.TextBox();
            this.txtNR = new System.Windows.Forms.TextBox();
            this.txtLanda2 = new System.Windows.Forms.TextBox();
            this.txtLanada1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtN = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lbl_form_text
            // 
            this._lbl_form_text.Size = new System.Drawing.Size(685, 30);
            this._lbl_form_text.Text = "F_Base";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(22, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Maximum Wavelength (nm): ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtN);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblt);
            this.groupBox1.Controls.Add(this.txtConversion);
            this.groupBox1.Controls.Add(this.txtAngel);
            this.groupBox1.Controls.Add(this.txtNR);
            this.groupBox1.Controls.Add(this.txtLanda2);
            this.groupBox1.Controls.Add(this.txtLanada1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(40, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(655, 382);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // lblt
            // 
            this.lblt.AutoSize = true;
            this.lblt.ForeColor = System.Drawing.Color.Ivory;
            this.lblt.Location = new System.Drawing.Point(22, 313);
            this.lblt.Name = "lblt";
            this.lblt.Size = new System.Drawing.Size(78, 20);
            this.lblt.TabIndex = 20;
            this.lblt.Text = "Thickness:";
            // 
            // txtConversion
            // 
            this.txtConversion.Location = new System.Drawing.Point(291, 213);
            this.txtConversion.Name = "txtConversion";
            this.txtConversion.Size = new System.Drawing.Size(127, 27);
            this.txtConversion.TabIndex = 18;
            this.txtConversion.TextChanged += new System.EventHandler(this.txtConversion_TextChanged);
            // 
            // txtAngel
            // 
            this.txtAngel.Location = new System.Drawing.Point(291, 173);
            this.txtAngel.Name = "txtAngel";
            this.txtAngel.Size = new System.Drawing.Size(127, 27);
            this.txtAngel.TabIndex = 17;
            this.txtAngel.TextChanged += new System.EventHandler(this.txtAngel_TextChanged);
            // 
            // txtNR
            // 
            this.txtNR.Location = new System.Drawing.Point(291, 125);
            this.txtNR.Name = "txtNR";
            this.txtNR.Size = new System.Drawing.Size(127, 27);
            this.txtNR.TabIndex = 16;
            this.txtNR.TextChanged += new System.EventHandler(this.txtNR_TextChanged);
            // 
            // txtLanda2
            // 
            this.txtLanda2.Location = new System.Drawing.Point(291, 78);
            this.txtLanda2.Name = "txtLanda2";
            this.txtLanda2.Size = new System.Drawing.Size(127, 27);
            this.txtLanda2.TabIndex = 14;
            this.txtLanda2.TextChanged += new System.EventHandler(this.txtLanda2_TextChanged);
            // 
            // txtLanada1
            // 
            this.txtLanada1.Location = new System.Drawing.Point(291, 35);
            this.txtLanada1.Name = "txtLanada1";
            this.txtLanada1.Size = new System.Drawing.Size(127, 27);
            this.txtLanada1.TabIndex = 13;
            this.txtLanada1.TextChanged += new System.EventHandler(this.txtLanada1_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(22, 216);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(209, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Conversion factor to Micron :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(22, 173);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Angle of incidence : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(22, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Refractive index :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(22, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Minimum Wavelength (nm): ";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(315, 478);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(188, 39);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "Calculate";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtN
            // 
            this.txtN.Location = new System.Drawing.Point(291, 254);
            this.txtN.Name = "txtN";
            this.txtN.Size = new System.Drawing.Size(127, 27);
            this.txtN.TabIndex = 22;
            this.txtN.TextChanged += new System.EventHandler(this.txtN_TextChanged_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(22, 261);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 20);
            this.label4.TabIndex = 21;
            this.label4.Text = "Number of fringes :";
            // 
            // FRefractive
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 596);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Name = "FRefractive";
            this.Text = "Thickness Measurement";
            this.Load += new System.EventHandler(this.FRefractive_Load);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtConversion;
        private System.Windows.Forms.TextBox txtAngel;
        private System.Windows.Forms.TextBox txtNR;
        private System.Windows.Forms.TextBox txtLanda2;
        private System.Windows.Forms.TextBox txtLanada1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblt;
        private System.Windows.Forms.TextBox txtN;
        private System.Windows.Forms.Label label4;
    }
}