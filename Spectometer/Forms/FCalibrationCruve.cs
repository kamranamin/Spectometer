using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace Spectometer.Forms
{
    public partial class FCalibrationCruve : Spectometer.Forms.F_Base
    {
      //  public float[] value;
        Timer t = new Timer();
        /// <summary>
        /// 
        /// </summary>
       public  float c1, Result;
        /// <summary>
        /// 
        /// </summary>
        public int wave;
        /// <summary>
        /// 
        /// </summary>
        public bool IsClose=true  ;
        /// <summary>
        /// //
        /// </summary>
        public string mode = "";
        /// <summary>
        /// 
        /// </summary>
        public string Sprate;
        FChart f =new FChart();



        public FCalibrationCruve()
        {
            InitializeComponent();
        }

        private void FCalibrationCruve_Load(object sender, EventArgs e)
        {
            int countC = 1;
            lblC.Text = "C" + countC.ToString() + " :";
            this._lbl_form_text.Text = "Calibration Curve";
            label6.Text = "R²=";
            IsClose = false;
         

            //chart1.Series.Add("calibration");
            //chart1.Series["calibration"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            //chart1.Series["calibration"].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Star10;
            //chart1.Series["calibration"].MarkerColor = Color.Black;

            //chart1.Series["calibration"].MarkerSize = 10;
            
           
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "{0.00}";
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0.0}";
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(192, 192, 192);
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(192, 192, 192);
            this.chart1.ChartAreas["ChartArea1"].AxisX.TitleFont = new Font("Times New Roman", 12, FontStyle.Bold);
            this.chart1.ChartAreas["ChartArea1"].AxisY.TitleFont = new Font("Times New Roman", 12, FontStyle.Bold);



        }

        private void T_Tick(object sender, EventArgs e)
        {

           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Add(c1, Result.ToString(Sprate)); 
                lblC.Text = "C" + (dataGridView1.Rows.Count + 1).ToString() + " :";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void txtWaveLenght_TextChanged(object sender, EventArgs e)
        {
            try
            {
                wave =Convert.ToInt16 ( txtWaveLenght.Text); 
            }
            catch
            {
                txtWaveLenght.Text = "0";
                wave = 0;
            
            }
        }

        private void txtC1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                c1 = Convert.ToSingle(txtC1.Text);
            }
            catch
            {
                txtC1.Text = "0";

            }
        }
        double a = 0;
        double b = 0;
        private void btnEnable_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (chart1.Series.IsUniqueName("calibration"))
                {
                    chart1.Series.Add("calibration");
                    chart1.Series["calibration"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                    chart1.Series["calibration"].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Star10;
                    chart1.Series["calibration"].MarkerColor = Color.Black;

                    chart1.Series["calibration"].MarkerSize = 10;
                }
                if (chart1.Series.IsUniqueName("Linear Regression"))
                {
                    chart1.Series.Add("Linear Regression");
                    chart1.Series["Linear Regression"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    chart1.ChartAreas["ChartArea1"].AxisX.Title = "Concentration";
                }
                if (chart1.Series.IsUniqueName("Result"))
                {
                    chart1.Series.Add("Result");
                    chart1.Series["Result"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                    chart1.Series["Result"].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Star10;
                    chart1.Series["Result"].MarkerColor = Color.Red;
                    chart1.Series["Result"].MarkerSize = 10;

                }
               
                double[] xValues = new double[dataGridView1.Rows.Count];
                double[] yVlaues = new double[dataGridView1.Rows.Count];
                double powX = 0;
                double powY = 0;
                double sumXY = 0;
                double sumX = 0;
                double sumY = 0;


                double[] yPrim = new double[dataGridView1.Rows.Count];
                if (chart1.Series["calibration"].Points.Count > 0)
                    chart1.Series["calibration"].Points.Clear();
                if (chart1.Series["Linear Regression"].Points.Count > 0)
                    chart1.Series["Linear Regression"].Points.Clear();
                if (chart1.Series["Result"].Points.Count > 0)
                    chart1.Series["Result"].Points.Clear();
               
                int i = 0;
               
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    chart1.Series["calibration"].Points.AddXY(row.Cells[0].Value, row.Cells[1].Value);
                    xValues[i] = Convert.ToDouble(row.Cells[0].Value);
                    yVlaues[i] = Convert.ToDouble(row.Cells[1].Value);
                    sumX += xValues[i];
                    sumY += yVlaues[i];
                    powX += Math.Pow(xValues[i], 2);
                    powY += Math.Pow(yVlaues[i], 2);
                    sumXY += xValues[i] * yVlaues[i];
                    i++;
                }
                if (dataGridView1.Rows.Count < 2)
                {
                    MessageBox.Show("Please input more than two values");
                    return;
                }
                a = ((sumY * powX) - (sumX * sumXY)) / ((xValues.Length * powX) - (Math.Pow(sumX, 2)));
                b = ((xValues.Length * sumXY) - (sumX * sumY)) / ((xValues.Length * powX) - (Math.Pow(sumX, 2)));
                if (double.IsNaN(a)||double.IsNaN(b))
                {
                    MessageBox.Show("Result was not the format");
                    return;
                }
                string sing = "";
                if (b >= 0)
                    sing = "+";
                else
                    sing = "";
                lblNan.Text = a.ToString("N3") + sing + b.ToString("N3") + "X";

                for (int f = 0; f < xValues.Length; f++)
                {
                    yPrim[f] = a + b * xValues[f];
                }
                chart1.Series["Linear Regression"].Points.DataBindXY(xValues, yPrim);

                double xMean = sumX / xValues.Length;
                double yMean = sumY / yVlaues.Length;
                double topR = 0;
                double bF1 = 0;
                double bF2 = 0;

                for (int r = 0; r < xValues.Length; r++)
                {
                    topR += (xValues[r] - xMean) * (yVlaues[r] - yMean);
                    bF1 += Math.Pow((xValues[r] - xMean), 2);
                    bF2 += Math.Pow((yVlaues[r] - yMean), 2);
                }
                double R = Math.Sqrt(topR / Math.Sqrt(bF1 * bF2));
                lblNan1.Text = R.ToString("N4");


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex  ].Name=="Delete")
            {
                if (dataGridView1.SelectedRows.Count>0)
                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                lblC.Text = "C" + (dataGridView1.Rows.Count + 1).ToString() + " :";
            }
        }

        float[] Co;
        float[] b1;
        private void button1_Click(object sender, EventArgs e)
        {
            try {
                // Co = new float[dataGridView1.Rows.Count];
                // b1 = new float[dataGridView1.Rows.Count];
                //int f = 0;
                //foreach (DataGridViewRow dr in dataGridView1.Rows )
                //{

                //    if (dataGridView1.Rows.Count>0)
                //    {
                //        Co[f] = float.Parse(dr.Cells[0].Value.ToString());
                //        b1[f] = float.Parse( dr.Cells[1].Value.ToString());

                //    }
                //    f++;


                //}
                //Random r = new Random();
                //int num = r.Next(1, dataGridView1.Rows.Count);
                if (dataGridView1.Rows.Count < 2)
                {
                    MessageBox.Show(" input  must be more than two values");
                    return;
                }


                if (double.IsNaN(a) || double.IsNaN(b) || double.IsInfinity (a) || double.IsInfinity(b))
                {
                    MessageBox.Show("Result was not the format");
                    return;
                }

                lblAbsrbance.Text = Result.ToString(Sprate);
               
                //   lblConceration.Text = ((b1[num] / Co[num]) * Result).ToString();

                lblConceration.Text = ((Result - a) / b).ToString("N3");
                if (chart1.Series["Result"].Points.Count > 0)
                    chart1.Series["Result"].Points.Clear();

                chart1.Series["Result"].Points.AddXY((Result - a) / b, Result);
            

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try {
                Co = new float[dataGridView1.Rows.Count];
                b1 = new float[dataGridView1.Rows.Count];
                int f = 0;
                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {

                    if (dataGridView1.Rows.Count > 0)
                    {
                        Co[f] = float.Parse(dr.Cells[0].Value.ToString());
                        b1[f] = float.Parse( dr.Cells[1].Value.ToString());

                    }
                    f++;


                }
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "|*.ccu";
                CalibrationCurve ca = new CalibrationCurve();
                ca.cor = Co;
                ca.abs = b1;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    IFormatter formatter = new BinaryFormatter();
                    FileStream seryalization = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write);
                    formatter.Serialize(seryalization, ca);
                    seryalization.Close();

                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
           
            
          
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "|*.ccu";
            CalibrationCurve ca = new CalibrationCurve();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                IFormatter formater = new BinaryFormatter();
                FileStream filestream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read) ;
                ca = formater.Deserialize(filestream) as CalibrationCurve;
                dataGridView1.Rows.Clear();
                 for (int i=0; i<ca.abs.Length ; i++)
                 {
                    dataGridView1.Rows.Add(ca.cor[i], ca.abs[i]);
                 }
                      filestream.Close();
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                txtC1.Text = "";
                txtWaveLenght.Text = "";
                textBox2.Text = "";
                lblC.Text = "C1";
                lblNan1.Text = lblAbsrbance.Text = lblConceration.Text = lblNan.Text = "";
                while (chart1.Series.Count > 0)
                {
                    chart1.Series.RemoveAt(0);

                }
                chart1.Series.Add("calibration");
                chart1.Series["calibration"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                chart1.Series["calibration"].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Star10;
                chart1.Series["calibration"].MarkerColor = Color.Black;

                chart1.Series["calibration"].MarkerSize = 10;
                chart1.Series.Add("Linear Regression");
                chart1.Series["Linear Regression"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.ChartAreas["ChartArea1"].AxisX.Title = "Concentration";
                chart1.Series.Add("Result");
                chart1.Series["Result"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                chart1.Series["Result"].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Star10;
                chart1.Series["Result"].MarkerColor = Color.Red;
                chart1.Series["Result"].MarkerSize = 10;
                chart1.ChartAreas[0].AxisY.LabelStyle.Format = "{0.0}";

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        } 

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void FCalibrationCruve_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsClose = true;
        }
    }
}
