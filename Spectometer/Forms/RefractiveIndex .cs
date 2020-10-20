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
    public partial class RefractiveIndex : F_Base
    {
        public RefractiveIndex()
        {
            InitializeComponent();
        }
        float Landa1, Landa2, NoF, Thinkness, Angel, Conversion, T;

        private void txtConversion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Conversion = Convert.ToSingle(txtConversion.Text);
            }
            catch { txtConversion.Text = "0"; }
        }

        private void RefractiveIndex_Load(object sender, EventArgs e)
        {
            txtAngel.Text = "7";
            txtConversion.Text = "0.001";
        }

        private void txtAngel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Angel = Convert.ToSingle(txtAngel.Text);
            }
            catch { txtAngel.Text = "0"; }
        }

        private void txtNR_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Thinkness = Convert.ToSingle(txtNR.Text);
            }
            catch { txtNR.Text = "0"; }
        }

        private void txtN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                NoF = Convert.ToSingle(txtN.Text);
            }
            catch { txtN.Text = "0"; }
        }

        private void txtLanda2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Landa2 = Convert.ToSingle(txtLanda2.Text);
            }
            catch { txtLanda2.Text = "0"; }
        
          }

        private void txtLanada1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Landa1 = Convert.ToSingle(txtLanada1.Text);
            }
            catch { txtLanada1.Text = "0"; }
        }
    

        private void btnSave_Click(object sender, EventArgs e)
        {
            float Ntop = Convert.ToSingle(Math.Pow((NoF * (Landa1 * Landa2) * Conversion) / (2 * (Landa1 - Landa2) * Thinkness), 2));
            float Ndown = Convert.ToSingle((Math.Pow(Math.Sin((Angel*Math.PI)/180), 2)));
            float result = Convert.ToSingle(Math.Pow((Ntop + Ndown), (1 / 2f)));
            lblResult.Text = "Refractive Index :" + result .ToString("N3");
        }
    }
}
