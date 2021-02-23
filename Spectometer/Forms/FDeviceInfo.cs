using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
            maskTxtDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            value = maskTxtDate.Text;
            maskTxtDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;

            value += txtVer.Text;

            txtSeialnumber.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            value += txtSeialnumber.Text;
            txtSeialnumber.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            //  txtRes .TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            value += "0000";
            // txtRes.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;


            write = Encoding.ASCII.GetBytes(value);
            if (write.Length == 28)
            {
                en.SetDeviceDateVersionSerialNum(write);
                MessageBox.Show("Hardware setting saved");
            }
            else
                MessageBox.Show("Error ");
        }
        byte[] write = new byte[28];
        EnScixLibrary.EnScix en = new EnScixLibrary.EnScix();
        private void LoadGainOffset()
        {
            var filename = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Spectrometer\\SoftwareSetup.dat");

            IFormatter formatter = new BinaryFormatter();
            FileStream serializationStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            softwarepro = formatter.Deserialize(serializationStream) as SofwaretProperties;
            txtGain.Text = softwarepro.Gain.ToString();

            txtOffset.Text = softwarepro.Offset.ToString();
           

            serializationStream.Close();
        }
        private void SaveGainOffset()
        {
            softwarepro.Gain = Convert.ToInt32(txtGain.Text);
            softwarepro.Offset = Convert.ToInt32(txtOffset.Text);
            var filename = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Spectrometer\\SoftwareSetup.dat");

            IFormatter formatter = new BinaryFormatter();
            FileStream serializationStream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            formatter.Serialize(serializationStream, softwarepro);
            serializationStream.Close();
            this.Close();
        }
        private void FDeviceInfo_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            maskTxtDate.Visible = false;
          
            txtSeialnumber.Visible = false;
            txtVer.Visible = false;
            button1.Visible = false ;
            groupBox1.Visible = false;
            LoadGainOffset();
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
                    groupBox1.Visible = true;
                    maskTxtDate.Visible = true ;
                 
                    txtSeialnumber.Visible = true;
                    txtVer.Visible = true;

                    button1.Visible = true;

                }
            }
        }
        SofwaretProperties softwarepro = new Spectometer.SofwaretProperties();
        private void btnSave_Click(object sender, EventArgs e)
        {
           

            en.ADCProgramInitialValue(Convert.ToByte(txtGain.Text), Convert.ToInt16(txtOffset.Text));
            SaveGainOffset();
        }

        private void Information_Enter(object sender, EventArgs e)
        {

        }
    }
}
