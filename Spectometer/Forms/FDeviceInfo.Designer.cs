namespace Spectometer.Forms
{
    partial class FDeviceInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FDeviceInfo));
            this.temp = new System.Windows.Forms.Label();
            this.Reserved = new System.Windows.Forms.Label();
            this.Serial = new System.Windows.Forms.Label();
            this.SerNumber = new System.Windows.Forms.Label();
            this.VerInfo = new System.Windows.Forms.Label();
            this.Version = new System.Windows.Forms.Label();
            this.F2Date = new System.Windows.Forms.Label();
            this.ManuDate = new System.Windows.Forms.Label();
            this.Information = new System.Windows.Forms.GroupBox();
            this.txtRes = new System.Windows.Forms.MaskedTextBox();
            this.txtSeialnumber = new System.Windows.Forms.MaskedTextBox();
            this.txtVer = new System.Windows.Forms.MaskedTextBox();
            this.maskTxtDate = new System.Windows.Forms.MaskedTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Information.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lbl_form_text
            // 
            this._lbl_form_text.Size = new System.Drawing.Size(521, 30);
            this._lbl_form_text.Text = "F_Base";
            // 
            // temp
            // 
            this.temp.AutoSize = true;
            this.temp.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.temp.Location = new System.Drawing.Point(192, 130);
            this.temp.Name = "temp";
            this.temp.Size = new System.Drawing.Size(0, 20);
            this.temp.TabIndex = 16;
            // 
            // Reserved
            // 
            this.Reserved.AutoSize = true;
            this.Reserved.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Reserved.Location = new System.Drawing.Point(34, 130);
            this.Reserved.Name = "Reserved";
            this.Reserved.Size = new System.Drawing.Size(152, 20);
            this.Reserved.TabIndex = 15;
            this.Reserved.Text = "Temprature               :";
            // 
            // Serial
            // 
            this.Serial.AutoSize = true;
            this.Serial.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Serial.Location = new System.Drawing.Point(192, 89);
            this.Serial.Name = "Serial";
            this.Serial.Size = new System.Drawing.Size(51, 20);
            this.Serial.TabIndex = 14;
            this.Serial.Text = "Serial ";
            // 
            // SerNumber
            // 
            this.SerNumber.AutoSize = true;
            this.SerNumber.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.SerNumber.Location = new System.Drawing.Point(31, 89);
            this.SerNumber.Name = "SerNumber";
            this.SerNumber.Size = new System.Drawing.Size(156, 20);
            this.SerNumber.TabIndex = 13;
            this.SerNumber.Text = "Serial Number           :";
            // 
            // VerInfo
            // 
            this.VerInfo.AutoSize = true;
            this.VerInfo.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.VerInfo.Location = new System.Drawing.Point(192, 54);
            this.VerInfo.Name = "VerInfo";
            this.VerInfo.Size = new System.Drawing.Size(59, 20);
            this.VerInfo.TabIndex = 12;
            this.VerInfo.Text = "VerInfo";
            // 
            // Version
            // 
            this.Version.AutoSize = true;
            this.Version.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Version.Location = new System.Drawing.Point(31, 54);
            this.Version.Name = "Version";
            this.Version.Size = new System.Drawing.Size(156, 20);
            this.Version.TabIndex = 11;
            this.Version.Text = "Version                       :";
            // 
            // F2Date
            // 
            this.F2Date.AutoSize = true;
            this.F2Date.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.F2Date.Location = new System.Drawing.Point(192, 23);
            this.F2Date.Name = "F2Date";
            this.F2Date.Size = new System.Drawing.Size(0, 20);
            this.F2Date.TabIndex = 10;
            this.F2Date.Click += new System.EventHandler(this.F2Date_Click);
            // 
            // ManuDate
            // 
            this.ManuDate.AutoSize = true;
            this.ManuDate.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ManuDate.Location = new System.Drawing.Point(31, 23);
            this.ManuDate.Name = "ManuDate";
            this.ManuDate.Size = new System.Drawing.Size(155, 20);
            this.ManuDate.TabIndex = 9;
            this.ManuDate.Text = "Manufacturing Date :";
            // 
            // Information
            // 
            this.Information.Controls.Add(this.txtRes);
            this.Information.Controls.Add(this.txtSeialnumber);
            this.Information.Controls.Add(this.txtVer);
            this.Information.Controls.Add(this.maskTxtDate);
            this.Information.Controls.Add(this.button1);
            this.Information.Controls.Add(this.ManuDate);
            this.Information.Controls.Add(this.temp);
            this.Information.Controls.Add(this.F2Date);
            this.Information.Controls.Add(this.Reserved);
            this.Information.Controls.Add(this.Version);
            this.Information.Controls.Add(this.Serial);
            this.Information.Controls.Add(this.VerInfo);
            this.Information.Controls.Add(this.SerNumber);
            this.Information.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Information.Location = new System.Drawing.Point(12, 57);
            this.Information.Name = "Information";
            this.Information.Size = new System.Drawing.Size(642, 285);
            this.Information.TabIndex = 17;
            this.Information.TabStop = false;
            this.Information.Text = "Information";
            // 
            // txtRes
            // 
            this.txtRes.Location = new System.Drawing.Point(351, 128);
            this.txtRes.Mask = "####";
            this.txtRes.Name = "txtRes";
            this.txtRes.Size = new System.Drawing.Size(87, 27);
            this.txtRes.TabIndex = 32;
            // 
            // txtSeialnumber
            // 
            this.txtSeialnumber.Location = new System.Drawing.Point(352, 89);
            this.txtSeialnumber.Mask = "############";
            this.txtSeialnumber.Name = "txtSeialnumber";
            this.txtSeialnumber.Size = new System.Drawing.Size(87, 27);
            this.txtSeialnumber.TabIndex = 31;
            // 
            // txtVer
            // 
            this.txtVer.Location = new System.Drawing.Point(353, 53);
            this.txtVer.Mask = "##.#";
            this.txtVer.Name = "txtVer";
            this.txtVer.Size = new System.Drawing.Size(87, 27);
            this.txtVer.TabIndex = 30;
            // 
            // maskTxtDate
            // 
            this.maskTxtDate.Location = new System.Drawing.Point(353, 20);
            this.maskTxtDate.Mask = "0000/00/00";
            this.maskTxtDate.Name = "maskTxtDate";
            this.maskTxtDate.Size = new System.Drawing.Size(87, 27);
            this.maskTxtDate.TabIndex = 26;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.Location = new System.Drawing.Point(346, 185);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 44);
            this.button1.TabIndex = 25;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FDeviceInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 384);
            this.Controls.Add(this.Information);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FDeviceInfo";
            this.Text = "Device Information";
            this.Load += new System.EventHandler(this.FDeviceInfo_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FDeviceInfo_KeyDown);
            this.Controls.SetChildIndex(this.Information, 0);
            this.Information.ResumeLayout(false);
            this.Information.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label temp;
        private System.Windows.Forms.Label Reserved;
        internal System.Windows.Forms.Label Serial;
        private System.Windows.Forms.Label SerNumber;
        internal System.Windows.Forms.Label Version;
        public System.Windows.Forms.Label ManuDate;
        private System.Windows.Forms.GroupBox Information;
        internal System.Windows.Forms.Label VerInfo;
        internal System.Windows.Forms.Label F2Date;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MaskedTextBox maskTxtDate;
        private System.Windows.Forms.MaskedTextBox txtRes;
        private System.Windows.Forms.MaskedTextBox txtSeialnumber;
        private System.Windows.Forms.MaskedTextBox txtVer;
    }
}