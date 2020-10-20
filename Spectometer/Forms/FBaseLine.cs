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
    public partial class FBaseLine : F_Base
    {
        public FBaseLine()
        {
            InitializeComponent();
        }
        public int BaseLine = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                BaseLine  = Convert.ToInt16(txtBaseLine.Text);
                this.DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FBaseLine_Load(object sender, EventArgs e)
        {
            txtBaseLine.Text = BaseLine.ToString();
            txtBaseLine.Focus();
        }
    }
}
