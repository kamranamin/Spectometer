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
        /// <summary>
        /// 
        /// </summary>
        public FRefractive()
        {
            InitializeComponent();
        }
        float Landa1, Landa2, NoF, RefeactiveIndex, Angel, Conversion, T;

      

        private void txtConversion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Conversion  = Convert.ToSingle(txtConversion  .Text);
            }
            catch { txtConversion .Text = "0"; }
        }

        private void FRefractive_Load(object sender, EventArgs e)
        {
            txtConversion.Text = "0.001";
            txtAngel.Text = "7";
        }

        private void txtN_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                NoF = Convert.ToSingle(txtN.Text);
            }
            catch { txtN.Text = "0"; }
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
            //try
            //{
            //    NoF  = Convert.ToSingle(txtN .Text);
            //}
            //catch { txtN .Text = "0"; }
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
             float Ttop = ((Landa1 *  Landa2) * Conversion)*NoF  ;
            double  Tdown =   2 * ((Landa1 - Landa2) * (Math.Sqrt(Math.Pow(RefeactiveIndex, 2) - Math.Pow(Math.Sin((Angel*Math.PI)/180 ),2 ))));
           //  float Tdown = 2 * (Landa1 - Landa2) *Convert.ToSingle ( Math.Pow ( Math.Pow(RefeactiveIndex, 2) - ((1 / 2) * (1 - Math.Cos(2 * Angel))),(1/2)));
            T =  Ttop /(float ) Tdown;
           // float Ntop =Convert.ToSingle ( Math.Pow ( (NoF * (Landa1 *  Landa2) * Conversion)/ ( 2 * (Landa1 - Landa2)*T ),2));
           // float Ndown =Convert.ToSingle(  ( Math.Pow ( Math.Sin (Angel),2)));
          //  RefeactiveIndex = Convert.ToSingle( Math.Pow((Ntop + Ndown), (1 / 2)));
           // lbln.Text ="T:"+ T.ToString();
            lblt.Text = "Thickness:"+ T.ToString()+"micron";


        }
    }
}
