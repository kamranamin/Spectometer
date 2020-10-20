namespace Spectometer.Forms
{
    partial class DeviceCounter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceCounter));
            this.label1 = new System.Windows.Forms.Label();
            this.lblDevice = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDeuterium = new System.Windows.Forms.Label();
            this.lblTangestan = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _lbl_form_text
            // 
            this._lbl_form_text.Size = new System.Drawing.Size(177, 30);
            this._lbl_form_text.Text = "F_Base";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(12, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 20);
            this.label1.TabIndex = 23;
            this.label1.Text = "Device Counter        :";
            // 
            // lblDevice
            // 
            this.lblDevice.AutoSize = true;
            this.lblDevice.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblDevice.Location = new System.Drawing.Point(173, 69);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(0, 20);
            this.lblDevice.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label5.Location = new System.Drawing.Point(12, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 20);
            this.label5.TabIndex = 25;
            this.label5.Text = "Tungsten  Counter    :";
            // 
            // lblDeuterium
            // 
            this.lblDeuterium.AutoSize = true;
            this.lblDeuterium.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblDeuterium.Location = new System.Drawing.Point(173, 126);
            this.lblDeuterium.Name = "lblDeuterium";
            this.lblDeuterium.Size = new System.Drawing.Size(0, 20);
            this.lblDeuterium.TabIndex = 28;
            // 
            // lblTangestan
            // 
            this.lblTangestan.AutoSize = true;
            this.lblTangestan.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblTangestan.Location = new System.Drawing.Point(173, 97);
            this.lblTangestan.Name = "lblTangestan";
            this.lblTangestan.Size = new System.Drawing.Size(0, 20);
            this.lblTangestan.TabIndex = 26;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label8.Location = new System.Drawing.Point(12, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(153, 20);
            this.label8.TabIndex = 27;
            this.label8.Text = "Deuterium Counter  :";
            // 
            // DeviceCounter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 207);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDevice);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblDeuterium);
            this.Controls.Add(this.lblTangestan);
            this.Controls.Add(this.label8);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DeviceCounter";
            this.Text = "Device Counter";
            this.Load += new System.EventHandler(this.DeviceCounter_Load);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.lblTangestan, 0);
            this.Controls.SetChildIndex(this.lblDeuterium, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.lblDevice, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label lblDevice;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label lblDeuterium;
        internal System.Windows.Forms.Label lblTangestan;
        private System.Windows.Forms.Label label8;
    }
}