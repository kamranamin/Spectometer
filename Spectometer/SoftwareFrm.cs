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
using System.Windows.Forms;

namespace Spectometer
{
    public partial class SoftwareFrm : Form
    {
        public SoftwareFrm()
        {
            InitializeComponent();
        }
        SofwaretProperties softwarepro = new SofwaretProperties();
        private void SaveSoftwareProperties()
        {
            try
            {
                Single s;
                foreach (TabPage tb in tabControl1.TabPages )
                {
                   
                    foreach (Control txt in tb.Controls)
                    {
                        if (txt is TextBox)
                        {
                            TextBox textBox = txt as TextBox;
                            if (string.IsNullOrEmpty(textBox.Text) ||!Single.TryParse(textBox.Text ,out s))
                            {
                                tabControl1.SelectedTab = tb;
                                textBox .Focus();
                                MessageBox.Show(textBox.Name.Remove(0, 3) + "Not Valid");
                                return;
                            }
                        }
                    }
                }
                softwarepro.AbsorbanceX1 = Convert.ToSingle(txtAbsorbanceX1.Text);
                softwarepro.AbsorbanceX2 = Convert.ToSingle(txtAbsorbanceX2.Text);
                softwarepro.AbsorbanceY1 = Convert.ToSingle(txtAbsorbanceY1.Text);
                softwarepro.AbsorbanceY1 = Convert.ToSingle(txtAbsorbanceY2.Text);
                softwarepro.IrradianceX1= Convert.ToSingle(txtIrradianceX1 .Text);
                softwarepro.IrradianceX2 = Convert.ToSingle(txtIrradianceX2.Text);
                softwarepro.IrradianceY1 = Convert.ToSingle(txtIrradianceY1.Text);
                softwarepro.IrradianceY2 = Convert.ToSingle(txtIrradianceY2.Text);
                softwarepro.ScopeX1  = Convert.ToSingle(txtScopeX1 .Text);
                softwarepro.ScopeX2 = Convert.ToSingle(txtScopeX2.Text);
                softwarepro.ScopeY1 = Convert.ToSingle(txtScopeY1.Text);
                softwarepro.ScopeY2= Convert.ToSingle(txtScopeY2.Text);
                softwarepro.TransmittanceX1  = Convert.ToSingle(txtTransmitanceX1 .Text);
                softwarepro.TransmittanceX2 = Convert.ToSingle(txtTransmitanceX2.Text);
                softwarepro.TransmittanceY1 = Convert.ToSingle(txtTransmitanceY1.Text);
                softwarepro.TransmittanceY2 = Convert.ToSingle(txtTransmitanceY2.Text);
                softwarepro.XmapC1  = Convert.ToSingle(txtXmapC1 .Text);
                softwarepro.XmapC2 = Convert.ToSingle(txtXmapC2.Text);
                softwarepro.XmapC3 = Convert.ToSingle(txtXmapC3.Text);
                softwarepro.XmapI  = Convert.ToSingle(txtXmapI .Text);
                softwarepro.YmapC1  = Convert.ToSingle(txtYmapC1 .Text);
                softwarepro.YmapC2 = Convert.ToSingle(txtYmapC2.Text);
                softwarepro.YmapC3 = Convert.ToSingle(txtYmapC3.Text);
                softwarepro.YmapC4 = Convert.ToSingle(txtYmapC4.Text);
                softwarepro.YmapC5 = Convert.ToSingle(txtYmapC5.Text);
                softwarepro.YmapC6 = Convert.ToSingle(txtYmapC6.Text);
                softwarepro.YmapC7 = Convert.ToSingle(txtYmapC7.Text);
                softwarepro.YmapC8 = Convert.ToSingle(txtYmapC8.Text);
               
              

                IFormatter formatter = new BinaryFormatter();
                FileStream serializationStream = new FileStream("SoftwareSetup.dat", FileMode.Create, FileAccess.Write);
                formatter.Serialize(serializationStream, softwarepro );
                serializationStream.Close();



            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void LoadSofwareProperties ()
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                FileStream serializationStream = new FileStream("SoftwareSetup.dat", FileMode.Open, FileAccess.Read);
                softwarepro  = formatter.Deserialize(serializationStream) as SofwaretProperties ;
                txtAbsorbanceX1.Text = softwarepro.AbsorbanceX1.ToString();
                txtAbsorbanceX2.Text= softwarepro.AbsorbanceX2.ToString();
                txtAbsorbanceY1.Text = softwarepro.TransmittanceY1.ToString();
                txtAbsorbanceY2.Text = softwarepro.TransmittanceY2.ToString();
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
                txtXmapC1.Text = softwarepro.XmapC1.ToString();
                txtXmapC2.Text = softwarepro.XmapC2.ToString();
                txtXmapC3.Text = softwarepro.XmapC3.ToString();
                txtXmapI.Text = softwarepro.XmapI .ToString();
                txtYmapC1.Text = softwarepro.YmapC1.ToString();
                txtYmapC2.Text = softwarepro.YmapC2.ToString();
                txtYmapC3.Text = softwarepro.YmapC3.ToString();
                txtYmapC4.Text = softwarepro.YmapC4.ToString();
                txtYmapC5.Text = softwarepro.YmapC5.ToString();
                txtYmapC6.Text = softwarepro.YmapC6.ToString();
                txtYmapC7.Text = softwarepro.YmapC7.ToString();
                txtYmapC8.Text = softwarepro.YmapC8.ToString();
                txtYmapI .Text = softwarepro.YmapI .ToString();
                
                serializationStream.Close();
               

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSoftwareProperties();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SoftwareFrm_Load(object sender, EventArgs e)
        {
            LoadSofwareProperties();
        }
    }
}
