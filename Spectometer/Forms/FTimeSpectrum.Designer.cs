namespace Spectometer.Forms
{
    partial class FTimeSpectrum
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTimeSpectrum));
            this.btnDisable = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtW6 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtW5 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtW4 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtW3 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtW2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtW1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTimeInterval = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTimeDuration = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblW6 = new System.Windows.Forms.Label();
            this.lblW5 = new System.Windows.Forms.Label();
            this.lblW4 = new System.Windows.Forms.Label();
            this.lblW3 = new System.Windows.Forms.Label();
            this.lblW2 = new System.Windows.Forms.Label();
            this.lblW1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lbltime = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel7 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lbl_form_text
            // 
            this._lbl_form_text.Size = new System.Drawing.Size(1228, 30);
            this._lbl_form_text.Text = "F_Base";
            // 
            // btnDisable
            // 
            this.btnDisable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnDisable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDisable.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnDisable.FlatAppearance.BorderSize = 0;
            this.btnDisable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisable.ForeColor = System.Drawing.Color.White;
            this.btnDisable.Location = new System.Drawing.Point(423, 118);
            this.btnDisable.Name = "btnDisable";
            this.btnDisable.Size = new System.Drawing.Size(103, 39);
            this.btnDisable.TabIndex = 16;
            this.btnDisable.Text = "Enable";
            this.btnDisable.UseVisualStyleBackColor = true;
            this.btnDisable.Click += new System.EventHandler(this.btnDisable_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(124, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "W6 :";
            // 
            // txtW6
            // 
            this.txtW6.Location = new System.Drawing.Point(169, 184);
            this.txtW6.Name = "txtW6";
            this.txtW6.Size = new System.Drawing.Size(79, 27);
            this.txtW6.TabIndex = 6;
            this.txtW6.TextChanged += new System.EventHandler(this.txtW6_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(124, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "W5 :";
            // 
            // txtW5
            // 
            this.txtW5.Location = new System.Drawing.Point(169, 147);
            this.txtW5.Name = "txtW5";
            this.txtW5.Size = new System.Drawing.Size(79, 27);
            this.txtW5.TabIndex = 5;
            this.txtW5.TextChanged += new System.EventHandler(this.txtW5_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Silver;
            this.label9.Location = new System.Drawing.Point(122, 113);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "W4 :";
            // 
            // txtW4
            // 
            this.txtW4.Location = new System.Drawing.Point(168, 108);
            this.txtW4.Name = "txtW4";
            this.txtW4.Size = new System.Drawing.Size(79, 27);
            this.txtW4.TabIndex = 4;
            this.txtW4.TextChanged += new System.EventHandler(this.txtW4_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Silver;
            this.label8.Location = new System.Drawing.Point(123, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "W3 :";
            // 
            // txtW3
            // 
            this.txtW3.Location = new System.Drawing.Point(168, 73);
            this.txtW3.Name = "txtW3";
            this.txtW3.Size = new System.Drawing.Size(79, 27);
            this.txtW3.TabIndex = 3;
            this.txtW3.TextChanged += new System.EventHandler(this.txtW3_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Silver;
            this.label7.Location = new System.Drawing.Point(123, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "W2 :";
            // 
            // txtW2
            // 
            this.txtW2.Location = new System.Drawing.Point(168, 38);
            this.txtW2.Name = "txtW2";
            this.txtW2.Size = new System.Drawing.Size(79, 27);
            this.txtW2.TabIndex = 2;
            this.txtW2.TextChanged += new System.EventHandler(this.txtW2_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(125, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "W1 :";
            // 
            // txtW1
            // 
            this.txtW1.Location = new System.Drawing.Point(168, 3);
            this.txtW1.Name = "txtW1";
            this.txtW1.Size = new System.Drawing.Size(79, 27);
            this.txtW1.TabIndex = 1;
            this.txtW1.TextChanged += new System.EventHandler(this.txtW1_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtTimeInterval);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtTimeDuration);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Location = new System.Drawing.Point(423, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(247, 100);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label5.Location = new System.Drawing.Point(209, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Sec";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label6.Location = new System.Drawing.Point(210, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Min";
            // 
            // txtTimeInterval
            // 
            this.txtTimeInterval.Location = new System.Drawing.Point(125, 18);
            this.txtTimeInterval.Name = "txtTimeInterval";
            this.txtTimeInterval.Size = new System.Drawing.Size(79, 27);
            this.txtTimeInterval.TabIndex = 13;
            this.txtTimeInterval.Text = "0";
            this.txtTimeInterval.TextChanged += new System.EventHandler(this.txtTimeInterval_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Silver;
            this.label4.Location = new System.Drawing.Point(7, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Time Interval :";
            // 
            // txtTimeDuration
            // 
            this.txtTimeDuration.Location = new System.Drawing.Point(124, 55);
            this.txtTimeDuration.Name = "txtTimeDuration";
            this.txtTimeDuration.Size = new System.Drawing.Size(79, 27);
            this.txtTimeDuration.TabIndex = 14;
            this.txtTimeDuration.Text = "0";
            this.txtTimeDuration.TextChanged += new System.EventHandler(this.txtTimeDuration_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Silver;
            this.label10.Location = new System.Drawing.Point(4, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(115, 20);
            this.label10.TabIndex = 10;
            this.label10.Text = "Time Duration :";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // lblW6
            // 
            this.lblW6.AutoSize = true;
            this.lblW6.ForeColor = System.Drawing.Color.Silver;
            this.lblW6.Location = new System.Drawing.Point(266, 191);
            this.lblW6.Name = "lblW6";
            this.lblW6.Size = new System.Drawing.Size(0, 20);
            this.lblW6.TabIndex = 18;
            // 
            // lblW5
            // 
            this.lblW5.AutoSize = true;
            this.lblW5.ForeColor = System.Drawing.Color.Silver;
            this.lblW5.Location = new System.Drawing.Point(265, 155);
            this.lblW5.Name = "lblW5";
            this.lblW5.Size = new System.Drawing.Size(0, 20);
            this.lblW5.TabIndex = 19;
            // 
            // lblW4
            // 
            this.lblW4.AutoSize = true;
            this.lblW4.ForeColor = System.Drawing.Color.Silver;
            this.lblW4.Location = new System.Drawing.Point(263, 118);
            this.lblW4.Name = "lblW4";
            this.lblW4.Size = new System.Drawing.Size(0, 20);
            this.lblW4.TabIndex = 20;
            // 
            // lblW3
            // 
            this.lblW3.AutoSize = true;
            this.lblW3.ForeColor = System.Drawing.Color.Silver;
            this.lblW3.Location = new System.Drawing.Point(264, 82);
            this.lblW3.Name = "lblW3";
            this.lblW3.Size = new System.Drawing.Size(0, 20);
            this.lblW3.TabIndex = 21;
            // 
            // lblW2
            // 
            this.lblW2.AutoSize = true;
            this.lblW2.ForeColor = System.Drawing.Color.Silver;
            this.lblW2.Location = new System.Drawing.Point(264, 45);
            this.lblW2.Name = "lblW2";
            this.lblW2.Size = new System.Drawing.Size(0, 20);
            this.lblW2.TabIndex = 22;
            // 
            // lblW1
            // 
            this.lblW1.AutoSize = true;
            this.lblW1.ForeColor = System.Drawing.Color.Silver;
            this.lblW1.Location = new System.Drawing.Point(266, 6);
            this.lblW1.Name = "lblW1";
            this.lblW1.Size = new System.Drawing.Size(0, 20);
            this.lblW1.TabIndex = 23;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 40;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.Gray;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 40;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(610, 250);
            this.dataGridView1.TabIndex = 24;
            // 
            // Column1
            // 
            dataGridViewCellStyle2.NullValue = null;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "Time";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "W1";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "W2";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "W3";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "W4";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "W5";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "W6";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.dataGridView1);
            this.panel5.Location = new System.Drawing.Point(737, 102);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(610, 250);
            this.panel5.TabIndex = 25;
            this.panel5.Paint += new System.Windows.Forms.PaintEventHandler(this.panel5_Paint);
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.panel6.Controls.Add(this.lbltime);
            this.panel6.Controls.Add(this.button1);
            this.panel6.Controls.Add(this.txtW1);
            this.panel6.Controls.Add(this.btnDisable);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.txtW2);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.txtW3);
            this.panel6.Controls.Add(this.label8);
            this.panel6.Controls.Add(this.lblW6);
            this.panel6.Controls.Add(this.txtW4);
            this.panel6.Controls.Add(this.lblW5);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Controls.Add(this.lblW4);
            this.panel6.Controls.Add(this.txtW5);
            this.panel6.Controls.Add(this.lblW3);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.lblW2);
            this.panel6.Controls.Add(this.txtW6);
            this.panel6.Controls.Add(this.lblW1);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.groupBox1);
            this.panel6.Location = new System.Drawing.Point(16, 17);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(678, 230);
            this.panel6.TabIndex = 26;
            this.panel6.Paint += new System.Windows.Forms.PaintEventHandler(this.panel6_Paint);
            // 
            // lbltime
            // 
            this.lbltime.AutoSize = true;
            this.lbltime.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lbltime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lbltime.Location = new System.Drawing.Point(419, 184);
            this.lbltime.Name = "lbltime";
            this.lbltime.Size = new System.Drawing.Size(0, 22);
            this.lbltime.TabIndex = 25;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(561, 118);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 39);
            this.button1.TabIndex = 24;
            this.button1.Text = "Reset";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(1, 31);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1384, 40);
            this.menuStrip1.TabIndex = 27;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excelToolStripMenuItem,
            this.imageToolStripMenuItem});
            this.resetToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(52, 36);
            this.resetToolStripMenuItem.Text = "&Export";
            // 
            // excelToolStripMenuItem
            // 
            this.excelToolStripMenuItem.Name = "excelToolStripMenuItem";
            this.excelToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.excelToolStripMenuItem.Text = "&Excel";
            this.excelToolStripMenuItem.Click += new System.EventHandler(this.excelToolStripMenuItem_Click);
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.imageToolStripMenuItem.Text = "&Image";
            this.imageToolStripMenuItem.Click += new System.EventHandler(this.imageToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel6);
            this.groupBox2.Location = new System.Drawing.Point(12, 91);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(719, 261);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.chart1);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox3.Location = new System.Drawing.Point(12, 358);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1335, 337);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(6, 13);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(1323, 318);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.FormatNumber += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.FormatNumberEventArgs>(this.chart1_FormatNumber);
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            this.chart1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseClick);
            this.chart1.MouseLeave += new System.EventHandler(this.chart1_MouseLeave);
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.Controls.Add(this.progressBar1);
            this.panel7.Location = new System.Drawing.Point(1009, 694);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(352, 32);
            this.panel7.TabIndex = 30;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(0, 0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(352, 32);
            this.progressBar1.TabIndex = 0;
            // 
            // FTimeSpectrum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(1386, 736);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel5);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FTimeSpectrum";
            this.Text = "Time Spectrum";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FTimeSpectrum_FormClosing);
            this.Load += new System.EventHandler(this.FTimeSpectrum_Load);
            this.Controls.SetChildIndex(this.panel5, 0);
            this.Controls.SetChildIndex(this.menuStrip1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.panel7, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox txtTimeInterval;
        public System.Windows.Forms.TextBox txtTimeDuration;
        public System.Windows.Forms.TextBox txtW6;
        public System.Windows.Forms.TextBox txtW5;
        public System.Windows.Forms.TextBox txtW4;
        public System.Windows.Forms.TextBox txtW3;
        public System.Windows.Forms.TextBox txtW2;
        public System.Windows.Forms.TextBox txtW1;
        public System.Windows.Forms.Label lblW4;
        public System.Windows.Forms.Label lblW3;
        public System.Windows.Forms.Label lblW2;
        public System.Windows.Forms.Label lblW1;
        public System.Windows.Forms.Label lblW6;
        public System.Windows.Forms.Label lblW5;
        public System.Windows.Forms.Button btnDisable;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label lbltime;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}
