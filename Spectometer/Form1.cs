using EnScixLibrary;
using Spectometer.Forms;
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
using System.Windows.Forms.DataVisualization.Charting;

using ComportDataReceiveEventApp;
using System.Reflection;
using System.Threading;
using System.IO.Ports;

namespace Spectometer
{
    public partial class Form1 : Form
    {
      
        private EnScixLibrary.EnScix ComportInterfacecs = new EnScixLibrary.EnScix();
      
     
      
        bool PortConnectionStatus = false;
        private byte[] SettingData = new byte[40];
        private float[] MainData,DataForShow = new float[2090];
        private byte[] ChipID = new byte[8];
        private byte[] ChipTemperatureVal = new byte[4];
        private byte[] Tangestanval = new byte[4];
        private byte[] ReadSerialnumber = new byte[28];
        private byte[] CounterValue = new byte[16];
        private bool  isRun=false;
        double[] x=new double[2090];
        float[][] AverageData = new float[][] {
            new float[0x82a], new float[0x82a], new float[0x82a], new float[0x82a], new float[0x82a], new float[0x82a], new float[0x82a], new float[0x82a], new float[0x82a], new float[0x82a], new float[0x82a], new float[0x82a], new float[0x82a], new float[0x82a], new float[0x82a], new float[0x82a],
            new float[0x82a], new float[0x82a], new float[0x82a], new float[0x82a], new float[0x82a], new float[0x82a], new float[0x82a], new float[0x82a], new float[0x82a], new float[0x82a]
        };
        int NumberOfFrame = 1;
        
      
        public Form1()
        {
          
            InitializeComponent();
        }
       
        #region Properties
      
        private ushort Average;
        private ushort LampBrightness;
        private ushort Smoothing;
        private float RE1;
        private ushort RE2;
        private UInt32   IntegrationTime;
        Environment enviroment = new Environment();
        HardwareFrm  setup1 = new HardwareFrm ();
        Business business = new Business();
        SofwaretProperties SoftwarePr = new SofwaretProperties();
        DataAnalysis dtAnalys = new DataAnalysis();
        FAddNewExperiment newExperiment=new FAddNewExperiment();
        FDeviceInfo deviceInfo = new FDeviceInfo();
        SpectometrMode Mode;
        float[] darkData;
        float[] refrenceData;
        string ExperimentName;
        bool tangestanIs=false ;
        bool shuterIs = false;
        private float _AbsorbanceX1 = 0f;
        private float _AbsorbanceX2 = 0f;
        private float _AbsorbanceY1 = 0f;
        private float _AbsorbanceY2 = 0f;
        private float _XmapC1 = 0f;
        private float _XmapC2 = 0f;
        private float _XmapC3 = 0f;
        private float _XmapI = 0f;
        private float _IrradianceX1 = 0f;
        private float _IrradianceX2 = 0f;
        private float _IrradianceY1 = 0f;
        private float _IrradianceY2 = 0f;
        private int _LampType = 0;
        private float _ScopeX1 = 0f;
        private float _ScopeX2 = 0f;
        private float _ScopeY1 = 0f;
        private float _ScopeY2 = 0f;
        private float _TransmittanceX1 = 0f;
        private float _TransmittanceX2 = 0f;
        private float _TransmittanceY1 = 0f;
        private float _TransmittanceY2 = 0f;
        private float _YmapC1 = 0f;
        private float _YmapC2 = 0f;
        private float _YmapC3 = 0f;
        private float _YmapC4 = 0f;
        private float _YmapC5 = 0f;
        private float _YmapC6 = 0f;
        private float _YmapC7 = 0f;
        private float _YmapC8 = 0f;
        private float _YmapI = 0f;
        private float _RamanX1 = 0f;
        private float _RamanX2 = 0f;
        private float _RamanY1 = 0f;
        private float _RamanY2 = 0f;
        private Point? prevPosition = null;
        private ToolTip tooltip = new ToolTip();
        private int ChartCount = 1;
        private  FTimeSpectrum timeSpectrumFrm;
        private FCalibrationCruve calibrationFrm;
        private System.Windows.Forms.Timer timeNanoDrop =new System.Windows.Forms.Timer ();
        private System.Windows.Forms.Timer timeSingleWaveLenght = new System.Windows.Forms.Timer();
        FSingleWaveLenght SingleWavefrm = new FSingleWaveLenght();
        private System.Windows.Forms  .Timer timeCalibrationCruve = new System.Windows.Forms.Timer();
        FNanoDrop nanoDropFrm = new FNanoDrop();

       
        public System.Windows.Forms.Timer timeSpec = new System.Windows.Forms.Timer();
        FSoftware softWareFrm = new FSoftware();

        #endregion Properties
        enum SpectometrMode  { Scope, Transmittance, Absorbance, Irradiance ,Raman,Reflectance,ND1,ND2,ND3,ND4};



        private void SaveEnvironment ()
        {
            try
            {
               
                enviroment.Average = this.Average;
                enviroment.LampBrightness = this.LampBrightness;
                enviroment.Smoothing = this.Smoothing;
                enviroment.RE1 = this.RE1;
                enviroment.RE2 = this.RE2;
                enviroment.IntegrationTime = this.IntegrationTime;
                IFormatter formatter = new BinaryFormatter();
               
                FileStream serializationStream = new FileStream( "EnvironmentProperties.dat", FileMode.Create, FileAccess.Write);
                formatter.Serialize(serializationStream, enviroment);
                serializationStream.Close();
                
            }
            catch
            {
                message("Error ocured in saving environment data.", false);

            }

        }
        private void LoadSofwareProperties()
        {
            try
            {
                
                IFormatter formatter = new BinaryFormatter();
                FileStream serializationStream = new FileStream("SoftwareSetup.dat", FileMode.Open, FileAccess.Read);
                SoftwarePr = formatter.Deserialize(serializationStream) as SofwaretProperties;
                this._AbsorbanceX1 = SoftwarePr.AbsorbanceX1;
                this._AbsorbanceX2 = SoftwarePr.AbsorbanceX2;
                _AbsorbanceY1 =  SoftwarePr.AbsorbanceY1 ;
                _AbsorbanceY2  = SoftwarePr.AbsorbanceY2 ;
                _TransmittanceX1 = SoftwarePr.TransmittanceX1;
                _TransmittanceX2 = SoftwarePr.TransmittanceX2;
                _TransmittanceY1 = SoftwarePr.TransmittanceY1;
                _TransmittanceY2 = SoftwarePr.TransmittanceY2;
                _IrradianceX1 = SoftwarePr.IrradianceX1;
                _IrradianceX2 = SoftwarePr.IrradianceX2;
                _IrradianceY1 = SoftwarePr.IrradianceY1;
                _IrradianceY2 = SoftwarePr.IrradianceY2;
                _ScopeX1 = SoftwarePr.ScopeX1;
                _ScopeX2 = SoftwarePr.ScopeX2;
                _ScopeY1   = SoftwarePr.ScopeY1;
               _ScopeY2     = SoftwarePr.ScopeY2;
              
             
               _XmapC1  = SoftwarePr.XmapC1;
                _XmapC2 = SoftwarePr.XmapC2;
                _XmapC3 = SoftwarePr.XmapC3;
                _XmapI = SoftwarePr.XmapI;
               _YmapC1   = SoftwarePr.YmapC1;
                _YmapC2 = SoftwarePr.YmapC2;
                _YmapC3 = SoftwarePr.YmapC3;
                _YmapC4 = SoftwarePr.YmapC4;
                _YmapC5 = SoftwarePr.YmapC5;
                _YmapC6= SoftwarePr.YmapC6;
                _YmapC7 = SoftwarePr.YmapC7;
                _YmapC8 = SoftwarePr.YmapC8;
                _YmapI  = SoftwarePr.YmapI;
                _RamanX1 = SoftwarePr.RamanX1;
                _RamanX2 = SoftwarePr.RamanX2;
                _RamanY1 = SoftwarePr.RamanY1;
                _RamanY2 = SoftwarePr.RamanY2;
             
                serializationStream.Close();


            }
            catch (Exception ex)
            {
                message(ex.ToString(), false);
             
            }
        }

