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
using Colourful;
using Colourful.Conversion;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using RA_Camera_Core;
using Spectometer.UserControl;

namespace Spectometer
{
    public partial class Form1 : Form
    {

        private EnScixLibrary.EnScix ComportInterfacecs = new EnScixLibrary.EnScix();
        private EnScix EnScix = new EnScix();
        int numPixel;
        private double prevXMax = 0;
        private double prevXMin = 0;
        private double prevYMax = 0;
        private double prevYMin = 0;
        private Point mDown = Point.Empty;
       
        bool PortConnectionStatus = false;
        private byte[] SettingData = new byte[40];
        private float[] MainData, DataForShow;
        private byte[] ChipID = new byte[8];
        private byte[] ChipTemperatureVal = new byte[4];
        private byte[] Tangestanval = new byte[4];
        private byte[] ReadSerialnumber = new byte[28];
        private byte[] CounterValue = new byte[16];
        private bool isRun = false;
        double[] x;
        float[][] AverageData;
        private Thread threadGetData;
        int NumberOfFrame = 1;
        float[] numScop;
        float[] numSmoothing;
        float[] refDt;
        int dataCount = 0;
        string serial;



        /// <summary>
        ///                                             
        /// </summary>
        public Form1()
        {

            InitializeComponent();
            _pnl_form_tools.Hide();
        }

        #region Properties

        private ushort Average;
        private ushort LampBrightness;
        private ushort Smoothing;
        private float RE1;
        private ushort RE2;
        private int BaseLineD;
        private UInt32 IntegrationTime;
        Environment enviroment = new Environment();
        CieData ciedata = new CieData();
        private float[] A, D65, X2, Y2, Z2, X10, Y10, Z10;
        Testcolor testcolor = new Testcolor();
        private float[] Red, Green, Blue, pirple;

        SofwaretProperties SoftwarePr = new SofwaretProperties();
        DataAnalysis dtAnalys = new DataAnalysis();
        FAddNewExperiment newExperiment = new FAddNewExperiment();
        FDeviceInfo deviceInfo = new FDeviceInfo();
        SpectometrMode Mode;

        float[] darkData;
        float[] refrenceData;
        string ExperimentName;
        bool tangestanIs = false;
        bool shuterIs = false;
        private float _AbsorbanceX1 = 0f;
        private float _AbsorbanceX2 = 0f;
        private float _AbsorbanceY1 = 0f;
        private float _AbsorbanceY2 = 0f;
        private float _XmapC1 = 0f;
        private float _XmapC2 = 0f;
        private float _XmapC3 = 0f;
        private float _XmapI = 0f;
        private float _XmapC4 = 0f;
        private float _XmapC5 = 0f;
        private float _IrradianceX1 = 0f;
        private float _IrradianceX2 = 0f;
        private float _IrradianceY1 = 0f;
        private float _IrradianceY2 = 0f;

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
        private int _Baseline = 0;
        private float _ReflectanceX1 = 0;
        private float _ReflectanceX2 = 0;
        private float _ReflectanceY1 = 0;
        private float _ReflectanceY2 = 0;
        private float _FluorescenceX1 = 0;
        private float _FluorescenceX2 = 0;
        private float _FluorescenceY1 = 0;
        private float _FluorescenceY2 = 0;
        private bool _EnableBaseLine = false;
        private Point? prevPosition = null;
        private ToolTip tooltip = new ToolTip();
        private System.Windows.Forms.Timer timeSeries = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timeSeriesD = new System.Windows.Forms.Timer();


        private FTimeSpectrum timeSpectrumFrm;
        private FCalibrationCruve calibrationFrm;
        private System.Windows.Forms.Timer timeNanoDrop = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timeSingleWaveLenght = new System.Windows.Forms.Timer();
        FSingleWaveLenght SingleWavefrm = new FSingleWaveLenght();
        private System.Windows.Forms.Timer timeCalibrationCruve = new System.Windows.Forms.Timer();
        FNanoDrop nanoDropFrm = new FNanoDrop();


        public System.Windows.Forms.Timer timeSpec = new System.Windows.Forms.Timer();
        FSoftware softWareFrm = new FSoftware();
        System.Windows.Forms.Timer BandGapTimet = new System.Windows.Forms.Timer();
        #endregion Properties
        enum SpectometrMode { Scope, Transmittance, Absorbance, Irradiance, Raman, Reflectance, ND1, ND2, ND3, ND4, calc, Fluorescence };
        enum d { Nd1, Nd2, Nd3, Nd4 };
        d de;
        public float[] dt;
        float[] xvalue;

        private void SaveEnvironment()
        {
          
            try
            {

                enviroment.Average = 10;
                enviroment.LampBrightness = 1;
                enviroment.Smoothing = this.Smoothing;
                enviroment.RE1 = this.RE1;
                enviroment.RE2 = this.RE2;
                enviroment.IntegrationTime = this.IntegrationTime;
                enviroment.BaseLineD = this.BaseLineD;
                IFormatter formatter = new BinaryFormatter();
                var filename = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Spectrometer\\EnvironmentProperties.dat");

                FileStream serializationStream = new FileStream(filename, FileMode.Create, FileAccess.Write);
                formatter.Serialize(serializationStream, enviroment);
                serializationStream.Close();


            }
            catch
            {
                message("Error ocured in saving environment data.", false);

            }

        }
        bool _nanoLed;
        bool _Durim;
        private void LoadSofwareProperties()
        {
            try
            {
                var filename = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Spectrometer\\SoftwareSetup.dat");
                
                IFormatter formatter = new BinaryFormatter();
                FileStream serializationStream = new FileStream(filename, FileMode.Open, FileAccess.Read,FileShare.ReadWrite);
                SoftwarePr = formatter.Deserialize(serializationStream) as SofwaretProperties;
                this._AbsorbanceX1 = SoftwarePr.AbsorbanceX1;
                this._AbsorbanceX2 = SoftwarePr.AbsorbanceX2;
                _AbsorbanceY1 = SoftwarePr.AbsorbanceY1;
                _AbsorbanceY2 = SoftwarePr.AbsorbanceY2;
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
                _ScopeY1 = SoftwarePr.ScopeY1;
                _ScopeY2 = SoftwarePr.ScopeY2;


                _XmapC1 = SoftwarePr.XmapC1;
                _XmapC2 = SoftwarePr.XmapC2;
                _XmapC3 = SoftwarePr.XmapC3;
                _XmapC4 = SoftwarePr.XmapC4;
                _XmapC5 = SoftwarePr.XmapC5;
                _XmapI = SoftwarePr.XmapI;
                _YmapC1 = SoftwarePr.YmapC1;
                _YmapC2 = SoftwarePr.YmapC2;
                _YmapC3 = SoftwarePr.YmapC3;
                _YmapC4 = SoftwarePr.YmapC4;
                _YmapC5 = SoftwarePr.YmapC5;
                _YmapC6 = SoftwarePr.YmapC6;
                _YmapC7 = SoftwarePr.YmapC7;
                _YmapC8 = SoftwarePr.YmapC8;
                _YmapI = SoftwarePr.YmapI;
                _RamanX1 = SoftwarePr.RamanX1;
                _RamanX2 = SoftwarePr.RamanX2;
                _RamanY1 = SoftwarePr.RamanY1;
                _Baseline = SoftwarePr.BaseLine;
                _RamanY2 = SoftwarePr.RamanY2;
                _ReflectanceX1 = SoftwarePr.TransmittanceX1;
                _ReflectanceX2 = SoftwarePr.TransmittanceX2;
                _ReflectanceY1 = SoftwarePr.TransmittanceY1;
                _ReflectanceY2 = SoftwarePr.TransmittanceY2;
                _FluorescenceX1 = SoftwarePr.FluorescenceX1;
                _FluorescenceX2 = SoftwarePr.FluorescenceX2;
                _FluorescenceY1 = SoftwarePr.FluorescenceY1;
                _FluorescenceY2 = SoftwarePr.FluorescenceY2;
                _EnableBaseLine = SoftwarePr.EnableBaseLine;
                _Durim = SoftwarePr.Dutrium;
                _nanoLed = SoftwarePr.NanoLed;

                serializationStream.Close();
                serializationStream.Dispose();
            


            }
            catch (Exception ex)
            {
                message(ex.ToString(), false);

            }
        }

        private void LoadEnvironment()
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                var filename = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Spectrometer\\EnvironmentProperties.dat");
             
                FileStream serializationStream = new FileStream(filename, FileMode.Open, FileAccess.Read,FileShare.Read);
                enviroment = formatter.Deserialize(serializationStream) as Environment;
                this.Average = enviroment.Average;
                this.BaseLineD = enviroment.BaseLineD;
                this.LampBrightness = enviroment.LampBrightness;
                this.Smoothing = enviroment.Smoothing;
                this.RE1 = enviroment.RE1;
                this.RE2 = enviroment.RE2;
                this.IntegrationTime = enviroment.IntegrationTime;

                serializationStream.Close();
                serializationStream.Dispose();
                numricalAverage.Value =Convert.ToDecimal(this.Average);
                numrSmosthing.Value  = Convert.ToDecimal(this.Smoothing);
                numricalLampBrightnes.Value = this.LampBrightness;
                numricIntegrationTime.Value = IntegrationTime ;
               

            }
            catch(Exception ex)
            {
               

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
        System.Windows.Forms.Timer TimeCalc = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer ColorTIme = new System.Windows.Forms.Timer();


        private void Form1_Load(object sender, EventArgs e)
        {
           
           
           // cmbChartType.DataSource = ((SeriesChartType[])Enum.GetValues(typeof(SeriesChartType))).OrderBy(p => p.ToString()).ToArray() ;
            var words = cmbChartType.DataSource as List<SeriesChartType>;
            List<SeriesChartType> chartTypes = new List<SeriesChartType>();
            chartTypes.Add(SeriesChartType.Line);
            chartTypes.Add(SeriesChartType.FastLine);
            chartTypes.Add(SeriesChartType.Area);
     
            chartTypes.Add(SeriesChartType.Spline);
            chartTypes.Add(SeriesChartType.SplineArea);
            chartTypes.Add(SeriesChartType.Point);
            chartTypes.Add(SeriesChartType.FastPoint);
            chartTypes.Add(SeriesChartType.Bubble);




            cmbChartType.DataSource = chartTypes;

                     //LoadSofwareProperties();
                     //LoadEnvironment();
            numricalAverage.Value = 10M;
            TimeCalc.Tick += TimeCalc_Tick;
            timeSpec.Tick += TimeSpec_Tick;
            ColorTIme.Tick += ColorTIme_Tick;
            timeSpec.Interval = 100;
            timeCalibrationCruve.Tick += TimeCalibrationCruve_Tick;
            timeNanoDrop.Tick += TimeNanoDrop_Tick;
            timeSeries.Tick += TimeSeries_Tick;
            timeSeriesD.Tick += TimeSeriesD_Tick;
            timeSingleWaveLenght.Tick += TimeSingleWaveLenght_Tick;
            //get form maximize
            btnShutter.Image = Properties.Resources.rec__1_;

            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
            Location = new Point(0, 0);
           
            objGraphics = chart1.CreateGraphics();
            numrSmosthing.TextChanged += NumrSmosthing_TextChanged;
            BandGapTimet.Interval = 1000;
            BandGapTimet.Tick += BandGapTimet_Tick;
            //DirectoryInfo directory = new DirectoryInfo( System.IO.Directory.GetCurrentDirectory());
            //directory.Attributes = FileAttributes.Normal;

            //foreach (FileInfo file in directory.GetFiles())
            //{
            //    file.Attributes = FileAttributes.Normal;
            //}
            //var refFilename = Path.Combine(System.Environment.CurrentDirectory, "Reference.dat");
            //var darkFileName = Path.Combine(System.Environment.CurrentDirectory, "Dark.dat");







        }
        bool DarkRefreceInvalid;
        FBandGap bandgapFrm = new FBandGap();
        private void BandGapTimet_Tick(object sender, EventArgs e)

        {
            if (bandgapFrm.IsRun)
            {
                bandgapFrm.Xvalues = xvalue;
                bandgapFrm.Yvalues = dt;
            }
            else
                BandGapTimet.Enabled = false;
        }

        private void ColorTIme_Tick(object sender, EventArgs e)
        {
            if (!fchart.IsRun)
            {
                ColorTIme.Enabled = false;
                fchart.Dispose();
            }
            int n = 0;
            for (int f = 380; f <= 780; f = f + 5)
            {

                fchart.dt1[n] = findPos(f);
                n++;

            }

        }

        private void NumrSmosthing_TextChanged(object sender, EventArgs e)
        {
            if (numrSmosthing.Value > 100)
                numrSmosthing.Value = 1;
        }

        private void TimeSeriesD_Tick(object sender, EventArgs e)
        {



            chTimeSeries.Checked = false;
            timeSeries.Enabled = false;
            timeSeriesD.Enabled = false;



        }

        private void TimeSingleWaveLenght_Tick(object sender, EventArgs e)
        {
            try
            {
                if (SingleWavefrm.isCLose)

                    timeSingleWaveLenght.Enabled = false;

                if (SingleWavefrm.W1 != 0)
                    SingleWavefrm.Wl1 = findPos(SingleWavefrm.W1); //dt[SingleWavefrm.W1 ];
                if (SingleWavefrm.W2 != 0)
                    SingleWavefrm.Wl2 = findPos(SingleWavefrm.W2);
                if (SingleWavefrm.W3 != 0)
                    SingleWavefrm.Wl3 = findPos(SingleWavefrm.W3);
                if (Mode == SpectometrMode.Scope || Mode == SpectometrMode.Raman || Mode == SpectometrMode.Fluorescence)

                {
                    SingleWavefrm.mode = "Int";
                    SingleWavefrm.format = "N1";
                }
                else if (Mode == SpectometrMode.Absorbance)
                {
                    SingleWavefrm.mode = "Abs  ";
                    SingleWavefrm.format = "N4";
                }
                else if (Mode == SpectometrMode.Transmittance)
                {
                    SingleWavefrm.mode = "% T  ";
                    SingleWavefrm.format = "N2";
                }
                else if (Mode == SpectometrMode.Reflectance || Mode == SpectometrMode.Irradiance)
                {
                    SingleWavefrm.mode = "% R  ";
                    SingleWavefrm.format = "N2";
                }

            }
            catch { }
        }

        private void TimeCalibrationCruve_Tick(object sender, EventArgs e)
        {
            try
            {
                if (calibrationFrm.IsClose)
                    timeCalibrationCruve.Enabled = false;
                calibrationFrm.Result = findPos(calibrationFrm.wave);
                if (Mode == SpectometrMode.Scope || Mode == SpectometrMode.Raman)
                {
                    calibrationFrm.mode = "Int";
                    calibrationFrm.Sprate = "N1";
                    calibrationFrm.label9.Text = "Intensity:";
                }
                else if (Mode == SpectometrMode.Absorbance)
                {
                    calibrationFrm.mode = "Abs(a.u.)  ";
                    calibrationFrm.Sprate = "N4";
                    calibrationFrm.label9.Text = "Absorbance:";
                }
                else if (Mode == SpectometrMode.Transmittance)
                {
                    calibrationFrm.mode = "% T  ";
                    calibrationFrm.Sprate = "N2";
                    calibrationFrm.label9.Text = "Transmittance:";
                }
                else if (Mode == SpectometrMode.Reflectance || Mode == SpectometrMode.Irradiance)
                {
                    calibrationFrm.mode = "% R  ";
                    calibrationFrm.Sprate = "N2";
                    calibrationFrm.label9.Text = "Reflectance:";
                }
                calibrationFrm.dataGridView1.Columns[1].HeaderText = calibrationFrm.mode;

                calibrationFrm.chart1.ChartAreas["ChartArea1"].AxisY.Title = calibrationFrm.mode;
            }
            catch { }
        }

        System.Diagnostics.Stopwatch stopwatchspec = new System.Diagnostics.Stopwatch();
        int[] timepecArrye = new int[6];
        //   FChart fchart = new FChart();
        private void TimeSpec_Tick(object sender, EventArgs e)
        {

            if (timeSpectrumFrm.cLOsefrm)
                timeSpec.Enabled = false;

            timeSpectrumFrm.value = dt;
            timeSpectrumFrm.xvalue = xvalue;
            if (Mode == SpectometrMode.Scope || Mode == SpectometrMode.Raman)
            {

                timeSpectrumFrm.mode = "Int";
                timeSpectrumFrm.format = "N1";
            }
            else if (Mode == SpectometrMode.Absorbance)
            {

                timeSpectrumFrm.mode = "Abs  ";
                timeSpectrumFrm.format = "N4";
            }
            else if (Mode == SpectometrMode.Transmittance)
            {

             timeSpectrumFrm.mode = "% T  ";
                timeSpectrumFrm.format = "N2";
            }
            else if (Mode == SpectometrMode.Reflectance)
            {

                timeSpectrumFrm.mode = "% R  ";
                timeSpectrumFrm.format = "N2";
            }
            else
            {
                timeSpectrumFrm.format = "N2";
            }






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

            if (Mode == SpectometrMode.Scope || Mode == SpectometrMode.Irradiance)
            {
                var filename = @"Dark.dat";
                dtAnalys.SavePacket(numScop, filename);
                message("Dark Saved", true);


                darkData = dtAnalys.ReadSavedPacket(filename);
                DarkRefreceInvalid = true;
            }
            else
            {
                message("Drak must be in Scope Mode and  Irradiance", false);
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
        { //TRUE = OPEN false=close
            if (!shuterIs)
            {
                ComportInterfacecs.ShutterSourceControl(true);
                btnShutter.Image = Properties.Resources.rec;
                shuterIs = true;
            }
            else
            {
                ComportInterfacecs.ShutterSourceControl(false);
                btnShutter.Image = Properties.Resources.rec__1_;
                shuterIs = false;
            }
        }
        int cmbselct = 0;
        private void _btn_opn_Click(object sender, EventArgs e)
        {

           // txtSeriesTiltle.Text = "WorkSpace1";

         

            newExperiment.comboBox1.SelectedItem= newExperiment.comboBox1.Items.IndexOf(cmbselct).ToString();
            newExperiment.comboBox1.Enabled = false;
            ScopSeriesCount++;
            this.newExperiment.txtExperimentName.Text = "Spec" + Convert.ToString((int)ScopSeriesCount);
            this.newExperiment.ShowDialog();
         
          
            this.newExperiment.Focus();

            if (newExperiment.DialogResult == DialogResult.OK)
            {

                IsRunStart = true;
                string text = this.newExperiment.txtExperimentName.Text;
                
                if (this.chart1.Series.IsUniqueName(text))
                {
                    this.ExperimentName = text;
                    this.chart1.Series.Add(this.ExperimentName);
                    COlorSelect++;
                    if (COlorSelect == 12)
                        COlorSelect = 0;
                    chart1.Series[ExperimentName].Color = selectColor(COlorSelect);

                }
                else
                {
                    switch (MessageBox.Show(this.ExperimentName + " already exists.\nDo you want to replace it?", "New experiment", MessageBoxButtons.YesNo))
                    {
                        case DialogResult.Yes:
                            this.ExperimentName = text;
                            this.chart1.Series[this.ExperimentName].Points.Clear();
                            break;
                    }
                }
                if (!btnDelete.Enabled)
                    btnDelete.Enabled = true;
                this.chart1.Legends["Legend1"].Title = txtSeriesTiltle.Text;
                chart1.Series[ExperimentName].ChartType = (SeriesChartType)cmbChartType.SelectedItem;
                chart1.Series[ExperimentName].BorderWidth = trackBar1.Value;
                

                cmbSeriesName.Items.Add(ExperimentName);

                cmbSeriesName.SelectedItem = ExperimentName;

                // chart1.Series[ExperimentName].Color= Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

                // timer1.Start();




            }
            DllInterface.LineScanCamera_SetRedExpousure(lineScanCameraIndex, this.IntegrationTime);
            isRun = false;
            //btnStart.PerformClick();
            //btnStart.Text = "Stop";

        }

        private void hardwareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FHardware hardwareFrm = new FHardware();
            hardwareFrm.Show();
        }

        private void softWareToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveEnvironment();
        }
        #region txtChange
        private void txtIntegrationTime_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    this.IntegrationTime = Convert.ToUInt32   (txtIntegrationTime.Text)*1000;
            //    if (isRun==true )
            //    {
            //        ComportInterfacecs.PeriodicImageSensorRead (this.IntegrationTime);
            //    }
            //}
            //catch
            //{
            //    message("IntegrationTime value not Valid", false);

            //    txtIntegrationTime.Text = "18";
            //    txtIntegrationTime.Focus();
            //}
        }

        private void txtAverage_TextChanged(object sender, EventArgs e)
        {

            //if ((this.txtAverage .Text == "") || (this.txtAverage .Text == "0"))
            //{
            //    this.txtAverage .Text = "10";
            //}
            //if (Convert.ToInt32(this.txtAverage .Text) > 26)
            //{
            //    this.txtAverage .Text = "26";
            //}
            //this.NumberOfFrame = Convert.ToInt32(this.txtAverage .Text);
            //for (int i = this.NumberOfFrame; i < 26; i++)
            //{
            //    Array.Clear(this.AverageData [i], 0, numPixel);
            //}

        }

        //private void txtSmoothing_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Convert.ToInt16(txtSmoothing.Text) > 100 || txtSmoothing.Text=="")
        //            txtSmoothing.Text = "3";
        //        this.Smoothing  = Convert.ToUInt16(txtSmoothing .Text);
        //    }
        //    catch
        //    {
        //       message("Smoothing value not Valid",false );
        //        txtSmoothing.Text = "3";
        //        txtSmoothing.Focus();
        //    }

        //}

        private void txtRE1_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void txtRE2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtLampBrightness_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if ((this.txtLampBrightness .Text == "") || (this.txtLampBrightness.Text == "0"))
            //    {
            //        this.txtLampBrightness.Text = "10";
            //    }
            //    if (Convert.ToInt32(this.txtLampBrightness.Text) > 100)
            //    {
            //        this.txtLampBrightness .Text = "100";
            //    }
            //    if(tangestanIs )
            //    {
            //        ComportInterfacecs.SetIntensityValueOfTungstenLamp(Convert.ToByte(txtLampBrightness.Text));
            //    }

            //}
            //catch
            //{

