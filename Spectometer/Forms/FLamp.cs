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
    public partial class FLamp : F_Base
    {
        public FLamp()
        {
            InitializeComponent();
        }
        SofwaretProperties softwarepro = new Spectometer.SofwaretProperties();
        private void FLamp_Load(object sender, EventArgs e)
        {
            var filename = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Spectrometer\\SoftwareSetup.dat");

            IFormatter formatter = new BinaryFormatter();
            FileStream serializationStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            softwarepro = formatter.Deserialize(serializationStream) as SofwaretProperties;
            chkTangestanLamp.Checked = softwarepro.Tngestan;
            chkUVLamp.Checked = softwarepro.Dutrium;
            chNanoLed.Checked = softwarepro.NanoLed;

            serializationStream.Close();
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            softwarepro.Tngestan = chkTangestanLamp.Checked;
            softwarepro.Dutrium = chkUVLamp.Checked;
            softwarepro.NanoLed = chNanoLed.Checked;
            var filename = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Spectrometer\\SoftwareSetup.dat");

            IFormatter formatter = new BinaryFormatter();
            FileStream serializationStream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            formatter.Serialize(serializationStream, softwarepro);
            serializationStream.Close();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
