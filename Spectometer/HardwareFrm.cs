using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Spectometer
{
    public partial class HardwareFrm : Form
    {
        private byte BlueOffsetAddress = 0;
        private ushort BlueOffsetData = 0;
        private byte BluePgaAdress = 0;
        private ushort BluPgaData = 0;
        Business business1 = new Business();
        private byte ConfigurationAdress = 0;
        private ushort ConfigurationData = 0;
        private byte GreenOffsetAddress = 0;
        private ushort GreenOffsetData = 0;
        private byte GreenPgaAddress = 0;
        private ushort GreenPgaData = 0;
        private byte[] Hardwaredata = new byte[0x10];
        private byte MuxAddress = 0;
        private ushort MuxData = 0;
        private byte RedOffsetAdress = 0;
        private ushort RedOffsetData = 0;
        private byte RedPgaAddress = 0;
        private ushort RedPgaData = 0;
        public bool Refresh = false;
        public bool save = false;
        public HardwareFrm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.save = true;
            this.ConfigurationAdress = Convert.ToByte(this.txtConfigurationAdress .Text);
            this.ConfigurationData = Convert.ToUInt16(this.txtConfigurationData .Text);
            this.Hardwaredata[0] = this.ConfigurationAdress;
            this.Hardwaredata[0] = Convert.ToByte(this.Hardwaredata[0]);
            if (this.ConfigurationData <= 0xff)
            {
                this.Hardwaredata[1] = Convert.ToByte(this.ConfigurationData);
            }
            else
            {
                this.ConfigurationData = Convert.ToByte((int)(this.ConfigurationData - 0x100));
                this.Hardwaredata[1] = Convert.ToByte(this.ConfigurationData);
                this.Hardwaredata[0] = Convert.ToByte((int)(this.Hardwaredata[0] + 1));
            }
            this.MuxAddress = Convert.ToByte(this.txtMuxAddress .Text);
            this.MuxData = Convert.ToUInt16(this.txtMuxData .Text);
            this.Hardwaredata[2] = this.MuxAddress;
            this.Hardwaredata[2] = Convert.ToByte(this.Hardwaredata[2]);
            if (this.MuxData <= 0xff)
            {
                this.Hardwaredata[3] = Convert.ToByte(this.MuxData);
            }
            else
            {
                this.MuxData = Convert.ToByte((int)(this.MuxData - 0x100));
                this.Hardwaredata[3] = Convert.ToByte(this.MuxData);
                this.Hardwaredata[2] = Convert.ToByte((int)(this.Hardwaredata[2] + 1));
            }
            this.RedPgaAddress = Convert.ToByte(this.txtRedPGA .Text);
            this.RedPgaData = Convert.ToUInt16(this.txtRedPgaData .Text);
            this.Hardwaredata[4] = this.RedPgaAddress;
            this.Hardwaredata[4] = Convert.ToByte(this.Hardwaredata[4]);
            if (this.RedPgaData <= 0xff)
            {
                this.Hardwaredata[5] = Convert.ToByte(this.RedPgaData);
            }
            else
            {
                this.RedPgaData = Convert.ToByte((int)(this.RedPgaData - 0x100));
                this.Hardwaredata[5] = Convert.ToByte(this.RedPgaData);
                this.Hardwaredata[4] = Convert.ToByte((int)(this.Hardwaredata[4] + 1));
            }
            this.GreenPgaAddress = Convert.ToByte(this.txtGreenPgaAddress .Text);
            this.GreenPgaData = Convert.ToUInt16(this.txtGreenPgaData .Text);
            this.Hardwaredata[6] = this.GreenPgaAddress;
            this.Hardwaredata[6] = Convert.ToByte(this.Hardwaredata[6]);
            if (this.GreenPgaData <= 0xff)
            {
                this.Hardwaredata[7] = Convert.ToByte(this.GreenPgaData);
            }
            else
            {
                this.GreenPgaData = Convert.ToByte((int)(this.GreenPgaData - 0x100));
                this.Hardwaredata[7] = Convert.ToByte(this.GreenPgaData);
                this.Hardwaredata[6] = Convert.ToByte((int)(this.Hardwaredata[6] + 1));
            }
            this.RedOffsetAdress = Convert.ToByte(this.txtRedOffsetAddress .Text);
            this.RedOffsetData = Convert.ToUInt16(this.txtRedOffsetData.Text);
            this.Hardwaredata[8] = this.RedOffsetAdress;
            this.Hardwaredata[8] = Convert.ToByte(this.Hardwaredata[8]);
            if (this.RedOffsetData <= 0xff)
            {
                this.Hardwaredata[9] = Convert.ToByte(this.RedOffsetData);
            }
            else
            {
                this.RedOffsetData = Convert.ToByte((int)(this.RedOffsetData - 0x100));
                this.Hardwaredata[9] = Convert.ToByte(this.RedOffsetData);
                this.Hardwaredata[8] = Convert.ToByte((int)(this.Hardwaredata[8] + 1));
            }
            this.GreenOffsetAddress = Convert.ToByte(this.txtGreenOffsetAddress .Text);
            this.GreenOffsetData = Convert.ToUInt16(this.txtGreenOffsetData.Text);
            this.Hardwaredata[10] = this.GreenOffsetAddress;
            this.Hardwaredata[10] = Convert.ToByte(this.Hardwaredata[10]);
            if (this.GreenOffsetData <= 0xff)
            {
                this.Hardwaredata[11] = Convert.ToByte(this.GreenOffsetData);
            }
            else
            {
                this.GreenOffsetData = Convert.ToByte((int)(this.GreenOffsetData - 0x100));
                this.Hardwaredata[11] = Convert.ToByte(this.GreenOffsetData);
                this.Hardwaredata[10] = Convert.ToByte((int)(this.Hardwaredata[10] + 1));
            }
            this.BluePgaAdress = Convert.ToByte(this.txtBluePgaAddress .Text);
            this.BluPgaData = Convert.ToUInt16(this.txtBluePgaData.Text);
            this.Hardwaredata[12] = this.BluePgaAdress;
            this.Hardwaredata[12] = Convert.ToByte(this.Hardwaredata[12]);
            if (this.BluPgaData <= 0xff)
            {
                this.Hardwaredata[13] = Convert.ToByte(this.BluPgaData);
            }
            else
            {
                this.BluPgaData = Convert.ToByte((int)(this.BluPgaData - 0x100));
                this.Hardwaredata[13] = Convert.ToByte(this.BluPgaData);
                this.Hardwaredata[12] = Convert.ToByte((int)(this.Hardwaredata[12] + 1));
            }
            this.BlueOffsetAddress = Convert.ToByte(this.txtBlueOffsetAddress.Text);
            this.BlueOffsetData = Convert.ToUInt16(this.txtBlueOffsetData .Text);
            this.Hardwaredata[14] = this.BlueOffsetAddress;
            this.Hardwaredata[14] = Convert.ToByte(this.Hardwaredata[14]);
            if (this.BlueOffsetData <= 0xff)
            {
                this.Hardwaredata[15] = Convert.ToByte(this.BlueOffsetData);
            }
            else
            {
                this.BlueOffsetData = Convert.ToByte((int)(this.BlueOffsetData - 0x100));
                this.Hardwaredata[15] = Convert.ToByte(this.BlueOffsetData);
                this.Hardwaredata[14] = Convert.ToByte((int)(this.Hardwaredata[14] + 1));
            }
            this.business1.SetHardwareData(this.Hardwaredata);
            this.business1.SaveHardwareFile(this.Hardwaredata);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.Refresh = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Hide();
        }
    }
}
