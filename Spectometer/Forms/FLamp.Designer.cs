namespace Spectometer.Forms
{
    partial class FLamp
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
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.chkUVLamp = new System.Windows.Forms.CheckBox();
            this.chkTangestanLamp = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.chNanoLed = new System.Windows.Forms.CheckBox();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lbl_form_text
            // 
            this._lbl_form_text.Size = new System.Drawing.Size(108, 30);
            this._lbl_form_text.Text = "Lamp Setting";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.chNanoLed);
            this.groupBox10.Controls.Add(this.chkUVLamp);
            this.groupBox10.Controls.Add(this.chkTangestanLamp);
            this.groupBox10.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.groupBox10.Location = new System.Drawing.Point(22, 59);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(200, 126);
            this.groupBox10.TabIndex = 43;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Lamp";
            // 
            // chkUVLamp
            // 
            this.chkUVLamp.AutoSize = true;
            this.chkUVLamp.ForeColor = System.Drawing.Color.White;
            this.chkUVLamp.Location = new System.Drawing.Point(12, 53);
            this.chkUVLamp.Name = "chkUVLamp";
            this.chkUVLamp.Size = new System.Drawing.Size(142, 24);
            this.chkUVLamp.TabIndex = 1;
            this.chkUVLamp.Text = "Deuterium Lamp";
            this.chkUVLamp.UseVisualStyleBackColor = true;
            // 
            // chkTangestanLamp
            // 
            this.chkTangestanLamp.AutoSize = true;
            this.chkTangestanLamp.ForeColor = System.Drawing.Color.White;
            this.chkTangestanLamp.Location = new System.Drawing.Point(13, 20);
            this.chkTangestanLamp.Name = "chkTangestanLamp";
            this.chkTangestanLamp.Size = new System.Drawing.Size(131, 24);
            this.chkTangestanLamp.TabIndex = 0;
            this.chkTangestanLamp.Text = "Tungsten Lamp";
            this.chkTangestanLamp.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(137, 191);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 31);
            this.btnCancel.TabIndex = 45;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(27, 191);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(84, 31);
            this.btnSave.TabIndex = 44;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chNanoLed
            // 
            this.chNanoLed.AutoSize = true;
            this.chNanoLed.ForeColor = System.Drawing.Color.White;
            this.chNanoLed.Location = new System.Drawing.Point(12, 85);
            this.chNanoLed.Name = "chNanoLed";
            this.chNanoLed.Size = new System.Drawing.Size(94, 24);
            this.chNanoLed.TabIndex = 2;
            this.chNanoLed.Text = "Nano Led";
            this.chNanoLed.UseVisualStyleBackColor = true;
            // 
            // FLamp
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 251);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox10);
            this.Name = "FLamp";
            this.Text = "Lamp Setting";
            this.Load += new System.EventHandler(this.FLamp_Load);
            this.Controls.SetChildIndex(this.groupBox10, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.CheckBox chkUVLamp;
        private System.Windows.Forms.CheckBox chkTangestanLamp;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chNanoLed;
    }
}