            //    txtLampBrightness.Text = "100";
            //    txtLampBrightness.Focus();
            //}
        }
        #endregion

        private void HardwareTimer_Tick(object sender, EventArgs e)
        {

        }
        Random rnd = new Random();
        bool IsRunStart = false;
        private void strartNewExpirmentToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void derivations1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void timeSpectrumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //   FChart fch = new FChart();
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
            //  this.chart2.Series[0].Points.AddXY((double)0.0, (double)0.0);



        }

        private void calibrationCurveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            calibrationFrm = new FCalibrationCruve();

            calibrationFrm.Show();
            timeCalibrationCruve.Enabled = true;
        }
        int lineScanCameraIndex = -1;//camera index
        IntPtr serialBuf;
        IntPtr descBuf;
        private bool ConectToDevice()
        {

            return true;
        }
        bool IsConected = false;
     
        private void Conect()
        {
          //  ComportInterfacecs = new EnScixLibrary.EnScix();
            string[] strArray2 = new string[4];
            strArray2[0] = "Error";
            string[] strArray = strArray2;
            ComportInterfacecs.SettingDataReadyToRead += new EventHandler(SettingDataReceivedEvent);
            //ComportInterfacecs.sensorType = 521;
            strArray = this.ComportInterfacecs.FindConnectedDevices();
            if (strArray[0] == "Error")
            {
               
                label9.Text = "Device not connected  ,close the program and conncet USB port to computer";
                btnStart.Enabled = false;
                this.PortConnectionStatus = false;
                IsConected = false;
            }
            else
            {

                int i = ComportInterfacecs.OpenComport(strArray[0]);
                if (i==1)
                {
                    label9.Text = "Device not connected  ,close the program and conncet USB port to computer";
                    return;
                }
               
                label9.Text = "Device  connected"+i.ToString()+"  Com port :"+strArray[0];
                btnStart.Enabled = true;


               
                ComportInterfacecs.DataReadyToRead += new EventHandler(DataReceivedEvent);
                SerialPortService.PortsChanged += SerialPortService_PortsChanged;
                ComportInterfacecs.ADCProgramInitialValue((byte)SoftwarePr.Gain, Convert.ToInt16(SoftwarePr.Offset));

                ComportInterfacecs.GetChipTemperature();
                System.Threading.Thread.Sleep(100);

                // ComportInterfacecs.DeuteriumLightSourceControl(false );
                PortConnectionStatus = true;

                ComportInterfacecs.StatusComport();
                IsConected = true;
                ComportInterfacecs.TungstenLightSourceControl(true);
                ComportInterfacecs.ShutterSourceControl(false);
                ComportInterfacecs.NanoLED5LightSourceControl(false);
                ComportInterfacecs.NanoLED6LightSourceControl(false);
                ComportInterfacecs.NanoLED1LightSourceControl(false);
                ComportInterfacecs.NanoAllLightSourceControl(false);
                btnlamp.Image = Properties.Resources.rec__1_;
                tangestanIs = false;               //  connectToDeviceToolStripMenuItem.Enabled = false;


            }

        }


        SerialPort serialPort;
      

        private void SerialPortService_PortsChanged(object sender, PortsChangedArgs e)
        {
         
            if (e.EventType== EventType.Removal)
            {
             
                if (InvokeRequired)
                {

                    this.Invoke(new MethodInvoker(delegate {
                        isRun = false;
                        ComportInterfacecs.StopRead = true;

                        btnStart.Image = Properties.Resources.Media_Play2;
                        btnStart.Text = "Start";

                       
                        chTimeSeries.Checked = false;
                        connectToDeviceToolStripMenuItem.Enabled = true;

                    }));

                   
                }
            }
            //  MessageBox.Show(e.EventType.ToString());
        }

        private void Conecting()
        {
            Conect();
            if (IsConected)
                return;
            DialogResult dr = MessageBox.Show("Device not connected", "Connecting", MessageBoxButtons.RetryCancel);
            if (dr == DialogResult.Retry)
            {
                Conect();
                if (IsConected)
                    return;
                else Conecting();
            }
            else if (dr == DialogResult.Cancel)
                return;
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            try
            {
                LoadEnvironment();
                LoadSofwareProperties();
                Conecting();
             
                progressBar1.Visible = false;
                SplashScreen sp1 = new SplashScreen();
                sp1.ShowDialog(); progressBar1.Visible = false;
                this.Show();
                cmbChartType.SelectedIndex = cmbChartType.FindStringExact("Line");

                chart1.MouseWheel += new MouseEventHandler(this.ChartMOuseWhell);

             




               
                newSpectrumToolStripMenuItem.PerformClick();


                Mode = SpectometrMode.Scope;
                chart1.Dock = DockStyle.Fill;
                this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum = _ScopeX1;
                this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum = _ScopeX2;
                this.chart1.ChartAreas["ChartArea1"].AxisY.ScrollBar.Enabled = false;
                this.chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.Enabled = false;
                this.chart1.ChartAreas["ChartArea1"].AxisX.Interval = 100;

                this.chart1.ChartAreas["ChartArea1"].AxisY.Minimum = _ScopeY1; ;
                this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum = _ScopeY2; ;
                this.chart1.ChartAreas["ChartArea1"].AxisY.Interval = 1000;

           
                chart1.ChartAreas[0].AxisY.LabelStyle.Format = "{0}";

                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0}";
                this.chart1.Series[this.ExperimentName].Points.AddXY((double)0.0, (double)0.0);
                this.chart1.ChartAreas["ChartArea1"].AxisX.TitleFont = new Font("Times New Roman", 12, FontStyle.Bold);
                this.chart1.ChartAreas["ChartArea1"].AxisY.TitleFont = new Font("Times New Roman", 12, FontStyle.Bold);
                this.chart1.ChartAreas["ChartArea1"].AxisX.Title = "Wavelength (nm)";
                this.chart1.ChartAreas["ChartArea1"].AxisY.Title = "  Intensity (count)     ";
                


                label8.Text = Mode.ToString();





                this.chart1.ChartAreas["ChartArea1"].AxisX.IsLabelAutoFit = true;
                this.chart1.ChartAreas["ChartArea1"].AxisY.IsLabelAutoFit = true;
                this.chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = true;
                this.chart1.ChartAreas["ChartArea1"].AxisY.LabelStyle.Enabled = true;
                this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
                this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = true;
                this.chart1.ChartAreas["ChartArea1"].CursorX.AutoScroll = true;
                this.chart1.ChartAreas["ChartArea1"].CursorY.AutoScroll = true;

                this.chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
                this.chart1.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;


                this.chart1.ChartAreas["ChartArea1"].CursorY.IsUserSelectionEnabled = true;
                this.chart1.ChartAreas["ChartArea1"].CursorY.IsUserEnabled = true;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(192, 192, 192);
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(192, 192, 192);
                // chart1.Series[ExperimentName].Color =  colorPicker1.Value;
              
                
              //  ComportInterfacecs.PeriodicImageSensorRead(this.IntegrationTime);
              //  isRun = true;
              //  btnStart.Image = Properties.Resources.stop;
               // btnStart.Text = "Stop";
                chart1.ChartAreas[0].InnerPlotPosition.Auto = true;
            
                HideColorBar();
             

            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

           

        }

        private void HideColorBar()
        {
            try
            {
                if (SoftwarePr.HideColorbar)
                {
                    if (chart1.Series.IsUniqueName("UV-IR"))
                    {

                        chart1.Series.Add("UV-IR");
                        chart1.Series["UV-IR"].IsVisibleInLegend = false;
                        chart1.Series["UV-IR"].MarkerSize = 20;
                        chart1.Series["UV-IR"].ChartType = SeriesChartType.Point;
                        chart1.Series["UV-IR"].MarkerStyle = MarkerStyle.Square;
                    }

                    List<SPoint> listColor = new List<SPoint>();
                    SPoint s = new SPoint();
                    WaveToRbg waveToRbg = new WaveToRbg();
                    int[] colorCon = new int[3];
                    int i = 0;
                    chart1.Series["UV-IR"].Points.Clear();
                  
                    for (int a = 380; a < 780; a++)
                    {

                        colorCon = waveToRbg.waveLengthToRGB(a);
                        chart1.Series["UV-IR"].Points.Add();
                        chart1.Series["UV-IR"].Points[i].SetValueXY(a, chart1.ChartAreas[0].AxisY.Maximum);
                        chart1.Series["UV-IR"].Points[i].Color = Color.FromArgb(colorCon[0], colorCon[1], colorCon[2]);
                        //  chart1.Series["UV-IR"].Points[i].MarkerColor= Color.FromArgb(colorCon[0], colorCon[1], colorCon[2]);
                        //  chart1.Series["UV-IR"].Points[i].MarkerSize = 20;
                        i++;





                    }

                   
                }
                else
                {

                    if (!chart1.Series.IsUniqueName("UV-IR"))
                    {
                        Series delete = chart1.Series["UV-IR"];
                        chart1.Series.Remove(delete);
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
                    Axis ax = chart1.ChartAreas[0].AxisX;
                    Axis ay = chart1.ChartAreas[0].AxisY;
                    //if (ax.ScaleView.IsZoomed)
                    //    ax.ScaleView.Size = double.IsNaN(ax.ScaleView.Size) ?
                    //                        ax.Maximum : ax.ScaleView.Size *= 1.25;

                    //if (ax.ScaleView.Size > ax.Maximum - ax.Minimum)
                    //{
                    //    ax.ScaleView.Zoom(ax.Minimum, ax.Maximum);
                    //    // ax.ScaleView.Size = ax.Maximum;
                    //    //ax.ScaleView.Position = ax.Minimum;

                    //}

                    //if (ay.ScaleView.IsZoomed)
                    //    ay.ScaleView.Size = double.IsNaN(ay.ScaleView.Size) ?
                    //                        ay.Maximum : ay.ScaleView.Size *= 1.25;
                    //if (ay.ScaleView.Size > ay.Maximum - ay.Minimum)
                    //{

                    //    //ay.ScaleView.Size = ay.Maximum;
                    //    //ay.ScaleView.Position = ay.Minimum ;
                    //    ay.ScaleView.Zoom(ay.Minimum, ay.Maximum);
                    //    chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset(1);

                    //}

                    viewMinimum = this.chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                    viewMaximum = this.chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                    num3 = this.chart1.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                    num4 = this.chart1.ChartAreas[0].AxisY.ScaleView.ViewMaximum;
                    if (!((viewMinimum > 300.0) | (viewMaximum < 2000)))
                    {
                        this.chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                        this.chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                    }
                    this.chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                    this.chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                }
                if (e.Delta > 0)
                {
                    //    Axis ax = chart1.ChartAreas[0].AxisX;
                    //    ax.ScaleView.Size = double.IsNaN(ax.ScaleView.Size) ?
                    //                        (ax.Maximum - ax.Minimum) / 1.25: ax.ScaleView.Size /= 1.25;

                    //    Axis ay = chart1.ChartAreas[0].AxisY;
                    //    ay.ScaleView.Size = double.IsNaN(ay.ScaleView.Size) ?
                    //                        (ay.Maximum - ay.Minimum) / 1.25 : ay.ScaleView.Size /= 1.25;

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
            catch (System.IO.IOException exp)
            {
                return;
            }
            catch (System.InvalidOperationException error)
            {
                return;
            }
            catch (Exception ex)
            { return; }
          


        }

        private void ShowData(object sender, EventArgs e)
        {
            try
            {

                LoadSofwareProperties();
                if (softWareFrm.Issave || fChartstep.IsSave)
                {
                    if (Mode == SpectometrMode.Absorbance)

                        btnAbsorbance_Click(sender, e);

                    if (Mode == SpectometrMode.Scope)
                        btnScope_Click(sender, e);
                    if (Mode == SpectometrMode.Reflectance)
                        btnIrradiance_Click(sender, e);
                    if (Mode == SpectometrMode.Transmittance)
                        btnTransmittance_Click(sender, e);
                    if (Mode == SpectometrMode.Irradiance)
                        button3_Click(sender, e);
                    if (Mode == SpectometrMode.Fluorescence)
                        button3_Click_2(sender, e);
                    if (Mode == SpectometrMode.Raman)
                    {
                        ramanToolStripMenuItem_Click(sender, e);

                    }
                    HideColorBar();
                    softWareFrm.Issave = false;
                    fChartstep.IsSave = false;
                }

                if (wait)
                    return;


                int num = Convert.ToInt32(this.numricalAverage.Value);
                if (this.NumberOfFrame < num)
                {
                    this.NumberOfFrame++;
                }
                else
                {
                    this.NumberOfFrame = 1;
                }
                float[] extendedData = new float[numPixel];
                if (numPixel == 580)
                {
                   
                    for (int i = 0; i < 580; i++)
                    {
                       
                        extendedData[i] = MainData[i/2];
                        extendedData[i+1] = (MainData[i/2] + MainData[(i / 2 )+1])/2 ;
                        i++;

                    }
                    Array.Copy(extendedData, this.AverageData[this.NumberOfFrame - 1], numPixel);
                }
                else
                {
                    Array.Copy(this.MainData, this.AverageData[this.NumberOfFrame - 1], numPixel);
                }
                float[] numArray = (from k in Enumerable.Range(0, this.AverageData[this.NumberOfFrame - 1].Length) select this.AverageData.Sum<float[]>((Func<float[], float>)(a => a[k]))).ToArray<float>();

                int startFrom=0;
              if (numPixel==580)
                {
                     double C92E;
            for (int i = 0; i < 580; i++)
            {
                C92E = i / 2 + 1;          // 340 t0 850nm

                        xvalue[i] = Convert.ToSingle(_XmapI)
                            + Convert.ToSingle(_XmapC1 * (float)C92E)
                           + Convert.ToSingle(_XmapC2 * Math.Pow((float)C92E, 2))
                           + Convert.ToSingle(_XmapC3 * Math.Pow((float)C92E, 3)) + Convert.ToSingle(_XmapC4 * Math.Pow((float)C92E, 4)) + Convert.ToSingle(_XmapC5 * Math.Pow((float)C92E, 5));



  
                        //     (SpectCoeffiecientB3) * (Math.Pow(C92E, 3)) + (SpectCoeffiecientB4) * (Math.Pow(C92E, 4)) + (SpectCoeffiecientB5) * (Math.Pow(C92E, 5)));

                        C92E = i / 2 + 1.5;
                i = i + 1;

                        xvalue[i] = Convert.ToSingle(_XmapI)
                           + Convert.ToSingle(_XmapC1 * (float)C92E)
                          + Convert.ToSingle(_XmapC2 * Math.Pow((float)C92E, 2))
                          + Convert.ToSingle(_XmapC3 * Math.Pow((float)C92E, 3)) + Convert.ToSingle(_XmapC4 * Math.Pow((float)C92E, 4)) + Convert.ToSingle(_XmapC5 * Math.Pow((float)C92E, 5));
                        // XAxisExtend[i] = (SpectCoeffiecientA0 + SpectCoeffiecientB1 * C92E + (SpectCoeffiecientB2) * (Math.Pow(C92E, 2)) +
                        //  (SpectCoeffiecientB3) * (Math.Pow(C92E, 3)) + (SpectCoeffiecientB4) * (Math.Pow(C92E, 4)) + (SpectCoeffiecientB5) * (Math.Pow(C92E, 5)));
                    }

                }
              
              else
                {
                    for (int j = 0; j < numPixel; j++)
                    {
                        startFrom++;
                        numArray[j] /= (float)num;

                        xvalue[j] = Convert.ToSingle(_XmapC5 * Math.Pow(startFrom, 5)) + Convert.ToSingle(_XmapC4 * Math.Pow(startFrom, 4))
                            + Convert.ToSingle((_XmapC3 * Math.Pow(startFrom, 3)) + (_XmapC2 * Math.Pow(startFrom, 2) + (_XmapC1 * startFrom)) + Convert.ToSingle(3.165694139E+02));




                        if (SoftwarePr.XvalCM)
                        {


                            if (j < 10)
                                xvalue[j] = 0;
                            else
                                xvalue[j] = 10000000 / xvalue[j];
                            if (float.IsInfinity(xvalue[j]) || float.IsNaN(xvalue[j]))
                                xvalue[j] = xvalue[j - 1];


                        }
                        for (int m = 0; m < 10; m++)
                            xvalue[m] = xvalue[10];
                        if (SoftwarePr.XvalCM)
                        {
                            chart1.ChartAreas[0].AxisX.Interval = 50000;
                            chart1.ChartAreas[0].AxisX.Maximum = 1000000;
                        }


                    }
                }

                //for (int d = Smoothing; d < numPixel; d++)
                //{
                //    xvalue[d] = ((float)(0.33)*Smoothing ) + xvalue[d];
                //}

                numSmoothing = dtAnalys.SmoothingS(numArray, Smoothing);

              



                float BaseF = 0;
                float BaseD = 0;
                if (this.Mode == SpectometrMode.Scope)
                {
                    scopeModeToolStripMenuItem.Checked = true;
                    numScop = dtAnalys.SmoothingS(numSmoothing, Smoothing);
                    dt = numScop;

                    if (_EnableBaseLine)
                    {
                        BaseF = Convert.ToInt32(findPos(_Baseline));

                        //   float numBaseline = numSmoothing[BaseF];
                        for (int j = 0; j < numPixel; j++)
                        {


                            if (BaseF >= 0)
                                numScop[j] = numScop[j] - BaseF;
                            else
                                numScop[j] = numScop[j] + BaseF;

                        }


                    }
                    BaseD = findPos(BaseLineD);
                    if (BaseLineD != 0)
                    {

                        for (int m = 0; m < numPixel; m++)
                        {
                            if (BaseD >= 0)
                                numScop[m] = numScop[m] - BaseD;
                            else
                                numScop[m] = numScop[m] + BaseD;



                        }
                    }

                    chart1.Series[ExperimentName].Points.DataBindXY(xvalue, numScop);
                    refDt = numScop;


                }
                if (Mode == SpectometrMode.Fluorescence)
                {
                    numScop = dtAnalys.SmoothingS(numSmoothing, Smoothing);
                    dt = numScop;

                    if (_EnableBaseLine)
                    {
                        BaseF = Convert.ToInt32(findPos(_Baseline));

                        //   float numBaseline = numSmoothing[BaseF];
                        for (int j = 0; j < numPixel; j++)
                        {


                            if (BaseF >= 0)
                                numScop[j] = numScop[j] - BaseF;
                            else
                                numScop[j] = numScop[j] + BaseF;


                        }


                    }
                    BaseD = findPos(BaseLineD);
                    if (BaseLineD != 0)
                    {

                        for (int m = 0; m < numPixel; m++)
                        {
                            if (BaseD >= 0)
                                numScop[m] = numScop[m] - BaseD;
                            else
                                numScop[m] = numScop[m] + BaseD;



                        }
                    }

                    chart1.Series[ExperimentName].Points.DataBindXY(xvalue, numScop);
                    refDt = numScop;


                }

                if (Mode == SpectometrMode.Transmittance)
                {
                    numScop = dtAnalys.SmoothingS(numSmoothing, Smoothing);
                    dt = numScop;
                    if (_EnableBaseLine)
                    {
                        BaseF = Convert.ToInt32(findPos(_Baseline));

                        //   float numBaseline = numSmoothing[BaseF];
                        for (int j = 0; j < numPixel; j++)
                        {


                            if (BaseF >= 0)
                                numScop[j] = numScop[j] - BaseF;
                            else
                                numScop[j] = numScop[j] + BaseF;

                        }


                    }

                    DataForShow = dtAnalys.Transmittance(numScop, darkData, refrenceData);
                    numSmoothing = dtAnalys.SmoothingS(DataForShow, Smoothing);
                    dt = numSmoothing;

                    BaseD = findPos(BaseLineD);
                    if (BaseLineD != 0)
                    {

                        for (int m = 0; m < numPixel; m++)
                        {
                            if (BaseD >= 0)
                                numSmoothing[m] = numSmoothing[m] - BaseD;
                            else
                                numSmoothing[m] = numSmoothing[m] + ((-1) * BaseD);



                        }
                    }
                    chart1.Series[ExperimentName].Points.DataBindXY(xvalue, numSmoothing);



                }

                if (Mode == SpectometrMode.Reflectance)
                {
                    numScop = dtAnalys.SmoothingS(numSmoothing, Smoothing);
                    dt = numScop;
                    if (_EnableBaseLine)
                    {
                        BaseF = Convert.ToInt32(findPos(_Baseline));

                        //   float numBaseline = numSmoothing[BaseF];
                        for (int j = 0; j < numPixel; j++)
                        {


                            if (BaseF >= 0)
                                numScop[j] = numScop[j] - BaseF;
                            else
                                numScop[j] = numScop[j] + BaseF;

                        }


                    }

                    DataForShow = dtAnalys.Reflectance(numScop, darkData, refrenceData);
                    numSmoothing = dtAnalys.SmoothingS(DataForShow, Smoothing);
                    dt = numSmoothing;

                    BaseD = findPos(BaseLineD);
                    if (BaseLineD != 0)
                    {

                        for (int m = 0; m < numPixel; m++)
                        {
                            if (BaseD >= 0)
                                numSmoothing[m] = numSmoothing[m] - BaseD;
                            else
                                numSmoothing[m] = numSmoothing[m] + ((-1) * BaseD);



                        }
                    }
                    chart1.Series[ExperimentName].Points.DataBindXY(xvalue, numSmoothing);


                }

                if (Mode == SpectometrMode.Absorbance)
                {

                    if (darkData.Length != numPixel || refDt.Length != numPixel)
                    {
                        label9.Text = "Refernce data or Dark data Not valid.Try aagin";
                        return;
                    }
                    numScop = dtAnalys.SmoothingS(numSmoothing, Smoothing);
                    dt = numScop;
                    if (_EnableBaseLine)
                    {
                        BaseF = Convert.ToInt32(findPos(_Baseline));

                        //   float numBaseline = numSmoothing[BaseF];
                        for (int j = 0; j < numPixel; j++)
                        {


                            if (BaseF >= 0)
                                numScop[j] = numScop[j] - BaseF;
                            else
                                numScop[j] = numScop[j] + BaseF;

                        }


                    }

                    DataForShow = dtAnalys.Absorbance(numScop, darkData, refrenceData);
                    numSmoothing = dtAnalys.SmoothingS(DataForShow, Smoothing);
                    DataForShow = numSmoothing;
                    dt = numSmoothing;

                    BaseD = findPos(BaseLineD);
                    if (BaseLineD != 0)
                    {

                        for (int m = 0; m < numPixel; m++)
                        {
                            if (BaseD >= 0)
                                numSmoothing[m] = numSmoothing[m] - BaseD;
                            else

                                numSmoothing[m] = numSmoothing[m] + ((-1) * BaseD);
                            if (m == 380)
                            {
                                float k = numSmoothing[m];
                            }



                        }
                    }
                    chart1.Series[ExperimentName].Points.DataBindXY(xvalue, numSmoothing);




                }
                if (Mode == SpectometrMode.ND1 || Mode == SpectometrMode.ND2 || Mode == SpectometrMode.ND3 || Mode == SpectometrMode.ND4)
                {

                   // chart1.Series[ExperimentName].Points.DataBindXY(xvalue, ND1(xvalue));
                    label8.Text = Mode.ToString();

                }
                if (Mode == SpectometrMode.Raman)
                {
                    if (_EnableBaseLine)
                    {
                        BaseF = Convert.ToInt32(findPos(_Baseline));

                        //   float numBaseline = numSmoothing[BaseF];
                        for (int j = 0; j < numPixel; j++)
                        {


                            if (BaseF >= 0)
                                numSmoothing[j] = numSmoothing[j] - BaseF;
                            else
                                numSmoothing[j] = numSmoothing[j] + BaseF;

                        }


                    }

                    DataForShow = dtAnalys.RamanData(xvalue);
                    BaseD = findPos(BaseLineD);
                    if (BaseLineD != 0)
                    {

                        for (int m = 0; m < numPixel; m++)
                        {
                            if (BaseD >= 0)
                                numSmoothing[m] = numSmoothing[m] - BaseD;
                            else

                                numSmoothing[m] = numSmoothing[m] + ((-1) * BaseD);
                            if (m == 380)
                            {
                                float k = numSmoothing[m];
                            }



                        }
                    }

                    chart1.Series[ExperimentName].Points.DataBindXY(DataForShow, numSmoothing);

                    //   label8.Text = DataForShow .Min().ToString()+":" + DataForShow .Max ().ToString() + ":";
                }
                if (Mode == SpectometrMode.Irradiance)
                {

                    numScop = dtAnalys.SmoothingS(numSmoothing, Smoothing);
                    dt = numScop;
                    if (_EnableBaseLine)
                    {
                        BaseF = Convert.ToInt32(findPos(_Baseline));

                        //   float numBaseline = numSmoothing[BaseF];
                        for (int j = 0; j < numPixel; j++)
                        {


                            if (BaseF >= 0)
                                numScop[j] = numScop[j] - BaseF;
                            else
                                numScop[j] = numScop[j] + BaseF;

                        }


                    }

                    DataForShow = dtAnalys.Irradians(numScop, darkData, refrenceData, xvalue);
                    numSmoothing = dtAnalys.SmoothingS(DataForShow, Smoothing);
                    DataForShow = numSmoothing;
                    dt = numSmoothing;

                    BaseD = findPos(BaseLineD);
                    if (BaseLineD != 0)
                    {

                        for (int m = 0; m < numPixel; m++)
                        {
                            if (BaseD >= 0)
                                numSmoothing[m] = numSmoothing[m] - BaseD;
                            else

                                numSmoothing[m] = numSmoothing[m] + ((-1) * BaseD);
                            if (m == 380)
                            {
                                float k = numSmoothing[m];
                            }



                        }
                    }
                    //float  [] wave = dtAnalys.FirstColumnLa("G:\\Irradiance\\led.xls", 1);
                    //float [] intensity = dtAnalys.FirstColumnLa("G:\\Irradiance\\led.xls", 2);
                    //float [] dark = dtAnalys.FirstColumnLa("G:\\Irradiance\\led.xls", 2);
                    //float [] refrence = dtAnalys.FirstColumnLa("G:\\Irradiance\\tungestan.xls", 2);
                    // DataForShow = dtAnalys.Irradians( intensity, dark, refrence, wave);



                    chart1.Series[ExperimentName].Points.DataBindXY(xvalue, numSmoothing);


                }
                if (Mode == SpectometrMode.calc)
                {

                }
                count++;
               
                    //this.Invoke(new MethodInvoker(delegate
                    //{
                    //    lblposition.Text = count.ToString();
                    //}));
                


            }
            catch(System.IO.IOException exp)
            {
                return;
            }
            catch(System.InvalidOperationException error)
            {
                return;
            }
            catch (Exception ex) 
            { return; }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static int GetDistance(Color current, Color match)
        {
            int redDifference;
            int greenDifference;
            int blueDifference;

            redDifference = current.R - match.R;
            greenDifference = current.G - match.G;
            blueDifference = current.B - match.B;

            return redDifference * redDifference + greenDifference * greenDifference + blueDifference * blueDifference;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="map"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        public static int FindNearestColor(Color[] map, Color current)
        {
            int shortestDistance;
            int index;

            index = -1;
            shortestDistance = int.MaxValue;

            for (int i = 0; i < map.Length; i++)
            {
                Color match;
                int distance;

                match = map[i];
                distance = GetDistance(current, match);

                if (distance < shortestDistance)
                {
                    index = i;
                    shortestDistance = distance;
                }
            }

            return index;
        }

        private double[] ND1(float[] xval)
        {
            double[] Nd = new double[numPixel];
            double[] Nd1 = new double[numPixel];
            double[] Nd2 = new double[numPixel];
            double[] Nd3 = new double[numPixel];
            double[] Nd4 = new double[numPixel];

            //   float[] abs =  dtAnalys.Absorbance(DataForShow , darkData, refrenceData);

            for (int i = 2; i < numPixel-1; i++)
            {
                double deltaX = xval[i + 1] - xval[i - 1];
                Nd1[i] = (numSmoothing[i + 1] - numSmoothing[i - 1]) / (2 * deltaX);


                

                Nd2[i] = (Nd1[i + 1] - Nd1[i - 1]) / Math.Pow(deltaX, 2);

                Nd3[i] = (Nd2[i + 1] - Nd2[i - 1]) / (xval[i + 1] - xval[i - 1]);

                Nd4[i] = (Nd3[i + 1] - Nd3[i - 1]) / (xval[i + 1] - xval[i - 1]);
             
                  


            }
            if (de == d.Nd1)
                Nd = Nd1;
            if (de == d.Nd2)
                Nd = Nd2;
            if (de == d.Nd3)
                Nd = Nd3;
            if (de == d.Nd4)
                Nd = Nd4;
            for (int m = 0; m < Nd.Length; m++)
            {
                if (double.IsNaN(Nd[m]) || double.IsInfinity(Nd[m]))
                    Nd[m] = 0;
            }

            return Nd;


        }
        private void SettingDataReceivedEvent(object sender, EventArgs e)
        {

            try
            {


                this.SettingData = ((ComportSettingDataReceiveEventArgs)e).GetReceivedData();

                if (this.SettingData[0] == 3)
                {

                    base.Invoke(new EventHandler(this.ChipTemperatureDisplay));

                }

                if (this.SettingData[0] == 5)
                {
                    base.Invoke(new EventHandler(DisplaySerialNumer));

                }
                if (this.SettingData[0] == 7)
                {
                    base.Invoke(new EventHandler(DiplayTangestanValue));
                }
                if (SettingData[0] == 6)
                {
                    base.Invoke(new EventHandler(DisplayCounterValues));
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayCounterValues(object sender, EventArgs e)
        {

            int[] numArray = new int[4];
            for (int i = 0; i < 16; i++)
            {
                CounterValue[i] = SettingData[i + 2];

            }

            
            fSetting.lblDevice.Text = "";
            fSetting.lblTangestan.Text = "";
            // deCounter.lblRes.Text = "";
            fSetting.lblDeuterium.Text = "";

            double n1 = (CounterValue[0] + (CounterValue[1] * 256) + (CounterValue[2] * (2 * 256)) + (CounterValue[3] * (256 * 3))) * 0.2;
            double n2 = (CounterValue[4] + (CounterValue[5] * 256) + (CounterValue[6] * (2 * 256)) + (CounterValue[7] * (256 * 3))) * 0.2;
            double n3 = (CounterValue[8] + (CounterValue[9] * 256) + (CounterValue[10] * (2 * 256)) + CounterValue[11] * (256 * 3)) * 0.2;

            TimeSpan time = TimeSpan.FromSeconds(n1);
            TimeSpan time2 = TimeSpan.FromSeconds(n2);
            TimeSpan time3 = TimeSpan.FromSeconds(n3);
            
            fSetting.lblDevice.Text = time.Days+" :Day "+ time.Hours+": H "+time.Minutes+": Min";
            fSetting.lblTangestan.Text = time2.Days + " :Day " + time2.Hours + ": H " + time2.Minutes + ": Min";
            fSetting.lblDeuterium.Text = time3.Days + " :Day " + time3.Hours + ": H " + time3.Minutes + ": Min";

        }

        private void DiplayTangestanValue(object sender, EventArgs e)
        {
            Tangestanval[0] = SettingData[3];
        }

        private void ChipTemperatureDisplay(object sender, EventArgs e)
        {

            this.ChipTemperatureVal[0] = this.SettingData[2];
           // deviceInfo.temp.Text = "Temp:" + ChipTemperatureVal[0].ToString() + "\u2103";
            Thread.Sleep(10);
        }
        string initDate ;
        private void DisplaySerialNumer(object sender, EventArgs e)
        {

            for (int i = 0; i < 28; i++)
            {
                this.ReadSerialnumber[i] = this.SettingData[i + 2];

            }
            this.deviceInfo.F2Date.Text = "";
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
            frm.lblDate.Text = deviceInfo.F2Date.Text;
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
           // this.deviceInfo.temp.Text = "";
            //this.deviceInfo.temp.Text = this.deviceInfo.temp.Text + Convert.ToChar(this.ReadSerialnumber[24]);
            //this.deviceInfo.temp.Text = this.deviceInfo.temp.Text + Convert.ToChar(this.ReadSerialnumber[25]);
            //this.deviceInfo.temp.Text = this.deviceInfo.temp.Text + Convert.ToChar(this.ReadSerialnumber[26]);
            //this.deviceInfo.temp.Text = this.deviceInfo.temp.Text + Convert.ToChar(this.ReadSerialnumber[27]);
        }
        float[] s;

        private void btnStart_Click(object sender, EventArgs e)
        {
            s = new float[numPixel];
            #region new Board
            //    try
            //    {
            //        if (!isRun)
            //        {

            //            if (lineScanCameraIndex < 0)//if is not added yet
            //                lineScanCameraIndex = DllInterface.LineScanCamera_Add();

            //            //now, lineScanCameraIndex must be valid
            //            if (lineScanCameraIndex < 0)
            //                return;

            //            unsafe
            //            {
            //                if (DllInterface.LineScanCamera_StartCameraCapture_WithGl
            //                          (
            //                              lineScanCameraIndex, Marshal.StringToHGlobalAnsi(serial),
            //                             0, null,
            //                             1, chart1.Handle.ToPointer(),
            //                              65536, 16) == 1)
            //                //start capture (also add scanner and graph)

            //                {
            //                    //~~~~~~~~~~~~
            //                    //get properties of camera frames 
            //                    numPixel = DllInterface.LineScanCamera_GetImageWidth(lineScanCameraIndex);
            //                    //int cameraImgHeight = DllInterface.LineScanCamera_GetImageHeight(lineScanCameraIndex); // == 1
            //                    int cameraImgBytesPerPixel = DllInterface.LineScanCamera_GetImageBytesPerPixel(lineScanCameraIndex);
            //                    int cameraImgColoringType = DllInterface.LineScanCamera_GetImageColoringType(lineScanCameraIndex);//8: monochrome,  1:RGB  
            //                                                                                                                      //~~~~~~~~~~~~
            //                                                                                                                      //set parameters of camera
            //                    uint time = (uint)((0 * 1000) + (0));

            //                    DataForShow = new float[numPixel];
            //                    MainData = new float[numPixel];
            //                    AverageData = new float[][] {
            //    new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel],
            //    new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel]
            //};
            //                    x = new double[numPixel];

            //                    numScop = new float[numPixel];
            //                    numSmoothing = new float[numPixel];
            //                    refDt = new float[numPixel];
            //                    xvalue = new float[numPixel];
            //                    dtAnalys.numPixel = numPixel;


            //                    DllInterface.LineScanCamera_SetRedContrast(lineScanCameraIndex, (uint)435);
            //                    DllInterface.LineScanCamera_SetRedBrightness(lineScanCameraIndex, (uint)450);

            //                    DllInterface.LineScanCamera_SetGreenContrast(lineScanCameraIndex, (uint)400);
            //                    DllInterface.LineScanCamera_SetGreenBrightness(lineScanCameraIndex, (uint)0);

            //                    DllInterface.LineScanCamera_SetBlueContrast(lineScanCameraIndex, (uint)400);
            //                    DllInterface.LineScanCamera_SetBlueBrightness(lineScanCameraIndex, (uint)0);
            //                    DllInterface.LineScanCamera_SetRedExpousure(lineScanCameraIndex, this.IntegrationTime);
            //                    isRun = true;
            //                    btnStart.Image = Properties.Resources.stop;
            //                    btnStart.Text = "Stop";
            //                    tmrGetData.Interval = 40;
            //                    tmrGetData.Enabled = true;

            //                    tmrRendering.Interval = 40;
            //                    tmrRendering.Enabled = true;



            //                }
            //                else
            //                {
            //                    DllInterface.LineScanCamera_Remove(lineScanCameraIndex);
            //                    lineScanCameraIndex = -1;
            //                }
            //                //remove camera



            //            }

            //        }
            //        else
            //        {
            //            isRun = false;
            //            tmrRendering.Enabled = false;
            //            tmrGetData.Enabled = false;
            //            btnStart.Image = Properties.Resources.Media_Play2;
            //            btnStart.Text = "Start";

            //            DllInterface.LineScanCamera_StopCameraCapture(lineScanCameraIndex);


            //            //lineScanCameraIndex = -1;
            //            return;
            //        }
            #endregion 
            try { 

            if (isRun == false)
                {
                    if ((chart1.Series.Count == 1 && !chart1.Series.IsUniqueName("UV-IR")) || chart1.Series.Count == 0)
                        return;
                    ComportInterfacecs.PeriodicImageSensorRead(this.IntegrationTime);
                    isRun = true;
                    btnStart.Image = Properties.Resources.stop;
                    btnStart.Text = "Stop";
                    s = dt;

                }
                else
                {
                    isRun = false;
                    btnStart.Image = Properties.Resources.Media_Play2;
                    btnStart.Text = "Start";
                    ComportInterfacecs.StopReadingImageSensor();
                    ComportInterfacecs.DiscardBufferPort();
                    ComportInterfacecs.StopRead = true;
                    chTimeSeries.Checked = false;

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
       
       
        private void colorMeasurment()

        {
            if (fchart.Colorm)
            {
                if (fchart.WindowState == FormWindowState.Minimized)
                    fchart.WindowState = FormWindowState.Normal;

                fchart.BringToFront();

                fchart.chart1.Series["palete"].Points.Clear();
                float[] T = dtAnalys.Transmittance(numSmoothing, darkData, refrenceData);
                float X = 0;
                float Y = 0;
                float Z = 0;
                float x = X;// ( X / (X + Y + Z)) ;
                float y = Y;// (Y / (X + Y + Z));
                float z = Z;// =( 1 - (x + y) );
                int k = 0;
                int n = 0;
                float Kcolor = 0;
                float[] dt1 = new float[900];
                int[] colordt = new int[500];
                if (dt == null)
                    return;


                //int result;
                //int s = 0;
                //for (int m = 80; m < 1270; m++)
                //{

                //    Math.DivRem(Convert.ToInt32(xvalue [m]), 5, out result);
                //    if (result == 0)
                //    {
                //        colordt [s] = Convert.ToInt32(xvalue  [m]);
                //        s++;
                //    }
                //}
                for (int f = 380; f <= 780; f = f + 5)
                {

                    dt1[n] = findPos(f);
                    n++;

                }
                colordt = colordt.Distinct().ToArray();
                if (fchart.rdA.Checked && fchart.rd2.Checked)
                {
                    for (int m = 0; m < 79; m++)
                    {
                        if (m == 78)
                        {
                            X = X + (A[m - 1] * X2[m - 1] * dt1[m]);
                            Y = Y + (A[m - 1] * Y2[m - 1] * dt1[m]);
                            Z = Z + (A[m - 1] * Z2[m - 1] * dt1[m]);
                            Kcolor += A[m - 1] * Y2[m - 1];
                        }
                        else
                        {
                            X = X + (A[m] * X2[m] * dt1[m]);
                            Y = Y + (A[m] * Y2[m] * dt1[m]);
                            Z = Z + (A[m] * Z2[m] * dt1[m]);
                            Kcolor += A[m] * Y2[m];
                        }


                    }
                    Kcolor = 100 / Kcolor;
                    X = Kcolor * X;
                    Y = Kcolor * Y;
                    Z = Kcolor * Z;
                    x = X / (X + Y + Z);
                    y = Y / (X + Y + Z);
                    z = 1 - (x + y);

                }
                else if (fchart.rdA.Checked && fchart.rd10.Checked)
                {
                    for (int i = 420; i <= 750; i = i + 5)
                    {
                        X = X + (A[k] * X10[k] * dt1[k]);
                        Y = Y + (A[k] * Y10[k] * dt1[k]);
                        Z = Z + (A[k] * Z10[k] * dt1[k]);
                        Kcolor += A[k] * Y10[k];
                        k = k + 1;
                    }
                    Kcolor = 100 / Kcolor;
                    X = Kcolor * X;
                    Y = Kcolor * Y;
                    Z = Kcolor * Z;
                    x = X / (X + Y + Z);
                    y = Y / (X + Y + Z);
                    z = 1 - (x + y);

                }
                else if (fchart.rdD65.Checked && fchart.rd10.Checked)
                {
                    for (int i = 420; i <= 750; i = i + 5)
                    {
                        X = X + (D65[k] * X10[k] * dt[k]);
                        Y = Y + (D65[k] * Y10[k] * dt[k]);
                        Z = Z + (D65[k] * Z10[k] * dt[k]);
                        Kcolor += D65[k] * Y10[k];
                        k = k + 1;
                    }
                    Kcolor = 100 / Kcolor;
                    X = Kcolor * X;
                    Y = Kcolor * Y;
                    Z = Kcolor * Z;
                    x = X / (X + Y + Z);
                    y = Y / (X + Y + Z);
                    z = 1 - (x + y);


                }

                else if (fchart.rdD65.Checked && fchart.rd2.Checked)
                {
                    //float[] p = new float[200];
                    //int countC = 0;
                    //for (int m = 0; m < pirple.Length; m = m + 16)
                    //{
                    //    p[countC] = pirple[m];
                    //    countC++;

                    //}

                    for (int m = 0; m < 79; m++)
                    {
                        if (m == 78)
                        {
                            X = X + (A[m - 1] * X2[m - 1] * dt1[m]);
                            Y = Y + (A[m - 1] * Y2[m - 1] * dt1[m]);
                            Z = Z + (A[m - 1] * Z2[m - 1] * dt1[m]);
                            Kcolor += D65[m - 1] * Y2[m - 1];
                        }
                        else
                        {
                            X = X + (D65[m] * X2[m] * dt1[m]);
                            Y = Y + (D65[m] * Y2[m] * dt1[m]);
                            Z = Z + (D65[m] * Z2[m] * dt1[m]);
                            Kcolor += D65[m] * Y2[m];
                        }



                    }
                    Kcolor = 100 / Kcolor;
                    X = Kcolor * X;
                    Y = Kcolor * Y;
                    Z = Kcolor * Z;
                    x = X / (X + Y + Z);
                    y = Y / (X + Y + Z);
                    z = 1 - (x + y);

                }

                fchart.XYZg.Visible = true;
                fchart.RGBg.Visible = true;
                fchart.CIELuvg.Visible = true;
                fchart.LABg.Visible = true;
                fchart.CIExyYg.Visible = true;
                fchart.CIElchuv.Visible = true;
                fchart.LCHABg.Visible = true;
                //   fchart.LMSg.Visible = true;
                fchart.Hunterg.Visible = true;
                fchart.CIEXlbl.Text = "X=" + x.ToString("N4");
                fchart.CIEYlbl.Text = "Y=" + y.ToString("N4");
                fchart.CIEZlbl.Text = "Z=" + z.ToString("N4");



                Colourful.Conversion.ColourfulConverter a = new ColourfulConverter();
                Colourful.XYZColor xyz = new Colourful.XYZColor(x, y, z);
                ColourfulConverter a1 = new ColourfulConverter();
                a1.ToLab(xyz);

                LabColor la = a1.ToLab(xyz);
                fchart.CIELlbl.Text = "L=" + la.L.ToString("N4");
                fchart.CIEAlbl.Text = "a=" + la.a.ToString("N4");
                fchart.CIEBlbl.Text = "b=" + la.b.ToString("N4");




                Colourful.LuvColor luvc = a1.ToLuv(xyz);
                fchart.CIELuvLlbl.Text = "L=" + luvc.L.ToString("N4");
                fchart.CIELuvUlbl.Text = "u=" + luvc.u.ToString("N4");
                fchart.CIELuvVlbl.Text = "v=" + luvc.v.ToString("N4");

                Colourful.xyYColor xyY = a1.ToxyY(xyz);
                fchart.CIExyYxlbl.Text = "x=" + xyY.x.ToString("N4");
                fchart.CIExyYylbl.Text = "y=" + xyY.y.ToString("N4");
                fchart.CIExyYy1lbl.Text = "L=" + xyY.Luminance.ToString("N4");
                Colourful.LChuvColor lchuv = a1.ToLChuv(xyz);

                fchart.LCHLlbl.Text = "L=" + lchuv.L.ToString("N4");
                fchart.LCHClbl.Text = "C=" + lchuv.C.ToString("N4");
                fchart.LCHHlbl.Text = "h=" + lchuv.h.ToString("N4");
                Colourful.LChabColor lchab = a1.ToLChab(xyz);

                fchart.LCHabLlbl.Text = "L=" + lchab.L.ToString("N4");
                fchart.LCHabClbl.Text = "C=" + lchab.C.ToString("N4");
                fchart.LCHabhlbl.Text = "h=" + lchab.h.ToString("N4");
                //Colourful.LMSColor lms = a1.ToLMS(xyz);
                //fchart.LMSLbl.Text = "L=" + lms.L.ToString("N4");
                //fchart.LMSMlbl.Text = "M=" + lms.M.ToString("N4");
                //fchart.LMSSlbl.Text = "S=" + lms.S.ToString("N4");
                Colourful.HunterLabColor hunter = a1.ToHunterLab(xyz);
                fchart.hunterLlbl.Text = "L=" + hunter.L.ToString("N4");
                fchart.Hunteralbl.Text = "a=" + hunter.a.ToString("N4");
                fchart.hunterblbl.Text = "b=" + hunter.b.ToString("N4");

                Color rgb = a.ToRGB(xyz);
                fchart.Rlbl.Text = "R=" + rgb.R.ToString();
                fchart.Glbl.Text = "G=" + rgb.G.ToString();
                fchart.Blbl.Text = "B=" + rgb.B.ToString();

                int match = FindNearestColor(colormach, rgb);





                fchart.chart1.Series["palete"].Points.AddXY(XvalColor[match], YvalColor[match]);
                fchart.measurment = false;
            }



        }

        private double DeltaE76(double l1, double a1, double b1, double l2, double a2, double b2)
        {
            return Math.Sqrt(Math.Pow(l1 - l2, 2) + Math.Pow(a1 - a2, 2) + Math.Pow(b1 - b2, 2));
        }

        private int findMin()
        {
            int pos1 = 0;
            for (int i = 0; i < xvalue.Length; i++)
            {
                if ((int)xvalue[i] == chart1.ChartAreas[0].AxisX.Minimum)
                {
                    pos1 = i;
                    break;
                }

            }
            return pos1;

        }
        private int findMax()
        {
            int pos1 = 0;
            for (int i = 0; i < xvalue.Length; i++)
            {
                if ((int)xvalue[i] == chart1.ChartAreas[0].AxisX.Maximum-1)
                {
                    pos1 = i;
                    break;
                }

            }
            return pos1;

        }
        private float findPos(int x)

        {
            int pos;
            float value = 0;
            for (int i = 0; i < xvalue.Length; i++)
            {
                if ((int)xvalue[i] == x)
                {
                    pos = i;
                    value = dt[pos];
                    break;
                }
            }

            return value;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                isRun = false;
                tmrRendering.Enabled = false;
                tmrGetData.Enabled = false;
                btnStart.Image = Properties.Resources.Media_Play2;
                btnStart.Text = "Start";

                //DllInterface.LineScanCamera_StopCameraCapture(lineScanCameraIndex);
                string[] strArray2 = new string[4];
                strArray2[0] = "Error";
                string[] strArray = strArray2;


                strArray = this.ComportInterfacecs.FindConnectedDevices();
                if (strArray[0] != "Error")
                {
                    ComportInterfacecs.TungstenLightSourceControl(true);
                    ComportInterfacecs.ShutterSourceControl(false);
                    ComportInterfacecs.NanoLED5LightSourceControl(false);
                    ComportInterfacecs.NanoLED6LightSourceControl(false);
                    ComportInterfacecs.NanoLED1LightSourceControl(false);
                    ComportInterfacecs.NanoAllLightSourceControl(false);


                    //  ComportInterfacecs.TungstenLightSourceControl(true);

                    ComportInterfacecs.DiscardBufferPort();


                }

                if (PortConnectionStatus)
                {

                }
            }
            catch { }
            
        }


        private void cmbChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (Series series in chart1.Series)
                {
                    if (series.Name!=chart1.Series["UV-IR"].Name)
                    series.ChartType = (SeriesChartType)cmbChartType.SelectedItem;
                }

            }
            catch { }
        }

        private void colorPicker1_Click(object sender, BlackBeltCoder.ColorPickerEventArgs e)
        {
            chart1.Series[cmbSeriesName.SelectedItem.ToString()].Color = colorPicker1.Value;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            chart1.Series[ExperimentName].BorderWidth = trackBar1.Value;
        }

        private void txtSeriesTiltle_TextChanged(object sender, EventArgs e)
        {
            chart1.Titles.Clear();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
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
                if (de == d.Nd1 || de == d.Nd2 || de == d.Nd3 || de == d.Nd4)
                    return;

                if (cmbSeriesName.Items.Count > 1)
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

            if (chHide.Checked)
            {
                chart1.Series[ExperimentName].Enabled = false;
            }
            else
                chart1.Series[ExperimentName].Enabled = true;
        }
        private Point mousePoint;
        int _currentPointX, _currentPointY;
        private Graphics objGraphics;
        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {

            Axis ax = chart1.ChartAreas[0].AxisX;
            Axis ay = chart1.ChartAreas[0].AxisY;

            //if mouse was moved and mouse left click
            if (e.Button == MouseButtons.Left && IsPanninig)
            {
                double x0 = ax.PixelPositionToValue(mDown.X);
                double x1 = ax.PixelPositionToValue(e.X);
                double y0 = ay.PixelPositionToValue(mDown.Y);
                double y1 = ay.PixelPositionToValue(e.Y);

                ax.Minimum = prevXMin + (x0 - x1);
                ax.Maximum = prevXMax + (x0 - x1);
                ay.Minimum = prevYMin + (y0 - y1);
                ay.Maximum = prevYMax + (y0 - y1);

            }

            //Rectangle rec = new Rectangle();
            //switch  (e.Button)
            //    {
            //    case MouseButtons.Left:
            //        rec.X = e.X;
            //        rec.Y = e.Y;
            //        rec.Width = 50;
            //        rec.Height = 50;
            //        objGraphics.DrawRectangle(System.Drawing.Pens.Blue, rec);
            //        break;

            //}

            //_currentPointX = e.X;
            //_currentPointY = e.Y;
            //chart1.ChartAreas[0].CursorX.SetCursorPixelPosition(new PointF(_currentPointX, _currentPointY), true);

            //chart1.ChartAreas[0].CursorY.SetCursorPixelPosition(new PointF(_currentPointX, _currentPointY), true);
            //Application.DoEvents();

            //try
            //{

            //    if (e.Button == System.Windows.Forms.MouseButtons.Left)
            //    {
            //        if (mousePoint.IsEmpty)
            //            mousePoint = e.Location;
            //        else
            //        {

            //            int newy = chart1.ChartAreas[0].Area3DStyle.Rotation + (e.Location.X - mousePoint.X);
            //            if (newy < -180)
            //                newy = -180;
            //            if (newy > 180)
            //                newy = 180;

            //            chart1.ChartAreas[0].Area3DStyle.Rotation = newy;

            //            newy = chart1.ChartAreas[0].Area3DStyle.Inclination + (e.Location.Y - mousePoint.Y);
            //            if (newy < -90)
            //                newy = -90;
            //            if (newy > 90)
            //                newy = 90;

            //            chart1.ChartAreas[0].Area3DStyle.Inclination = newy;

            //            mousePoint = e.Location;
            //        }
            //    }
            //    Point location = e.Location;
            //    if (!this.prevPosition.HasValue || (location != this.prevPosition.Value))
            //    {
            //        this.tooltip.RemoveAll();
            //        this.prevPosition = new Point?(location);
            //        HitTestResult[] resultArray = this.chart1.HitTest(location.X, location.Y, false, new ChartElementType[] { ChartElementType.DataPoint });
            //        foreach (HitTestResult result in resultArray)
            //        {
            //            if (result.ChartElementType == ChartElementType.DataPoint)
            //            {
            //                DataPoint point2 = result.Object as DataPoint;
            //                if (point2 != null)
            //                {
            //                    double num = result.ChartArea.AxisX.ValueToPixelPosition(point2.XValue);
            //                    double num2 = result.ChartArea.AxisY.ValueToPixelPosition(point2.YValues[0]);
            //                    if ((Math.Abs((double)(location.X - num)) < 2.0) && (Math.Abs((double)(location.Y - num2)) < 2.0))
            //                        if (Mode != SpectometrMode.Absorbance)
            //                        {
            //                            this.tooltip.Show(string.Concat(new object[] { "X=", point2.XValue.ToString("N2"), ", Y=", point2.YValues[0].ToString("N2") }), this.chart1, location.X, location.Y - 15);
            //                            lblposition.Text = "X=" + point2.XValue.ToString("N2") + " , " + "Y=" + point2.YValues[0].ToString("N2");
            //                        }
            //                        else
            //                        {
            //                            this.tooltip.Show(string.Concat(new object[] { "X=", point2.XValue.ToString("N4"), ", Y=", point2.YValues[0].ToString("N4") }), this.chart1, location.X, location.Y - 15);
            //                            lblposition.Text = "X=" + point2.XValue.ToString("N4") + " , " + "Y=" + point2.YValues[0].ToString("N4");
            //                        }


            //                }
            //            }
            //        }
            //    }
            //}
            //catch { }

        }

        private void message(string txt, bool i)
        {

            New message1 = new New();
            if (i)
            {
                message1.pictureBox1.Image = Properties.Resources.ok;

            }
            else if (!i)
            {
                message1.pictureBox1.Image = Properties.Resources.alert;
            }
            message1.label1.Text = txt;
            message1.Show();
        }
        private void btnRefrence_Click(object sender, EventArgs e)
        {
           
            if (!isRun)
            {
                message("selcet start  for save reference ", false);
                return;
            }

            if (Mode == SpectometrMode.Scope || Mode == SpectometrMode.Irradiance)
            {
                var a = @"Reference.dat";
              
              //  EnvironmentPermission envPerm1 = new EnvironmentPermission(EnvironmentPermissionAccess.AllAccess,System.Environment.CurrentDirectory);
                

                dtAnalys.SavePacket(refDt, a);
                //MessageBox.Show("Refrence Saved");
                //New message = new New();
                //message.pictureBox1.Image = Properties.Resources.ok;
                //message.label1.Text  = "Refrence Saved";
                //message.Show();
                message("Reference Saved", true);

                refrenceData = dtAnalys.ReadSavedPacket("Reference.dat");
                DarkRefreceInvalid = true;
            }
            else
            {
                message("Reference must be in Scope Mode or  Irradiance Mode and Run Mode ", false);
            }
        }

        private void btnScope_Click(object sender, EventArgs e)
        {


            foreach (Control item in _pnl_options.Controls)
            {
                item.ForeColor = Color.Gray;

            }
            btnScope.ForeColor = Color.White;
            tooltip.RemoveAll();
            foreach (ToolStripMenuItem item in viewToolStripMenuItem.DropDownItems)
                item.Checked = false;
            scopeModeToolStripMenuItem.Checked = true;
            //set enable option 
           
            scopeModeToolStripMenuItem.Checked = true;
            Mode = SpectometrMode.Scope;
            LoadSofwareProperties();
            this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.ZoomReset();
            this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.ZoomReset();
            this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum = _ScopeX1;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum = _ScopeX2;
            ;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Interval = 50;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Minimum = _ScopeY1;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum = _ScopeY2;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Interval = 1000;
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "{0}";
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0}";
            //  this.chart1.Series[this.ExperimentName].Points.AddXY((double)0.0, (double)0.0);
            if (SoftwarePr.XvalNM)
            this.chart1.ChartAreas["ChartArea1"].AxisX.Title = "Wavelength (nm)";
            else if (SoftwarePr.XvalCM)
                this.chart1.ChartAreas["ChartArea1"].AxisX.Title = " ( cm" + " \u23BB¹ )";
            this.chart1.ChartAreas["ChartArea1"].AxisY.Title = " Intensity (count) ";
            label8.Text = Mode.ToString();
            HideColorBar();


        }

        private void btnAbsorbance_Click(object sender, EventArgs e)
        {
            if (!DarkRefreceInvalid)
            {
                label9.Text = "Dark Data or Reference Data Invalid";
                return;
            }
            label9.Text = "";
            tooltip.RemoveAll();
            foreach (Control item in _pnl_options.Controls)
            {
                item.ForeColor = Color.Gray;

            }
            foreach (ToolStripMenuItem item in viewToolStripMenuItem.DropDownItems)
                item.Checked = false;
            absorbanceModeToolStripMenuItem.Checked = true;
            //set enable option 
            btnAbsorbance.ForeColor = Color.White;
            Mode = SpectometrMode.Absorbance;
            this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.ZoomReset();
            this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.ZoomReset();
            LoadSofwareProperties();
            //   this.chart1.ChartAreas["ChartArea1"].AxisY.IntervalType = DateTimeIntervalType.Number;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum = _AbsorbanceX1;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum = _AbsorbanceX2;

            this.chart1.ChartAreas["ChartArea1"].AxisY.Minimum = _AbsorbanceY1;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum = _AbsorbanceY2;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Interval = 50;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Interval = 0.2;


            this.chart1.ChartAreas["ChartArea1"].CursorY.Interval = 0.01;


            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "{0.0}";
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0}";

            

            this.chart1.ChartAreas["ChartArea1"].AxisX.Title = "Wavelength (nm)";
               if (SoftwarePr.XvalCM)
                this.chart1.ChartAreas["ChartArea1"].AxisX.Title = " ( cm" + " \u23BB¹ )";
            this.chart1.ChartAreas["ChartArea1"].AxisY.Title = "Absorbance (OD) ";
            //     this.chart1.ChartAreas["ChartArea1"].AxisX.TitleFont = new Font("Times New Roman", 14, FontStyle.Bold);


            label8.Text = Mode.ToString();
            HideColorBar();
        }

        private void btnTransmittance_Click(object sender, EventArgs e)
        {
            if (!DarkRefreceInvalid)
            {
                label9.Text = "Dark Data or Reference Data Invalid";
                return;
            }
            label9.Text = "";
            tooltip.RemoveAll();
            foreach (Control item in _pnl_options.Controls)
            {
                item.ForeColor = Color.Gray;

            }
            foreach (ToolStripMenuItem item in viewToolStripMenuItem.DropDownItems)
                item.Checked = false;
            transmittanceModeToolStripMenuItem.Checked = true;
            //set enable option 
            btnTransmittance.ForeColor = Color.White;

            Mode = SpectometrMode.Transmittance;
            LoadSofwareProperties();
            this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.ZoomReset();
            this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.ZoomReset();
            this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum = _TransmittanceX1; ;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum = _TransmittanceX2;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Interval = 50;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Minimum = _TransmittanceY1;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum = _TransmittanceY2;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Interval = 10;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Title = "Wavelength (nm)";
            if (SoftwarePr.XvalCM)
                this.chart1.ChartAreas["ChartArea1"].AxisX.Title = " ( cm" + " \u23BB¹ )";
            this.chart1.ChartAreas["ChartArea1"].AxisY.Title = "Transmission (%)      ";
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "{0}";
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0}";
            label8.Text = Mode.ToString();
            HideColorBar();
        }
        
        private void btnIrradiance_Click(object sender, EventArgs e)
        {
            if (!DarkRefreceInvalid)
            {
                label9.Text = "Dark Data or Reference Data Invalid";
                return;
            }
            label9.Text = "";
            tooltip.RemoveAll();
            foreach (Control item in _pnl_options.Controls)
            {
                item.ForeColor = Color.Gray;

            }
            //set enable option 

            foreach (ToolStripMenuItem item in viewToolStripMenuItem.DropDownItems)
                item.Checked = false;
            ReflectanceModetoolStripMenuItem2.Checked = true;
            btnReflectance.ForeColor = Color.White;
            Mode = SpectometrMode.Reflectance;
            LoadSofwareProperties();
            this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.ZoomReset();
            this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.ZoomReset();
            this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum = _TransmittanceX1; ;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum = _TransmittanceX2;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Interval = 50;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Minimum = _TransmittanceY1;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum = _TransmittanceY2;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Interval = 10;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Title = "Wavelength (nm)";
            if (SoftwarePr.XvalCM)
                this.chart1.ChartAreas["ChartArea1"].AxisX.Title = " ( cm" + " \u23BB¹ )";
            this.chart1.ChartAreas["ChartArea1"].AxisY.Title = "Reflection (%)     ";
            label8.Text = Mode.ToString();
            HideColorBar();
        }
        bool IsDeleteEnd = false;
        private void btnDelete_Click(object sender, EventArgs e)
        {
           
            
            if (chart1.Series.Count > 0)
            {
                if ((chart1.Series.Count == 1 && !chart1.Series.IsUniqueName("UV-IR")) || chart1.Series.Count == 0)
                {
                    isRun = false;
                    btnStart.Image = Properties.Resources.Media_Play2;
                    btnStart.Text = "Start";
                    ComportInterfacecs.StopReadingImageSensor();
                    ComportInterfacecs.DiscardBufferPort();
                    ComportInterfacecs.StopRead = true;
                    btnDelete.Enabled = false;
                    chTimeSeries.Checked = false;
                    IsDeleteEnd = true;
                    return;
                }
                //if (cmbSeriesName.Items.Count <= 0)
                //{
                //    btnStart.PerformClick();
                //    btnDelete.Enabled = false;
                //    IsDeleteEnd = true;

                //    return;
                //}
                if (cmbSeriesName.SelectedItem == "1nd Derivations")
                    ndDerivationsToolStripMenuItem2.Checked = false;
                if (cmbSeriesName.SelectedItem == "2nd Derivations")
                    ndDerivationsToolStripMenuItem3.Checked = false;
                if (cmbSeriesName.SelectedItem == "3nd Derivations")
                    ndDerivationsToolStripMenuItem4.Checked = false;
                if (cmbSeriesName.SelectedItem == "4nd Derivations")
                    ndDerivationsToolStripMenuItem5.Checked = false;
              
                
                Series seriesDelete = chart1.Series[cmbSeriesName.Text];
                chart1.Series.Remove(seriesDelete);
                cmbSeriesName.Items.Remove(cmbSeriesName.SelectedItem);
                cmbSeriesName.Items.Clear();
                cmbSeriesName.Text = "";


                foreach (Series s in chart1.Series)
                    if (s.Name!="UV-IR")
                    cmbSeriesName.Items.Add(s.Name);

                if (cmbSeriesName.Items.Count > 0)

                {

                    cmbSeriesName.SelectedItem = cmbSeriesName.Items[cmbSeriesName.Items.Count - 1];
                    if (cmbSeriesName.SelectedItem == "1nd Derivations" || cmbSeriesName.SelectedItem == "2nd Derivations" || cmbSeriesName.SelectedItem == "3nd Derivations" || cmbSeriesName.SelectedItem == "4nd Derivations")
                        return;
                    else
                        ExperimentName = cmbSeriesName.Items[cmbSeriesName.Items.Count - 1].ToString();
                }
               
            }

        }
        System.Windows.Forms.Timer tLamp = new System.Windows.Forms.Timer();
        private void btnlamp_Click(object sender, EventArgs e)
        {
            LoadSofwareProperties();
            if (numPixel ==580)
            {
                if (!tangestanIs)
                {
                    if (SoftwarePr.NanoLed)
                    {
                        ComportInterfacecs.NanoLED5LightSourceControl(true);
                        ComportInterfacecs.NanoLED6LightSourceControl(true);
                    }
                if (SoftwarePr.Dutrium)
                        ComportInterfacecs.NanoAllLightSourceControl(true);

                    btnlamp.Image = Properties.Resources.rec;
                    tangestanIs = true;
                    return;
                }
                else
                {

                    //  ComportInterfacecs.DeuteriumLightSourceControl(false);
                    ComportInterfacecs.NanoLED5LightSourceControl(false);
                    ComportInterfacecs.NanoLED6LightSourceControl(false);
                    ComportInterfacecs.NanoAllLightSourceControl(false);
                    btnlamp.Image = Properties.Resources.rec__1_;
                    tangestanIs = false;
                    return;
                   
                }
            }
            if (!tangestanIs)
            {

                if (SoftwarePr.Tngestan && SoftwarePr.Dutrium)
                    ComportInterfacecs.AllLightSourceControl(false);
                else if (SoftwarePr.Tngestan && !SoftwarePr.Dutrium)
                    ComportInterfacecs.TungstenLightSourceControl(false);
                else if (!SoftwarePr.Tngestan && SoftwarePr.Dutrium)

                    ComportInterfacecs.DeuteriumLightSourceControl(false);


                ComportInterfacecs.SetIntensityValueOfTungstenLamp(Convert.ToByte(numricalLampBrightnes.Value));


                btnlamp.Image = Properties.Resources.rec;
                tangestanIs = true;
            }
            else
            {

                //  ComportInterfacecs.DeuteriumLightSourceControl(false);
                ComportInterfacecs.TungstenLightSourceControl(true);
                ComportInterfacecs.DeuteriumLightSourceControl(true);

                tangestanIs = false;
                tLamp.Interval = 30000;
                tLamp.Tick += TLamp_Tick;
                tLamp.Start();
                btnlamp.Enabled = false;
            }

        }

        private void TLamp_Tick(object sender, EventArgs e)
        {
            btnlamp.Image = Properties.Resources.rec__1_;

            btnlamp.Enabled = true;
            btnlamp.ForeColor = Color.White;
            tLamp.Stop();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Scope (.spec)|*.spec|Absorbance (.abs)|*.abs|Irradiance (.ira)|*.ira|Raman (.ram)|*.ram|Reflectance (.ref)|*.ref|Transmittance (.tra)|*.tra|Fluorescence (.flu)|*.flu";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                chart1.Serializer.Format = System.Windows.Forms.DataVisualization.Charting.SerializationFormat.Binary;

              
                chart1.Serializer.Save(saveFileDialog1.FileName );
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Scope (.spec)|*.spec|Absorbance (.abs)|*.abs|Irradiance (.ira)|*.ira|Raman (.ram)|*.ram|Reflectance (.ref)|*.ref|Transmittance (.tra)|*.tra|Fluorescence (.flu)|*.flu";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //  chart1.Serializer.Reset();
                chart1.Serializer.Format = System.Windows.Forms.DataVisualization.Charting.SerializationFormat.Binary;

                //chart1.Serializer.SerializableContent += ",DataPoint.AxisLabel,Series.AxisLabels,Series.Name,ChartArea.Name";
                //chart1.Serializer.Content= SerializationContents.Appearance;
                chart1.Serializer.Load(openFileDialog1.FileName);
                chart1.Serializer.IsResetWhenLoading = true;
                cmbSeriesName.Items.Clear();
                cmbSeriesName.Text = "";
                foreach (Series s in chart1.Series)
                {
                    if (s.Name != "UV-IR")
                        cmbSeriesName.Items.Add(s.Name);
                }
                cmbSeriesName.SelectedItem = cmbSeriesName.Items[cmbSeriesName.Items.Count - 1];
                string ext = Path.GetExtension(openFileDialog1.FileName);
                if (ext == ".spec")
                {
                    Mode = SpectometrMode.Scope;
                    btnScope_Click(sender, e);
                }

                else if (ext == ".abs")
                {
                    Mode = SpectometrMode.Absorbance;
                    btnAbsorbance_Click(sender, e);
                }
                else if (ext == ".ira")
                {
                    Mode = SpectometrMode.Irradiance;
                    button3_Click(sender, e);
                }
                else if (ext == ".ram")
                {
                    Mode = SpectometrMode.Raman;
                    ramanToolStripMenuItem_Click(sender, e);
                }
                else if (ext == ".ref")
                {
                    Mode = SpectometrMode.Reflectance;
                    btnIrradiance_Click(sender, e);
                }
                else if (ext == ".tra")
                {
                    Mode = SpectometrMode.Transmittance;
                    btnTransmittance_Click(sender, e);
                }
                else if (ext == ".flu")
                {
                    Mode = SpectometrMode.Fluorescence;
                    button3.PerformClick();
                }



            }


        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }



        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog svpic = new SaveFileDialog();
            svpic.Filter = "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png|Tiff Image (.tiff)|*.tiff|Wmf Image (.wmf)|*.wmf";
            if (svpic.ShowDialog() == DialogResult.OK)
            {
                string tempFile = System.IO.Path.GetTempPath() + "Temp";
                string a = "Int :" + " " + numricIntegrationTime.Text;
                string b = "Ave :" + " " + numricalAverage.Text;
                string c = "Smo :" + " " + numrSmosthing.Text;
                string d = "Mode : " + " " + Mode.ToString();
                PointF firstLocation = new PointF(chart1.Width - 150, chart1.Height - 150);
                PointF s = new PointF(40, 20);

                chart1.SaveImage(tempFile, ChartImageFormat.Jpeg);
                Bitmap bitmap = (Bitmap)Image.FromFile(tempFile);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Font arialFont = new Font("Arial", 10, FontStyle.Regular))
                    {
                        graphics.DrawString(d, arialFont, Brushes.Black, s);
                        graphics.DrawString(a, arialFont, Brushes.Black, s.X + 150, s.Y);
                        graphics.DrawString(b, arialFont, Brushes.Black, s.X + 225, s.Y);
                        graphics.DrawString(c, arialFont, Brushes.Black, s.X + 300, s.Y);

                        //graphics.DrawString(d, arialFont, Brushes.Black    , firstLocation);
                        //graphics.DrawString(a, arialFont, Brushes.Black, firstLocation.X, firstLocation.Y + 20);
                        //graphics.DrawString(b, arialFont, Brushes.Black, firstLocation.X, firstLocation.Y + 40);
                        //graphics.DrawString(c, arialFont, Brushes.Black, firstLocation.X, firstLocation.Y +60);

                    }
                }

                bitmap.Save(svpic.FileName);
                imageToolStripMenuItem.Checked = false;
                exportToolStripMenuItem1.Checked = false;
            }


        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDialog ppd = new PrintDialog();
            this.chart1.Printing.Print(false);
            ppd.Document = this.chart1.Printing.PrintDocument;
            ppd.ShowDialog();

        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog ppd = new PrintPreviewDialog();

            ppd.Document = this.chart1.Printing.PrintDocument;
            ppd.Document.DefaultPageSettings.Landscape = true;
            ppd.ShowDialog();
        }
        bool wait = false;
        private String Number2String(int number)
        {
            string result = "";

            if (number > 26 && number <= 52)
            {

                Char c = Convert.ToChar(96 + (number - 26)) ;
                result = "a" + c.ToString();
            }
            else if (number > 52)
            {

                Char c = (Char) (96 + (number - 52));
                result = "b" + c.ToString();
            }
            else
            {
                Char c = (Char)(96 + number);
                result = c.ToString();
            }
            return result;
        }

        private void ExportToExcel()
        {


            try
            {
                if (InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        bool firstCol = true;

                        progressBar1.Visible = true;
                        Microsoft.Office.Interop.Excel.Application xlApp;
                        Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet1;
                        object misValue = System.Reflection.Missing.Value;
                        xlApp = new Microsoft.Office.Interop.Excel.Application();
                        xlWorkBook = xlApp.Workbooks.Add(misValue);
                        xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                        Microsoft.Office.Interop.Excel.ChartObjects xlCharts = (Microsoft.Office.Interop.Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
                        Microsoft.Office.Interop.Excel.ChartObject myChart = (Microsoft.Office.Interop.Excel.ChartObject)xlCharts.Add(500, 100, 500, 500);



                        Microsoft.Office.Interop.Excel.Chart chartPage = myChart.Chart;
                        Microsoft.Office.Interop.Excel.SeriesCollection ser = chartPage.SeriesCollection();
                        chartPage.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine;
                        int SeresNmae = 1;

                        int c = 0;
                        int step = 0;

                        if (export.checkBox2.Checked)
                            step = 1;
                        else
                            step = Convert.ToUInt16(export.comboBox1.SelectedItem.ToString()) * 3;
                        //foreach (var series in chart1.Series)
                        //    c += series .Points.Count;
                        for (int m = 0; m < export.checkBoxComboBox1.Items.Count; m++)
                        {
                            if (export.checkBoxComboBox1.CheckBoxItems[m].Checked)
                            {
                                c += (int)chart1.Series[m].Points.Count;
                            }
                        }

                        progressBar1.Minimum = 0;
                        progressBar1.Maximum = (int)(c / step) + export.checkBoxComboBox1.Items.Count+1;
                        progressBar1.Value = 0;
                        xlWorkSheet.Cells[1] = "Wavelength";

                        for (int m = 0; m < export.checkBoxComboBox1.Items.Count; m++)
                        {
                            if (export.checkBoxComboBox1.CheckBoxItems[m].Checked)
                            {

                                Series item = chart1.Series[export.checkBoxComboBox1.CheckBoxItems[m].Text];
                                int min = (int)this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum;
                                int max = (int)this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum;

                                SeresNmae++;
                                xlWorkSheet.Cells[SeresNmae] = item.Name;

                                int i = 2;
                                //  if (item.Name == ExperimentName)
                                //  wait = true;


                                for (int j = 0; j < numPixel; j = j + step)
                                {
                                    if (progressBar1.Value == progressBar1.Maximum)
                                        progressBar1.Maximum += 1;
                                    progressBar1.Value++;

                                    if (firstCol)
                                        xlWorkSheet.Cells[i, 1] = item.Points[j].XValue.ToString("N0");
                                    var y = item.Points[j].YValues[0];
                                    xlWorkSheet.Cells[i, SeresNmae] = y.ToString("N" + export.cmbpoint.SelectedItem.ToString());





                                    i++;


                                }

                                firstCol = false;
                                Microsoft.Office.Interop.Excel.Series series1 = ser.NewSeries();
                                series1.Name = item.Name;
                                string colY = Number2String(SeresNmae) + "2" + ":" + Number2String(SeresNmae) + i.ToString();
                                string colX = "A2:A" + i.ToString();
                                series1.Values = xlWorkSheet.get_Range(colY);
                                series1.XValues = xlWorkSheet.get_Range(colX);




                            }

                        }



                        xlWorkSheet.Name = Mode.ToString();
                        xlWorkBook.SaveAs(saveExcel.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        xlWorkBook.Close(true, misValue, misValue);
                        xlApp.Quit();

                        releaseObject(xlWorkSheet);
                        releaseObject(xlWorkBook);
                        releaseObject(xlApp);

                        System.Diagnostics.Process.Start(saveExcel.FileName);
                        message("Excel file created ", true);
                        firstCol = true;
                        progressBar1.Visible = false;


                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Dispose();
                progressBar1.Visible = false;

            }
            finally
            {
                GC.Collect();

            }
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
                message("Exception Occured while releasing object ", false);

            }
            finally
            {
                GC.Collect();
            }
        }
        SaveFileDialog saveExcel = new SaveFileDialog();
        FExport export = new FExport();
        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            export = new FExport();

            export.checkBoxComboBox1.Items.AddRange(cmbSeriesName.Items.Cast<Object>().ToArray());
            export.checkBoxComboBox1.SelectedItem = export.checkBoxComboBox1.Items[0];
            export.cmbpoint.SelectedItem = export.cmbpoint.Items[0];
            export.comboBox1.SelectedItem = export.comboBox1.Items[0];
            // export.checkBoxComboBox1.CheckBoxItems[0].Checked = true;  
            int count = 0;


            if (export.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < chart1.Series.Count-1; i++)
                {
                    if (export.checkBoxComboBox1.CheckBoxItems[i].Checked)
                        count++;

                }
                if (count <= 0)

                {
                    MessageBox.Show("please Select the items for export data");
                    excelToolStripMenuItem.Checked = false;
                    exportToolStripMenuItem1.Checked = false;
                    return;
                }
                saveExcel.Title = "Export to Excel";
                saveExcel.Filter = "Excel File|*.xlsx";
                saveExcel.Tag = "Scpectomer analysis";
                saveExcel.ShowDialog();

                Thread t = new Thread(ExportToExcel);
                t.Start();
            }

            excelToolStripMenuItem.Checked = false;
            exportToolStripMenuItem1.Checked = false;


            count = 0;


        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            //    txtLampBrightness.Text =(Convert.ToInt16 (txtLampBrightness.Text )- 1).ToString();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            //   txtLampBrightness.Text = (Convert.ToInt16(txtLampBrightness.Text) + 1).ToString();
            //  getExcelFile();

        }

        private void contentToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (isRun)
            {
                btnStart.Image = Properties.Resources.Media_Play2;
                btnStart.Text = "Start";
                isRun = false;
                ComportInterfacecs.StopReadingImageSensor();
            }
            this.numricalAverage.Value = 1;
            //  this.ComportInterfacecs .OneTimeImageSensorRead (Convert.ToUInt32(numricIntegrationTime.Value)*1000);
        }





        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!DarkRefreceInvalid)
            {
                label9.Text = "Dark Data or Reference Data Invalid";
                return;
            }
            label9.Text = "";
            tooltip.RemoveAll();
            foreach (Control item in _pnl_options.Controls)
            {
                item.ForeColor = Color.Gray;

            }
            foreach (ToolStripMenuItem item in viewToolStripMenuItem.DropDownItems)
                item.Checked = false;
            irradianceModeToolStripMenuItem.Checked = true;
            Mode = SpectometrMode.Irradiance;
            //set enable option 
            btnIrradiance.ForeColor = Color.White;
            LoadSofwareProperties();
            this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.ZoomReset();
            this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.ZoomReset();
            this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum = _IrradianceX1;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum = _IrradianceX2;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Interval = 50;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Minimum = _IrradianceY1;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum = _IrradianceY2;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Interval = 1000;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Title = "Wavelength (nm)";
            if (SoftwarePr.XvalCM)
                this.chart1.ChartAreas["ChartArea1"].AxisX.Title = " ( cm" + " \u23BB¹ )";
            this.chart1.ChartAreas["ChartArea1"].AxisY.Title = "Irradiance     ";
            label8.Text = Mode.ToString();
            HideColorBar();

        }

        private void ramanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click_1(sender, e);


        }

        double zoomx = 0;
        double zoomY = 0;
        double zoomCOunter = 0;
        int stepZ = 0;

        private void btnZoomX_Click(object sender, EventArgs e)
        {
            span /= 1.2;  // zoom in 2x each time
                          // spany /= 1.2;
            Axis ax = chart1.ChartAreas[0].AxisX;
            //  Axis ay = chart1.ChartAreas[0].AxisY;
            ax.ScaleView.Zoom(nextDPoint.X - span, nextDPoint.X + span);
            // ay.ScaleView.Zoom(nextDPoint.Y - spany, nextDPoint.Y + spany);
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0}";

            //Axis ax = chart1.ChartAreas[0].AxisX;
            //this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum = this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum - 100; ;
            //this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum = this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum + 100; ;
            //this.chart1.ChartAreas["ChartArea1"].AxisX.Interval = this.chart1.ChartAreas["ChartArea1"].AxisX.Interval-50;
            //ax.ScaleView.Zoom(this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum , this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum );

            #region oldcode
            //   ax.ScaleView.Size = double.IsNaN(ax.ScaleView.Size) ?
            //  (ax.Maximum - ax.Minimum) / 2 : ax.ScaleView.Size /= 2;

            //int     step = (int) (chart1.ChartAreas["ChartArea1"].AxisX.Maximum - chart1.ChartAreas["ChartArea1"].AxisX.Minimum)/20;
            //if (zoomx>chart1.ChartAreas["ChartArea1"].AxisX.Maximum )
            //{
            //    zoomx-= step;
            //   // zoomx-= this.chart1.ChartAreas["ChartArea1"].AxisX.
            //}
            //else
            //    zoomx += step ;
            //this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoom(this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum+ zoomx, this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum  - zoomx);
            //zoomCOunter += 1;
            #endregion
        }

        private void btnZomY_Click(object sender, EventArgs e)
        {
            //  span /= 2;  // zoom in 2x each time
            spany /= 1.2;
            // Axis ax = chart1.ChartAreas[0].AxisX;
            Axis ay = chart1.ChartAreas[0].AxisY;
            // ax.ScaleView.Zoom(nextDPoint.X - span, nextDPoint.X + span);
            ay.ScaleView.Zoom(nextDPoint.Y - spany, nextDPoint.Y + spany);
            //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0}";

            //Axis ay = chart1.ChartAreas[0].AxisY;
            //this.chart1.ChartAreas["ChartArea1"].AxisY.Minimum = this.chart1.ChartAreas["ChartArea1"].AxisY.Minimum - 100; ;
            //this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum = this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum + 100; ;
            //this.chart1.ChartAreas["ChartArea1"].AxisY.Interval = this.chart1.ChartAreas["ChartArea1"].AxisY.Interval - 50;
            //ay.ScaleView.Zoom(this.chart1.ChartAreas["ChartArea1"].AxisY.Minimum, this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum);


            #region 
            //  ay.ScaleView.Size= double.IsNaN(ay.ScaleView.Size) ?
            //     (ay.Maximum - ay.Minimum) / 2 : ay.ScaleView.Size /= 2;

            //double step = 0;

            //if (Mode == SpectometrMode.Absorbance)
            //{
            //    step = 0.2f;



            //}
            //else
            //    step = (double)(chart1.ChartAreas["ChartArea1"].AxisY.Maximum - chart1.ChartAreas["ChartArea1"].AxisY.Minimum) / 20;
            //if (zoomY > chart1.ChartAreas["ChartArea1"].AxisY.Maximum)
            //{
            //    zoomY -= step;
            //    // zoomx-= this.chart1.ChartAreas["ChartArea1"].AxisX.
            //}
            //else
            //    zoomY += step;
            //this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoom(this.chart1.ChartAreas["ChartArea1"].AxisY.Minimum + zoomY, this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum - zoomY);
            //zoomCOunter += 1;
            #endregion

        }
        private bool IsPanninig = false;
        private void btnReset_Click(object sender, EventArgs e)
        {
            span *= 1.5;  // zoom in 2x each time
            spany *= 1.5; 
            Axis ax = chart1.ChartAreas[0].AxisX;
            Axis ay = chart1.ChartAreas[0].AxisY;
            ax.ScaleView.Zoom(nextDPoint.X - span, nextDPoint.X + span);
            ay.ScaleView.Zoom(nextDPoint.Y - spany, nextDPoint.Y + spany);
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0}";
            #region
            //IsPanninig = !IsPanninig;
            ////chart1.ChartAreas[0].RecalculateAxesScale();
            ////Series s = chart1.Series[0];
            ////ChartArea ca = chart1.ChartAreas[0];
            ////ca.AxisX.Maximum = s.Points.Select(x => x.XValue).Max() - 100;
            ////ca.AxisX.Minimum = s.Points.Select(x => x.XValue).Min() - 100;
            ////ca.AxisX.ScaleView.Zoom(ca.AxisX.Minimum, ca.AxisX.Maximum);
            ////ca.AxisY.ScaleView.Zoom(ca.AxisY.Minimum - 100, ca.AxisY.Maximum - 100);

            //Axis ax = chart1.ChartAreas[0].AxisX;
            //Axis ay = chart1.ChartAreas[0].AxisY;
            //if(ax.ScaleView.IsZoomed)
            //ax.ScaleView.Size = double.IsNaN(ax.ScaleView.Size) ?
            //                    ax.Maximum : ax.ScaleView.Size *= 1.2;

            //if (ax.ScaleView.Size > ax.Maximum - ax.Minimum)
            //{
            //    ax.ScaleView.Zoom(ax.Minimum, ax.Maximum);
            //    // ax.ScaleView.Size = ax.Maximum;
            //    //ax.ScaleView.Position = ax.Minimum;

            //}

            //if(ay.ScaleView.IsZoomed)
            //ay.ScaleView.Size = double.IsNaN(ay.ScaleView.Size) ?
            //                    ay.Maximum : ay.ScaleView.Size *= 1.2;
            //if (ay.ScaleView.Size > ay.Maximum - ay.Minimum)
            //{

            //    //ay.ScaleView.Size = ay.Maximum;
            //    //ay.ScaleView.Position = ay.Minimum ;
            //    ay.ScaleView.Zoom(ay.Minimum, ay.Maximum);
            //      chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset(1);

            //}
            //this.chart1.ChartAreas["ChartArea1"].AxisX.Interval = this.chart1.ChartAreas["ChartArea1"].AxisX.Interval + ((this.chart1.ChartAreas["ChartArea1"].AxisX.Interval * 2) / 100);
            //this.chart1.ChartAreas["ChartArea1"].AxisY.Interval = this.chart1.ChartAreas["ChartArea1"].AxisY.Interval +((this.chart1.ChartAreas["ChartArea1"].AxisY.Interval * 2) / 100);
            #endregion

        }
        double step = 0;
        double step1 = 0;
        private void btnZoomXY_Click(object sender, EventArgs e)
        {
            #region 
            ////Axis ax = chart1.ChartAreas[0].AxisX;
            ////ax.ScaleView.Size = double.IsNaN(ax.ScaleView.Size) ?
            ////                    (ax.Maximum - ax.Minimum) / 1.2 : ax.ScaleView.Size /= 1.2;

            ////Axis ay = chart1.ChartAreas[0].AxisY;
            ////ay.ScaleView.Size = double.IsNaN(ay.ScaleView.Size) ?
            ////                    (ay.Maximum - ay.Minimum) / 1.2 : ay.ScaleView.Size /= 1.2;
            ////this.chart1.ChartAreas["ChartArea1"].AxisX.Interval = this.chart1.ChartAreas["ChartArea1"].AxisX.Interval - ((this.chart1.ChartAreas["ChartArea1"].AxisX.Interval * 10) / 100);
            ////this.chart1.ChartAreas["ChartArea1"].AxisY.Interval = this.chart1.ChartAreas["ChartArea1"].AxisY.Interval - ((this.chart1.ChartAreas["ChartArea1"].AxisY.Interval * 10) / 100);

            //    step = (int)(chart1.ChartAreas["ChartArea1"].AxisX.Maximum - lastPoint.X ) / 20;
            //if (zoomx > chart1.ChartAreas["ChartArea1"].AxisX.Maximum)
            //{
            //    zoomx -= step;

            //}
            //else
            //    zoomx += step;
            //this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoom(lastPoint.X+ zoomx, this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum - zoomx);
            //zoomCOunter += 1;

            //if (Mode == SpectometrMode.Absorbance)
            //{
            //    step1 = 0.2f;
            //}
            //else
            //     step1 = (int)(chart1.ChartAreas["ChartArea1"].AxisY.Maximum - lastPoint.Y) / 20;
            //if (zoomY > chart1.ChartAreas["ChartArea1"].AxisY.Maximum)
            //{
            //    zoomY -= step1;

            //}
            //else
            //    zoomY += step1;
            //this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoom(lastPoint.Y+ zoomY, this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum - zoomY);
            //zoomCOunter += 1;
            #endregion

            span /= 2;  // zoom in 2x each time
            spany /= 2;
            Axis ax = chart1.ChartAreas[0].AxisX;
            Axis ay = chart1.ChartAreas[0].AxisY;
            ax.ScaleView.Zoom(nextDPoint.X - span, nextDPoint.X + span);
            ay.ScaleView.Zoom(nextDPoint.Y - spany, nextDPoint.Y + spany);
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0}";
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
            foreach (ToolStripMenuItem item in toolStripMenuItem3.DropDownItems)
                item.Checked = false;

            nanoDropToolStripMenuItem1.Checked = true;
            nanoDropFrm = new FNanoDrop();
            nanoDropFrm.Show();

            timeNanoDrop.Enabled = true;
            timeNanoDrop.Interval = 100;

        }

        private void TimeNanoDrop_Tick(object sender, EventArgs e)
        {
            nanoDropFrm.a230 = findPos(230);
            nanoDropFrm.a260 = findPos(260);
            nanoDropFrm.a280 = findPos(280);
            nanoDropFrm.a320 = findPos(320);

            if (nanoDropFrm.IsClose)

            {
                timeNanoDrop.Enabled = false;
                nanoDropToolStripMenuItem1.Checked = false;

            }
        }

        private void ndDerivationsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in derivationsToolStripMenuItem.DropDownItems)
                item.Checked = false;
            ndDerivationsToolStripMenuItem2.Checked = true;
            if (chart1.Series.IsUniqueName("1nd Derivations"))
            {
                //  ExperimentName = "1nd Derivations";
                this.chart1.Series.Add("1nd Derivations");


                cmbSeriesName.Items.Add("1nd Derivations");
            }

            //   ExperimentName = "1nd Derivations";
            cmbSeriesName.SelectedItem = "1nd Derivations";
            chart1.Series["1nd Derivations"].ChartType = (SeriesChartType)cmbChartType.SelectedItem;
            chart1.Series["1nd Derivations"].BorderWidth = trackBar1.Value;
            de = d.Nd1;
            chart1.Series["1nd Derivations"].Color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            chart1.Series["1nd Derivations"].Points.DataBindXY(xvalue, ND1(xvalue));


        }

        private void ndDerivationsToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in derivationsToolStripMenuItem.DropDownItems)
                item.Checked = false;
            ndDerivationsToolStripMenuItem3.Checked = true;
            if (chart1.Series.IsUniqueName("2nd Derivations"))
            {

                this.chart1.Series.Add("2nd Derivations");


                cmbSeriesName.Items.Add("2nd Derivations");
            }

            //ExperimentName = "2nd Derivations";
            cmbSeriesName.SelectedItem = "2nd Derivations";
            chart1.Series["2nd Derivations"].ChartType = (SeriesChartType)cmbChartType.SelectedItem;
            chart1.Series["2nd Derivations"].BorderWidth = trackBar1.Value;

            de = d.Nd2;
            chart1.Series["2nd Derivations"].Color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            chart1.Series["2nd Derivations"].Points.DataBindXY(xvalue, ND1(xvalue));

        }

        private void ndDerivationsToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in derivationsToolStripMenuItem.DropDownItems)
                item.Checked = false;
            ndDerivationsToolStripMenuItem4.Checked = true;
            if (chart1.Series.IsUniqueName("3nd Derivations"))
            {
                //  ExperimentName = "3nd Derivations";
                this.chart1.Series.Add("3nd Derivations");


                cmbSeriesName.Items.Add("3nd Derivations");
            }

            // ExperimentName = nd Derivations;
            cmbSeriesName.SelectedItem = "3nd Derivations";
            chart1.Series["3nd Derivations"].ChartType = (SeriesChartType)cmbChartType.SelectedItem;
            chart1.Series["3nd Derivations"].BorderWidth = trackBar1.Value;

            de = d.Nd3;
            chart1.Series["3nd Derivations"].Color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            chart1.Series["3nd Derivations"].Points.DataBindXY(xvalue, ND1(xvalue));
        }

        private void ndDerivationsToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in derivationsToolStripMenuItem.DropDownItems)
                item.Checked = false;
            ndDerivationsToolStripMenuItem5.Checked = true;
            if (chart1.Series.IsUniqueName("4nd Derivations"))
            {
                ///  ExperimentName = "4nd Derivations";
                this.chart1.Series.Add("4nd Derivations");


                cmbSeriesName.Items.Add("4nd Derivations");
            }

            // ExperimentName = "4nd Derivations";
            cmbSeriesName.SelectedItem = "4nd Derivations";
            chart1.Series["4nd Derivations"].ChartType = (SeriesChartType)cmbChartType.SelectedItem;
            chart1.Series["4nd Derivations"].BorderWidth = trackBar1.Value;

            de = d.Nd4;
            chart1.Series["4nd Derivations"].Color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            chart1.Series["4nd Derivations"].Points.DataBindXY(xvalue, ND1(xvalue));
        }
        FChart fchart = new FChart();
        double[] xColor, yColor, zCOlor;
        List<SPoint> sPoints;
        Color[] colormach = new Color[90000];
        double[] XvalColor = new double[90000];
        double[] YvalColor = new double[90000];


        private void colorMeasurementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in toolStripMenuItem3.DropDownItems)
                item.Checked = false;

            colorMeasurementToolStripMenuItem.Checked = true;
            btncolor_Click_1(sender, e);

        }

        private void btnFit_Click(object sender, EventArgs e)
        {
            zoomx = 0;
            zoomY = 0;
            chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
            chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
            if (Mode == SpectometrMode.Absorbance)
                btnAbsorbance_Click(sender, e);
            if (Mode == SpectometrMode.Scope)
                btnScope_Click(sender, e);
            if (Mode == SpectometrMode.Reflectance)
                btnIrradiance_Click(sender, e);
            if (Mode == SpectometrMode.Transmittance)
                btnTransmittance_Click(sender, e);
            if (Mode == SpectometrMode.Irradiance)
                button3_Click(sender, e);
            if (Mode == SpectometrMode.Raman)

                ramanToolStripMenuItem_Click(sender, e);

        }

        private void refractiveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        ToolTip tooltip1 = new ToolTip();
        Point? clickPosition = null;
        DeviceCounter fSetting = new Forms.DeviceCounter();

        private void button1_Click(object sender, EventArgs e)
        {
            ComportInterfacecs.TungstenLightSourceControl(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ComportInterfacecs.TungstenLightSourceControl(false);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            ComportInterfacecs.DeuteriumLightSourceControl(false);
        }

        private void thinFilmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in toolStripMenuItem3.DropDownItems)
                item.Checked = false;

            thinFilmToolStripMenuItem.Checked = true;
            FRefractive refrective = new FRefractive();
            refrective.Show();
        }
        List<Point> points = new List<Point>();
        Point lastPoint = Point.Empty;
        double span = 0;
        double spany = 0;

        PointF nextDPoint = PointF.Empty;  // the next closest DataPoint's values
        DataPoint closePoint = null;       // the next closest DataPoint

        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {

                lastPoint = e.Location;
                Axis ax = chart1.ChartAreas[0].AxisX;
                Axis ay = chart1.ChartAreas[0].AxisY;

                if (closePoint != null) closePoint.MarkerColor = chart1.Series[0].MarkerColor;

                nextDPoint = new PointF((float)ax.PixelPositionToValue(lastPoint.X),
                                        (float)ay.PixelPositionToValue(lastPoint.Y));

                closePoint = chart1.Series[0].Points.Where(x => x.XValue >= nextDPoint.X).First();
                closePoint.MarkerColor = Color.Red;  // optionally mark the point

                // optionally move clicked position to actual datapoint
                nextDPoint = new PointF((float)closePoint.XValue, (float)closePoint.YValues[0]);

                span = ax.Maximum - ax.Minimum;  // the full range of values
                spany = ay.Maximum - ay.Minimum;
                chart1.Invalidate();
                var pos = e.Location;
                clickPosition = pos;

                var results = chart1.HitTest(pos.X, pos.Y, true,
                                             ChartElementType.PlottingArea);
                foreach (var result in results)
                {
                    if (result.ChartElementType == ChartElementType.PlottingArea)
                    {
                        var xVal = result.ChartArea.AxisX.PixelPositionToValue(pos.X);
                        var yVal = result.ChartArea.AxisY.PixelPositionToValue(pos.Y);
                        string xSrting = this.chart1.ChartAreas["ChartArea1"].AxisX.Title;
                        string yString = this.chart1.ChartAreas["ChartArea1"].AxisY.Title;
                        if (Mode != SpectometrMode.Absorbance)
                        {
                            tooltip.Show(xSrting.Remove(xSrting.IndexOf('(')) + "=" + xVal.ToString("N3") + "," + yString.Remove(yString.IndexOf('(')) + "=" + yVal.ToString("N3"), this.chart1, e.Location.X + 15, e.Location.Y - 15);
                            lblposition.Text = xSrting.Remove(xSrting.IndexOf('(')) + "=" + xVal.ToString("N2") + "," + yString.Remove(yString.IndexOf('(')) + "=" + yVal.ToString("N2");
                        }
                        else
                        {
                            tooltip.Show(xSrting.Remove(xSrting.IndexOf('(')) + "=" + xVal.ToString("N4") + "," + yString.Remove(yString.IndexOf('(')) + "=" + yVal.ToString("N4"), this.chart1, e.Location.X + 15, e.Location.Y - 15);
                            lblposition.Text = xSrting.Remove(xSrting.IndexOf('(')) + "=" + xVal.ToString("N4") + "," + yString.Remove(yString.IndexOf('(')) + "=" + yVal.ToString("N4");
                        }
                    }


                }
            }
            catch { }
        }

        PointF PolarValueToPixelPosition(DataPoint dp, Chart chart, ChartArea ca)
        {
            RectangleF ipp = InnerPlotPositionClientRectangle(chart, ca);
            double crossing = ca.AxisX.Crossing != double.NaN ? ca.AxisX.Crossing : 0;

            // for RangeChart change 90 zo 135 !
            float phi = (float)(360f / ca.AxisX.Maximum / 180f * Math.PI *
                     (dp.XValue - 90 + crossing));

            float yMax = (float)ca.AxisY.Maximum;
            float yMin = (float)ca.AxisY.Minimum;
            float radius = ipp.Width / 2f;
            float len = (float)(dp.YValues[0] - yMin) / (yMax - yMin);
            PointF C = new PointF(ipp.X + ipp.Width / 2f, ipp.Y + ipp.Height / 2f);

            float xx = (float)(Math.Cos(phi) * radius * len);
            float yy = (float)(Math.Sin(phi) * radius * len);
            return new PointF(C.X + xx, C.Y + yy);
        }
        float distance(PointF pt1, PointF pt2)
        {
            float d = (float)Math.Sqrt((pt1.X - pt2.X) * (pt1.X - pt2.X)
             + (pt1.Y - pt2.Y) * (pt1.Y - pt2.Y));
            return d;
        }
        RectangleF ChartAreaClientRectangle(Chart chart, ChartArea CA)
        {
            RectangleF CAR = CA.Position.ToRectangleF();
            float pw = chart.ClientSize.Width / 100f;
            float ph = chart.ClientSize.Height / 100f;
            return new RectangleF(pw * CAR.X, ph * CAR.Y, pw * CAR.Width, ph * CAR.Height);
        }
        bool CA, C2, C10, C65 = false;
        bool ColorIsRun = false;

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {

            //if (!ColorIsRun)
            //    SelectColor();

            //C65 = C10 = true;
            //CA = C2 = false;
        }

        private void fluorcscnceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3_Click_2(sender, e);

            //foreach (ToolStripMenuItem item in toolStripMenuItem3 .DropDownItems)
            //    item.Checked = false;

            //fluorcscnceToolStripMenuItem.Checked = true;
            //Mode = SpectometrMode.Fluorescence;
            //this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum = _ScopeX1;
            //this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum = _ScopeX2;
            //;
            //this.chart1.ChartAreas["ChartArea1"].AxisX.Interval = 200;
            //this.chart1.ChartAreas["ChartArea1"].AxisY.Minimum = _ScopeY1; ;
            //this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum = _ScopeY2; ;
            //this.chart1.ChartAreas["ChartArea1"].AxisY.Interval = 10000;
            //chart1.ChartAreas[0].AxisY.LabelStyle.Format = "{0}";
            //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0}";
            //this.chart1.Series[this.ExperimentName].Points.AddXY((double)0.0, (double)0.0);
            //this.chart1.ChartAreas["ChartArea1"].AxisX.Title = "Wavelength (nm)";
            //this.chart1.ChartAreas["ChartArea1"].AxisY.Title = "Intensity      ";
            //label8.Text = Mode.ToString();
        }

        private void plasmaMonitoringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in toolStripMenuItem3.DropDownItems)
                item.Checked = false;
            plasmaMonitoringToolStripMenuItem.Checked = true;
        }

        private void deviceInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            isRun = false;
            btnStart.Image = Properties.Resources.Media_Play2;
            btnStart.Text = "Start";
            ComportInterfacecs.StopReadingImageSensor();
            ComportInterfacecs.DiscardBufferPort();
            ComportInterfacecs.StopRead = true;

            ComportInterfacecs.GetDeviceDateVersionSerialNum();
            Thread.Sleep(100);
            //  ComportInterfacecs.GetDeviceTimers();


            deviceInfo.ShowDialog();
            //  ComportInterfacecs.GetIntensityValueOfTungstenLamp();


        }

        private void deviceCounterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isRun = false;
            btnStart.Image = Properties.Resources.Media_Play2;
            btnStart.Text = "Start";
            ComportInterfacecs.StopReadingImageSensor();
            ComportInterfacecs.DiscardBufferPort();
            ComportInterfacecs.StopRead = true;
            fSetting = new DeviceCounter();
            fSetting.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        System.Diagnostics.Stopwatch stopeatchTimeseries = new System.Diagnostics.Stopwatch();
        int timeSeriesInterval = 0;
        int timeSeriesDouration = 2;
        Thread threadTimeseri;



        private void chTimeSeries_CheckedChanged(object sender, EventArgs e)
        {

            if (chTimeSeries.Checked)
            {
                chTimeSeries.ForeColor = Color.AliceBlue;
                //threadTimeseri = new Thread(Ts);
                //threadTimeseri.Start();


                // stopeatchTimeseries.Start();
                timeSeriesInterval = Convert.ToInt32(txtTimeInterval.Text);
                timeSeriesDouration = Convert.ToInt32(txtDouration.Text);
                timeSeries.Interval = (int)TimeSpan.FromSeconds(timeSeriesInterval).TotalMilliseconds;
                timeSeriesD.Interval = (int)TimeSpan.FromMinutes(timeSeriesDouration).TotalMilliseconds;
                timeSeries.Enabled = true;
                timeSeriesD.Enabled = true;

                // stopeatchTimeseries.Start();
                //  timeSeries.Enabled = true;




            }
            else
            {

                message("Time Series Complete", true);
                chTimeSeries.ForeColor = Color.Red;
                timeSeriesD.Enabled = false;
                //  timeSeries.Enabled = false;
                // stopeatchTimeseries.Reset();
            }
        }

        private void TimeSeries_Tick(object sender, EventArgs e)
        {
            COlorSelect++;
            if (COlorSelect == 12)
                COlorSelect = 0;

            string text = "Spec" + Convert.ToString((int)(this.chart1.Series.Count + 1));
            ExperimentName = text;
            chart1.Series.Add(ExperimentName);
            chart1.Series[ExperimentName].ChartType = (SeriesChartType)cmbChartType.SelectedItem;

            //  chart1.Series[ExperimentName].Color = selectColor(COlorSelect);

            cmbSeriesName.Items.Add(ExperimentName);

            //cmbSeriesName.SelectedItem = ExperimentName;



        }
        [SecurityPermissionAttribute(SecurityAction.Demand, ControlThread = true)]
        private void KillTheThread()
        {
            threadTimeseri.Abort();
        }

        private Color selectColor(int i)
        {
            Color[] color = new Color[13];
            color[1] = Color.Red;
            color[2] = Color.Blue;
            color[3] = Color.Brown;
            color[4] = Color.DarkCyan;
            color[5] = Color.Gold;
            color[6] = Color.Green;
            color[7] = Color.HotPink;
            color[9] = Color.Orange;
            color[10] = Color.Pink;
            color[11] = Color.Orchid;
            color[12] = Color.DarkRed;

            return color[i];
        }
        int COlorSelect = 0;
        private void Ts()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            do
            {

                if (InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {

                    }));

                }
                Thread.Sleep(Convert.ToInt32(txtTimeInterval.Text) * 1000);


            } while (sw.ElapsedMilliseconds <= timeSeriesDouration * 60 * 1000);
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate { chTimeSeries.Checked = false; }));

            }
        }

        private void colorMesurmentToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btncolor_Click(object sender, EventArgs e)
        {
            ColorTIme.Enabled = true;
            fchart = new FChart();
            fchart.Show();
        }

        private void iIIAToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void softwareToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            softWareFrm = new FSoftware();

            softWareFrm.Show();
        }

        private void calibrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FCalibration calib = new Forms.FCalibration();
            calib.Show();
        }

        private void lampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FLamp lamp = new Forms.FLamp();
            lamp.Show();
        }

        private void txtIntegrationTime_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            //  tt.SetToolTip(txtIntegrationTime, "(1-1000)");
        }



        private void txtAverage_Enter(object sender, EventArgs e)
        {

        }

        private void txtSmoothing_Enter(object sender, EventArgs e)
        {

        }

        private void _btn_opn_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(_btn_opn, "Add spec");

        }

        private void btnDelete_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btnDelete, "Delete spec");
        }

        private void btnSave_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btnSave, "Save A");
        }

        private void btnPrint_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btnPrint, "Print A");
        }

        private void btnZoomX_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btnZoomX, "Zoom in X axis");
        }

        private void btnZomY_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btnZomY, "Zoom in Y axis");
        }

        private void btnZoomXY_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.btnZoomXY, "Zoom in XY axis");
        }

        private void btnReset_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.btnReset, "Zoom Out");
        }

        private void btnFit_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.btnFit, "Auto Fit");
        }

        private void btnYgide_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.btnYgide, "Data Cursor");
        }

        private void txtAverage_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            //   tt.SetToolTip(txtAverage, "(1-25)");
        }

        private void txtSmoothing_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            //  tt.SetToolTip(txtSmoothing, "(1-100)");
        }

        private void txtLampBrightness_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            //  tt.SetToolTip(txtLampBrightness , "(1-100)");
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            isRun = false;
            timeSeries.Enabled = false;

            timeSeriesD.Enabled = false;
            btnStart.PerformClick();




            //   DllInterface.LineScanCamera_Remove(lineScanCameraIndex);

            //ComportInterfacecs.StopReadingImageSensor();
            //ComportInterfacecs.DiscardBufferPort();
            //ComportInterfacecs.StopRead = true;
            chTimeSeries.Checked = false;
            while (chart1.Series.Count > 0) btnDelete_Click(sender, e);

            btnScope_Click(sender, e);
            //chart1.Series.RemoveAt(0);
            //  cmbSeriesName.Items.Clear();

        }
        string strCalcSeirs;
        private void TimeCalc_Tick(object sender, EventArgs e)
        {
            try
            {
                strCalcSeirs = fMath.txtSeriesName.Text;
                double[] math = new double[numPixel];


                if (fMath.isRun)
                {
                    if (fMath.isCalc)
                    {
                        if (chart1.Series.IndexOf(fMath.txtSeriesName.Text) == -1)
                        {

                            chart1.Series.Add(strCalcSeirs);
                            chart1.Series[strCalcSeirs].ChartType = (SeriesChartType)cmbChartType.SelectedItem;

                        }

                        if (fMath.comboBox1.Text == "+")
                        {
                            for (int i = 0; i < numPixel; i++)
                            {

                                math[i] = chart1.Series[fMath.cmb1cmb1.Text].Points[i].YValues[0] + chart1.Series[fMath.comboBox2.Text].Points[i].YValues[0];

                            }
                        }
                        else if (fMath.comboBox1.Text == "-")
                        {
                            for (int i = 0; i < numPixel; i++)
                            {
                                math[i] = chart1.Series[fMath.cmb1cmb1.Text].Points[i].YValues[0] - chart1.Series[fMath.comboBox2.Text].Points[i].YValues[0];

                            }
                        }
                        else if (fMath.comboBox1.Text == "*")
                        {
                            for (int i = 0; i < numPixel; i++)
                            {
                                math[i] = chart1.Series[fMath.cmb1cmb1.Text].Points[i].YValues[0] * chart1.Series[fMath.comboBox2.Text].Points[i].YValues[0];
                                if (math[i] > (double)SoftwarePr.ScopeY2)
                                {
                                    message("Overflow", false);
                                    fMath.isCalc = false;
                                    return;
                                }


                            }
                        }
                        else if (fMath.comboBox1.Text == "/")
                        {
                            for (int i = 0; i < numPixel; i++)
                            {
                                math[i] = chart1.Series[fMath.cmb1cmb1.Text].Points[i].YValues[0] / chart1.Series[fMath.comboBox2.Text].Points[i].YValues[0];

                            }
                        }




                        chart1.Series[strCalcSeirs].Points.DataBindXY(xvalue, math);
                        fMath.isCalc = false;
                    }
                }
                else
                    TimeCalc.Enabled = false;
            }
            catch { }
        }
        FMath fMath = new FMath();
        private void btnMath_Click(object sender, EventArgs e)
        {


        }

        private void formulaSpectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fMath = new FMath();
            fMath.Show();


            fMath.cmb1cmb1.Items.AddRange(cmbSeriesName.Items.Cast<Object>().ToArray());
            fMath.comboBox2.Items.AddRange(cmbSeriesName.Items.Cast<Object>().ToArray());
            fMath.cmb1cmb1.SelectedItem = fMath.cmb1cmb1.Items[0];
            fMath.comboBox2.SelectedItem = fMath.comboBox2.Items[0];
            fMath.comboBox1.SelectedItem = fMath.comboBox1.Items[0];

            TimeCalc.Enabled = true;
            TimeCalc.Interval = 1000;
        }

        private void txtBaseLineDefault_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //   BaseLineD = Convert.ToInt32(txtBaseLineDefault.Text);
            }
            catch { }
        }

        private void btnDown_Click_1(object sender, EventArgs e)
        {
            //   txtLampBrightness.Text = (Convert.ToInt16(txtLampBrightness.Text) - 1).ToString();
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            if (!DarkRefreceInvalid)
            {
                label9.Text = "Dark Data or Reference Data Invalid";
                return;
            }
            label9.Text = "";
            tooltip.RemoveAll();
            foreach (Control item in _pnl_options.Controls)
            {
                item.ForeColor = Color.Gray;

            }

            foreach (ToolStripMenuItem item in viewToolStripMenuItem.DropDownItems)
                item.Checked = false;
            fluresanceToolStripMenuItem.Checked = true;

            //set enable option 
            button3.ForeColor = Color.White;
            LoadSofwareProperties();
            this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.ZoomReset();
            this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.ZoomReset();
            this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum = _FluorescenceX1;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum = _FluorescenceX2;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Interval = 50;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Minimum = _FluorescenceY1;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum = _FluorescenceY2;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Interval = 10000;

            this.chart1.ChartAreas["ChartArea1"].CursorY.Interval = 0.01;


            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "{0}";
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0}";



            this.chart1.ChartAreas["ChartArea1"].AxisX.Title = "Wavelength (nm)";
            if (SoftwarePr.XvalCM)
                this.chart1.ChartAreas["ChartArea1"].AxisX.Title = " ( cm" + " \u23BB¹ )";
            this.chart1.ChartAreas["ChartArea1"].AxisY.Title = " Intensity (count) ";
            Mode = SpectometrMode.Fluorescence;
            HideColorBar();
        }

        private void fluresanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3_Click_2(sender, e);
        }

        private void btncolor_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.btncolor, "Color Measurement");

        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.button2, "Nano Drop");
        }

        //private void button4_MouseHover(object sender, EventArgs e)
        //{
        //    ToolTip tt = new ToolTip();
        //    tt.SetToolTip(this.button4, "Thin Film");
        //}

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            MessageBox.Show(dataCount.ToString());
        }


        private void button4_Click(object sender, EventArgs e)
        {
            thinFilmToolStripMenuItem_Click(sender, e);
        }

        private void btncolor_Click_1(object sender, EventArgs e)
        {
            ColorTIme.Interval = 100;
            ColorTIme.Start();
            btncolor_Click(sender, e);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            nanoDropToolStripMenuItem1_Click(sender, e);

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            button4_Click(sender, e);
        }
        FChartStep fChartstep = new Forms.FChartStep();
        List<Point> pointsDraw = new List<Point>();
        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && IsPanninig)
            {
                mDown = e.Location;
                prevXMax = chart1.ChartAreas[0].AxisX.Maximum;
                prevXMin = chart1.ChartAreas[0].AxisX.Minimum;
                prevYMax = chart1.ChartAreas[0].AxisY.Maximum;
                prevYMin = chart1.ChartAreas[0].AxisY.Minimum;
            }

        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(button6, "Chart Setting");
        }

        private void btncolor_MouseHover_1(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btncolor, "Color Measurement");
        }

        private void button2_MouseHover_1(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(button2, "Nano Drop");
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(button4, "Thin Film");
        }

        private void btnCopyToClip_Click(object sender, EventArgs e)
        {
            try
            {
                if (chart1.Series.Count == 0)
                    return;
                string clipboardSave = "";
                Clipboard.Clear();
                clipboardSave = "Wave Length" + "\t";
                for (int i = 0; i < chart1.Series.Count; i++)
                {
                    if (chart1.Series[i].Name!="UV-IR")
                    clipboardSave += chart1.Series[i].Name + "\t";
                }
                clipboardSave += System.Environment.NewLine;
                for (int k = 0; k < numPixel; k++)
                {
                    clipboardSave = clipboardSave + chart1.Series[0].Points[k].XValue.ToString() + "\t ";
                    for (int m = 0; m < chart1.Series.Count; m++)
                    {
                        if (chart1.Series[m].Name != "UV-IR")
                        {
                            if (!string.IsNullOrEmpty(chart1.Series[m].Points[k].YValues.FirstOrDefault().ToString()))
                                clipboardSave = clipboardSave + chart1.Series[m].Points[k].YValues.FirstOrDefault() + "\t ";
                            else
                                clipboardSave = clipboardSave + "\t ";


                           
                        }
                        else
                            clipboardSave = clipboardSave + "\t ";
                      
                    }
                    clipboardSave += System.Environment.NewLine;
                }
                Clipboard.SetText(clipboardSave);
                message("Clipboard saved", true);
            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCopyToClip_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btnCopyToClip, "Copy To Clipboard");
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button5_MouseHover(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            FBaseLine fBaseline = new FBaseLine();
            fBaseline.BaseLine = BaseLineD;
            DialogResult dlr = fBaseline.ShowDialog();

            if (dlr == DialogResult.OK)
            {
                BaseLineD = fBaseline.BaseLine;
                //  txtBaseLineDefault.Text = BaseLineD.ToString();
            }
        }

        private void button5_MouseHover_1(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(button5, "Base Line");
        }

        private void numSmosthing_ValueChanged(object sender, EventArgs e)
        {
            try
            {


                if (numrSmosthing.Value > 100)
                    numrSmosthing.Value = 1;
                this.Smoothing = Convert.ToUInt16(numrSmosthing.Value);
            }
            catch
            {
                message("Smoothing value not Valid", false);
                numrSmosthing.Value = 3;
                numrSmosthing.Focus();
            }
        }

        private void numricIntegrationTime_ValueChanged(object sender, EventArgs e)
        {

            try
            {
                this.IntegrationTime = Convert.ToUInt32(numricIntegrationTime.Value) ;
                if (isRun == true)
                {
                    ComportInterfacecs.PeriodicImageSensorRead(this.IntegrationTime);
                }
            }
            catch
            {
                message("IntegrationTime value not Valid", false);

                numricIntegrationTime.Text = "18";
                numricIntegrationTime.Focus();
            }
        }

        private void numricalAverage_ValueChanged(object sender, EventArgs e)
        {
            
            this.NumberOfFrame = Convert.ToInt32(this.numricalAverage.Value);
            if (isRun)
            {
                for (int i = this.NumberOfFrame; i < 26; i++)
                {
                    Array.Clear(this.AverageData[i], 0, numPixel);
                }
            }


        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {


                if (tangestanIs)
                {
                    ComportInterfacecs.SetIntensityValueOfTungstenLamp(Convert.ToByte(numricalLampBrightnes.Value));
                }

            }
            catch
            {

                numricalLampBrightnes.Value = 100;
                numricalLampBrightnes.Focus();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            // chart1.ChartAreas[0].AxisY.StripLines.Clear();
            // Axis axeY = chart1.ChartAreas[0].AxisY;
            // axeY.IntervalAutoMode = IntervalAutoMode.FixedCount;
            //// axeY.Interval = 500;//espace entre les valeurs affichées sur l'échelle
            // axeY.ScaleView.Zoom(1, 100);//zoom l'axe Y entre deux et 10

            ////Pour l'axe des X

            //Axis axeX = chart1.ChartAreas[0].AxisX;
            //axeX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            //axeX.ScaleView.Zoom(1, 4, DateTimeIntervalType.Seconds);//zoom l'axe Y entre deux et 10
            //axeX.ScaleView.SmallScrollMinSize = .01;
            //  axeX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Seconds;
            //  axeX.ScaleView.SmallScrollSizeType = DateTimeIntervalType.Seconds;
        }


        SizeF curRange = SizeF.Empty;
        List<SizeF> ranges = new List<SizeF>();
        List<int> selectedIndices = new List<int>();
        private void chart1_SelectionRangeChanging(object sender, CursorEventArgs e)
        {
            //    curRange = new SizeF((float)e.NewSelectionStart, (float)e.NewSelectionEnd);

        }

        List<int> collectDataPoints(Series s, double min, double max)
        {
            List<int> hits = new List<int>();
            for (int i = 0; i < s.Points.Count; i++)
                if (s.Points[i].XValue >= min && s.Points[i].XValue <= max) hits.Add(i);
            return hits;
        }

        private void numricalAverage_Enter(object sender, EventArgs e)
        {
            // MessageBox.Show(numricalAverage.Value.ToString());
        }

        private void numricalAverage_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            MessageBox.Show(numricalAverage.Value.ToString());
        }

        private void numricalAverage_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void numricalAverage_KeyUp(object sender, KeyEventArgs e)
        {
            numricalAverage_ValueChanged(sender, e);
        }

        private void numricIntegrationTime_KeyUp(object sender, KeyEventArgs e)
        {
            numricIntegrationTime_ValueChanged(sender, e);
        }

        private void numrSmosthing_KeyUp(object sender, KeyEventArgs e)
        {
            numSmosthing_ValueChanged(sender, e);
        }

        private void serachToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void bandGapToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bandGapToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bandgapFrm = new Forms.FBandGap();
            bandgapFrm.Show();
            BandGapTimet.Enabled = true;
        }

        private void recractiveIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefractiveIndex refIndex = new RefractiveIndex();
            refIndex.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void ramanToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("help2.html");
        }

        private void spectrometerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("help2.html");
        }
        int ScopSeriesCount = 0;
        private void newSpectrumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // txtSeriesTiltle.Text = "WorkSpace1";
            IsDeleteEnd = false;
            if (chart1.Series.Count > 0)
            {

                DialogResult result = MessageBox.Show("Do you want to delete all series?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                    while (chart1.Series.Count > 0 && !IsDeleteEnd ) btnDelete_Click(sender, e);

                }

            }
            ScopSeriesCount++;
           this.newExperiment.txtExperimentName.Text = "Spec" + Convert.ToString((int)ScopSeriesCount);
           
            this.newExperiment.ShowDialog();
            this.newExperiment.Focus();
            if (!btnDelete.Enabled)
                btnDelete.Enabled = true;

            if (newExperiment.DialogResult == DialogResult.OK)
            {

                IsRunStart = true;
                string text = this.newExperiment.txtExperimentName.Text;

                if (newExperiment.comboBox1.SelectedIndex == 0)
                {
                    ComportInterfacecs.deviceSensorType(0);
                    DeviceType.NumberOfIndex = 2090;
                    cmbselct = 0;
                    btnShutter.Enabled = false;
                    btnlamp.Enabled = false;
                    numricalLampBrightnes.Enabled = false;
                }
                else if (newExperiment.comboBox1.SelectedIndex == 1)
                {
                    ComportInterfacecs.deviceSensorType(1);
                    DeviceType.NumberOfIndex = 3070;
                    cmbselct = 1;
                }
                else if (newExperiment.comboBox1.SelectedIndex == 2)
                {
                    ComportInterfacecs.deviceSensorType(2);
                    DeviceType.NumberOfIndex = 3700;
                    cmbselct = 2;
                }
                else if (newExperiment.comboBox1.SelectedIndex == 3)
                {
                    ComportInterfacecs.deviceSensorType(3);
                    DeviceType.NumberOfIndex = 580;
                    cmbselct = 3;
                    btnShutter.Enabled = false;
                    numricalLampBrightnes.Enabled = false;
                }
                numPixel = DeviceType.NumberOfIndex;
                 darkData=new float[numPixel];
                 refrenceData=new float[numPixel];
                   dt=new float[numPixel];
                   xvalue=new float[numPixel];
                MainData = new float[numPixel];
                 AverageData = new float[][] {
            new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel],
            new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel], new float[numPixel]

        };

                if (dtAnalys.ReadSavedPacket("Reference.dat").Length != DeviceType.NumberOfIndex)
                    DarkRefreceInvalid = false;
                else
                {
                    refrenceData = dtAnalys.ReadSavedPacket("Reference.dat");
                    darkData = dtAnalys.ReadSavedPacket("Dark.dat");
                    DarkRefreceInvalid = true;
                }

                if (this.chart1.Series.IsUniqueName(text))
                {
                    this.ExperimentName = text;
                    this.chart1.Series.Add(this.ExperimentName);
                    COlorSelect++;
                    if (COlorSelect == 12)
                        COlorSelect = 0;
                    chart1.Series[ExperimentName].Color = selectColor(COlorSelect);
                }
                else
                {
                    switch (MessageBox.Show(this.ExperimentName + " already exists.\nDo you want to replace it?", "New experiment", MessageBoxButtons.YesNo))
                    {
                        case DialogResult.Yes:
                            this.ExperimentName = text;
                            this.chart1.Series[this.ExperimentName].Points.Clear();
                            break;
                    }
                }
                this.chart1.Legends["Legend1"].Title = txtSeriesTiltle.Text;
                chart1.Series[ExperimentName].ChartType = (SeriesChartType)cmbChartType.SelectedItem;
                chart1.Series[ExperimentName].BorderWidth = trackBar1.Value;

                cmbSeriesName.Items.Add(ExperimentName);

                cmbSeriesName.SelectedItem = ExperimentName;

                // chart1.Series[ExperimentName].Color= Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

                // timer1.Start();




            }
        }

        private void absToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newSpectrumToolStripMenuItem.PerformClick();
            btnAbsorbance.PerformClick();
        }

        private void transToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newSpectrumToolStripMenuItem.PerformClick();
            btnTransmittance.PerformClick();
        }

        private void refToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newSpectrumToolStripMenuItem.PerformClick();
            btnReflectance.PerformClick();
        }

        private void irraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newSpectrumToolStripMenuItem.PerformClick();
            btnIrradiance.PerformClick();
        }

        private void ramanToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            newSpectrumToolStripMenuItem.PerformClick();
            Ramanbtn.PerformClick();
        }

        private void fluoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newSpectrumToolStripMenuItem.PerformClick();
            button3.PerformClick();
        }





        private void button7_1MouseHover(object sender, EventArgs e)
        {

        }

        private void btnFurmula_Click(object sender, EventArgs e)
        {
            formulaSpectToolStripMenuItem.PerformClick();
        }

        private void btnFurmula_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btnFurmula, "Formula Spectrum");
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            saveFileDialog1 = new SaveFileDialog(); saveFileDialog1.ShowDialog();

            dtAnalys.exportToExcel(dtAnalys.B, saveFileDialog1.FileName);
        }
        int count = 0;
        private void getData()
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    if (lineScanCameraIndex >= 0)
                    {
                        //~~~~~~~~~~~~~
                        //availble lines
                        int av = DllInterface.LineScanCamera_AvailableImageFrames(lineScanCameraIndex);
                        //~~~~~~~~~~~~~
                        //image properties
                        int cameraImgBytesPerPixel = DllInterface.LineScanCamera_GetImageBytesPerPixel(lineScanCameraIndex);
                        int cameraImgWidth = DllInterface.LineScanCamera_GetImageWidth(lineScanCameraIndex);
                        int extraBytesCount = DllInterface.LineScanCamera_GetExtraBytesCount(lineScanCameraIndex);

                        unsafe
                        {

                            //~~~~~~~~~~~~~~~~~~~~~~
                            //prevent overflow if system is too slow

                            {
                                DllInterface.LineScanCamera_DiscardAvailableImageFrames(lineScanCameraIndex, av - (av - 100));

                            }
                            //~~~~~~~~~~~~~~~~~~~~~~
                            for (int i = 0; i < av; i++)
                            {
                                byte[] camImageFrameBuf = new byte[cameraImgWidth * cameraImgBytesPerPixel];
                                byte[] camExtraBytesBuf = new byte[extraBytesCount];
                                //~~~~~~~~~~~
                                fixed (byte* rawBuf = camImageFrameBuf)
                                fixed (byte* exBuf = camExtraBytesBuf)
                                {
                                    //get one frame (one line) of data
                                    //update scanner on every line
                                    //update graph on last line
                                    if (DllInterface.LineScanCamera_GetImageFrame_ByArray(lineScanCameraIndex, rawBuf, exBuf, 0, 0) == 1/*OK*/)
                                    {
                                        int pixCount = cameraImgWidth;
                                        float[] maindata = new float[pixCount];
                                        for (int q = 0; q < pixCount; q++)
                                        {
                                            int k = 2 * 1 * q;
                                            maindata[q] = *(ushort*)&rawBuf[k];

                                        }
                                        MainData = maindata;
                                        dataCount++;

                                        LoadSofwareProperties();
                                        if (softWareFrm.Issave || fChartstep.IsSave)
                                        {
                                            //if (Mode == SpectometrMode.Absorbance)
                                            //    btnAbsorbance_Click(sender, e);
                                            //if (Mode == SpectometrMode.Scope)
                                            //    btnScope_Click(sender, e);
                                            //if (Mode == SpectometrMode.Reflectance)
                                            //    btnIrradiance_Click(sender, e);
                                            //if (Mode == SpectometrMode.Transmittance)
                                            //    btnTransmittance_Click(sender, e);
                                            //if (Mode == SpectometrMode.Irradiance)
                                            //    button3_Click(sender, e);
                                            //if (Mode == SpectometrMode.Fluorescence)
                                            //    button3_Click_2(sender, e);
                                            //if (Mode == SpectometrMode.Raman)
                                            //{
                                            //    ramanToolStripMenuItem_Click(sender, e);

                                            //}

                                            softWareFrm.Issave = false;
                                            fChartstep.IsSave = false;
                                        }

                                        if (wait)
                                            return;


                                        int num = Convert.ToInt32(this.numricalAverage.Value);
                                        if (this.NumberOfFrame < num)
                                        {
                                            this.NumberOfFrame++;
                                        }
                                        else
                                        {
                                            this.NumberOfFrame = 1;
                                        }
                                        Array.Copy(this.MainData, this.AverageData[this.NumberOfFrame - 1], numPixel);
                                        float[] numArray = (from k in Enumerable.Range(0, this.AverageData[this.NumberOfFrame - 1].Length) select this.AverageData.Sum<float[]>((Func<float[], float>)(a => a[i]))).ToArray<float>();



                                        for (int j = 0; j < numPixel; j++)
                                        {

                                            numArray[j] /= (float)num;
                                            xvalue[j] = Convert.ToSingle((_XmapC3 * Math.Pow(j, 3)) + (_XmapC2 * Math.Pow(j, 2) + (_XmapC1 * j)) + _XmapI);


                                        }

                                        //for (int d = Smoothing; d < numPixel; d++)
                                        //{
                                        //    xvalue[d] = ((float)(0.33)*Smoothing ) + xvalue[d];
                                        //}

                                        numSmoothing = dtAnalys.SmoothingS(numArray, Smoothing);





                                        float BaseF = 0;
                                        float BaseD = 0;
                                        if (this.Mode == SpectometrMode.Scope)
                                        {
                                            scopeModeToolStripMenuItem.Checked = true;
                                            numScop = dtAnalys.SmoothingS(numSmoothing, Smoothing);
                                            dt = numScop;

                                            if (_EnableBaseLine)
                                            {
                                                BaseF = Convert.ToInt32(findPos(_Baseline));

                                                //   float numBaseline = numSmoothing[BaseF];
                                                for (int j = 0; j < numPixel; j++)
                                                {


                                                    if (BaseF >= 0)
                                                        numScop[j] = numScop[j] - BaseF;
                                                    else
                                                        numScop[j] = numScop[j] + BaseF;

                                                }


                                            }
                                            BaseD = findPos(BaseLineD);
                                            if (BaseLineD != 0)
                                            {

                                                for (int m = 0; m < numPixel; m++)
                                                {
                                                    if (BaseD >= 0)
                                                        numScop[m] = numScop[m] - BaseD;
                                                    else
                                                        numScop[m] = numScop[m] + BaseD;



                                                }
                                            }

                                            chart1.Series[ExperimentName].Points.DataBindXY(xvalue, numScop);
                                            refDt = numScop;
                                            count++;
                                            lblposition.Text = count.ToString();

                                        }
                                        if (Mode == SpectometrMode.Fluorescence)
                                        {
                                            numScop = dtAnalys.SmoothingS(numSmoothing, Smoothing);
                                            dt = numScop;

                                            if (_EnableBaseLine)
                                            {
                                                BaseF = Convert.ToInt32(findPos(_Baseline));

                                                //   float numBaseline = numSmoothing[BaseF];
                                                for (int j = 0; j < numPixel; j++)
                                                {


                                                    if (BaseF >= 0)
                                                        numScop[j] = numScop[j] - BaseF;
                                                    else
                                                        numScop[j] = numScop[j] + BaseF;


                                                }


                                            }
                                            BaseD = findPos(BaseLineD);
                                            if (BaseLineD != 0)
                                            {

                                                for (int m = 0; m < numPixel; m++)
                                                {
                                                    if (BaseD >= 0)
                                                        numScop[m] = numScop[m] - BaseD;
                                                    else
                                                        numScop[m] = numScop[m] + BaseD;



                                                }
                                            }

                                            chart1.Series[ExperimentName].Points.DataBindXY(xvalue, numScop);
                                            refDt = numScop;


                                        }

                                        if (Mode == SpectometrMode.Transmittance)
                                        {
                                            numScop = dtAnalys.SmoothingS(numSmoothing, Smoothing);
                                            dt = numScop;
                                            if (_EnableBaseLine)
                                            {
                                                BaseF = Convert.ToInt32(findPos(_Baseline));

                                                //   float numBaseline = numSmoothing[BaseF];
                                                for (int j = 0; j < numPixel; j++)
                                                {


                                                    if (BaseF >= 0)
                                                        numScop[j] = numScop[j] - BaseF;
                                                    else
                                                        numScop[j] = numScop[j] + BaseF;

                                                }


                                            }

                                            DataForShow = dtAnalys.Transmittance(numScop, darkData, refrenceData);
                                            numSmoothing = dtAnalys.SmoothingS(DataForShow, Smoothing);
                                            dt = numSmoothing;

                                            BaseD = findPos(BaseLineD);
                                            if (BaseLineD != 0)
                                            {

                                                for (int m = 0; m < numPixel; m++)
                                                {
                                                    if (BaseD >= 0)
                                                        numSmoothing[m] = numSmoothing[m] - BaseD;
                                                    else
                                                        numSmoothing[m] = numSmoothing[m] + ((-1) * BaseD);



                                                }
                                            }
                                            chart1.Series[ExperimentName].Points.DataBindXY(xvalue, numSmoothing);



                                        }

                                        if (Mode == SpectometrMode.Reflectance)
                                        {
                                            numScop = dtAnalys.SmoothingS(numSmoothing, Smoothing);
                                            dt = numScop;
                                            if (_EnableBaseLine)
                                            {
                                                BaseF = Convert.ToInt32(findPos(_Baseline));

                                                //   float numBaseline = numSmoothing[BaseF];
                                                for (int j = 0; j < numPixel; j++)
                                                {


                                                    if (BaseF >= 0)
                                                        numScop[j] = numScop[j] - BaseF;
                                                    else
                                                        numScop[j] = numScop[j] + BaseF;

                                                }


                                            }

                                            DataForShow = dtAnalys.Reflectance(numScop, darkData, refrenceData);
                                            numSmoothing = dtAnalys.SmoothingS(DataForShow, Smoothing);
                                            dt = numSmoothing;

                                            BaseD = findPos(BaseLineD);
                                            if (BaseLineD != 0)
                                            {

                                                for (int m = 0; m < numPixel; m++)
                                                {
                                                    if (BaseD >= 0)
                                                        numSmoothing[m] = numSmoothing[m] - BaseD;
                                                    else
                                                        numSmoothing[m] = numSmoothing[m] + ((-1) * BaseD);



                                                }
                                            }
                                            chart1.Series[ExperimentName].Points.DataBindXY(xvalue, numSmoothing);


                                        }

                                        if (Mode == SpectometrMode.Absorbance)
                                        {

                                            numScop = dtAnalys.SmoothingS(numSmoothing, Smoothing);
                                            dt = numScop;
                                            if (_EnableBaseLine)
                                            {
                                                BaseF = Convert.ToInt32(findPos(_Baseline));

                                                //   float numBaseline = numSmoothing[BaseF];
                                                for (int j = 0; j < numPixel; j++)
                                                {


                                                    if (BaseF >= 0)
                                                        numScop[j] = numScop[j] - BaseF;
                                                    else
                                                        numScop[j] = numScop[j] + BaseF;

                                                }


                                            }

                                            DataForShow = dtAnalys.Absorbance(numScop, darkData, refrenceData);
                                            numSmoothing = dtAnalys.SmoothingS(DataForShow, Smoothing);
                                            DataForShow = numSmoothing;
                                            dt = numSmoothing;

                                            BaseD = findPos(BaseLineD);
                                            if (BaseLineD != 0)
                                            {

                                                for (int m = 0; m < numPixel; m++)
                                                {
                                                    if (BaseD >= 0)
                                                        numSmoothing[m] = numSmoothing[m] - BaseD;
                                                    else

                                                        numSmoothing[m] = numSmoothing[m] + ((-1) * BaseD);
                                                    if (m == 380)
                                                    {
                                                        float k = numSmoothing[m];
                                                    }



                                                }
                                            }
                                            chart1.Series[ExperimentName].Points.DataBindXY(xvalue, numSmoothing);




                                        }
                                        if (Mode == SpectometrMode.ND1 || Mode == SpectometrMode.ND2 || Mode == SpectometrMode.ND3 || Mode == SpectometrMode.ND4)
                                        {

                                            chart1.Series[ExperimentName].Points.DataBindXY(xvalue, ND1(xvalue));
                                            label8.Text = Mode.ToString();

                                        }
                                        if (Mode == SpectometrMode.Raman)
                                        {
                                            if (_EnableBaseLine)
                                            {
                                                BaseF = Convert.ToInt32(findPos(_Baseline));

                                                //   float numBaseline = numSmoothing[BaseF];
                                                for (int j = 0; j < numPixel; j++)
                                                {


                                                    if (BaseF >= 0)
                                                        numSmoothing[j] = numSmoothing[j] - BaseF;
                                                    else
                                                        numSmoothing[j] = numSmoothing[j] + BaseF;

                                                }


                                            }

                                            DataForShow = dtAnalys.RamanData(xvalue);
                                            BaseD = findPos(BaseLineD);
                                            if (BaseLineD != 0)
                                            {

                                                for (int m = 0; m < numPixel; m++)
                                                {
                                                    if (BaseD >= 0)
                                                        numSmoothing[m] = numSmoothing[m] - BaseD;
                                                    else

                                                        numSmoothing[m] = numSmoothing[m] + ((-1) * BaseD);
                                                    if (m == 380)
                                                    {
                                                        float k = numSmoothing[m];
                                                    }



                                                }
                                            }

                                            chart1.Series[ExperimentName].Points.DataBindXY(DataForShow, numSmoothing);

                                            //   label8.Text = DataForShow .Min().ToString()+":" + DataForShow .Max ().ToString() + ":";
                                        }
                                        if (Mode == SpectometrMode.Irradiance)
                                        {

                                            numScop = dtAnalys.SmoothingS(numSmoothing, Smoothing);
                                            dt = numScop;
                                            if (_EnableBaseLine)
                                            {
                                                BaseF = Convert.ToInt32(findPos(_Baseline));

                                                //   float numBaseline = numSmoothing[BaseF];
                                                for (int j = 0; j < numPixel; j++)
                                                {


                                                    if (BaseF >= 0)
                                                        numScop[j] = numScop[j] - BaseF;
                                                    else
                                                        numScop[j] = numScop[j] + BaseF;

                                                }


                                            }

                                            DataForShow = dtAnalys.Irradians(numScop, darkData, refrenceData, xvalue);
                                            numSmoothing = dtAnalys.SmoothingS(DataForShow, Smoothing);
                                            DataForShow = numSmoothing;
                                            dt = numSmoothing;

                                            BaseD = findPos(BaseLineD);
                                            if (BaseLineD != 0)
                                            {

                                                for (int m = 0; m < numPixel; m++)
                                                {
                                                    if (BaseD >= 0)
                                                        numSmoothing[m] = numSmoothing[m] - BaseD;
                                                    else

                                                        numSmoothing[m] = numSmoothing[m] + ((-1) * BaseD);
                                                    if (m == 380)
                                                    {
                                                        float k = numSmoothing[m];
                                                    }



                                                }
                                            }
                                            //float  [] wave = dtAnalys.FirstColumnLa("G:\\Irradiance\\led.xls", 1);
                                            //float [] intensity = dtAnalys.FirstColumnLa("G:\\Irradiance\\led.xls", 2);
                                            //float [] dark = dtAnalys.FirstColumnLa("G:\\Irradiance\\led.xls", 2);
                                            //float [] refrence = dtAnalys.FirstColumnLa("G:\\Irradiance\\tungestan.xls", 2);
                                            // DataForShow = dtAnalys.Irradians( intensity, dark, refrence, wave);



                                            chart1.Series[ExperimentName].Points.DataBindXY(xvalue, numSmoothing);


                                        }
                                        if (Mode == SpectometrMode.calc)
                                        {

                                        }


                                    }
                                    else
                                        continue;
                                }

                                //show extra information about frame

                            }
                            //~~~~~~~~~~~~~~~~~~~~~~
                            //update running time




                        }
                    }
                }));
            }
        }
        private void tmrGetData_Tick(object sender, EventArgs e)
        {
            if (lineScanCameraIndex >= 0)
            {
                //~~~~~~~~~~~~~
                //availble lines
                int av = DllInterface.LineScanCamera_AvailableImageFrames(lineScanCameraIndex);
                //~~~~~~~~~~~~~
                //image properties
                int cameraImgBytesPerPixel = DllInterface.LineScanCamera_GetImageBytesPerPixel(lineScanCameraIndex);
                int cameraImgWidth = DllInterface.LineScanCamera_GetImageWidth(lineScanCameraIndex);
                int extraBytesCount = DllInterface.LineScanCamera_GetExtraBytesCount(lineScanCameraIndex);

                unsafe
                {

                    //~~~~~~~~~~~~~~~~~~~~~~
                    //prevent overflow if system is too slow
                    if (av > 200)
                    {
                        DllInterface.LineScanCamera_DiscardAvailableImageFrames(lineScanCameraIndex, av - 100);

                    }
                    //~~~~~~~~~~~~~~~~~~~~~~
                    for (int i = 0; i < av; i++)
                    {
                        byte[] camImageFrameBuf = new byte[cameraImgWidth * cameraImgBytesPerPixel];
                        byte[] camExtraBytesBuf = new byte[extraBytesCount];
                        //~~~~~~~~~~~
                        fixed (byte* rawBuf = camImageFrameBuf)
                        fixed (byte* exBuf = camExtraBytesBuf)
                        {
                            //get one frame (one line) of data
                            //update scanner on every line
                            //update graph on last line
                            if (DllInterface.LineScanCamera_GetImageFrame_ByArray(lineScanCameraIndex, rawBuf, exBuf, 0, 0) == 1/*OK*/)
                            {
                                int pixCount = cameraImgWidth;
                                float[] maindata = new float[pixCount];
                                for (int q = 0; q < pixCount; q++)
                                {
                                    int k = 2 * 1 * q;
                                    maindata[q] = *(ushort*)&rawBuf[k];

                                }
                                MainData = maindata;
                                dataCount++;
                            }
                            else
                                continue;
                        }
                    }
                }
            }
        }

        private void tmrRendering_Tick(object sender, EventArgs e)
        {
            try
            {
               
                LoadSofwareProperties();
                if (softWareFrm.Issave || fChartstep.IsSave)
                {
                    if (Mode == SpectometrMode.Absorbance)
                                       
                          btnAbsorbance_Click(sender, e);

                    if (Mode == SpectometrMode.Scope)
                        btnScope_Click(sender, e);
                    if (Mode == SpectometrMode.Reflectance)
                        btnIrradiance_Click(sender, e);
                    if (Mode == SpectometrMode.Transmittance)
                        btnTransmittance_Click(sender, e);
                    if (Mode == SpectometrMode.Irradiance)
                        button3_Click(sender, e);
                    if (Mode == SpectometrMode.Fluorescence)
                        button3_Click_2(sender, e);
                    if (Mode == SpectometrMode.Raman)
                    {
                        ramanToolStripMenuItem_Click(sender, e);

                    }

                    softWareFrm.Issave = false;
                    fChartstep.IsSave = false;
                }

                if (wait)
                    return;


                int num = Convert.ToInt32(this.numricalAverage.Value);
                if (this.NumberOfFrame < num)
                {
                    this.NumberOfFrame++;
                }
                else
                {
                    this.NumberOfFrame = 1;
                }
                Array.Copy(this.MainData, this.AverageData[this.NumberOfFrame - 1], numPixel);
                float[] numArray = (from k in Enumerable.Range(0, this.AverageData[this.NumberOfFrame - 1].Length) select this.AverageData.Sum<float[]>((Func<float[], float>)(a => a[k]))).ToArray<float>();



                for (int j = 0; j < numPixel; j++)
                {

                    numArray[j] /= (float)num;
                    xvalue[j] = Convert.ToSingle((_XmapC3 * Math.Pow(j, 3)) + (_XmapC2 * Math.Pow(j, 2) + (_XmapC1 * j)) + _XmapI);


                }

                //for (int d = Smoothing; d < numPixel; d++)
                //{
                //    xvalue[d] = ((float)(0.33)*Smoothing ) + xvalue[d];
                //}

                numSmoothing = dtAnalys.SmoothingS(numArray, Smoothing);





                float BaseF = 0;
                float BaseD = 0;
                if (this.Mode == SpectometrMode.Scope)
                {
                    scopeModeToolStripMenuItem.Checked = true;
                    numScop = dtAnalys.SmoothingS(numSmoothing, Smoothing);
                    dt = numScop;

                    if (_EnableBaseLine)
                    {
                        BaseF = Convert.ToInt32(findPos(_Baseline));

                        //   float numBaseline = numSmoothing[BaseF];
                        for (int j = 0; j < numPixel; j++)
                        {


                            if (BaseF >= 0)
                                numScop[j] = numScop[j] - BaseF;
                            else
                                numScop[j] = numScop[j] + BaseF;

                        }


                    }
                    BaseD = findPos(BaseLineD);
                    if (BaseLineD != 0)
                    {

                        for (int m = 0; m < numPixel; m++)
                        {
                            if (BaseD >= 0)
                                numScop[m] = numScop[m] - BaseD;
                            else
                                numScop[m] = numScop[m] + BaseD;



                        }
                    }

                    chart1.Series[ExperimentName].Points.DataBindXY(xvalue, numScop);
                    refDt = numScop;


                }
                if (Mode == SpectometrMode.Fluorescence)
                {
                    numScop = dtAnalys.SmoothingS(numSmoothing, Smoothing);
                    dt = numScop;

                    if (_EnableBaseLine)
                    {
                        BaseF = Convert.ToInt32(findPos(_Baseline));

                        //   float numBaseline = numSmoothing[BaseF];
                        for (int j = 0; j < numPixel; j++)
                        {


                            if (BaseF >= 0)
                                numScop[j] = numScop[j] - BaseF;
                            else
                                numScop[j] = numScop[j] + BaseF;


                        }


                    }
                    BaseD = findPos(BaseLineD);
                    if (BaseLineD != 0)
                    {

                        for (int m = 0; m < numPixel; m++)
                        {
                            if (BaseD >= 0)
                                numScop[m] = numScop[m] - BaseD;
                            else
                                numScop[m] = numScop[m] + BaseD;



                        }
                    }

                    chart1.Series[ExperimentName].Points.DataBindXY(xvalue, numScop);
                    refDt = numScop;


                }

                if (Mode == SpectometrMode.Transmittance)
                {
                    numScop = dtAnalys.SmoothingS(numSmoothing, Smoothing);
                    dt = numScop;
                    if (_EnableBaseLine)
                    {
                        BaseF = Convert.ToInt32(findPos(_Baseline));

                        //   float numBaseline = numSmoothing[BaseF];
                        for (int j = 0; j < numPixel; j++)
                        {


                            if (BaseF >= 0)
                                numScop[j] = numScop[j] - BaseF;
                            else
                                numScop[j] = numScop[j] + BaseF;

                        }


                    }

                    DataForShow = dtAnalys.Transmittance(numScop, darkData, refrenceData);
                    numSmoothing = dtAnalys.SmoothingS(DataForShow, Smoothing);
                    dt = numSmoothing;

                    BaseD = findPos(BaseLineD);
                    if (BaseLineD != 0)
                    {

                        for (int m = 0; m < numPixel; m++)
                        {
                            if (BaseD >= 0)
                                numSmoothing[m] = numSmoothing[m] - BaseD;
                            else
                                numSmoothing[m] = numSmoothing[m] + ((-1) * BaseD);



                        }
                    }
                    chart1.Series[ExperimentName].Points.DataBindXY(xvalue, numSmoothing);



                }

                if (Mode == SpectometrMode.Reflectance)
                {
                    numScop = dtAnalys.SmoothingS(numSmoothing, Smoothing);
                    dt = numScop;
                    if (_EnableBaseLine)
                    {
                        BaseF = Convert.ToInt32(findPos(_Baseline));

                        //   float numBaseline = numSmoothing[BaseF];
                        for (int j = 0; j < numPixel; j++)
                        {


                            if (BaseF >= 0)
                                numScop[j] = numScop[j] - BaseF;
                            else
                                numScop[j] = numScop[j] + BaseF;

                        }


                    }

                    DataForShow = dtAnalys.Reflectance(numScop, darkData, refrenceData);
                    numSmoothing = dtAnalys.SmoothingS(DataForShow, Smoothing);
                    dt = numSmoothing;

                    BaseD = findPos(BaseLineD);
                    if (BaseLineD != 0)
                    {

                        for (int m = 0; m < numPixel; m++)
                        {
                            if (BaseD >= 0)
                                numSmoothing[m] = numSmoothing[m] - BaseD;
                            else
                                numSmoothing[m] = numSmoothing[m] + ((-1) * BaseD);



                        }
                    }
                    chart1.Series[ExperimentName].Points.DataBindXY(xvalue, numSmoothing);


                }

                if (Mode == SpectometrMode.Absorbance)
                {

                    if(darkData.Length!=numPixel || refDt.Length!=numPixel)
                    {

                    }
                    numScop = dtAnalys.SmoothingS(numSmoothing, Smoothing);
                    dt = numScop;
                    if (_EnableBaseLine)
                    {
                        BaseF = Convert.ToInt32(findPos(_Baseline));

                        //   float numBaseline = numSmoothing[BaseF];
                        for (int j = 0; j < numPixel; j++)
                        {


                            if (BaseF >= 0)
                                numScop[j] = numScop[j] - BaseF;
                            else
                                numScop[j] = numScop[j] + BaseF;

                        }


                    }

                    DataForShow = dtAnalys.Absorbance(numScop, darkData, refrenceData);
                    numSmoothing = dtAnalys.SmoothingS(DataForShow, Smoothing);
                    DataForShow = numSmoothing;
                    dt = numSmoothing;

                    BaseD = findPos(BaseLineD);
                    if (BaseLineD != 0)
                    {

                        for (int m = 0; m < numPixel; m++)
                        {
                            if (BaseD >= 0)
                                numSmoothing[m] = numSmoothing[m] - BaseD;
                            else

                                numSmoothing[m] = numSmoothing[m] + ((-1) * BaseD);
                            if (m == 380)
                            {
                                float k = numSmoothing[m];
                            }



                        }
                    }
                    chart1.Series[ExperimentName].Points.DataBindXY(xvalue, numSmoothing);




                }
                if (Mode == SpectometrMode.ND1 || Mode == SpectometrMode.ND2 || Mode == SpectometrMode.ND3 || Mode == SpectometrMode.ND4)
                {

                    chart1.Series[ExperimentName].Points.DataBindXY(xvalue, ND1(xvalue));
                    label8.Text = Mode.ToString();

                }
                if (Mode == SpectometrMode.Raman)
                {
                    if (_EnableBaseLine)
                    {
                        BaseF = Convert.ToInt32(findPos(_Baseline));

                        //   float numBaseline = numSmoothing[BaseF];
                        for (int j = 0; j < numPixel; j++)
                        {


                            if (BaseF >= 0)
                                numSmoothing[j] = numSmoothing[j] - BaseF;
                            else
                                numSmoothing[j] = numSmoothing[j] + BaseF;

                        }


                    }

                    DataForShow = dtAnalys.RamanData(xvalue);

                    BaseD = findPos(BaseLineD);
                    if (BaseLineD != 0)
                    {

                        for (int m = 0; m < numPixel; m++)
                        {
                            if (BaseD >= 0)
                                numSmoothing[m] = numSmoothing[m] - BaseD;
                            else

                                numSmoothing[m] = numSmoothing[m] + ((-1) * BaseD);
                            if (m == 380)
                            {
                                float k = numSmoothing[m];
                            }



                        }
                    }

                    chart1.Series[ExperimentName].Points.DataBindXY(DataForShow, numSmoothing);

                    //   label8.Text = DataForShow .Min().ToString()+":" + DataForShow .Max ().ToString() + ":";
                }
                if (Mode == SpectometrMode.Irradiance)
                {

                    numScop = dtAnalys.SmoothingS(numSmoothing, Smoothing);
                    dt = numScop;
                    if (_EnableBaseLine)
                    {
                        BaseF = Convert.ToInt32(findPos(_Baseline));

                        //   float numBaseline = numSmoothing[BaseF];
                        for (int j = 0; j < numPixel; j++)
                        {


                            if (BaseF >= 0)
                                numScop[j] = numScop[j] - BaseF;
                            else
                                numScop[j] = numScop[j] + BaseF;

                        }


                    }

                    DataForShow = dtAnalys.Irradians(numScop, darkData, refrenceData, xvalue);
                    numSmoothing = dtAnalys.SmoothingS(DataForShow, Smoothing);
                    DataForShow = numSmoothing;
                    dt = numSmoothing;

                    BaseD = findPos(BaseLineD);
                    if (BaseLineD != 0)
                    {

                        for (int m = 0; m < numPixel; m++)
                        {
                            if (BaseD >= 0)
                                numSmoothing[m] = numSmoothing[m] - BaseD;
                            else

                                numSmoothing[m] = numSmoothing[m] + ((-1) * BaseD);
                            if (m == 380)
                            {
                                float k = numSmoothing[m];
                            }



                        }
                    }
                    //float  [] wave = dtAnalys.FirstColumnLa("G:\\Irradiance\\led.xls", 1);
                    //float [] intensity = dtAnalys.FirstColumnLa("G:\\Irradiance\\led.xls", 2);
                    //float [] dark = dtAnalys.FirstColumnLa("G:\\Irradiance\\led.xls", 2);
                    //float [] refrence = dtAnalys.FirstColumnLa("G:\\Irradiance\\tungestan.xls", 2);
                    // DataForShow = dtAnalys.Irradians( intensity, dark, refrence, wave);



                    chart1.Series[ExperimentName].Points.DataBindXY(xvalue, numSmoothing);


                }
                if (Mode == SpectometrMode.calc)
                {

                }
                count++;
                lblposition.Text = count.ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void _pnl_form_tools_Paint(object sender, PaintEventArgs e)
        {

        }

        private void setupToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void connectToDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Conecting();
        }

        private void Chart1_Click(object sender, EventArgs e)
        {

        }

        private void btnShutter_EnabledChanged(object sender, EventArgs e)
        {
            btnShutter.ForeColor = Color.Gray;
        }

        private void btnShutter_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;

            btn.Text = string.Empty;

            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;   // center the text

            Point p = new Point(e.ClipRectangle.Width + 10, e.ClipRectangle.Height / 2);
            TextRenderer.DrawText(e.Graphics, "Shutter", btn.Font, p, btn.ForeColor, flags);
        }
        FrmAbout frm = new FrmAbout();
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isRun = false;
            btnStart.Image = Properties.Resources.Media_Play2;
            btnStart.Text = "Start";
            ComportInterfacecs.StopReadingImageSensor();
            ComportInterfacecs.DiscardBufferPort();
            ComportInterfacecs.StopRead = true;

            frm.ShowDialog();
        }
        List<DataPoint> prePoints = new List<DataPoint>();
        private void findPeackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PeckFIlterFrm filterFrm = new PeckFIlterFrm();
            if (filterFrm.ShowDialog() == DialogResult.OK)
            {
                if (prePoints.Count > 0)
                {
                    foreach (DataPoint data in prePoints)
                        chart1.Series[ExperimentName].Points.Remove(data);
                }
                var a1 = chart1.Series[ExperimentName];


                var dt = chart1.Series[ExperimentName].Points.Select(x => x.YValues[0]).ToArray();
                double[,] peak = FindPeaks.findPeaks(dt, filterFrm.NumFilter);

                for (int i = 0; i < (peak.Length / 2); i++)
                {
                    if (peak[0, i] > filterFrm.NumFilter)
                    {

                        int num = (int)peak[1, i];
                        DataPoint a = chart1.Series[ExperimentName].Points[num];
                        prePoints.Add(a);
                        a.MarkerColor = Color.SeaGreen;
                        a.MarkerSize = 10;
                        a.MarkerStyle = MarkerStyle.Circle;
                        a.Label = a.XValue.ToString("N2");
                        a.LabelAngle = 90;
                        
                    }

                }
            }

        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TxtToChartUserControl txt = new TxtToChartUserControl();
            chart1.Controls.Add(txt);
            txt.AllowDrop = true;




            txt.Location = new Point(chart1.ClientSize.Width - 500, 200);
        }

        private void numricalLampBrightnes_KeyUp(object sender, KeyEventArgs e)
        {
            numericUpDown1_ValueChanged(sender, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {

            fChartstep.Mode = Mode.ToString();
            fChartstep.ShowDialog();

        }

        private void txtDouration_TextChanged(object sender, EventArgs e)
        {
            try
            {
                timeSeriesDouration = Convert.ToInt32(txtDouration.Text);

            }
            catch { txtDouration.Text = "2"; }

        }

        private void txtTimeInterval_TextChanged(object sender, EventArgs e)
        {
            try
            {
                timeSeriesInterval = Convert.ToInt32(txtTimeInterval.Text);

            }
            catch { txtTimeInterval.Text = "20"; timeSeriesInterval = 20; }
        }

        private void ramanModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click_1(sender, e);
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            if (!ColorIsRun)
                //   SelectColor();
                C65 = C2 = true;
            CA = C10 = false;

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            if (!ColorIsRun)
                //  SelectColor();
                CA = C10 = true;
            C2 = C65 = false;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (!ColorIsRun)
                //    SelectColor();
                CA = C2 = true;
            C10 = C65 = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!DarkRefreceInvalid)
            {
                label9.Text = "Dark Data or Reference Data Invalid";
                return;
            }
            label9.Text = "";
            tooltip.RemoveAll();
            foreach (Control item in _pnl_options.Controls)
            {
                item.ForeColor = Color.Gray;

            }
            foreach (ToolStripMenuItem item in viewToolStripMenuItem.DropDownItems)
                item.Checked = false;

            ramanModeToolStripMenuItem.Checked = true;

            Ramanbtn.ForeColor = Color.White;
            Mode = SpectometrMode.Raman;
            LoadSofwareProperties();
            this.chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.ZoomReset();
            this.chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.ZoomReset();
            this.chart1.ChartAreas["ChartArea1"].AxisX.Minimum = _RamanX1; ;
            this.chart1.ChartAreas["ChartArea1"].AxisX.Maximum = _RamanX2;

            this.chart1.ChartAreas["ChartArea1"].AxisX.Interval = 150;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Minimum = _RamanY1; ; ;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Maximum = _RamanY2; ;
            this.chart1.ChartAreas["ChartArea1"].AxisY.Interval = 10000;
            this.chart1.Series[this.ExperimentName].Points.AddXY((double)0.0, (double)0.0);
            //  "Raman Shift ( cm" + " \u23BB ¹ )";
            this.chart1.ChartAreas["ChartArea1"].AxisX.Title = "Raman Shift ( cm" + " \u23BB¹ )";
            this.chart1.ChartAreas["ChartArea1"].AxisY.Title = " Intensity (count) ";

            label8.Text = Mode.ToString();
            HideColorBar();
            //   ramanToolStripMenuItem_Click(sender, e);
        }

        RectangleF InnerPlotPositionClientRectangle(Chart chart, ChartArea CA)
        {
            RectangleF IPP = CA.InnerPlotPosition.ToRectangleF();
            RectangleF CArp = ChartAreaClientRectangle(chart, CA);

            float pw = CArp.Width / 100f;
            float ph = CArp.Height / 100f;

            return new RectangleF(CArp.X + pw * IPP.X, CArp.Y + ph * IPP.Y,
                                    pw * IPP.Width, ph * IPP.Height);
        }

        private void ch3D_CheckedChanged(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].Area3DStyle.Enable3D = ch3D.Checked;
            chart1.ChartAreas[0].Area3DStyle.PointGapDepth = 50;
            chart1.ChartAreas[0].Area3DStyle.PointDepth = 2;
            chart1.ChartAreas[0].Area3DStyle.Perspective = 3;
            //chart1.ChartAreas[0].InnerPlotPosition.X = 0;
            //chart1.ChartAreas[0].InnerPlotPosition.Y = 0;

            //Height and width are in percentage(%)
            //chart1.ChartAreas[0].InnerPlotPosition.Height = 100;
            //chart1.ChartAreas[0].InnerPlotPosition.Width = 100;

        }



        private void btnlamp_EnabledChanged(object sender, EventArgs e)
        {
            if (DeviceType.NumberOfIndex == 2090)
                btnlamp.ForeColor = Color.Gray;
            else
                btnlamp.ForeColor = Color.Red;
          
        }

        private void btnlamp_Paint(object sender, PaintEventArgs e)
        {

            Button btn = (Button)sender;

            btn.Text = string.Empty;

            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;   // center the text

            Point p = new Point(e.ClipRectangle.Width + 10, e.ClipRectangle.Height / 2);
            TextRenderer.DrawText(e.Graphics, "Lamp", btn.Font, p, btn.ForeColor, flags);
        }

        bool chartUserSelection = true;
        private void btnYgide_Click(object sender, EventArgs e)
        {
            if (chartUserSelection)
            {

                this.chart1.ChartAreas["ChartArea1"].CursorY.LineColor = Color.White;
                this.chart1.ChartAreas["ChartArea1"].CursorX.LineColor = Color.White;

                chartUserSelection = false;
            }
            else
            {
                this.chart1.ChartAreas["ChartArea1"].CursorY.LineColor = Color.Red;
                this.chart1.ChartAreas["ChartArea1"].CursorX.LineColor = Color.Red;
                chartUserSelection = true;
            }
            //chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            //chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            //chart1.ChartAreas[0].AxisX.LineColor = Color.Transparent;
            //chart1.ChartAreas[0].AxisY.LineColor = Color.Transparent;

        }
        public void getExcelFile()
        {
            CieData ciedata1 = new CieData();

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\Development Unit\Documents\visual studio 2015\Projects\ciedata\ciedata\bin\Debug\CieData3.xlsx");
            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            int f = 0;
            for (int i = 2; i <= rowCount - 1; i++)
            {

                ciedata1.A[f] = Convert.ToSingle(xlRange.Cells[i, 2].Value2);
                ciedata1.D65[f] = (float)xlRange.Cells[i, 3].Value2;
                ciedata1.X2[f] = (float)xlRange.Cells[i, 4].Value2;
                ciedata1.Y2[f] = (float)xlRange.Cells[i, 5].Value2;
                ciedata1.Z2[f] = (float)xlRange.Cells[i, 6].Value2;
                ciedata1.X10[f] = (float)xlRange.Cells[i, 7].Value2;
                ciedata1.Y10[f] = (float)xlRange.Cells[i, 8].Value2;
                ciedata1.Z10[f] = (float)xlRange.Cells[i, 9].Value2;
                f++;

            }


            GC.Collect();
            GC.WaitForPendingFinalizers();




            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);


            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);


            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
            IFormatter formatter = new BinaryFormatter();
            FileStream seryalization = new FileStream("CIEdata.dat", FileMode.Create, FileAccess.Write);
            formatter.Serialize(seryalization, ciedata1);
            seryalization.Close();

        }

        public void getExcelFile1()
        {
            Testcolor tstColor = new Spectometer.Testcolor();

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"E:\color.xlsx");
            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            int f = 0;
            for (int i = 2; i <= rowCount - 1; i++)
            {

                tstColor.blue[f] = (float)xlRange.Cells[i, 1].Value2;

                tstColor.green[f] = (float)xlRange.Cells[i, 2].Value2;
                tstColor.pirole[f] = (float)xlRange.Cells[i, 3].Value2;
                tstColor.red[f] = (float)xlRange.Cells[i, 4].Value2;

                f++;

            }


            GC.Collect();
            GC.WaitForPendingFinalizers();




            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);


            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);


            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
            IFormatter formatter = new BinaryFormatter();
            FileStream seryalization = new FileStream("testcolor.dat", FileMode.Create, FileAccess.Write);
            formatter.Serialize(seryalization, tstColor);
            seryalization.Close();

        }

    }
}
