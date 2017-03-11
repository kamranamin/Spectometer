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
       public  float c1, Result;
        public int wave;
        public bool IsClose=true  ;
        FChart f =new FChart();



        public FCalibrationCruve()
        {
            InitializeComponent();
        }

        private void FCalibrationCruve_Load(object sender, EventArgs e)
        {
            int countC = 1;
            lblC.Text = "C" + countC.ToString() + " :";
            this._lbl_form_text.Text = "Calibration Cruve";
            IsClose = false;

         
           
          
        }

        private void T_Tick(object sender, EventArgs e)
        {

           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           
            dataGridView1.Rows.Add( c1 ,Result);
            lblC.Text = "C" +( dataGridView1.Rows.Count + 1).ToString()+" :";
        }

        private void txtWaveLenght_TextChanged(object sender, EventArgs e)
        {
            try
            {
                wave =Convert.ToInt16 ( txtWaveLenght.Text); 
            }
            catch
            {
                wave = 100;
                txtWaveLenght.Text = "420";
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
                

            }
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            if (!f.IsRun)
            {
                f = new FChart();
                f.Show();
                f.chart1.Series.Add("calibration");
                f.chart1.Series["calibration"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
                f.chart1.Series["calibration"].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Star10;
                f.chart1.Series["calibration"].MarkerColor = Color.GreenYellow;

                f.chart1.Series["calibration"].MarkerSize = 20;
               

            }
            if(f.chart1.Series["calibration"].Points.Count>0)
            f.chart1.Series["calibration"].Points.Clear();
            foreach (DataGridViewRow row in dataGridView1.Rows  )
            {
                f.chart1.Series["calibration"].Points.AddXY(row.Cells[0].Value, row.Cells[1].Value);
            }
          
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
        float[] b;
        private void button1_Click(object sender, EventArgs e)
        {
             Co = new float[dataGridView1.Rows.Count];
             b = new float[dataGridView1.Rows.Count];
            int f = 0;
            foreach (DataGridViewRow dr in dataGridView1.Rows )
            {
               
                if (dataGridView1.Rows.Count>0)
                {
                    Co[f] = (float)dr.Cells[0].Value;
                    b[f] = (float)dr.Cells[0].Value;

                }
                f++;


            }
            Random r = new Random();
            int num = r.Next(1, dataGridView1.Rows.Count);
            lblAbsrbance.Text = Result.ToString();
            lblConceration.Text = ((b[num] / Co[num]) * Result).ToString();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try {
                Co = new float[dataGridView1.Rows.Count];
                b = new float[dataGridView1.Rows.Count];
                int f = 0;
                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {

                    if (dataGridView1.Rows.Count > 0)
                    {
                        Co[f] = (float)dr.Cells[0].Value;
                        b[f] = (float)dr.Cells[0].Value;

                    }
                    f++;


                }
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "|*.ccu";
                CalibrationCurve ca = new CalibrationCurve();
                ca.cor = Co;
                ca.abs = b;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    IFormatter formatter = new BinaryFormatter();
                    FileStream seryalization = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write);
                    formatter.Serialize(seryalization, ca);
                    seryalization.Close();

                }
            }catch
            {

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
            dataGridView1.Rows.Clear();
            txtC1.Text = "";
            txtWaveLenght.Text = "";
            textBox2.Text = "";
        }

        private void FCalibrationCruve_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsClose = true;
        }
    }
}
