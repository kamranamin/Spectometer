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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spectometer.Forms
{
    public partial class FChartStep : F_Base
    {
        public FChartStep()
        {
            InitializeComponent();
        }
        public string Mode;
        public bool IsSave = false;
        private void FChartStep_Load(object sender, EventArgs e)
        {

            rdCm.Text = "cm" + " \u23BB¹";

            groupBox3.Text = Mode;
            IFormatter formatter = new BinaryFormatter();
            var filename = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Spectrometer\\SoftwareSetup.dat");
            FileStream serializationStream = new FileStream(filename, FileMode.Open, FileAccess.Read,FileShare.ReadWrite);
            softwarepro = formatter.Deserialize(serializationStream) as SofwaretProperties;
            switch (Mode)
            {
                case "Scope":
                    txtScopeX1.Text = softwarepro.ScopeX1.ToString();
                    txtScopeX2.Text = softwarepro.ScopeX2.ToString();
                    txtScopeY1.Text = softwarepro.ScopeY1.ToString();
                    txtScopeY2.Text = softwarepro.ScopeY2.ToString();

                    break;
                case "Absorbance":
                    txtScopeX1.Text = softwarepro.AbsorbanceX1.ToString();
                    txtScopeX2.Text = softwarepro.AbsorbanceX2.ToString();
                    txtScopeY1.Text = softwarepro.AbsorbanceY1.ToString();
                    txtScopeY2.Text = softwarepro.AbsorbanceY2.ToString();
                    break;
                case "Transmittance":
                    txtScopeX1.Text = softwarepro.TransmittanceX1.ToString();
                    txtScopeX2.Text = softwarepro.TransmittanceX2.ToString();
                    txtScopeY1.Text = softwarepro.TransmittanceY1.ToString();
                    txtScopeY2.Text = softwarepro.TransmittanceY2.ToString();

                    break;
                case "Reflectance":
                    txtScopeX1.Text = softwarepro.TransmittanceX1.ToString();
                    txtScopeX2.Text = softwarepro.TransmittanceX2.ToString();
                    txtScopeY1.Text = softwarepro.TransmittanceY1.ToString();
                    txtScopeY2.Text = softwarepro.TransmittanceY2.ToString();

                    break;
                case "Irradiance":
                    txtScopeX1.Text = softwarepro.IrradianceX1.ToString();
                    txtScopeX2.Text = softwarepro.IrradianceX2.ToString();
                    txtScopeY1.Text = softwarepro.IrradianceY1.ToString();
                    txtScopeY2.Text = softwarepro.IrradianceY2.ToString();

                    break;
                case "Raman":
                    txtScopeX1.Text = softwarepro.RamanX1.ToString();
                    txtScopeX2.Text = softwarepro.RamanX2.ToString();
                    txtScopeY1.Text = softwarepro.RamanY1.ToString();
                    txtScopeY2.Text = softwarepro.RamanY2.ToString();

                    break;
                case "Fluorescence":
                    txtScopeX1.Text = softwarepro.FluorescenceX1.ToString();
                    txtScopeX2.Text = softwarepro.FluorescenceX2.ToString();
                    txtScopeY1.Text = softwarepro.FluorescenceY1.ToString();
                    txtScopeY2.Text = softwarepro.FluorescenceY2.ToString();

                    break;


            }
            rdCm.Checked = softwarepro.XvalCM;
            rdNm.Checked = softwarepro.XvalNM;
            serializationStream.Close();
            serializationStream.Dispose();

        }
        SofwaretProperties softwarepro = new SofwaretProperties();
        private void SaveSetting()
        {
            IsSave = true;
            IFormatter formatter = new BinaryFormatter();

            var filename = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Spectrometer\\SoftwareSetup.dat");
            FileStream serializationStream = new FileStream(filename, FileMode.Open, FileAccess.Write,FileShare.ReadWrite,4096,FileOptions.Asynchronous);
            formatter.Serialize(serializationStream, softwarepro);
            serializationStream.Close();
           


        }
        //  Scope, Transmittance, Absorbance, Irradiance ,Raman,Reflectance,ND1,ND2,ND3,ND4,calc,Fluorescence
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                switch (Mode)
                {
                    case "Scope":
                        softwarepro.ScopeX1 = Convert.ToSingle(txtScopeX1.Text);
                        softwarepro.ScopeX2= Convert.ToSingle(txtScopeX2.Text);
                        softwarepro.ScopeY1 = Convert.ToSingle(txtScopeY1.Text);
                        softwarepro.ScopeY2 = Convert.ToSingle(txtScopeY2.Text);
                        SaveSetting();
                        break;
                    case "Absorbance":
                        softwarepro.AbsorbanceX1 = Convert.ToSingle(txtScopeX1.Text);
                        softwarepro.AbsorbanceX2 = Convert.ToSingle(txtScopeX2.Text);
                        softwarepro.AbsorbanceY1  = Convert.ToSingle(txtScopeY1.Text);
                        softwarepro.AbsorbanceY2 = Convert.ToSingle(txtScopeY2.Text);
                        SaveSetting();
                        break;
                    case "Transmittance":
                        softwarepro.TransmittanceX1  = Convert.ToSingle(txtScopeX1.Text);
                        softwarepro.TransmittanceX2 = Convert.ToSingle(txtScopeX2.Text);
                        softwarepro.TransmittanceY1  = Convert.ToSingle(txtScopeY1.Text);
                        softwarepro.TransmittanceY2 = Convert.ToSingle(txtScopeY2.Text);
                        SaveSetting();
                        break;
                    case "Reflectance":
                        softwarepro.TransmittanceX1 = Convert.ToSingle(txtScopeX1.Text);
                        softwarepro.TransmittanceX2 = Convert.ToSingle(txtScopeX2.Text);
                        softwarepro.TransmittanceY1 = Convert.ToSingle(txtScopeY1.Text);
                        softwarepro.TransmittanceY2 = Convert.ToSingle(txtScopeY2.Text);
                        SaveSetting();
                        break;
                    case "Irradiance":
                        softwarepro.IrradianceX1  = Convert.ToSingle(txtScopeX1.Text);
                        softwarepro.IrradianceX2 = Convert.ToSingle(txtScopeX2.Text);
                        softwarepro.IrradianceY1 = Convert.ToSingle(txtScopeY1.Text);
                        softwarepro.IrradianceY2  = Convert.ToSingle(txtScopeY2.Text);
                        SaveSetting();
                        break;
                    case "Raman":
                        softwarepro.RamanX1 = Convert.ToSingle(txtScopeX1.Text);
                        softwarepro.RamanX2 = Convert.ToSingle(txtScopeX2.Text);
                        softwarepro.RamanY1 = Convert.ToSingle(txtScopeY1.Text);
                        softwarepro.RamanY2 = Convert.ToSingle(txtScopeY2.Text);
                        SaveSetting();
                        break;
                    case "Fluorescence":
                        softwarepro.FluorescenceX1 = Convert.ToInt32 (txtScopeX1.Text);
                        softwarepro.FluorescenceX2  = Convert.ToInt32(txtScopeX2.Text);
                        softwarepro.FluorescenceY1  = Convert.ToInt32 (txtScopeY1.Text);
                        softwarepro.FluorescenceY2 = Convert.ToInt32 (txtScopeY2.Text);
                        SaveSetting();
                        break;
                       

                }
                {
                    softwarepro.XvalNM = rdNm.Checked;
                    softwarepro.XvalCM = rdCm.Checked;
                    Thread.Sleep(100);
                    SaveSetting();
                }
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
