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
        public int W1 = 0;
        public int W2 = 0;
        public int W3 = 0;
        public float  Wl1 = 0;
        public float  Wl2 = 0;
        public float  Wl3 = 0;
        public bool isCLose = false;
        public FSingleWaveLenght()
        {
            _lbl_form_text.Text = "Single WaveLenght Monitoring";
            InitializeComponent();
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            W1 = Convert.ToInt32(txtW1.Text);
            W2 = Convert.ToInt32(txtW2.Text);
            W3 = Convert.ToInt32(txtW3.Text);
            lblW1.Text = Wl1.ToString();
            lblW2.Text = Wl2.ToString();
            lblW3.Text = Wl3.ToString();
        }

        private void FSingleWaveLenght_FormClosing(object sender, FormClosingEventArgs e)
        {
            isCLose = true;
        }
    }
}
