using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Spectometer.Forms
{
    public partial class FTimeSpectrum : Spectometer.Forms.F_Base
    {
        public int W1 = 400, W2 = 0, W3 = 0, W4 = 0, W5 = 0, W6 = 0;
        public int TimeInterval = 2, TimeDuration = 2;
        public int[] timeSpecW = new int[6];
        public bool timeSpecIsRun = false;
        public int chartCOunt = 0;
        public float[] value;
        public float[] xvalue;
        public bool cLOsefrm = false;
        public string mode = "";
        public string format = "";
        DateTime dateStrat;
        System.Timers.Timer  timetest = new System.Timers.Timer();


        System.Windows.Forms.   Timer t = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer showTime = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timeD = new System.Windows.Forms.Timer(); 
        System.Diagnostics.Stopwatch stopwatchspec = new System.Diagnostics.Stopwatch();
        int[] timepecArrye = new int[6];
           FChart fchart1 = new FChart();

        private float findPos(int x)

        {
            int pos;
            float value = 0;
            for (int i = 0; i < xvalue.Length; i++)
            {
                if ((int)xvalue[i] == x)
                {
                    pos = i;
                    value = this.value [pos];
                    break;
                }
            }

            return value;
        }
        private void txtW4_TextChanged(object sender, EventArgs e)
        {
            string strNum = (sender as TextBox).Text;
            int numValue = 0;
           try
            {
                numValue = Convert.ToInt16(strNum);
            }
            catch
            {
                (sender as TextBox).Text = "";
            }
            timeSpecW[3]= W4 = numValue;
           

        }

        private void txtW5_TextChanged(object sender, EventArgs e)
        {
            string strNum = (sender as TextBox).Text;
            int numValue = 0;
            try
            {
                numValue = Convert.ToInt16(strNum);
            }
            catch
            {
                (sender as TextBox).Text = "";
            }
            timeSpecW[4]= W5 = numValue;
        }

        private void txtW6_TextChanged(object sender, EventArgs e)
        {
            string strNum = (sender as TextBox).Text;
            int numValue = 0;
            try
            {
                numValue = Convert.ToInt16(strNum);
            }
            catch
            {
                (sender as TextBox).Text = "";
            }
            timeSpecW[5]=  W6 = numValue;
        }

        private void txtTimeInterval_TextChanged(object sender, EventArgs e)
        {
            string strNum = (sender as TextBox).Text;
            int numValue = 0;
            try
            {
                numValue = Convert.ToInt16(strNum);
            }
            catch
            {
                (sender as TextBox).Text = "0";
            }
            TimeInterval  = numValue;
        }
        
        private void btnDisable_Click(object sender, EventArgs e)
        {

         
            if (!timeSpecIsRun)
            {
              
                chartCOunt++;
                btnDisable.Text = "Disable";
                t.Enabled = true;
                timeD.Enabled = true;
                int  timedu= (int)TimeSpan.FromMinutes(TimeInterval).TotalMilliseconds + (int)TimeSpan.FromSeconds(1).TotalMilliseconds;
               // System.Threading.Thread.Sleep((int)TimeSpan.FromSeconds(TimeDuration).TotalMilliseconds);
                showTime.Tick += ShowTime_Tick;
                dateStrat = DateTime.Now;
                timeD.Interval = timedu;
                timeSpecIsRun = true;
                showTime.Interval = (int)TimeSpan.FromSeconds(1).TotalMilliseconds;
                showTime.Enabled = true;
                
                
                this.chart1.ChartAreas["ChartArea1"].AxisX.Title = "Time (Sec)";

                this.chart1.ChartAreas["ChartArea1"].AxisY.Title = mode ;
                this.chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot ;
                this.chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot ;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Gray;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gray;



            }
            else
            {

                chartCOunt--;
                btnDisable.Text = "Enable";
                timeSpecIsRun = false;
                t.Interval = 1;
                t.Enabled = false;
                timeD.Enabled = false;
               




            }
        }
        TimeSpan d;
        private void ShowTime_Tick(object sender, EventArgs e)
        {
         d = DateTime.Now - dateStrat;
           
            lbltime.Text = (int)d.TotalMinutes  + ":" + d.Seconds;
            
            //stopwatchspec.Elapsed.Minutes.ToString() + ":" + stopwatchspec.Elapsed.Seconds.ToString();
        }

        private void Fchart1_FormClosed(object sender, FormClosedEventArgs e)
        {
            fchart1 = null;
        }

     
        private void FTimeSpectrum_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            cLOsefrm = true;
            this.Dispose();
          
        }
        
        private void FTimeSpectrum_Load(object sender, EventArgs e)
        {
            progressBar1.Visible = false;
            this._lbl_form_text.Text = "Time Spectrum";
            txtTimeDuration.Text = "0";

            cLOsefrm = false;
       //     fchart1.Show();
            t.Tick += T_Tick;
            timeD.Tick += TimeD_Tick;
            chart1.MouseWheel += new MouseEventHandler(this.ChartMOuseWhell);
            this.chart1.ChartAreas["ChartArea1"].AxisX.IsLabelAutoFit = true;
            this.chart1.ChartAreas["ChartArea1"].AxisY.IsLabelAutoFit = true;
            this.chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = true;
            this.chart1.ChartAreas["ChartArea1"].AxisY.LabelStyle.Enabled = true;
            this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = true;
            this.chart1.ChartAreas["ChartArea1"].CursorX.AutoScroll = true;
            this.chart1.ChartAreas["ChartArea1"].CursorY.AutoScroll = true;
            this.chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            this.chart1.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;

            this.chart1.ChartAreas["ChartArea1"].CursorY.IsUserSelectionEnabled = true;
            this.chart1.ChartAreas["ChartArea1"].CursorY.IsUserEnabled = true;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(192, 192, 192);
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(192, 192, 192);
            this.chart1.ChartAreas["ChartArea1"].CursorY.Interval = 0.01;
            this.chart1.ChartAreas["ChartArea1"].CursorX.Interval = 0.1;



        }

        private void ChartMOuseWhell(object sender, MouseEventArgs e)
        {
            try
            {
                double viewMinimum;
                double viewMaximum;
                double num3;
                double num4;
                if (e.Delta < 0)
                {
                    viewMinimum = this.chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                    viewMaximum = this.chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                    num3 = this.chart1.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                    num4 = this.chart1.ChartAreas[0].AxisY.ScaleView.ViewMaximum;
                    if (!((viewMinimum > 300.0) | (viewMaximum < 2000)))
                    {
                        this.chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                        this.chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                    }
                    this.chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                    this.chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                }
                if (e.Delta > 0)
                {
                    viewMinimum = this.chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                    viewMaximum = this.chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                    num3 = this.chart1.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                    num4 = this.chart1.ChartAreas[0].AxisY.ScaleView.ViewMaximum;
                    double num5 = this.chart1.ChartAreas[0].AxisX.PixelPositionToValue((double)e.Location.X) - ((viewMaximum - viewMinimum) / 2.0);
                    double num6 = this.chart1.ChartAreas[0].AxisX.PixelPositionToValue((double)e.Location.X) + ((viewMaximum - viewMinimum) / 2.0);
                    double num7 = this.chart1.ChartAreas[0].AxisY.PixelPositionToValue((double)e.Location.Y) - ((num4 - num3) / 2.0);
                    double num8 = this.chart1.ChartAreas[0].AxisY.PixelPositionToValue((double)e.Location.Y) + ((num4 - num3) / 2.0);
                    this.chart1.ChartAreas[0].AxisX.ScaleView.Zoom(Math.Round(num5, 0), Math.Round(num6, 0));
                    this.chart1.ChartAreas[0].AxisY.ScaleView.Zoom(Math.Round(num7, 0), Math.Round(num8, 0));
                }
            }
            catch
            {

            }
        }

        private void Timetest_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string ss = e.SignalTime.Month.ToString() + e.SignalTime.Second.ToString();
            lbltime.Text = ss;
        }

        private void TimeD_Tick(object sender, EventArgs e)
        {
            timeSpecIsRun = false;
        }
        int TimeIncrement = 0;
        int count = 0;
        private void T_Tick(object sender, EventArgs e)
        {
            try
            {

                if (!stopwatchspec.IsRunning)
                {
                    stopwatchspec.Start();

                }
                double a = TimeSpan.FromSeconds(this.TimeDuration).TotalMilliseconds;

                t.Interval = (int)a;
                count++;
                
                if (this .timeSpecIsRun)
                {
                    dataGridView1.Columns[0].HeaderText = "Time(Sec)";
                    dataGridView1.Columns[1].HeaderText = "W1" + " (" + txtW1.Text + "nm)";
                    dataGridView1.Columns[2].HeaderText = "W2" + " (" + txtW2.Text + "nm)";
                    dataGridView1.Columns[3].HeaderText = "W3" + " (" + txtW3.Text + "nm)";
                    dataGridView1.Columns[4].HeaderText = "W4" + " (" + txtW4.Text + "nm)";
                    dataGridView1.Columns[5].HeaderText = "W5" + " (" + txtW5.Text + "nm)";
                    dataGridView1.Columns[6].HeaderText = "W6" + " (" + txtW6.Text + "nm)";
                    chart1.ChartAreas[0].AxisX.Minimum = 0;
                    chart1.ChartAreas[0].AxisX.Maximum = TimeInterval * 60;
                 //  chart1.ChartAreas[0].AxisX.Interval = 10 ;
                   
                   
                    timepecArrye = this .timeSpecW;
                    double[] num = new double[6];

                    for (int i = 1; i <= 6; i++)
                    {
                        string strChartName = "W" + i.ToString();
                        if (timepecArrye[i - 1] != 0 && chart1.Series.IsUniqueName(strChartName))
                        {
                           chart1.Series.Add(strChartName);
                           chart1.Series[strChartName].ChartType = SeriesChartType.FastLine;
                            chart1.Series[strChartName].BorderWidth = 2;


                        }

                        if (chart1.Series.IndexOf(strChartName) != -1)
                        {
                           chart1.Series[strChartName].Points.AddXY((int)TimeIncrement    , findPos (timepecArrye[i - 1]));
                           
                            
                            num[i - 1] = findPos(timepecArrye[i - 1]);


                        }


                    }
                    this.lblW1.Text = num[0].ToString(format ) +" "+ mode; ;
                    this .lblW2.Text = num[1].ToString(format) + " " + mode;
                    this .lblW3.Text = num[2].ToString(format) + " " + mode;
                    this .lblW4.Text = num[3].ToString(format) + " " + mode;
                    this .lblW5.Text = num[4].ToString(format) + " " + mode;
                    this .lblW6.Text = num[5].ToString(format) + " " + mode;

                    int firstDisplayed = dataGridView1.FirstDisplayedScrollingRowIndex;
                    int displayed = dataGridView1.DisplayedRowCount(true);
                    int lastVisible = (firstDisplayed + displayed) - 1;
                    int lastIndex = dataGridView1.RowCount - 1;
                    dataGridView1.Rows.Add(TimeIncrement, num[0].ToString(format), num[1].ToString(format), num[2].ToString(format), num[3].ToString(format), num[4].ToString(format), num[5].ToString(format));
                    dataGridView1.Sort(Column1 as DataGridViewTextBoxColumn, ListSortDirection.Ascending);
                   
                    //  dataGridView1.Rows.Add();  //Add your row

                    if (lastVisible == lastIndex)
                    {
                        dataGridView1.FirstDisplayedScrollingRowIndex = firstDisplayed + 1;
                    }
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[dataGridView1.Rows.Count-1 ].Selected = true;
                    TimeIncrement += TimeDuration;
                    TimeSpan result = TimeSpan.FromHours(TimeIncrement);

                    //  lbltime.Text =stopwatchspec.Elapsed.Minutes.ToString() + ":" + stopwatchspec.Elapsed.Seconds.ToString();
                    //timeshow  = ((int) TimeInterval*60 - stopwatchspec.Elapsed.TotalSeconds );
                    // TimeSpan result = TimeSpan.FromMinutes(timeshow);
                    // lbltime.Text = result.ToString("mm':'ss");



                }
                else
                {
                    this.timeSpecIsRun = false;
                    showTime.Enabled = false;
                  //  lbltime.Text = TimeInterval.ToString() + ":00";
                    this.btnDisable.Text = "Enable";
                    stopwatchspec.Reset();
                    timeD.Enabled = false;
                    TimeIncrement = 0;

                   
                }
            }
            catch { }
        }
        private void txtW3_TextChanged(object sender, EventArgs e)
        {
            string strNum = (sender as TextBox).Text;
            int numValue = 0;
            try
            {
                numValue = Convert.ToInt16(strNum);
            }
            catch
            {
                (sender as TextBox).Text = "";
            }
            timeSpecW[2]=  W3 = numValue;
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {

            saveExcel.Title = "Export to Excel";
            saveExcel.Filter = "Excel File|*.xlsx";
            saveExcel.Tag = "Scpectomer Analiz";
            if (saveExcel.ShowDialog() == DialogResult.OK) 
            {

                Thread t = new Thread(ExportToExcel);
                t.Start();
            }

        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string a = "";
            SaveFileDialog svpic = new SaveFileDialog();
            svpic.Filter = "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png|Tiff Image (.tiff)|*.tiff|Wmf Image (.wmf)|*.wmf";
            if (svpic.ShowDialog() == DialogResult.OK)
            {
                string tempFile = System.IO.Path.GetTempPath() + "Temp";
                for (int i = 0; i < 6; i++)
                {
                    if (timeSpecW[i] != 0)
                    {
                        a += "W" + (i + 1).ToString() + "=" + timeSpecW[i] + "\n\n";
                    }

                   
                }
               

                chart1.SaveImage(tempFile, ChartImageFormat.Jpeg);
              
                Bitmap bitmap = (Bitmap)Image.FromFile(tempFile);
              
                PointF firstLocation = new PointF(bitmap.Width-122, 150f);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Font arialFont = new Font("Arial", 10, FontStyle.Bold))
                    {
                        graphics.DrawString(a, arialFont, Brushes.DarkSlateGray , firstLocation);

                    }
                }

                bitmap.Save(svpic.FileName);
            }
        }

        private void txtW2_TextChanged(object sender, EventArgs e)
        {
            string strNum = (sender as TextBox).Text;
            int numValue = 0;
            try
            {
                numValue = Convert.ToInt16(strNum);
            }
            catch
            {
                (sender as TextBox).Text = "";
            }
            timeSpecW[1]=  W2 = numValue;
        }

        private void txtW1_TextChanged(object sender, EventArgs e)
        {
            string strNum = (sender as TextBox).Text;
            int numValue = 0;
            try
            {
                numValue = Convert.ToInt16(strNum);
            }
            catch
            {
                (sender as TextBox).Text = "";
            }
            timeSpecW[0]= W1 = numValue;

        }

        public FTimeSpectrum()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void txtTimeDuration_TextChanged(object sender, EventArgs e)
        {
            string strNum = (sender as TextBox).Text;
            int numValue = 0;
            try
            {
                numValue = Convert.ToInt16(strNum);
            }
            catch
            {
                (sender as TextBox).Text = "";
            }
            TimeDuration  = numValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(Control txt in panel6.Controls)
            {
                if (txt is TextBox)
                    txt.Text = "";
            }
            txtTimeDuration.Text ="0";
            txtTimeInterval.Text = "0";

            while  (chart1.Series.Count > 0)
            {
                chart1.Series.RemoveAt(0);
               
            }
               
            lblW1.Text = "";
            lblW2.Text = "";
            lblW3.Text = "";
            lblW4.Text = "";
            lblW5.Text = "";
            lblW6.Text = "";

            dataGridView1.Rows.Clear();
           
            chartCOunt--;
            btnDisable.Text = "Enable";
            timeSpecIsRun = false;
            dataGridView1.Columns[0].HeaderText = "Time(Sec)";
            dataGridView1.Columns[1].HeaderText = "W1" + " (" + txtW1.Text + "nm)";
            dataGridView1.Columns[2].HeaderText = "W2" + " (" + txtW2.Text + "nm)";
            dataGridView1.Columns[3].HeaderText = "W3" + " (" + txtW3.Text + "nm)";
            dataGridView1.Columns[4].HeaderText = "W4" + " (" + txtW4.Text + "nm)";
            dataGridView1.Columns[5].HeaderText = "W5" + " (" + txtW5.Text + "nm)";
            dataGridView1.Columns[6].HeaderText = "W6" + " (" + txtW6.Text + "nm)";
            showTime.Enabled = false;
            showTime.Interval = 1;
            timeD.Enabled = false;

            t.Enabled = false;
            t.Interval = 1;
            TimeIncrement = 0;
            lbltime.Text = "";
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chart1_FormatNumber(object sender, FormatNumberEventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
        List<Point> points = new List<Point>();
        Point lastPoint = Point.Empty;
        ToolTip tooltip1 = new ToolTip();
        Point? clickPosition = null;
        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            tooltip1.Active = true;
            lastPoint = e.Location;
            chart1.Invalidate();
            var pos = e.Location;
            clickPosition = pos;

            var results = chart1.HitTest(pos.X, pos.Y, true,
                                         ChartElementType.PlottingArea);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.PlottingArea)
                {
                    var xVal = result.ChartArea.AxisX.PixelPositionToValue(pos.X);
                    var yVal = result.ChartArea.AxisY.PixelPositionToValue(pos.Y);
                   
                        tooltip1.Show("X=" + xVal.ToString("N3") + ", Y=" + yVal.ToString("N3"), this.chart1, e.Location.X, e.Location.Y - 15);
                      
                   
                     
                       
                    }
                }


            }

        private void chart1_MouseLeave(object sender, EventArgs e)
        {
         
        }

        private void ExportToExcel()
        {
            try
            {
                if (InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        bool firstCol = true;
                        progressBar1.Visible = true;
                        int c = 0;


                        foreach (var series in chart1.Series)
                            c += series.Points.Count;
                        progressBar1.Minimum = 0;
                        progressBar1.Maximum = c;
                        progressBar1.Value = 0;
                        Microsoft.Office.Interop.Excel.Application xlApp;
                        Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                        object misValue = System.Reflection.Missing.Value;
                        xlApp = new Microsoft.Office.Interop.Excel.Application();
                        xlWorkBook = xlApp.Workbooks.Add(misValue);
                        xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                        Microsoft.Office.Interop.Excel.ChartObjects xlCharts = (Microsoft.Office.Interop.Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
                        Microsoft.Office.Interop.Excel.ChartObject myChart = (Microsoft.Office.Interop.Excel.ChartObject)xlCharts.Add(500, 100, 500, 500);



                        Microsoft.Office.Interop.Excel.Chart chartPage = myChart.Chart;
                        Microsoft.Office.Interop.Excel.SeriesCollection ser = chartPage.SeriesCollection();
                        chartPage.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DLine;
                        int SeresNmae = 1;
                        int XRange = 0;
                        string W = "";
                        int n = 2;
                        xlWorkSheet.Cells[1] = "Time(Sec)";
                        int Xwvae = 0;
                        for (int m = 0; m < 6; m++)
                        {
                            if (timeSpecW[m] != 0)

                            {
                                xlWorkSheet.Cells[2, n] = timeSpecW[m];
                                n++;
                            }
                        }
                        foreach (var item in chart1.Series)
                        {
                            int m = 0;
                           
                            switch (SeresNmae)
                            {
                                case 1:
                                    W = "(" + txtW1.Text + "nm)";
                                    break;
                                case 2:
                                    W = "(" + txtW2.Text + "nm)";
                                    break;
                                case 3:
                                    W = "(" + txtW3.Text + "nm)";
                                    break;
                                case 4:
                                    W = "(" + txtW4.Text + "nm)";
                                    break;
                                case 5:
                                    W = "(" + txtW5.Text + "nm)";
                                    break;
                                case 6:
                                    W = "(" + txtW6.Text + "nm)";
                                    break;

                            }


                            xlWorkSheet.Cells[SeresNmae+1 ] = item.Name + W;
                            SeresNmae++;

                            int i = 3;
                            XRange += 2;
                            //if (item.Name == ExperimentName)
                            //    wait = true;
                            if (chart1.Series.Count > 1 && chart1.Series[1].Points.Count > item.Points.Count)
                            {
                                int k = chart1.Series[1].Points.Count - item.Points.Count;
                                i = i + k;
                            }
                            for (int j = 0; j < item.Points.Count; j++)
                            {
                                
                                progressBar1.Value++;
                                if (firstCol)
                                {
                                    xlWorkSheet.Cells[i, 1] = item.Points[j].XValue;
                                  
                                }
                               
                                xlWorkSheet.Cells[i, SeresNmae ] = item.Points[j].YValues;
                               
                                
                                
                                i++;

                            }
                            firstCol = false;
                            Xwvae++;

                            Microsoft.Office.Interop.Excel.Series series1 = ser.NewSeries();
                            series1.Name = item.Name;
                            string colY = Number2String((SeresNmae)) + "2" + ":" + Number2String((SeresNmae)) + i.ToString();
                            string colX = "A2:A" + i.ToString();
                            series1.Values = xlWorkSheet.get_Range(colY);
                            series1.XValues = xlWorkSheet.get_Range(colX);
                        //   wait = false;

                    }



                        Microsoft.Office.Interop.Excel.Range chartRange;



                        string col1 = "1:" + chart1.Series[0].Points.Count;
                        string col2 = chart1.Series.Count + ":" + chart1.Series[chart1.Series.Count - 1].Points.Count;


                    //chartRange = xlWorkSheet.get_Range(col1, col2);
                    //chartPage.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine;
                    //chartPage.SetSourceData(chartRange, misValue);



                    xlWorkBook.SaveAs(saveExcel.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        xlWorkBook.Close(true, misValue, misValue);
                        xlApp.Quit();

                        releaseObject(xlWorkSheet);
                        releaseObject(xlWorkBook);
                        releaseObject(xlApp);
                        System.Diagnostics.Process.Start(saveExcel.FileName);
                        message("Excel file created ", true);
                        progressBar1.Visible = false;


                    }));
                }

            }catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }
        private void message(string txt, bool i)
        {
            New message = new New();
            if (i)
            {
                message.pictureBox1.Image = Properties.Resources.ok;

            }
            else if (!i)
            {
                message.pictureBox1.Image = Properties.Resources.alert;
            }
            message.label1.Text = txt;
            message.Show();
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                message("Exception Occured while releasing object ", false);

            }
            finally
            {
                GC.Collect();
            }
        }
        SaveFileDialog saveExcel = new SaveFileDialog();
        private String Number2String(int number)
        {
            Char c = (Char)((97) + (number - 1));
            return c.ToString();
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
