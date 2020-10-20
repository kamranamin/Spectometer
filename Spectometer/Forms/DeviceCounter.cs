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
    public partial class DeviceCounter : F_Base 
    {
        EnScixLibrary.EnScix  en = new EnScixLibrary.EnScix();
        public DeviceCounter()
        {
          
            InitializeComponent();
        }

        private void DeviceCounter_Load(object sender, EventArgs e)
        {
            en.GetDeviceTimers();
        }
    }
}
