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
            string value = "";
            maskTxtDate .TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            value = maskTxtDate.Text;
            maskTxtDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
          
            value += txtVer.Text;
           
            txtSeialnumber .TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            value += txtSeialnumber.Text;
            txtSeialnumber.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            txtRes .TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            value += txtRes.Text;
            txtRes.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;


            write = Encoding.ASCII.GetBytes(value);
            if (write.Length == 28)
                en.SetDeviceDateVersionSerialNum(write);
            else
                MessageBox.Show("Error ");
        }
        byte[] write = new byte[28];
        EnScixLibrary.EnScix en = new EnScixLibrary.EnScix();
        private void FDeviceInfo_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            maskTxtDate.Visible = false;
            txtRes.Visible  = false;
            txtSeialnumber.Visible = false;
            txtVer.Visible = false;
            button1.Visible = false ;
            maskTxtDate.Text = DateTime.Now.ToString("yyyy/MM/dd");

            en.DiscardBufferPort();
            en.GetDeviceDateVersionSerialNum();
            System.Threading.Thread.Sleep(10);
            en.GetDeviceTimers();
            System.Threading.Thread.Sleep(10);
            en.GetChipTemperature();

            en.GetDeviceTimers();
        }

        private void FDeviceInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.F9  )
            {
                FSetting fSettin = new FSetting();
                fSettin.ShowDialog();
                if(fSettin.DialogResult==DialogResult.OK )
                {
                    maskTxtDate.Visible = true ;
                    txtRes.Visible = true;
                    txtSeialnumber.Visible = true;
                    txtVer.Visible = true;

                    button1.Visible = true;

                }
            }
        }
    }
}
