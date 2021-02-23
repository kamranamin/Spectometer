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
    public partial class PeckFIlterFrm : F_Base
    {
        public PeckFIlterFrm()
        {
            InitializeComponent();
        }
        public double NumFilter;
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                NumFilter = Convert.ToDouble(textBox1.Text);
            }
            catch { NumFilter = 0; }
            this.DialogResult = DialogResult.OK;
        }
    }
}
