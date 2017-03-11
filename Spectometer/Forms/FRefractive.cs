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
    public partial class FRefractive : F_Base
    {
        public FRefractive()
        {
            InitializeComponent();
        }
        float Landa1, Landa2, NoF, RefeactiveIndex, Angel, Conversion, T, X;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            try
            {
                X = Convert.ToSingle(txtX .Text);
            }
            catch { txtX .Text = "0"; }

        }

        private void txtConversion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Conversion  = Convert.ToSingle(txtConversion  .Text);
            }
            catch { txtConversion .Text = "0"; }
        }

        private void txtAngel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Angel  = Convert.ToSingle(txtAngel .Text);
            }
            catch { txtAngel .Text = "0"; }
        }

        private void txtNR_TextChanged(object sender, EventArgs e)
        {
            try
            {
                RefeactiveIndex  = Convert.ToSingle(txtNR .Text);
            }
            catch { txtNR .Text = "0"; }
        }

        private void txtN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                NoF  = Convert.ToSingle(txtN .Text);
            }
            catch { txtN .Text = "0"; }
        }

        private void txtLanda2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Landa2 = Convert.ToSingle(txtLanda2 .Text);
            }
            catch { txtLanda2 .Text = "0"; }
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
             float Ttop = ((Landa1 * X * Landa2) * Conversion)  ;
             float Tdown = 2 * (Landa1 - Landa2) *Convert.ToSingle ( Math.Pow ( Math.Pow(NoF, 2) - ((1 / 2) * (1 - Math.Cos(2 * Angel))),(1/2)));
            T = Ttop / Tdown;
            float Ntop =Convert.ToSingle ( Math.Pow ( (NoF * (Landa1 * X * Landa2) * Conversion)/ ( 2 * (Landa1 - Landa2)*T ),2));
            float Ndown =Convert.ToSingle( (1 / 2) * (1 - Math.Cos(2 * Angel)));
            RefeactiveIndex = Convert.ToSingle( Math.Pow((Ntop + Ndown), (1 / 2)));
            lbln.Text = RefeactiveIndex.ToString();
            lblt.Text = T.ToString();

        }
    }
}