        private void LoadEnvironment ()
        {
            try {
                IFormatter formatter = new BinaryFormatter();
                FileStream serializationStream = new FileStream( "EnvironmentProperties.dat", FileMode.Open, FileAccess.Read);
                enviroment = formatter.Deserialize(serializationStream) as Environment;
                this.Average = enviroment.Average;
                this.LampBrightness = enviroment.LampBrightness;
                this.Smoothing = enviroment.Smoothing;
                this.RE1 = enviroment.RE1;
                this.RE2 = enviroment.RE2;
                this.IntegrationTime = enviroment.IntegrationTime;
                serializationStream.Close();
                txtAverage.Text = Convert.ToString(this.Average);
                txtSmoothing.Text = Convert.ToString(this.Smoothing);
                txtLampBrightness.Text = this.LampBrightness.ToString();
                txtIntegrationTime.Text =( this.IntegrationTime/1000).ToString();
                txtRE1.Text = this.RE1.ToString();
                txtRE2.Text = this.RE2.ToString();
               
            }catch
            {
                message("Error in reading environment variables", false);
               
            }

        }
        private void _btn_close_MouseMove(object sender, MouseEventArgs e)
        {
            _btn_close.Image = Spectometer.Properties.Resources.Close_02;
        }

        private void _btn_close_MouseLeave(object sender, EventArgs e)
        {
            _btn_close.Image = Spectometer.Properties.Resources.Close_01;

        }

        private void _brn_max_min_MouseMove(object sender, MouseEventArgs e)
        {
            _btn_max_min.Image = Spectometer.Properties.Resources.Tiles2;

        }

        private void _btn_max_min_MouseLeave(object sender, EventArgs e)
        {
            _btn_max_min.Image = Spectometer.Properties.Resources.Tiles;

        }

        private void _btn_min_MouseMove(object sender, MouseEventArgs e)
        {
            _btn_min.Image = Spectometer.Properties.Resources.Minus2;

        }

        private void _btn_min_MouseLeave(object sender, EventArgs e)
        {
            _btn_min.Image = Spectometer.Properties.Resources.Minus;

        }

        private void _btn_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
            cmbChartType.DataSource= Enum.GetValues(typeof(SeriesChartType));
            timeSpec.Tick += TimeSpec_Tick;
            timeSpec.Interval = 100;
            timeCalibrationCruve.Tick += TimeCalibrationCruve_Tick;
            timeNanoDrop.Tick += TimeNanoDrop_Tick;
            timeSingleWaveLenght.Tick += TimeSingleWaveLenght_Tick;
            //get form maximize
            btnShutter.Image = Properties.Resources.rec__1_;
            
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
            Location = new Point(0, 0);
            LoadEnvironment();
        }

        private void TimeSingleWaveLenght_Tick(object sender, EventArgs e)
        {
         try
            {
                if(SingleWavefrm.isCLose )
                
                    timeSingleWaveLenght.Enabled = false;
                    SingleWavefrm.Wl1 = DataForShow[SingleWavefrm.W1 ];
                    SingleWavefrm.Wl2 = DataForShow[SingleWavefrm.W2];
                    SingleWavefrm.Wl3 = DataForShow[SingleWavefrm.W3];
                

            }catch { }
        }

        private void TimeCalibrationCruve_Tick(object sender, EventArgs e)
        {
            try {
                if (calibrationFrm.IsClose)
                    timeCalibrationCruve.Enabled = false;
                calibrationFrm.Result = DataForShow[calibrationFrm.wave];
            }catch { }
        }

        System.Diagnostics.Stopwatch stopwatchspec = new System.Diagnostics.Stopwatch();
        int [] timepecArrye = new int [6];
     //   FChart fchart = new FChart();
        private void TimeSpec_Tick(object sender, EventArgs e)
        {

            if (timeSpectrumFrm.cLOsefrm  )
               timeSpec.Enabled = false;
          
            timeSpectrumFrm.value = DataForShow;

            
          



        }


