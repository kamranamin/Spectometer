namespace Spectometer.Forms
{
    partial class FChart
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel5 = new System.Windows.Forms.Panel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this._pnl_status_splitor = new System.Windows.Forms.Panel();
            this.lblposition = new System.Windows.Forms.Label();
            this._pnl_status = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this._pnl_status.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lbl_form_text
            // 
            this._lbl_form_text.Size = new System.Drawing.Size(468, 38);
            this._lbl_form_text.Text = "F_Base";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.chart1);
            this.panel5.Location = new System.Drawing.Point(22, 84);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(598, 437);
            this.panel5.TabIndex = 6;
            // 
            // chart1
            // 
            this.chart1.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chart1.BorderlineColor = System.Drawing.Color.Silver;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(598, 437);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseMove);
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(20, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(47, 24);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "3D";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // _pnl_status_splitor
            // 
            this._pnl_status_splitor.Dock = System.Windows.Forms.DockStyle.Left;
            this._pnl_status_splitor.Location = new System.Drawing.Point(5, 5);
            this._pnl_status_splitor.Name = "_pnl_status_splitor";
            this._pnl_status_splitor.Size = new System.Drawing.Size(4, 30);
            this._pnl_status_splitor.TabIndex = 1;
            // 
            // lblposition
            // 
            this.lblposition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblposition.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblposition.ForeColor = System.Drawing.Color.Silver;
            this.lblposition.Location = new System.Drawing.Point(9, 5);
            this.lblposition.Name = "lblposition";
            this.lblposition.Padding = new System.Windows.Forms.Padding(1);
            this.lblposition.Size = new System.Drawing.Size(301, 30);
            this.lblposition.TabIndex = 2;
            // 
            // _pnl_status
            // 
            this._pnl_status.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._pnl_status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this._pnl_status.Controls.Add(this.panel6);
            this._pnl_status.Controls.Add(this.lblposition);
            this._pnl_status.Controls.Add(this._pnl_status_splitor);
            this._pnl_status.Location = new System.Drawing.Point(1, 527);
            this._pnl_status.Name = "_pnl_status";
            this._pnl_status.Padding = new System.Windows.Forms.Padding(5);
            this._pnl_status.Size = new System.Drawing.Size(599, 40);
            this._pnl_status.TabIndex = 9;
            // 
            // panel6
            // 
            this.panel6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel6.Controls.Add(this.checkBox1);
            this.panel6.Location = new System.Drawing.Point(506, 5);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(85, 26);
            this.panel6.TabIndex = 3;
            // 
            // FChart
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 568);
            this.Controls.Add(this._pnl_status);
            this.Controls.Add(this.panel5);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "FChart";
            this.Text = "FChart";
            this.TransparencyKey = System.Drawing.Color.DarkSalmon;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FChart_FormClosing);
            this.Load += new System.EventHandler(this.FChart_Load);
            this.Controls.SetChildIndex(this.panel5, 0);
            this.Controls.SetChildIndex(this._pnl_status, 0);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this._pnl_status.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel _pnl_status_splitor;
        private System.Windows.Forms.Label lblposition;
        private System.Windows.Forms.Panel _pnl_status;
        private System.Windows.Forms.Panel panel6;
    }
}