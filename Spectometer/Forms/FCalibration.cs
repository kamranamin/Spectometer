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
    public partial class FCalibration : F_Base
    {
        SofwaretProperties softwarepro = new Spectometer.SofwaretProperties();
        public FCalibration()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FCalibration_Load(object sender, EventArgs e)
        {
            IFormatter formatter = new BinaryFormatter();
            var filename = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Spectrometer\\SoftwareSetup.dat");

            FileStream serializationStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            softwarepro = formatter.Deserialize(serializationStream) as SofwaretProperties;
            txtXmapC1.Text = softwarepro.XmapC1.ToString();
            txtXmapC2.Text = softwarepro.XmapC2.ToString();
            txtXmapC3.Text = softwarepro.XmapC3.ToString();
            txtXmapI.Text = softwarepro.XmapI.ToString();
            txtYmapC1.Text = softwarepro.YmapC1.ToString();
            txtYmapC2.Text = softwarepro.YmapC2.ToString();
            txtYmapC3.Text = softwarepro.YmapC3.ToString();
            txtYmapC4.Text = softwarepro.YmapC4.ToString();
            txtYmapC5.Text = softwarepro.YmapC5.ToString();
            txtYmapC6.Text = softwarepro.YmapC6.ToString();
            txtYmapC7.Text = softwarepro.YmapC7.ToString();
            txtYmapC8.Text = softwarepro.YmapC8.ToString();
            txtYmapI.Text = softwarepro.YmapI.ToString();
            chEnableBaselINE.Checked = softwarepro.EnableBaseLine;
            txtBaseLine.Text = softwarepro.BaseLine.ToString();
            serializationStream.Close();


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
                softwarepro.XmapC1 = Convert.ToSingle(txtXmapC1.Text);
                softwarepro.XmapC2 = Convert.ToSingle(txtXmapC2.Text);
                softwarepro.XmapC3 = Convert.ToSingle(txtXmapC3.Text);
                softwarepro.XmapI = Convert.ToSingle(txtXmapI.Text);
                softwarepro.YmapC1 = Convert.ToSingle(txtYmapC1.Text);
                softwarepro.YmapC2 = Convert.ToSingle(txtYmapC2.Text);
                softwarepro.YmapC3 = Convert.ToSingle(txtYmapC3.Text);
                softwarepro.YmapC4 = Convert.ToSingle(txtYmapC4.Text);
                softwarepro.YmapC5 = Convert.ToSingle(txtYmapC5.Text);
                softwarepro.YmapC6 = Convert.ToSingle(txtYmapC6.Text);
                softwarepro.YmapC7 = Convert.ToSingle(txtYmapC7.Text);
                softwarepro.YmapC8 = Convert.ToSingle(txtYmapC8.Text);
                softwarepro.EnableBaseLine = chEnableBaselINE.Checked;
                softwarepro.BaseLine = Convert.ToInt16(txtBaseLine.Text);
            IFormatter formatter = new BinaryFormatter();
            var filename = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Spectrometer\\SoftwareSetup.dat");


            FileStream serializationStream = new FileStream(filename, FileMode.OpenOrCreate , FileAccess.ReadWrite);
              formatter.Serialize(serializationStream, softwarepro);
              serializationStream.Close();
            this.Close();
        }

        private void txtXmapC1_MouseLeave(object sender, EventArgs e)
        {
            txtXmapC1.Select(0, 0);
        }

        private void txtXmapC2_Leave(object sender, EventArgs e)
        {
            txtXmapC2.Select(0, 0);

        }

        private void txtXmapC3_Leave(object sender, EventArgs e)
        {
            txtXmapC3.Select(0, 0);
        }

        private void txtXmapI_Leave(object sender, EventArgs e)
        {
            txtXmapI.Select(0, 0);
        }
    }
}
