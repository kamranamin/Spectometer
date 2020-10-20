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
    public partial class FMath : F_Base
    {
        public bool isRun=true;
        public bool isCalc = false;
        public FMath()
        {
            InitializeComponent();
        }

        private void _lbl_form_text_Click(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
         //   isRun = true;
             
               
        }

        private void FMath_FormClosing(object sender, FormClosingEventArgs e)
        {
            isRun = false;
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            isCalc = true;

        }

        private void FMath_Load(object sender, EventArgs e)
        {

        }
    }
}