        private void _pb_form_icon_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
           //get find active option
            
          
           
        }


        private void button16_Click(object sender, EventArgs e)
        {

            if (Mode == SpectometrMode.Scope)
            {
                dtAnalys .SavePacket(MainData, "Dark.dat");
                message("Dark Saved", true );

               
                 darkData = dtAnalys.ReadSavedPacket("Dark.dat");
            }
            else
            {

            }
        }

        private void _btn_un_pin_Click(object sender, EventArgs e)
        {
            //un pin
            _pnl_right_window.Width = _pnl_windows_pin.Width;
            _pnl_right_window.Padding = new Padding(0);
            _pnl_windows_pin.Visible = true;

        }

        private void button21_Click(object sender, EventArgs e)
        {
            //pin
            _pnl_right_window.Width = 250;
            _pnl_right_window.Padding = new Padding(1);
            _pnl_windows_pin.Visible = false;
        }

        private void _btn_min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void _btn_max_min_Click(object sender, EventArgs e)
        {

           
        }

        private void btnShutter_Click(object sender, EventArgs e)
        {
            if (!shuterIs)
            {
                ComportInterfacecs.ShutterSourceControl  (false );
                btnShutter.Image = Properties.Resources.rec;
                shuterIs = true;
            }else
            {
                ComportInterfacecs.ShutterSourceControl  (true);
                btnShutter.Image = Properties.Resources.rec__1_ ;
                shuterIs = false ;
            }

        }

        private void _btn_opn_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click( sender,  e);
        }

        private void hardwareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FHardware hardwareFrm = new FHardware ();
            hardwareFrm.Show();
        }

        private void softWareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            softWareFrm = new FSoftware();
          
            softWareFrm.Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveEnvironment();
        }
        #region txtChange
        private void txtIntegrationTime_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.IntegrationTime = Convert.ToUInt32   (txtIntegrationTime.Text)*1000;
                if (isRun==true )
                {
                    ComportInterfacecs.PeriodicImageSensorRead (this.IntegrationTime);
                }
            }
            catch
            {
                message("IntegrationTime value not Valid", false);
             
                txtIntegrationTime.Text = "18";
                txtIntegrationTime.Focus();
            }
        }

        private void txtAverage_TextChanged(object sender, EventArgs e)
        {

            if ((this.txtAverage .Text == "") || (this.txtAverage .Text == "0"))
            {
                this.txtAverage .Text = "10";
            }
            if (Convert.ToInt32(this.txtAverage .Text) > 26)
            {
                this.txtAverage .Text = "26";
            }
            this.NumberOfFrame = Convert.ToInt32(this.txtAverage .Text);
            for (int i = this.NumberOfFrame; i < 26; i++)
            {
                Array.Clear(this.AverageData [i], 0, 0x82a);
            }
           
        }

        private void txtSmoothing_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.Smoothing  = Convert.ToUInt16(txtSmoothing .Text);
            }
            catch
            {
               message("Smoothing value not Valid",false );
                txtSmoothing.Text = "";
                txtSmoothing.Focus();
            }

        }

        private void txtRE1_TextChanged(object sender, EventArgs e)
        {
             try
            {
                this.RE1   = Convert.ToUInt16  (txtRE1  .Text);
            }
            catch
            {
               message("RE1 Value not Valid",false );
                txtRE1.Text = "";
                txtRE1.Focus();
            }
        }

        private void txtRE2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.RE2  = Convert.ToUInt16(txtRE2 .Text);
            }
            catch
            {
               message("RE2 value not Valid",false);
                txtRE2 .Text = "";
                txtRE2 .Focus();
            }
        }

        private void txtLampBrightness_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if ((this.txtLampBrightness .Text == "") || (this.txtLampBrightness.Text == "0"))
                {
                    this.txtLampBrightness.Text = "10";
                }
                if (Convert.ToInt32(this.txtLampBrightness.Text) > 100)
                {
                    this.txtLampBrightness .Text = "100";
                }
                if(tangestanIs )
                {
                    ComportInterfacecs.SetIntensityValueOfTungstenLamp(Convert.ToByte(txtLampBrightness.Text));
                }

            }
            catch
            {
              
                txtLampBrightness.Text = "100";
                txtLampBrightness.Focus();
            }
        }
        #endregion

        private void HardwareTimer_Tick(object sender, EventArgs e)
        {
        }

        private void strartNewExpirmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtSeriesTiltle.Text = "WorkSpace1";
          //  this.newExperiment.WorkspaceName.Enabled = this.WorkspaceStatus;
          this.newExperiment.txtExperimentName .Text = "Experiment" + Convert.ToString((int)(this.chart1 .Series.Count + 1));
            this.newExperiment.ShowDialog();
            string text = this.newExperiment.txtExperimentName .Text;
            if (this.chart1 .Series.IsUniqueName(text))
            {
                this.ExperimentName = text;
                this.chart1 .Series.Add(this.ExperimentName);
            }
            else
            {
                switch (MessageBox.Show(this.ExperimentName + " already exists.\nDo you want to replace it?", "New experiment", MessageBoxButtons.YesNo))
                {
                    case DialogResult.Yes:
                        this.ExperimentName = text;
                        this.chart1 .Series[this.ExperimentName].Points.Clear();
                        break;
                }
            }
            this.chart1.Legends["Legend1"].Title = txtSeriesTiltle.Text;
            chart1.Series[ExperimentName].ChartType = (SeriesChartType)cmbChartType.SelectedItem;
            chart1.Series[ExperimentName].BorderWidth = trackBar1.Value;
          
            cmbSeriesName.Items.Add(ExperimentName);

            cmbSeriesName.SelectedItem = ExperimentName;
         
        }

        private void derivations1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void timeSpectrumToolStripMenuItem_Click(object sender, EventArgs e)
        {
        //    FChart fch = new FChart();
         //   fch.Show();
            //MessageBox.Show(chart1.Location.ToString ());
            timeSpectrumFrm = new FTimeSpectrum();
            timeSpectrumFrm.Show();
            timeSpec.Enabled = true;
          
            //chart1.Size = new Size(_pnl_main.Width-50,(int) (_pnl_main.Height / 3.5));
            //chart2.Location = new Point( (int)chart1.Legends[0].Position.X-38, (int)chart1.Legends[0].Position.Y + chart1.Size.Height+40 );
            //chart2.Size = new Size(_pnl_main.Width-105 , (int)(_pnl_main.Height / 3.5));
           
            //chart2.Visible = true;
            //this.chart2.Legends["Legend1"].Title = "Time Spectrum";
         //   this.chart2.Series[0].Points.AddXY((double)0.0, (double)0.0);



        }

        private void nanoDropToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

        private void calibrationCurveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            calibrationFrm = new FCalibrationCruve();
            calibrationFrm .Show();
            timeCalibrationCruve .Enabled = true;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

     

        string[] strArray2 = new string[4];
            strArray2[0] = "Error";
            string[] strArray = strArray2;
          chart1 .MouseWheel += new MouseEventHandler(this.ChartMOuseWhell );
            refrenceData = dtAnalys.ReadSavedPacket("Refrence.dat");
            darkData= dtAnalys.ReadSavedPacket("Dark.dat");
            
            ComportInterfacecs.SettingDataReadyToRead += new EventHandler(SettingDataReceivedEvent);
            strArray = this.ComportInterfacecs.FindConnectedDevices();
            if (strArray[0] == "Error")
            {
               message("Device not found",false );
                btnStart.Enabled = false;
                this.PortConnectionStatus = false;
            }
            else
            {
                int i = ComportInterfacecs.OpenComport(strArray[0]);



                ComportInterfacecs.DataReadyToRead += new EventHandler(DataReceivedEvent);
              
                ComportInterfacecs.GetChipTemperature();
                System.Threading.Thread.Sleep(100);
           
                ComportInterfacecs.ADCProgramInitialValue(0, 0);
                PortConnectionStatus = true;
                ComportInterfacecs.StatusComport();
            }
            this.strartNewExpirmentToolStripMenuItem_Click(sender ,e);
            Mode = SpectometrMode.Scope;
            LoadSofwareProperties();
            chart1.Dock = DockStyle.Fill ;
         
        
            this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum = _ScopeX1;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum = _ScopeX2;
            
            this.chart1.ChartAreas["ChartArea1"].AxisX.Interval = 50.0;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Minimum = _ScopeY1  ; ;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum = _ScopeY2; ;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Interval = 10000.0;
            this.chart1.Series[this.ExperimentName].Points.AddXY((double)0.0, (double)0.0);
            this.chart1.ChartAreas["ChartArea1"].AxisX.Title = "WaveLenght(nm)";
            this.chart1.ChartAreas["ChartArea1"].AxisY.Title = "Intensity      ";
            this.chart2.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            this.chart2.ChartAreas["ChartArea1"].AxisX.Maximum = 120;

            this.chart2.ChartAreas["ChartArea1"].AxisX.Interval = 5.0;
            this.chart2.ChartAreas["ChartArea1"].AxisY.Minimum = 0 ; 
            this.chart2.ChartAreas["ChartArea1"].AxisY.Maximum = 1000; 

            this.chart2.ChartAreas["ChartArea1"].AxisY.Interval = 80.0;
          //  this.chart2.Series[this.ExperimentName].Points.AddXY((double)0.0, (double)0.0);
            this.chart2.ChartAreas["ChartArea1"].AxisX.Title = "WaveLenght(nm)";
            this.chart2.ChartAreas["ChartArea1"].AxisY.Title = "Intensity      ";
            this.chart1.ChartAreas["ChartArea1"].AxisX.IsLabelAutoFit = true;
            this.chart1.ChartAreas["ChartArea1"].AxisY.IsLabelAutoFit = true;
            this.chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = true;
            this.chart1.ChartAreas["ChartArea1"].AxisY.LabelStyle.Enabled = true;
            this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = true;
            this.chart1.ChartAreas["ChartArea1"].CursorX.AutoScroll = true;
            this.chart1.ChartAreas["ChartArea1"].CursorY.AutoScroll = true;
            this.chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true ;
             this.chart1.ChartAreas["ChartArea1"].CursorX .IsUserEnabled = true;

             this.chart1.ChartAreas["ChartArea1"].CursorY.IsUserSelectionEnabled  = true;
            this.chart1.ChartAreas["ChartArea1"].CursorY.IsUserEnabled = true;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(192,192,192);
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(192, 192, 192);
            cmbChartType.SelectedIndex = cmbChartType.FindStringExact("Line");
          



        }

        private void ChartMOuseWhell(object sender, MouseEventArgs e)
        {
            try
            {
                double viewMinimum;
                double viewMaximum;
                double num3;
                double num4;
                if (e.Delta < 0)
                {
                    viewMinimum = this.chart1 .ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                    viewMaximum = this.chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                    num3 = this.chart1.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                    num4 = this.chart1.ChartAreas[0].AxisY.ScaleView.ViewMaximum;
                    if (!((viewMinimum > 300.0) | (viewMaximum < 1000.0)))
                    {
                        this.chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                        this.chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                    }
                    this.chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                    this.chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                }
                if (e.Delta > 0)
                {
                    viewMinimum = this.chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                    viewMaximum = this.chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                    num3 = this.chart1.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                    num4 = this.chart1.ChartAreas[0].AxisY.ScaleView.ViewMaximum;
                    double num5 = this.chart1.ChartAreas[0].AxisX.PixelPositionToValue((double)e.Location.X) - ((viewMaximum - viewMinimum) / 2.0);
                    double num6 = this.chart1.ChartAreas[0].AxisX.PixelPositionToValue((double)e.Location.X) + ((viewMaximum - viewMinimum) / 2.0);
                    double num7 = this.chart1.ChartAreas[0].AxisY.PixelPositionToValue((double)e.Location.Y) - ((num4 - num3) / 2.0);
                    double num8 = this.chart1.ChartAreas[0].AxisY.PixelPositionToValue((double)e.Location.Y) + ((num4 - num3) / 2.0);
                    this.chart1.ChartAreas[0].AxisX.ScaleView.Zoom(Math.Round(num5, 0), Math.Round(num6, 0));
                    this.chart1.ChartAreas[0].AxisY.ScaleView.Zoom(Math.Round(num7, 0), Math.Round(num8, 0));
                }
            }
            catch
            {
            }
        }

        private void DataReceivedEvent(object sender, EventArgs arg)
        {
            try

            {
              
                MainData = ((ComportDataReceiveEventArgs)arg).GetReceivedData();
                base.Invoke(new EventHandler(ShowData));

            }
            catch {  }

        }
        float[] numSmoothing = new float[2090];
        private void ShowData(object sender, EventArgs e)
        {
            if (softWareFrm.Issave )
            {
                if (Mode==SpectometrMode.Absorbance )
                btnAbsorbance_Click(sender, e);
                if (Mode == SpectometrMode.Scope )
                    btnScope_Click(sender, e);
                if (Mode == SpectometrMode.Reflectance )
                    btnRefrence_Click(sender, e);
                if (Mode == SpectometrMode.Transmittance)
                    btnTransmittance_Click(sender, e);
                if (Mode == SpectometrMode.Irradiance)
                    btnIrradiance_Click(sender, e);
                if (Mode == SpectometrMode.Raman)
                    ramanToolStripMenuItem_Click(sender, e);
                    
            }
            LoadSofwareProperties();
            if (wait == true)
                return;
        
          
            int num = Convert.ToInt32(this.txtAverage .Text);
            if (this.NumberOfFrame < num)
            {
                this.NumberOfFrame++;
            }
            else
            {
                this.NumberOfFrame = 1;
            }
            Array.Copy(this.MainData , this.AverageData [this.NumberOfFrame - 1], 0x82a);
            float[] numArray =  (from i in Enumerable.Range(0, this.AverageData [this.NumberOfFrame - 1].Length) select this.AverageData .Sum<float[]>((Func<float[], float>)(a => a[i]))).ToArray<float>();

            float[] xvalue =new float[2090];
            for (int j = 0; j < 2090; j++)
            {
              
                numArray[j] /= (float)num;
                xvalue[j] = Convert.ToSingle((_XmapC3 * Math.Pow(j, 3))+(_XmapC2*(Math.Pow(j,2))+(_XmapC1*j) )+_XmapI );

            }

         
            for ( int k =0;k<numArray.Length-Smoothing ;k++)
            {
                float temp=0;
                for (int i = 0; i < Smoothing; i++)
                {
                    temp += numArray[k + i];
                }
                numSmoothing[k]  = temp  / Smoothing ;
              //  numSmoothing[k] = (numArray[k - 2] + ( 2*numArray[k-1]) +( 3*numArray[k])+(2*numArray[k+1])+numArray[k+2]) / 9;
            }
            if (Mode == SpectometrMode.Scope)
            {
                DataForShow = numSmoothing ;  
                chart1.Series[ExperimentName].Points.DataBindXY (xvalue   , DataForShow  );
            }
            if(Mode==SpectometrMode.Transmittance )
            {
              
              
                DataForShow = dtAnalys.Transmittance(numSmoothing, darkData, refrenceData);
                chart1.Series[ExperimentName].Points.DataBindXY (xvalue ,DataForShow  );


            }
           
            if (Mode == SpectometrMode.Reflectance )
            {


                 DataForShow = dtAnalys.Transmittance(numSmoothing, darkData, refrenceData);
                chart1.Series[ExperimentName].Points.DataBindXY(xvalue, DataForShow);

            }
            if (Mode == SpectometrMode.Absorbance )
            {

               

                DataForShow= dtAnalys.Absorbance (numSmoothing, darkData, refrenceData);
                chart1.Series[ExperimentName].Points.DataBindXY (xvalue, DataForShow );

            }
            if (Mode == SpectometrMode.ND1 || Mode == SpectometrMode.ND2|| Mode == SpectometrMode.ND3|| Mode == SpectometrMode.ND4)
            {

                chart1.Series[ExperimentName].Points.DataBindXY(xvalue, ND1(xvalue));
            }
            if (Mode==SpectometrMode.Raman )
            {
               DataForShow = dtAnalys.RamanData(xvalue);
                chart1.Series[ExperimentName].Points.DataBindXY(DataForShow  ,numSmoothing    );

             //   label8.Text = DataForShow .Min().ToString()+":" + DataForShow .Max ().ToString() + ":";
            }
            if (Mode==SpectometrMode.Irradiance )
            {
                DataForShow = dtAnalys.Refractive(xvalue, refrenceData);
                chart1.Series[ExperimentName].Points.DataBindXY(xvalue, DataForShow);
            }

        }
        private double  [] ND1 (float [] xval)
        {
            double [] Nd1 = new double [2090];
            int k=1;
            float[] abs = DataForShow = dtAnalys.Absorbance(numSmoothing, darkData, refrenceData);
            if (Mode == SpectometrMode.ND1)
                k = 1;
            if (Mode == SpectometrMode.ND2 )
                k = 2;
            if (Mode == SpectometrMode.ND3 )
                k = 3;
            if (Mode == SpectometrMode.ND4)
                k = 4;

            for (int i=0;i<2090;i++)
            {
                Nd1[i] =Math.Pow((double ) abs[i] / xval[i],k); 
            }
            return Nd1;
        }
        private void SettingDataReceivedEvent(object sender, EventArgs e)
        {
            try {
                this.SettingData = ((ComportSettingDataReceiveEventArgs)e).GetReceivedData();

                if (this.SettingData[0] == 3)
                {
                    base.Invoke(new EventHandler(this.ChipTemperatureDisplay));
                }

                if (this.SettingData[0] == 5)
                {
                    base.Invoke(new EventHandler(DisplaySerialNumer));

                }
                if (this.SettingData[0]==7)
                {
                    base.Invoke(new EventHandler(DiplayTangestanValue));
                }
                if(SettingData[0]==6)
                {
                    base.Invoke(new EventHandler(DisplayCounterValues));
                }
            }catch { }
        }

        private void DisplayCounterValues(object sender, EventArgs e)
        {
            ComportInterfacecs.StopReadingImageSensor();
            int[] numArray = new int[4];
            for (int i=0;i<16; i++)
            {
                CounterValue[i] = SettingData[i + 2];

            }
           
          
            deviceInfo.lblDevice.Text  = "";
            deviceInfo.lblTangestan.Text = "";
            deviceInfo.lblRes.Text = "";
            deviceInfo.lblDeuterium.Text = "";
           
            double  n1 = ((CounterValue[0] + (CounterValue[1]*256) +( CounterValue[2]*(2*256)) + (CounterValue[3]*(256*3))) * 0.2);
            double n2= ((CounterValue[4] + (CounterValue[5] * 256) + (CounterValue[6] * (2 * 256)) + (CounterValue[7] * (256 * 3)) ) * 0.2) ;
            double n3= ((CounterValue[8] + (CounterValue[9] * 256) + (CounterValue[10] * (2 * 256)) + (CounterValue[11] * (256 * 3)) * 0.2)) ;
          
            TimeSpan time = TimeSpan.FromSeconds(n1);
            TimeSpan time2 = TimeSpan.FromSeconds(n2);
            TimeSpan time3 = TimeSpan.FromSeconds(n3);
       
            deviceInfo.lblDevice.Text = time.ToString(@"d'd 'hh\:mm\:ss");
            deviceInfo.lblTangestan.Text = time2.ToString(@"d'd 'hh\:mm\:ss");
            deviceInfo.lblDeuterium.Text = time3 .ToString(@"d'd 'hh\:mm\:ss");
        
        }

        private void DiplayTangestanValue(object sender, EventArgs e)
        {
            Tangestanval[0] = SettingData[3];         
        }

        private void ChipTemperatureDisplay(object sender, EventArgs e)
        {
            this.ChipTemperatureVal[0] = this.SettingData[2];
           lblTem.Text = "Temp:"+ChipTemperatureVal[0].ToString()+ "\u2103";
        }

        private void DisplaySerialNumer(object sender, EventArgs e)
        {
        
            for (int i = 0; i < 28; i++)
            {
                this.ReadSerialnumber[i] = this.SettingData[i + 2];
            
            }
            this.deviceInfo .F2Date.Text = "";
            this.deviceInfo.F2Date.Text = this.deviceInfo.F2Date.Text + Convert.ToChar(this.ReadSerialnumber[0]);
            this.deviceInfo.F2Date.Text = this.deviceInfo.F2Date.Text + Convert.ToChar(this.ReadSerialnumber[1]);
            this.deviceInfo.F2Date.Text = this.deviceInfo.F2Date.Text + Convert.ToChar(this.ReadSerialnumber[2]);
            this.deviceInfo.F2Date.Text = this.deviceInfo.F2Date.Text + Convert.ToChar(this.ReadSerialnumber[3]);
            this.deviceInfo.F2Date.Text = this.deviceInfo.F2Date.Text + "/";
            this.deviceInfo.F2Date.Text = this.deviceInfo.F2Date.Text + Convert.ToChar(this.ReadSerialnumber[4]);
            this.deviceInfo.F2Date.Text = this.deviceInfo.F2Date.Text + Convert.ToChar(this.ReadSerialnumber[5]);
            this.deviceInfo.F2Date.Text = this.deviceInfo.F2Date.Text + "/";
            this.deviceInfo.F2Date.Text = this.deviceInfo.F2Date.Text + Convert.ToChar(this.ReadSerialnumber[6]);
            this.deviceInfo.F2Date.Text = this.deviceInfo.F2Date.Text + Convert.ToChar(this.ReadSerialnumber[7]);
            this.deviceInfo.VerInfo.Text = "";
            this.deviceInfo.VerInfo.Text = this.deviceInfo.VerInfo.Text + Convert.ToChar(this.ReadSerialnumber[8]);
            this.deviceInfo.VerInfo.Text = this.deviceInfo.VerInfo.Text + Convert.ToChar(this.ReadSerialnumber[9]);
            this.deviceInfo.VerInfo.Text = this.deviceInfo.VerInfo.Text + Convert.ToChar(this.ReadSerialnumber[10]);
            this.deviceInfo.VerInfo.Text = this.deviceInfo.VerInfo.Text + Convert.ToChar(this.ReadSerialnumber[11]);
            this.deviceInfo.Serial.Text = "";
            this.deviceInfo.Serial.Text = this.deviceInfo.Serial.Text + Convert.ToChar(this.ReadSerialnumber[12]);
            this.deviceInfo.Serial.Text = this.deviceInfo.Serial.Text + Convert.ToChar(this.ReadSerialnumber[13]);
            this.deviceInfo.Serial.Text = this.deviceInfo.Serial.Text + Convert.ToChar(this.ReadSerialnumber[14]);
            this.deviceInfo.Serial.Text = this.deviceInfo.Serial.Text + Convert.ToChar(this.ReadSerialnumber[15]);
            this.deviceInfo.Serial.Text = this.deviceInfo.Serial.Text + Convert.ToChar(this.ReadSerialnumber[16]);
            this.deviceInfo.Serial.Text = this.deviceInfo.Serial.Text + Convert.ToChar(this.ReadSerialnumber[17]);
            this.deviceInfo.Serial.Text = this.deviceInfo.Serial.Text + Convert.ToChar(this.ReadSerialnumber[18]);
            this.deviceInfo.Serial.Text = this.deviceInfo.Serial.Text + Convert.ToChar(this.ReadSerialnumber[19]);
            this.deviceInfo.Serial.Text = this.deviceInfo.Serial.Text + Convert.ToChar(this.ReadSerialnumber[20]);
            this.deviceInfo.Serial.Text = this.deviceInfo.Serial.Text + Convert.ToChar(this.ReadSerialnumber[21]);
            this.deviceInfo.Serial.Text = this.deviceInfo.Serial.Text + Convert.ToChar(this.ReadSerialnumber[22]);
            this.deviceInfo.Serial.Text = this.deviceInfo.Serial.Text + Convert.ToChar(this.ReadSerialnumber[23]);
            this.deviceInfo.Res.Text = "";
            this.deviceInfo.Res.Text = this.deviceInfo.Res.Text + Convert.ToChar(this.ReadSerialnumber[24]);
            this.deviceInfo.Res.Text = this.deviceInfo.Res.Text + Convert.ToChar(this.ReadSerialnumber[25]);
            this.deviceInfo.Res.Text = this.deviceInfo.Res.Text + Convert.ToChar(this.ReadSerialnumber[26]);
            this.deviceInfo.Res.Text = this.deviceInfo.Res.Text + Convert.ToChar(this.ReadSerialnumber[27]);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (isRun == false)
            {
                ComportInterfacecs.PeriodicImageSensorRead(this.IntegrationTime);
                isRun = true;
                btnStart.Image = Properties.Resources.stop;
              btnStart.Text = "Stop";
            }else
            {
                isRun = false;
                btnStart.Image = Properties.Resources.Media_Play2;
               btnStart.Text = "Start";
                ComportInterfacecs.StopReadingImageSensor ();
                ComportInterfacecs.DiscardBufferPort();
                ComportInterfacecs.StopRead = true;

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            ComportInterfacecs.AllLightSourceControl( true  );
          //  ComportInterfacecs.TungstenLightSourceControl(true);

            ComportInterfacecs.DiscardBufferPort();
        }
       
        private void cmbChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (Series series in chart1.Series)
                {
                    series.ChartType = (SeriesChartType)cmbChartType.SelectedItem; 
                }
          
            }
            catch { }
        }
     
        private void colorPicker1_Click(object sender, BlackBeltCoder.ColorPickerEventArgs e)
        {
            chart1.Series[ExperimentName  ].Color = colorPicker1.Value;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            chart1.Series[ExperimentName ].BorderWidth = trackBar1.Value;
        }

        private void txtSeriesTiltle_TextChanged(object sender, EventArgs e)
        {
            chart1.Titles.Clear();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            title1.Text = txtSeriesTiltle.Text;
            chart1.Titles.Add(title1);
          
        }

        private void cmbChartType_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cmbSeriesName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSeriesName.Items.Count>1)
                ExperimentName = cmbSeriesName.SelectedItem.ToString();
            }
            catch { }
        }

        private void cmbSeriesName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (cmbSeriesName.Items.Count > 0)

                    cmbSeriesName.Items.Remove(cmbSeriesName.SelectedItem);
            }
        }

        private void chHide_CheckedChanged(object sender, EventArgs e)
        {
            if (chHide.Checked )
            {
                chart1.Series[ExperimentName].Enabled = false;
            }else
                chart1.Series[ExperimentName].Enabled = true;
        }
        private Point mousePoint;

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                chart1.ChartAreas[0].Area3DStyle.Enable3D = ch3D.Checked;
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (mousePoint.IsEmpty)
                        mousePoint = e.Location;
                    else
                    {

                        int newy = chart1.ChartAreas[0].Area3DStyle.Rotation + (e.Location.X - mousePoint.X);
                        if (newy < -180)
                            newy = -180;
                        if (newy > 180)
                            newy = 180;

                        chart1.ChartAreas[0].Area3DStyle.Rotation = newy;

                        newy = chart1.ChartAreas[0].Area3DStyle.Inclination + (e.Location.Y - mousePoint.Y);
                        if (newy < -90)
                            newy = -90;
                        if (newy > 90)
                            newy = 90;

                        chart1.ChartAreas[0].Area3DStyle.Inclination = newy;

                        mousePoint = e.Location;
                    }
                }
                Point location = e.Location;
                if (!this.prevPosition.HasValue || (location != this.prevPosition.Value))
                {
                    this.tooltip.RemoveAll();
                    this.prevPosition = new Point?(location);
                    HitTestResult[] resultArray = this.chart1.HitTest(location.X, location.Y, false, new ChartElementType[] { ChartElementType.DataPoint });
                    foreach (HitTestResult result in resultArray)
                    {
                        if (result.ChartElementType == ChartElementType.DataPoint)
                        {
                            DataPoint point2 = result.Object as DataPoint;
                            if (point2 != null)
                            {
                                double num = result.ChartArea.AxisX.ValueToPixelPosition(point2.XValue);
                                double num2 = result.ChartArea.AxisY.ValueToPixelPosition(point2.YValues[0]);
                                if ((Math.Abs((double)(location.X - num)) < 2.0) && (Math.Abs((double)(location.Y - num2)) < 2.0))

                                    this.tooltip.Show(string.Concat(new object[] { "X=", point2.XValue, ", Y=", point2.YValues[0] }), this.chart1, location.X, location.Y - 15);
                                lblposition.Text = "X=" + point2.XValue.ToString("N4") +" , "+ "Y=" + point2.YValues[0].ToString("N4");


                            }
                        }
                    }
                }
            }
            catch { }

        }

        private void message(string txt,bool i)
        {
            New message = new New();
            if(i)
            { 
            message.pictureBox1.Image = Properties.Resources.ok;
           
             }
            else if(!i)
            {
                message.pictureBox1.Image = Properties.Resources.alert;
            }
            message.label1.Text = txt;
            message.Show();
        }
        private void btnRefrence_Click(object sender, EventArgs e)
        {
            if (Mode == SpectometrMode.Scope )
            {
                dtAnalys  .SavePacket(MainData, "Refrence.dat");
                //MessageBox.Show("Refrence Saved");
                //New message = new New();
                //message.pictureBox1.Image = Properties.Resources.ok;
                //message.label1.Text  = "Refrence Saved";
                //message.Show();
                message("Refrence Saved", true);
                
                 refrenceData = dtAnalys.ReadSavedPacket("Refrence.dat");
            }
            else
            {
                message("Refrence must be in Scope Mode", false );
            }
        } 

        private void btnScope_Click(object sender, EventArgs e)
        {
            foreach (Control item in _pnl_options.Controls)
            {
                item.ForeColor = Color.Gray;

            }
            foreach (ToolStripMenuItem item in viewToolStripMenuItem.DropDownItems)
                item.Checked = false;
            scopeModeToolStripMenuItem.Checked = true;
            //set enable option 
            btnScope .ForeColor = Color.White;
            scopeModeToolStripMenuItem.Checked = true;
            Mode = SpectometrMode.Scope ;
            LoadSofwareProperties();
            this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum = _ScopeX1;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum = _ScopeX2;
                ;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Interval = 50.0;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Minimum = _ScopeY1; ;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum = _ScopeY2; ;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Interval = 10000.0;
            this.chart1.Series[this.ExperimentName].Points.AddXY((double)0.0, (double)0.0);
            this.chart1.ChartAreas["ChartArea1"].AxisX.Title = "WaveLenght(nm)";
            this.chart1.ChartAreas["ChartArea1"].AxisY.Title = "Intensity      ";
            label8.Text = Mode.ToString();


        }

        private void btnAbsorbance_Click(object sender, EventArgs e)
        {
            foreach (Control item in _pnl_options.Controls)
            {
                item.ForeColor = Color.Gray;

            }
            foreach (ToolStripMenuItem item in viewToolStripMenuItem.DropDownItems)
                item.Checked = false;
            absorbanceModeToolStripMenuItem.Checked = true;
            //set enable option 
            btnAbsorbance .ForeColor = Color.White;
            Mode = SpectometrMode.Absorbance  ;
            LoadSofwareProperties();
            this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum = _AbsorbanceX1; ;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum = _AbsorbanceX2;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Interval = 50.0;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Minimum =  Convert.ToDouble( _AbsorbanceY1.ToString("N2"));
            this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum = _AbsorbanceY2;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Interval = 0.5;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Title = "Pixel";
            this.chart1.ChartAreas["ChartArea1"].AxisY.Title = "Intensity      ";
            label8.Text = Mode.ToString();
        }

        private void btnTransmittance_Click(object sender, EventArgs e)
        {
            foreach (Control item in _pnl_options.Controls)
            {
                item.ForeColor = Color.Gray;

            }
            foreach (ToolStripMenuItem item in viewToolStripMenuItem.DropDownItems)
                item.Checked = false;
            transmittanceModeToolStripMenuItem .Checked = true;
            //set enable option 
            btnTransmittance. ForeColor = Color.White;

            Mode = SpectometrMode.Transmittance ;
            LoadSofwareProperties();
            this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum = _TransmittanceX1; ;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum = _TransmittanceX2 ;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Interval = 100.0;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Minimum = _TransmittanceY1;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum = _TransmittanceY2 ;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Interval = 5;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Title = "Pixel";
            this.chart1.ChartAreas["ChartArea1"].AxisY.Title = "Intensity      ";
            label8.Text = Mode.ToString();
        }

        private void btnIrradiance_Click(object sender, EventArgs e)
        {

            foreach (Control item in _pnl_options.Controls)
            {
                item.ForeColor = Color.Gray;

            }
            //set enable option 

            foreach (ToolStripMenuItem item in viewToolStripMenuItem.DropDownItems)
                item.Checked = false;
            ReflectanceModetoolStripMenuItem2 .Checked = true;
            btnReflectance .ForeColor = Color.White;
            Mode = SpectometrMode.Reflectance  ;
            LoadSofwareProperties();
            this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum = _TransmittanceX1; ;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum = _TransmittanceX2;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Interval = 100.0;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Minimum = _TransmittanceY1;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum = _TransmittanceY2;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Interval = 5;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Title = "Pixel";
            this.chart1.ChartAreas["ChartArea1"].AxisY.Title = "Intensity      ";
            label8.Text = Mode.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (chart1.Series.Count > 0)
            {
                Series seriesDelete = chart1.Series[cmbSeriesName.Text];
                chart1.Series.Remove(seriesDelete);
                cmbSeriesName.Items.Remove(cmbSeriesName.SelectedItem);
                cmbSeriesName.Items.Clear();
                cmbSeriesName.Text = "";
                foreach (Series s in chart1.Series)
                    cmbSeriesName.Items.Add(s.Name);

                if (cmbSeriesName.Items.Count > 0)
                {
                    cmbSeriesName.SelectedItem = cmbSeriesName.Items[cmbSeriesName.Items.Count - 1];
                    ExperimentName = cmbSeriesName.Items[cmbSeriesName.Items.Count - 1].ToString();
                }
            }

        }

        private void btnlamp_Click(object sender, EventArgs e)
        {
            if (!tangestanIs )
            {
                ComportInterfacecs.TungstenLightSourceControl(false );
             //   ComportInterfacecs.DeuteriumLightSourceControl(true);
             

                ComportInterfacecs.SetIntensityValueOfTungstenLamp(Convert.ToByte  (txtLampBrightness.Text));
               // ComportInterfacecs.DecrementTungstenLampIntensity();
             
               // ComportInterfacecs.GetIntensityValueOfTungstenLamp();
               
                btnlamp .Image = Properties.Resources.rec;
                tangestanIs = true;
            }
            else
            {
              //  ComportInterfacecs.DeuteriumLightSourceControl(false);
                ComportInterfacecs.TungstenLightSourceControl(true);
             
                btnlamp .Image = Properties.Resources.rec__1_;
                tangestanIs = false;
            }
            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK )
            {

                chart1.Serializer.Format = System.Windows.Forms.DataVisualization.Charting.SerializationFormat.Binary;
                chart1.Serializer.Save(saveFileDialog1.FileName+".Spec" );
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                chart1.Serializer.Format = System.Windows.Forms.DataVisualization.Charting.SerializationFormat.Binary;
                chart1.Serializer.Load(openFileDialog1.FileName );
                chart1.Serializer.IsResetWhenLoading = false;
                cmbSeriesName.Items.Clear();
                cmbSeriesName.Text = "";
                foreach (Series s in chart1.Series)
                    cmbSeriesName.Items.Add(s.Name);
                cmbSeriesName.SelectedItem = cmbSeriesName.Items[cmbSeriesName.Items.Count - 1];
            }


        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog  svpic = new SaveFileDialog();
            svpic.Filter = "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png|Tiff Image (.tiff)|*.tiff|Wmf Image (.wmf)|*.wmf";
            if (svpic.ShowDialog() == DialogResult.OK)
            {
                string tempFile = System.IO.Path.GetTempPath() + "Temp";
                string a = "Integration time :" + txtIntegrationTime.Text + "\t\t" + "Average :" + txtAverage.Text + "\t\t" + "Smoothing :" + txtSmoothing.Text+ "\t\t" + Mode.ToString();
                PointF firstLocation = new PointF(10f, 10f);

                chart1.SaveImage(tempFile, ChartImageFormat.Jpeg);
                Bitmap bitmap = (Bitmap)Image.FromFile(tempFile);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Font arialFont = new Font("Arial", 10, FontStyle.Bold))
                    {
                        graphics.DrawString(a, arialFont, Brushes.Red, firstLocation);
                     
                    }
                }

                bitmap.Save(svpic.FileName );
            }


        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDialog  ppd = new PrintDialog ();
            this.chart1.Printing.Print(false);
           ppd.Document   = this.chart1.Printing.PrintDocument;
            ppd.ShowDialog();

        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog ppd = new PrintPreviewDialog();
            ppd.Document = this.chart1.Printing.PrintDocument;
            ppd.ShowDialog();
        }
        bool wait = false;
        private String Number2String(int number)
        {
            Char c = (Char)(( 97) + (number - 1));
            return c.ToString();
        }
        private void ExportToExcel()
        {
           
            
            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            Microsoft.Office.Interop.Excel.ChartObjects xlCharts = (Microsoft.Office.Interop.Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
            Microsoft.Office.Interop.Excel.ChartObject myChart = (Microsoft.Office.Interop.Excel.ChartObject)xlCharts.Add(500, 100, 500, 500);



            Microsoft.Office.Interop.Excel.Chart chartPage = myChart.Chart;
            Microsoft.Office.Interop.Excel.SeriesCollection ser = chartPage.SeriesCollection();
            chartPage.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine;
            int SeresNmae = 0;
            foreach (var item in chart1.Series)
            {
                SeresNmae++;
                xlWorkSheet.Cells[(SeresNmae*2)-1] = item.Name;
                int  i = 2;
                if (item.Name == ExperimentName)
                    wait = true;
                    for (int j = 0; j < item.Points.Count - 1; j++)
                {
                    

                    xlWorkSheet.Cells[i, (SeresNmae*2)-1 ] = item.Points[j].YValues ;
                    xlWorkSheet.Cells[i, SeresNmae*2] = item.Points[j].XValue;
                    i++;

                }

                Microsoft.Office.Interop.Excel.Series series1 = ser.NewSeries();
                series1.Name = item.Name;
                string colY = Number2String((SeresNmae * 2) - 1)+"2"+":"+Number2String((SeresNmae * 2) - 1)+item.Points.Count.ToString() ;
                string colX = Number2String((SeresNmae * 2) ) + "2" +":"+ Number2String((SeresNmae * 2) ) + item.Points.Count.ToString();
                series1.Values = xlWorkSheet.get_Range(colY  );
                series1.XValues = xlWorkSheet.get_Range(colX  );
                wait = false;

            }



            Microsoft.Office.Interop.Excel.Range chartRange;



            string col1 = "1:" + chart1.Series[0].Points.Count;
            string col2 = chart1.Series.Count + ":" + chart1.Series[chart1.Series.Count-1].Points.Count ;

         
          //  chartRange = xlWorkSheet.get_Range(col1 ,col2 );
            //  chartPage.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine;
        //    chartPage.SetSourceData(chartRange, misValue );

           

            xlWorkBook.SaveAs(saveExcel .FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
            message("Excel file created ", true);
          


        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                message("Exception Occured while releasing object ", false );
              
            }
            finally
            {
                GC.Collect();
            }
        }
        SaveFileDialog saveExcel = new SaveFileDialog();
        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {

           

            saveExcel.Title = "Export to Excel";
            saveExcel.Filter = "Excel File|*.xls";
            saveExcel.Tag = "Scpectomer Analiz";
            saveExcel.ShowDialog();

            Thread t = new Thread(ExportToExcel);
            t.Start();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            txtLampBrightness.Text =(Convert.ToInt16 (txtLampBrightness.Text )- 1).ToString();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            txtLampBrightness.Text = (Convert.ToInt16(txtLampBrightness.Text) + 1).ToString();
        }

        private void contentToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
            ComportInterfacecs.GetDeviceDateVersionSerialNum();
            Thread.Sleep(100);
            ComportInterfacecs.GetDeviceTimers();
          
          
            deviceInfo.ShowDialog();
          //  ComportInterfacecs.GetIntensityValueOfTungstenLamp();




        }

     
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(isRun )
            {
                btnStart.Image = Properties.Resources.Media_Play2;
                  btnStart.Text = "Start";
                isRun = false;
                ComportInterfacecs.StopReadingImageSensor();
            }
            this.txtAverage .Text = "1";
            this.ComportInterfacecs .OneTimeImageSensorRead (Convert.ToUInt32(this.txtIntegrationTime .Text)*1000);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ComportInterfacecs.DeuteriumLightSourceControl(false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ComportInterfacecs.DeuteriumLightSourceControl(true);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
          
        }

        private void button3_Click(object sender, EventArgs e)
        {

            foreach (Control item in _pnl_options.Controls)
            {
                item.ForeColor = Color.Gray;

            }
            foreach (ToolStripMenuItem item in viewToolStripMenuItem.DropDownItems)
                item.Checked = false;
            irradianceModeToolStripMenuItem .Checked = true;
            //set enable option 
            btnIrradiance.ForeColor  = Color.White;
            Mode = SpectometrMode.Irradiance;
        }

        private void ramanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mode = SpectometrMode.Raman;
            LoadSofwareProperties();
            this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum = _RamanX1; ;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum = _RamanX2 ;
            
            this.chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1000;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Minimum = _RamanY1; ; ;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum = _RamanY2 ; ;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Interval = 10000;
            this.chart1.Series[this.ExperimentName].Points.AddXY((double)0.0, (double)0.0);
            this.chart1.ChartAreas["ChartArea1"].AxisX.Title = "Raman Shift/cm ─¹";
            this.chart1.ChartAreas["ChartArea1"].AxisY.Title = "Intensity      ";
            label8.Text = Mode.ToString();
           
        }
      
        double  zoomx = 0;
        double zoomY = 0;
        int zoomCOunter = 0;
        private void btnZoomX_Click(object sender, EventArgs e)
        {
            double  step = (chart1.ChartAreas["ChartArea1"].AxisX.Maximum - chart1.ChartAreas["ChartArea1"].AxisX.Minimum)/20;
            if (zoomx>chart1.ChartAreas["ChartArea1"].AxisX.Maximum )
            {
                zoomx-= step;
               // zoomx-= this.chart1.ChartAreas["ChartArea1"].AxisX.
            }
            else
                zoomx += step ;
            this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoom(this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum+ zoomx, this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum  - zoomx);
            zoomCOunter += 1;
        }

        private void btnZomY_Click(object sender, EventArgs e)
        {

            double step = (chart1.ChartAreas["ChartArea1"].AxisY .Maximum - chart1.ChartAreas["ChartArea1"].AxisY.Minimum) / 20;
            if (zoomY > chart1.ChartAreas["ChartArea1"].AxisY.Maximum)
            {
                zoomY -= step;
                // zoomx-= this.chart1.ChartAreas["ChartArea1"].AxisX.
            }
            else
                zoomY += step;
            this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoom(this.chart1.ChartAreas["ChartArea1"].AxisY.Minimum + zoomY, this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum - zoomY);
            zoomCOunter += 1;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
          
            zoomCOunter -= 1;
            if (zoomCOunter < 1)
                zoomCOunter = 0;
         
            
        }

        private void btnZoomXY_Click(object sender, EventArgs e)
        {
            double step = (chart1.ChartAreas["ChartArea1"].AxisX.Maximum - chart1.ChartAreas["ChartArea1"].AxisX.Minimum) / 20;
            if (zoomx > chart1.ChartAreas["ChartArea1"].AxisX.Maximum)
            {
                zoomY -= step;
                // zoomx-= this.chart1.ChartAreas["ChartArea1"].AxisX.
            }
            else
                zoomY += step;
            this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoom(this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum + zoomY, this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum - zoomY);
            double stepy = (chart1.ChartAreas["ChartArea1"].AxisY.Maximum - chart1.ChartAreas["ChartArea1"].AxisY.Minimum) / 20;
            if (zoomx > chart1.ChartAreas["ChartArea1"].AxisY.Maximum)
            {
                zoomx -= stepy;
                // zoomx-= this.chart1.ChartAreas["ChartArea1"].AxisX.
            }
            else
                zoomx += stepy;
            this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoom(this.chart1.ChartAreas["ChartArea1"].AxisY.Minimum + zoomx, this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum - zoomx);
        }

        private void signalWaveLenghtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SingleWavefrm = new FSingleWaveLenght();
            SingleWavefrm.Show();
            timeSingleWaveLenght.Enabled = true;
            timeSingleWaveLenght.Interval = 100;
        }

        private void nanoDropToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            nanoDropFrm = new FNanoDrop();
            nanoDropFrm.Show();
         
            timeNanoDrop.Enabled = true;
            timeNanoDrop.Interval = 100;
          
        }

        private void TimeNanoDrop_Tick(object sender, EventArgs e)
        {
            nanoDropFrm.a230 = DataForShow[230];
            nanoDropFrm.a260 = DataForShow[260];
            nanoDropFrm.a280 = DataForShow[280];
            nanoDropFrm.a320 = DataForShow[320];
            if (nanoDropFrm.IsClose)
                timeNanoDrop.Enabled = false;
        }

        private void chart1_Click(object sender, EventArgs e)
        {
                       
        }

        private void ndDerivationsToolStripMenuItem2_Click(object sender, EventArgs e)
        {

            if (chart1.Series.IsUniqueName("1nd Derivations"))
            {
                ExperimentName = "1nd Derivations";
                this.chart1.Series.Add(ExperimentName);


                cmbSeriesName.Items.Add(ExperimentName);
            }

            ExperimentName = "1nd Derivations";
            cmbSeriesName.SelectedItem = ExperimentName;
                chart1.Series[ExperimentName].ChartType = (SeriesChartType)cmbChartType.SelectedItem;
                chart1.Series[ExperimentName].BorderWidth = trackBar1.Value;

                Mode = SpectometrMode.ND1;  
            

        }

        private void ndDerivationsToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (chart1.Series.IsUniqueName("2nd Derivations"))
            {
                ExperimentName = "2nd Derivations";
                this.chart1.Series.Add(ExperimentName);


                cmbSeriesName.Items.Add(ExperimentName);
            }

            ExperimentName = "2nd Derivations";
            cmbSeriesName.SelectedItem = ExperimentName;
            chart1.Series[ExperimentName].ChartType = (SeriesChartType)cmbChartType.SelectedItem;
            chart1.Series[ExperimentName].BorderWidth = trackBar1.Value;

            Mode = SpectometrMode.ND2;
        }

        private void ndDerivationsToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (chart1.Series.IsUniqueName("3nd Derivations"))
            {
                ExperimentName = "3nd Derivations";
                this.chart1.Series.Add(ExperimentName);


                cmbSeriesName.Items.Add(ExperimentName);
            }

            ExperimentName = "3nd Derivations";
            cmbSeriesName.SelectedItem = ExperimentName;
            chart1.Series[ExperimentName].ChartType = (SeriesChartType)cmbChartType.SelectedItem;
            chart1.Series[ExperimentName].BorderWidth = trackBar1.Value;

            Mode = SpectometrMode.ND3;
        }

        private void ndDerivationsToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (chart1.Series.IsUniqueName("4nd Derivations"))
            {
                ExperimentName = "4nd Derivations";
                this.chart1.Series.Add(ExperimentName);


                cmbSeriesName.Items.Add(ExperimentName);
            }

            ExperimentName = "4nd Derivations";
            cmbSeriesName.SelectedItem = ExperimentName;
            chart1.Series[ExperimentName].ChartType = (SeriesChartType)cmbChartType.SelectedItem;
            chart1.Series[ExperimentName].BorderWidth = trackBar1.Value;

            Mode = SpectometrMode.ND4;
        }

        private void colorMeasurementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FChart    fchart = new FChart  ();
            fchart.Show();
            Series s =fchart. chart1.Series[0];
            ChartArea ca = fchart . chart1.ChartAreas[0];

            ca.AxisX.Minimum = 0;
            ca.AxisY.Minimum = 0;
            ca.AxisX.Maximum = 1;
            ca.AxisY.Maximum = 1;
            ca.AxisX.Interval = 0.1f;
            ca.AxisY.Interval = 0.1f;
            ca.AxisX.LabelStyle.Format = "0.00";
            ca.AxisY.LabelStyle.Format = "0.00";
          fchart.  chart1.Series[0].ChartType = SeriesChartType.Point;
            IFormatter formatter = new BinaryFormatter();
            FileStream serializationStream = new FileStream("CIE.dat", FileMode.Open, FileAccess.Read);
            List<SPoint> sPoints = (List<SPoint>)formatter.Deserialize(serializationStream);
            serializationStream.Close();
            foreach (var sp in sPoints) fchart. chart1.Series[0].Points.Add(SPoint.FromSPoint(sp));
          //  var dps = GetCieColorPoints("E:\\CIExy1931_T2.png", 125000);
          ////  var points = chart1.Series[0].Points;
          //  List<SPoint> sPoints = dps.Cast<DataPoint>()
          //                   .Select(x => SPoint.FromDataPoint(x))
          //                   .ToList();

          //  IFormatter formatter = new BinaryFormatter();
          //  FileStream seryalization = new FileStream("CIE.dat", FileMode.Create, FileAccess.Write);
          //  formatter.Serialize(seryalization, sPoints);
          //  seryalization.Close();


          
            //foreach (var dp in dps)
            //{
                         

            //    s.Points.Add(dp);

            //}

        }

        private void btnFit_Click(object sender, EventArgs e)
        {
            zoomx = 0;
            zoomY = 0;
            chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
            chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
        }

        private void refractiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRefractive refrective = new FRefractive();
            refrective.Show();
        }

        List<DataPoint> GetCieColorPoints(string filename, int count)
        {

            var data = new List<DataPoint>();

            float cr = (float)Math.Sqrt(count);
            using (Bitmap bmp = new Bitmap(filename))
            {
                int sx = (int)(bmp.Width / cr);
                int sy = (int)(bmp.Height / cr);

                float scx = 0.8f / bmp.Width;
                float scy = 0.9f / bmp.Height;

                for (int x = 0; x < bmp.Width; x += sx)
                    for (int y = 0; y < bmp.Height; y += sy)
                    {
                        Color c = bmp.GetPixel(x, y);
                        float b = c.GetBrightness();
                        if (b > 0.01f && b < 0.99f)
                        {
                            var dp = new DataPoint(x * scx, 0.9f - y * scy);
                            dp.Color = c;


                            dp.MarkerColor = dp.Color;
                            dp.MarkerStyle = MarkerStyle.Circle;
                            data.Add(dp);
                        }
                    }
            }


            return data;
        }
    }
}
