using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Spectometer.Forms
{
    public partial class FNanoDrop : Spectometer.Forms.F_Base
    {
        public double a230, a260, a280, a320;

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            double  result;

            if (txtDiluationFilter.Text == "")
            {
                MessageBox.Show("Please insert Diluation Filter ");
                return;
            }
            if(txtSSFN.Text=="")
            {
                MessageBox.Show("Please insert value ");
                return;
            }
            txtA23.Text = a230.ToString("N4");
            txtA26.Text = a260.ToString("N4");
            txtA28.Text = a280.ToString("N4");
            txtA32.Text = a320.ToString("N4");
            txtA2623.Text = (a260 / a230).ToString("F2");
            txtA2628.Text = (a260 / a280).ToString("F2");
            //Protein
           // Nucleic
            if (comboBox1.SelectedItem.ToString()== "Nucleic")
            {
                result = (a260 * Convert.ToSingle(txtDiluationFilter.Text) * Convert.ToSingle(txtSSFN.Text));
                label13.Text = "Nucleic Acid Conc[ng/uL]  "+"  =  "+result.ToString("F2");
            }
            else
            {
               result  = ((1 / Convert.ToSingle(txtSSFN.Text)) *a280 * Convert.ToSingle(txtDiluationFilter.Text)  );
                label13.Text = "Protein Conc [mg/mL]"+"  =  "+result.ToString("F2");
            }
        }

        private void FNanoDrop_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 1;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDiluationFilter_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void FNanoDrop_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsClose = true;
        }

        public bool IsClose = false;
        public FNanoDrop()
        {

            InitializeComponent();
           
        }
    }
}
