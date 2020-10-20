using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace Spectometer.Forms
{
    public partial class FSoftware : Spectometer.Forms.F_Base
    {
        public bool Issave = false;
        public FSoftware()
        {
            InitializeComponent();
        }
        SofwaretProperties softwarepro = new SofwaretProperties();
        private void SaveSoftwareProperties()
        {
            try
            {
                Single s;
                

                    foreach (Control   txt in this.Controls )
                    {
                        if (txt is TextBox)
                        {
                            TextBox textBox = txt as TextBox;
                            if (string.IsNullOrEmpty(textBox.Text) || !Single.TryParse(textBox.Text, out s))
                            {
                              
                                textBox.Focus();
                                MessageBox.Show(textBox.Name.Remove(0, 3) + "Not Valid");
                                return;
                            }
                        }
                    
                }
               
                softwarepro.AbsorbanceX1 = Convert.ToSingle(txtAbsorbanceX1.Text);
                softwarepro.AbsorbanceX2 = Convert.ToSingle(txtAbsorbanceX2.Text);
                softwarepro.AbsorbanceY1 = Convert.ToSingle(txtAbsorbanceY1.Text);
                softwarepro.AbsorbanceY2  = Convert.ToSingle(txtAbsorbanceY2.Text);
                softwarepro.IrradianceX1 = Convert.ToSingle(txtIrradianceX1.Text);
                softwarepro.IrradianceX2 = Convert.ToSingle(txtIrradianceX2.Text);
                softwarepro.IrradianceY1 = Convert.ToSingle(txtIrradianceY1.Text);
                softwarepro.IrradianceY2 = Convert.ToSingle(txtIrradianceY2.Text);
                softwarepro.ScopeX1 = Convert.ToSingle(txtScopeX1.Text);
                softwarepro.ScopeX2 = Convert.ToSingle(txtScopeX2.Text);
                softwarepro.ScopeY1 = Convert.ToSingle(txtScopeY1.Text);
                softwarepro.ScopeY2 = Convert.ToSingle(txtScopeY2.Text);
                softwarepro.TransmittanceX1 = Convert.ToSingle(txtTransmitanceX1.Text);
                softwarepro.TransmittanceX2 = Convert.ToSingle(txtTransmitanceX2.Text);
                softwarepro.TransmittanceY1 = Convert.ToSingle(txtTransmitanceY1.Text);
                softwarepro.TransmittanceY2 = Convert.ToSingle(txtTransmitanceY2.Text);
               
                softwarepro.RamanX1 = Convert.ToSingle(txtRamanX1.Text);
                softwarepro.RamanX2 = Convert.ToSingle(txtramanX2 .Text);
                softwarepro.RamanY1 = Convert.ToSingle(txtRamanY1 .Text);
                softwarepro.RamanY2 = Convert.ToSingle(txtRamanY2 .Text);
                softwarepro.FluorescenceX1  = Convert.ToInt32(txtReflectanceX1.Text);
                softwarepro.FluorescenceX2  = Convert.ToInt32(txtReflectanceX2.Text);
                softwarepro.FluorescenceY1  = Convert.ToInt32(txtReflectanceY1.Text);
                softwarepro.FluorescenceY2  = Convert.ToInt32(txtReflectanceY2.Text);
                softwarepro.HideColorbar = chHideColorBar.Checked;

               

               

                IFormatter formatter = new BinaryFormatter();
                var filename = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Spectrometer\\SoftwareSetup.dat");
               // var filename = Path.Combine(System.Environment.CurrentDirectory, );
                FileStream serializationStream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite );
                formatter.Serialize(serializationStream, softwarepro);
                serializationStream.Close();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void LoadSofwareProperties()
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();

                var filename = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Spectrometer\\SoftwareSetup.dat");

                FileStream serializationStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                softwarepro = formatter.Deserialize(serializationStream) as SofwaretProperties;
                txtAbsorbanceX1.Text = softwarepro.AbsorbanceX1.ToString();
                txtAbsorbanceX2.Text = softwarepro.AbsorbanceX2.ToString();
                txtAbsorbanceY1.Text = softwarepro.AbsorbanceY1 .ToString();
                txtAbsorbanceY2.Text = softwarepro.AbsorbanceY2.ToString();
                txtIrradianceX1.Text = softwarepro.IrradianceX1.ToString();
                txtIrradianceX2.Text = softwarepro.IrradianceX2.ToString();
                txtIrradianceY1.Text = softwarepro.IrradianceY1.ToString();
                txtIrradianceY2.Text = softwarepro.IrradianceY2.ToString();
                txtScopeX1.Text = softwarepro.ScopeX1.ToString();
                txtScopeX2.Text = softwarepro.ScopeX2.ToString();
                txtScopeY1.Text = softwarepro.ScopeY1.ToString();
                txtScopeY2.Text = softwarepro.ScopeY2.ToString();
                txtTransmitanceX1.Text = softwarepro.TransmittanceX1.ToString();
                txtTransmitanceX2.Text = softwarepro.TransmittanceX2.ToString();
                txtTransmitanceY1.Text = softwarepro.TransmittanceY1.ToString();
                txtTransmitanceY2.Text = softwarepro.TransmittanceY2.ToString();
               
                txtRamanX1.Text = softwarepro.RamanX1.ToString();
                txtramanX2.Text = softwarepro.RamanX2.ToString();
                txtRamanY1.Text = softwarepro.RamanY1.ToString();
                txtRamanY2.Text = softwarepro.RamanY2 .ToString();
                txtReflectanceX1.Text = softwarepro.FluorescenceX1 .ToString();
                txtReflectanceX2.Text = softwarepro.FluorescenceX2 .ToString();
                txtReflectanceY1.Text = softwarepro.FluorescenceY1 .ToString();
                txtReflectanceY2.Text = softwarepro.FluorescenceY2.ToString();
                chHideColorBar.Checked = softwarepro.HideColorbar;
               
              
                serializationStream.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSoftwareProperties();
            Issave = true;
            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FSoftware_Load(object sender, EventArgs e)
        {
            LoadSofwareProperties();
        }
    }
}
