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
    public partial class FExport : F_Base
    {
        public FExport()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                for (int i = 0; i < checkBoxComboBox1.Items.Count; i++)
                    checkBoxComboBox1.CheckBoxItems[i].Checked = true;
            else
                for (int i = 0; i < checkBoxComboBox1.Items.Count; i++)
                    checkBoxComboBox1.CheckBoxItems[i].Checked = false ;

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                comboBox1.Enabled = false;
            }
            else
                comboBox1.Enabled  = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void FExport_Load(object sender, EventArgs e)
        {

        }
    }
}
