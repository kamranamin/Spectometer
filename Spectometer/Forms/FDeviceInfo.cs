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
    public partial class FDeviceInfo : F_Base 
    {
        public FDeviceInfo()
        {
            InitializeComponent();
        }

        private void F2Date_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            EnScixLibrary.EnScix  en = new EnScixLibrary.EnScix ();
           // en.();

        }
    }
}
