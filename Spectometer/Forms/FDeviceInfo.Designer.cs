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
            this.Res = new System.Windows.Forms.Label();
            this.Reserved = new System.Windows.Forms.Label();
            this.Serial = new System.Windows.Forms.Label();
            this.SerNumber = new System.Windows.Forms.Label();
            this.VerInfo = new System.Windows.Forms.Label();
            this.Version = new System.Windows.Forms.Label();
            this.F2Date = new System.Windows.Forms.Label();
            this.ManuDate = new System.Windows.Forms.Label();
            this.Information = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRes = new System.Windows.Forms.Label();
            this.lblDevice = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDeuterium = new System.Windows.Forms.Label();
            this.lblTangestan = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Information.SuspendLayout();
            this.SuspendLayout();
            // 
            // Res
            // 
            this.Res.AutoSize = true;
            this.Res.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Res.Location = new System.Drawing.Point(192, 110);
            this.Res.Name = "Res";
            this.Res.Size = new System.Drawing.Size(32, 20);
            this.Res.TabIndex = 16;
            this.Res.Text = "Res";
            // 
            // Reserved
            // 
            this.Reserved.AutoSize = true;
            this.Reserved.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Reserved.Location = new System.Drawing.Point(34, 110);
            this.Reserved.Name = "Reserved";
            this.Reserved.Size = new System.Drawing.Size(152, 20);
            this.Reserved.TabIndex = 15;
            this.Reserved.Text = "Reserved                   :";
            // 
            // Serial
            // 
            this.Serial.AutoSize = true;
            this.Serial.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Serial.Location = new System.Drawing.Point(192, 80);
            this.Serial.Name = "Serial";
            this.Serial.Size = new System.Drawing.Size(51, 20);
            this.Serial.TabIndex = 14;
            this.Serial.Text = "Serial ";
            // 
            // SerNumber
            // 
            this.SerNumber.AutoSize = true;
            this.SerNumber.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.SerNumber.Location = new System.Drawing.Point(31, 80);
            this.SerNumber.Name = "SerNumber";
            this.SerNumber.Size = new System.Drawing.Size(156, 20);
            this.SerNumber.TabIndex = 13;
            this.SerNumber.Text = "Serial Number           :";
            // 
            // VerInfo
            // 
            this.VerInfo.AutoSize = true;
            this.VerInfo.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.VerInfo.Location = new System.Drawing.Point(192, 51);
            this.VerInfo.Name = "VerInfo";
            this.VerInfo.Size = new System.Drawing.Size(59, 20);
            this.VerInfo.TabIndex = 12;
            this.VerInfo.Text = "VerInfo";
            // 
            // Version
            // 
            this.Version.AutoSize = true;
            this.Version.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Version.Location = new System.Drawing.Point(31, 51);
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
            this.Information.Controls.Add(this.button1);
            this.Information.Controls.Add(this.label1);
            this.Information.Controls.Add(this.lblRes);
            this.Information.Controls.Add(this.lblDevice);
            this.Information.Controls.Add(this.label4);
            this.Information.Controls.Add(this.label5);
            this.Information.Controls.Add(this.lblDeuterium);
            this.Information.Controls.Add(this.lblTangestan);
            this.Information.Controls.Add(this.label8);
            this.Information.Controls.Add(this.ManuDate);
            this.Information.Controls.Add(this.Res);
            this.Information.Controls.Add(this.F2Date);
            this.Information.Controls.Add(this.Reserved);
            this.Information.Controls.Add(this.Version);
            this.Information.Controls.Add(this.Serial);
            this.Information.Controls.Add(this.VerInfo);
            this.Information.Controls.Add(this.SerNumber);
            this.Information.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Information.Location = new System.Drawing.Point(12, 57);
            this.Information.Name = "Information";
            this.Information.Size = new System.Drawing.Size(412, 366);
            this.Information.TabIndex = 17;
            this.Information.TabStop = false;
            this.Information.Text = "Information";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(149, 276);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 44);
            this.button1.TabIndex = 25;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(34, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "Device Counter        :";
            // 
            // lblRes
            // 
            this.lblRes.AutoSize = true;
            this.lblRes.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblRes.Location = new System.Drawing.Point(195, 230);
            this.lblRes.Name = "lblRes";
            this.lblRes.Size = new System.Drawing.Size(50, 20);
            this.lblRes.TabIndex = 24;
            this.lblRes.Text = "label2";
            // 
            // lblDevice
            // 
            this.lblDevice.AutoSize = true;
            this.lblDevice.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblDevice.Location = new System.Drawing.Point(195, 143);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(0, 20);
            this.lblDevice.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(37, 230);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 20);
            this.label4.TabIndex = 23;
            this.label4.Text = "Reserved                   :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label5.Location = new System.Drawing.Point(34, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 20);
            this.label5.TabIndex = 19;
            this.label5.Text = "Tungsten  Counter    :";
            // 
            // lblDeuterium
            // 
            this.lblDeuterium.AutoSize = true;
            this.lblDeuterium.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblDeuterium.Location = new System.Drawing.Point(195, 200);
            this.lblDeuterium.Name = "lblDeuterium";
            this.lblDeuterium.Size = new System.Drawing.Size(0, 20);
            this.lblDeuterium.TabIndex = 22;
            // 
            // lblTangestan
            // 
            this.lblTangestan.AutoSize = true;
            this.lblTangestan.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblTangestan.Location = new System.Drawing.Point(195, 171);
            this.lblTangestan.Name = "lblTangestan";
            this.lblTangestan.Size = new System.Drawing.Size(0, 20);
            this.lblTangestan.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label8.Location = new System.Drawing.Point(34, 200);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(153, 20);
            this.label8.TabIndex = 21;
            this.label8.Text = "Deuterium Counter  :";
            // 
            // FDeviceInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 448);
            this.Controls.Add(this.Information);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FDeviceInfo";
            this.Text = "Device Information";
            this.Controls.SetChildIndex(this.Information, 0);
            this.Information.ResumeLayout(false);
            this.Information.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label Res;
        private System.Windows.Forms.Label Reserved;
        internal System.Windows.Forms.Label Serial;
        private System.Windows.Forms.Label SerNumber;
        internal System.Windows.Forms.Label Version;
        public System.Windows.Forms.Label ManuDate;
        private System.Windows.Forms.GroupBox Information;
        internal System.Windows.Forms.Label VerInfo;
        internal System.Windows.Forms.Label F2Date;
        public System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label lblRes;
        internal System.Windows.Forms.Label lblDevice;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label lblDeuterium;
        internal System.Windows.Forms.Label lblTangestan;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
    }
}