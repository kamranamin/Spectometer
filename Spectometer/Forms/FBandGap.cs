using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Spectometer.Forms
{
    public partial class FBandGap : F_Base
    {
        public bool IsRun;
        public float [] Yvalues=new float[DeviceType.NumberOfIndex];
        public float [] Xvalues=new float [DeviceType.NumberOfIndex];
        float   nval = 0f;
        SaveFileDialog saveExcel = new SaveFileDialog();
        public FBandGap()
        {
            InitializeComponent();
        }

        private void FBandGap_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsRun = false;
        }

        private void FBandGap_Load(object sender, EventArgs e)
        {

            IsRun = true;
            progressBar1.Enabled = false;
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
           
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "{0.0}";
            chart1.ChartAreas[0].AxisY.Title = "Energy Band Gap";
            chart1.ChartAreas[0].AxisX.Title = "Energy ( ev )";
            chart1.ChartAreas[0].AxisX.Minimum = 1;
            chart1.ChartAreas[0].AxisX.Maximum = 6;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0}";
            rd1.Checked = true;
        }

        private void flatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (rd1.Checked)
                    nval = 2f;
                else if (rd2.Checked)
                    nval = (2 / 3f);
                else if (rd3.Checked)
                    nval = (1 / 3f);
                else if (rd4.Checked)
                    nval = (1 / 2f);

                chart1.Series[0].Points.Clear();
                float Thinkness = Convert.ToInt16(textBox1.Text);
                float[] abso = new float[DeviceType.NumberOfIndex];
                float[] hv = new float[DeviceType.NumberOfIndex];
                double[] alphahv = new double[DeviceType.NumberOfIndex];
                for (int i = 1; i < DeviceType.NumberOfIndex; i++)
                {
                    abso[i] = Yvalues[i] / Thinkness;
                    hv[i] = 1240 / Xvalues[DeviceType.NumberOfIndex- i];
                    float result = abso[i] * hv[i];
                    alphahv[i] = Math.Pow(Convert.ToDouble(result), nval);
                    chart1.Series[0].Points.AddXY(hv[i], alphahv[i]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
          
        }

        private void exoprtToExcellToolStripMenuItem_Click(object sender, EventArgs e)
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
                        xlWorkSheet.Cells[1] = "X";
                        int Xwvae = 0;

                        foreach (var item in chart1.Series)
                        {


                            xlWorkSheet.Cells[SeresNmae + 1] = "Y";
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

                                xlWorkSheet.Cells[i, SeresNmae] = item.Points[j].YValues;



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
                        progressBar1.Visible = false;
                        System.Diagnostics.Process.Start(saveExcel.FileName);
                        message("Excel file created ", true);



                    }));
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
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
      
        private String Number2String(int number)
        {
            Char c = (Char)((97) + (number - 1));
            return c.ToString();
        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveExcel.Title = "Export to Excel";
            saveExcel.Filter = "Excel File|*.xlsx";
            saveExcel.Tag = "Scpectomer Analiz";
            if (saveExcel.ShowDialog() == DialogResult.OK)
            {

                System.Threading.Thread t = new System.Threading.Thread(ExportToExcel);
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





                chart1.SaveImage(tempFile, ChartImageFormat.Jpeg);

                Bitmap bitmap = (Bitmap)Image.FromFile(tempFile);

                PointF firstLocation = new PointF(bitmap.Width - 122, 150f);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Font arialFont = new Font("Arial", 10, FontStyle.Bold))
                    {
                        graphics.DrawString(a, arialFont, Brushes.DarkSlateGray, firstLocation);

                    }
                }

                bitmap.Save(svpic.FileName);
            }
        }
        }

    }

  

