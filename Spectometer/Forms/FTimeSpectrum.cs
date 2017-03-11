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
        public double TimeInterval = 2, TimeDuration = 2;
        public int[] timeSpecW = new int[6];
        public bool timeSpecIsRun = false;
        public int chartCOunt = 0;
        public float[] value;
        public bool cLOsefrm = false;


        System.Windows.Forms.   Timer t = new System.Windows.Forms.Timer();
        System.Diagnostics.Stopwatch stopwatchspec = new System.Diagnostics.Stopwatch();
        int[] timepecArrye = new int[6];
           FChart fchart1 = new FChart();

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
                (sender as TextBox).Text = "0";
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
                (sender as TextBox).Text = "0";
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
                (sender as TextBox).Text = "0";
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
            bool isChartRun=false ;
            if (!timeSpecIsRun)
            {
                FormCollection fr = Application.OpenForms;
                foreach(Form  frm in  fr)
                {
                    if(frm == fchart1  )
                    {
                        isChartRun = true;

                    }
                }
                if (isChartRun)
                {
                    fchart1.chart1.ResetAutoValues();

                    //foreach (Series s in fchart1.chart1.Series)
                    //{
                    //    fchart1.chart1.Series[s.Name ].Points.Clear();
                    //    fchart1.chart1.Series.Remove(s);
                    //}

                }
                else
                {
                  
                        fchart1 = new FChart();
                                    
                    fchart1.Show();
                }
                chartCOunt++;
                btnDisable.Text = "Disable";


                timeSpecIsRun = true;
                t.Enabled = true;


            }
            else
            {

                chartCOunt--;
                btnDisable.Text = "Enable";
                timeSpecIsRun = false;
                t.Enabled = false;
                

            }
        }

        private void Fchart1_FormClosed(object sender, FormClosedEventArgs e)
        {
            fchart1 = null;
        }

     
        private void FTimeSpectrum_FormClosing(object sender, FormClosingEventArgs e)
        {
            cLOsefrm = true;
          
        }

        private void FTimeSpectrum_Load(object sender, EventArgs e)
        {

            this._lbl_form_text.Text = "Time Spectrum";
            txtTimeDuration.Text = "2";

            cLOsefrm = false;
       //     fchart1.Show();
            t.Tick += T_Tick;
        }
        private void T_Tick(object sender, EventArgs e)
        {
            try
            {
                
                if (!fchart1.IsRun )
                {

                    timeSpecIsRun = false;
                    btnDisable.Text = "Enable";


                    stopwatchspec.Reset();


                }
                if (this .timeSpecIsRun)
                {
                    fchart1.chart1.ChartAreas[0].AxisX.Interval = TimeDuration ;
                    double a = TimeSpan.FromSeconds(this .TimeDuration).TotalMilliseconds;
                    t.Interval = (int)a;
                    timepecArrye = this .timeSpecW;
                    double[] num = new double[6];
                    for (int i = 1; i <= 6; i++)
                    {
                        string strChartName = "W" + i.ToString();
                        if (timepecArrye[i - 1] != 0 && fchart1.chart1.Series.IsUniqueName(strChartName))
                        {
                            fchart1. chart1.Series.Add(strChartName);
                            fchart1.chart1.Series[strChartName].ChartType = SeriesChartType.FastLine;
                           

                        }

                        if (fchart1.chart1.Series.IndexOf(strChartName) != -1)
                        {
                            fchart1.chart1.Series[strChartName].Points.AddXY((int)stopwatchspec.Elapsed.TotalSeconds   , value[timepecArrye[i - 1]]);
                           
                            
                            num[i - 1] = (int)value[timepecArrye[i - 1]];


                        }


                    }
                    this .lblW1.Text = num[0].ToString();
                    this .lblW2.Text = num[1].ToString();
                    this .lblW3.Text = num[2].ToString();
                    this .lblW4.Text = num[3].ToString();
                    this .lblW5.Text = num[4].ToString();
                    this .lblW6.Text = num[5].ToString();
                    dataGridView1.Rows.Add((int)stopwatchspec.Elapsed.TotalSeconds, num[0], num[1], num[2], num[3], num[4], num[5]);
                    dataGridView1.Sort(Column1 as DataGridViewTextBoxColumn, ListSortDirection.Descending);
                    dataGridView1.Rows[0].Selected = true;
                    if (!stopwatchspec.IsRunning)
                    {
                        stopwatchspec.Start();

                    }
                    if (stopwatchspec.Elapsed < TimeSpan.FromMinutes(this .TimeInterval))
                    {

                        //  label8.Text = stopwatchspec.Elapsed.Seconds.ToString();
                    }
                    else
                    {
                      
                        this .timeSpecIsRun = false;
                        this .btnDisable.Text = "Enable";
                        
                        //  chart2.Visible = false;
                        //stopwatchspec.Stop();
                        stopwatchspec.Reset();
                    }

                }
                else
                {
                    stopwatchspec.Reset();
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
                (sender as TextBox).Text = "0";
            }
            timeSpecW[2]=  W3 = numValue;
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {

            saveExcel.Title = "Export to Excel";
            saveExcel.Filter = "Excel File|*.xls";
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
               

                fchart1.chart1.SaveImage(tempFile, ChartImageFormat.Jpeg);
              
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
                (sender as TextBox).Text = "0";
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
                (sender as TextBox).Text = "0";
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
                (sender as TextBox).Text = "0";
            }
            TimeDuration  = numValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
         
        }

        private void ExportToExcel()
        {


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
            chartPage.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine;
            int SeresNmae = 0;
            foreach (var item in fchart1. chart1.Series)
            {
                SeresNmae++;
                xlWorkSheet.Cells[(SeresNmae * 2) - 1] = item.Name;
                int i = 2;
                //if (item.Name == ExperimentName)
                //    wait = true;
                for (int j = 0; j < item.Points.Count - 1; j++)
                {


                    xlWorkSheet.Cells[i, (SeresNmae * 2) - 1] = item.Points[j].YValues;
                    xlWorkSheet.Cells[i, SeresNmae * 2] = item.Points[j].XValue;
                    i++;

                }

                Microsoft.Office.Interop.Excel.Series series1 = ser.NewSeries();
                series1.Name = item.Name;
                string colY = Number2String((SeresNmae * 2) - 1) + "2" + ":" + Number2String((SeresNmae * 2) - 1) + item.Points.Count.ToString();
                string colX = Number2String((SeresNmae * 2)) + "2" + ":" + Number2String((SeresNmae * 2)) + item.Points.Count.ToString();
                series1.Values = xlWorkSheet.get_Range(colY);
                series1.XValues = xlWorkSheet.get_Range(colX);
               // wait = false;

            }



            Microsoft.Office.Interop.Excel.Range chartRange;



            string col1 = "1:" +fchart1. chart1.Series[0].Points.Count;
            string col2 = fchart1.chart1.Series.Count + ":" + fchart1.chart1.Series[fchart1.chart1.Series.Count - 1].Points.Count;


            //  chartRange = xlWorkSheet.get_Range(col1 ,col2 );
            //  chartPage.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine;
            //    chartPage.SetSourceData(chartRange, misValue );



            xlWorkBook.SaveAs(saveExcel.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
            message("Excel file created ", true);



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
