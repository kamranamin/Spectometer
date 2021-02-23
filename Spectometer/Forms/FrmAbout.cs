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
    public partial class FrmAbout : F_Base
    {
        public FrmAbout()
        {
          
            InitializeComponent();
        }
        EnScixLibrary.EnScix en = new EnScixLibrary.EnScix();

        private void FrmAbout_Load(object sender, EventArgs e)
        {
            this._lbl_form_text.Text = "About";
            en.DiscardBufferPort();
            en.GetDeviceDateVersionSerialNum();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://electron-co.com/");
        }
    }
}
