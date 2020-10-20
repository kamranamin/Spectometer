using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spectometer.Forms
{
    public partial class FSingleWaveLenght :F_Base 
    {
        public int W1;
        public int W2;
        public int W3;
        public double  Wl1;
        public double   Wl2;
        public double  Wl3;
        public string mode = "";
        public bool isCLose = false;

        public FSingleWaveLenght()
        {
            _lbl_form_text.Text = "Single Wavelength Monitoring";
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
    public string format = "";
        private void btnCalc_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtW1.Text))
                {
                    W1 = Convert.ToInt32(txtW1.Text);
                    lblW1.Text = Wl1.ToString(format) + " " + mode;
                }
                else
                    lblW1.Text = "0";
                if (!string.IsNullOrWhiteSpace(txtW2.Text))
                {
                    W2 = Convert.ToInt32(txtW2.Text);
                    lblW2.Text = Wl2.ToString(format) + " " + mode;
                }
                else
                    lblW2.Text = "0";
                if (!string.IsNullOrWhiteSpace(txtW3.Text))
                {
                    W3 = Convert.ToInt32(txtW3.Text);
                    lblW3.Text = Wl3.ToString(format) + " " + mode;
                }
                else
                    lblW3.Text = "0";

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

        }

        private void FSingleWaveLenght_FormClosing(object sender, FormClosingEventArgs e)
        {
            isCLose = true;
        }

        private void FSingleWaveLenght_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtW1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtW1.Text))
                {
                    W1 = Convert.ToInt32(txtW1.Text);

                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); txtW1.Focus();
            }
           
        }

        private void txtW2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtW2.Text))
                {
                    W2 = Convert.ToInt32(txtW2.Text);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); txtW2.Focus();
            }
        }

        private void txtW3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtW3.Text))
                {
                    W3 = Convert.ToInt32(txtW3.Text);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); txtW3.Focus();
            }
        }
    }
}
