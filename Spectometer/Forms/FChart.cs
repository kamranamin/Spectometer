
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Colourful;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using Colourful.Conversion;

namespace Spectometer.Forms
{
    public partial class FChart : F_Base
    {

        public bool IsRun;
        public bool Colorm = false;
        //   FTimeSpectrum  timeSpectrumFrm1 = new FTimeSpectrum ();


        private Point mousePoint;
        private Point? prevPosition = null;
        private ToolTip tooltip = new ToolTip();
        public bool measurment = false;
        public double lSamlpe1, aSamlpe1, bSamlpe1, lSamlpe2, aSamlpe2, bSamlpe2;
        double[] xColor, yColor, zCOlor;
        List<SPoint> sPoints;
        Color[] colormach = new Color[90000];
        double[] XvalColor = new double[90000];
        double[] YvalColor = new double[90000];
        CieData ciedata = new CieData();
        private float[] A, D65, X2, Y2, Z2, X10, Y10, Z10;
        Testcolor testcolor = new Testcolor();
        private float[] Red, Green, Blue, pirple;
        Timer t = new Timer();


        public FChart()
        {

            InitializeComponent();
        }
     

    

        private void SelectColor()
        {
          
                // getExcelFile1();
                //fchart = new FChart();
                //fchart = new FChart();
                //fchart.Show();
                //  Series s =fchart. chart1.Series[0];
                ChartArea ca = chart1.ChartAreas[0];

                ca.AxisX.Minimum = 0;
                ca.AxisY.Minimum = 0;
                ca.AxisX.Maximum = 1;
                ca.AxisY.Maximum = 1;
                ca.AxisX.Interval = 0.1f;
                ca.AxisY.Interval = 0.1f;
                ca.AxisX.LabelStyle.Format = "0.00";
                ca.AxisY.LabelStyle.Format = "0.00";
                chart1.Series[0].ChartType = SeriesChartType.Point;
                IFormatter formatter = new BinaryFormatter();
                FileStream serializationStream = new FileStream("CIE.dat", FileMode.Open, FileAccess.Read);
                sPoints = (List<SPoint>)formatter.Deserialize(serializationStream);
                serializationStream.Close();
                int i = 0;
                foreach (var sp in sPoints)
                {

                    chart1.Series[0].Points.Add(SPoint.FromSPoint(sp));
                    colormach[i] = sp.color;
                    XvalColor[i] = sp.XValue;
                    YvalColor[i] = sp.YValue;
                    i++;


                }

                chart1.Series.Add("palete");
                chart1.Series["palete"].IsVisibleInLegend = false;

                chart1.Series["palete"].MarkerStyle = MarkerStyle.Diamond;
                chart1.Series["palete"].MarkerSize = 10;
                chart1.Series["palete"].MarkerColor = Color.DarkRed;
                chart1.Series["palete"].ChartType = SeriesChartType.Line;


                ColorMeasurment colorMeasurment = new Spectometer.ColorMeasurment();
                IFormatter formatter2 = new BinaryFormatter();
                FileStream serializationStream2 = new FileStream("ColorMeasurment.dat", FileMode.Open, FileAccess.Read);
                colorMeasurment = formatter2.Deserialize(serializationStream2) as ColorMeasurment;
                xColor = colorMeasurment.X;
                yColor = colorMeasurment.Y;
                zCOlor = colorMeasurment.Z;
                serializationStream2.Close();

                IFormatter formatter3 = new BinaryFormatter();
                FileStream serializationStream3 = new FileStream("CIEData.dat", FileMode.Open, FileAccess.Read);
                ciedata = formatter3.Deserialize(serializationStream3) as CieData;
                A = ciedata.A;
                D65 = ciedata.D65;
                X2 = ciedata.X2;
                Y2 = ciedata.Y2;
                Z2 = ciedata.Z2;
                X10 = ciedata.X10;
                Y10 = ciedata.Y10;
                Z10 = ciedata.Z10;
                serializationStream3.Close();

                IFormatter formatter4 = new BinaryFormatter();
                FileStream serializationStream4 = new FileStream("testcolor.dat", FileMode.Open, FileAccess.Read);
                testcolor = formatter3.Deserialize(serializationStream4) as Testcolor;
                Red = testcolor.red;
                Blue = testcolor.blue;
                Green = testcolor.green;
                pirple = testcolor.pirole;
                serializationStream4.Close();

              //  fchart.Colorm = true;
                //    fchart.button1.Visible = true;

                #region Write
                //xColor = dtAnalys.FirstColumnLa(@"E:\Programing\Spectometer (2)\Spectometer\Spectometer\bin\Debug\data.xls", 5);
                //yColor = dtAnalys.FirstColumnLa(@"E:\Programing\Spectometer (2)\Spectometer\Spectometer\bin\Debug\data.xls", 6);
                //zCOlor = dtAnalys.FirstColumnLa(@"E:\Programing\Spectometer (2)\Spectometer\Spectometer\bin\Debug\data.xls", 7);
                //ColorMeasurment colorMeasurment = new Spectometer.ColorMeasurment();
                //colorMeasurment.X = xColor;
                //colorMeasurment.Y = yColor;
                //colorMeasurment.Z = zCOlor;
                //IFormatter formatter1 = new BinaryFormatter();
                //FileStream seryalization = new FileStream("ColorMeasurment.dat", FileMode.Create, FileAccess.Write);
                //formatter.Serialize(seryalization, colorMeasurment);
                //seryalization.Close();





                //foreach (var dp in dps)
                //{


                //    s.Points.Add(dp);

                //}
                #endregion Write
            


        }

      public   float[] dt1 = new float[900];

        private void button1_Click(object sender, EventArgs e)
        {
            colorMeasurment();
        }
      
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                button1.Enabled = false;
                t.Start();
            }
            else
            {
                button1.Enabled = true;
                t.Enabled = false;
            }

        }

        private void colorMeasurment()

        {


            if (!IsRun)
                return;
            chart1.Series["palete"].Points.Clear();
            //   float[] T = dtAnalys.Transmittance(numSmoothing, darkData, refrenceData);
            float X = 0;
            float Y = 0;
            float Z = 0;
            float x = X;// ( X / (X + Y + Z)) ;
            float y = Y;// (Y / (X + Y + Z));
            float z = Z;// =( 1 - (x + y) );
            int k = 0;
          
            float Kcolor = 0;
        
            int[] colordt = new int[500];
            if (dt1 == null)
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
           
            colordt = colordt.Distinct().ToArray();
            if (rdA.Checked && rd2.Checked)
            {
                for (int m = 0; m < 79; m++)
                {
                    if (m == 78)
                    {
                        X = X + (A[m - 1] * X2[m - 1] * dt1[m]);
                        Y = Y + (A[m - 1] * Y2[m - 1] * dt1[m]);
                        Z = Z + (A[m - 1] * Z2[m - 1] * dt1[m]);
                        Kcolor += (A[m - 1] * Y2[m - 1]);
                    }
                    else
                    {
                        X = X + (A[m] * X2[m] * dt1[m]);
                        Y = Y + (A[m] * Y2[m] * dt1[m]);
                        Z = Z + (A[m] * Z2[m] * dt1[m]);
                        Kcolor += (A[m] * Y2[m]);
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
            else if (rdA.Checked && rd10.Checked)
            {
                for (int i = 420; i <= 750; i = i + 5)
                {
                    X = X + (A[k] * X10[k] * dt1[k]);
                    Y = Y + (A[k] * Y10[k] * dt1[k]);
                    Z = Z + (A[k] * Z10[k] * dt1[k]);
                    Kcolor += (A[k] * Y10[k]);
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
            else if (rdD65.Checked && rd10.Checked)
            {
                for (int i = 420; i <= 750; i = i + 5)
                {
                    X = X + (D65[k] * X10[k] * dt1[k]);
                    Y = Y + (D65[k] * Y10[k] * dt1[k]);
                    Z = Z + (D65[k] * Z10[k] * dt1[k]);
                    Kcolor += (D65[k] * Y10[k]);
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

            else if (rdD65.Checked && rd2.Checked)
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
                        Kcolor += (D65[m - 1] * Y2[m - 1]);
                    }
                    else
                    {
                        X = X + (D65[m] * X2[m] * dt1[m]);
                        Y = Y + (D65[m] * Y2[m] * dt1[m]);
                        Z = Z + (D65[m] * Z2[m] * dt1[m]);
                        Kcolor += (D65[m] * Y2[m]);
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

            XYZg.Visible = true;
            RGBg.Visible = true;
            CIELuvg.Visible = true;
            LABg.Visible = true;
            CIExyYg.Visible = true;
            CIElchuv.Visible = true;
            LCHABg.Visible = true;
            //   fchart.LMSg.Visible = true;
            Hunterg.Visible = true;
            CIEXlbl.Text = "X=" + x.ToString("N4");
            CIEYlbl.Text = "Y=" + y.ToString("N4");
            CIEZlbl.Text = "Z=" + z.ToString("N4");



            Colourful.Conversion.ColourfulConverter a = new ColourfulConverter();
            Colourful.XYZColor xyz = new Colourful.XYZColor(x, y, z);
            ColourfulConverter a1 = new ColourfulConverter();
            a1.ToLab(xyz);
          //  lblWithepoint.Text =   a1.WhitePoint.ToString();
                
                
                
                LabColor la = a1.ToLab(xyz);
            
            CIELlbl.Text = "L=" + la.L.ToString("N4");
            CIEAlbl.Text = "a=" + la.a.ToString("N4");
            CIEBlbl.Text = "b=" + la.b.ToString("N4");

           


            Colourful.LuvColor luvc = a1.ToLuv(xyz);
            CIELuvLlbl.Text = "L=" + luvc.L.ToString("N4");
            CIELuvUlbl.Text = "u=" + luvc.u.ToString("N4");
            CIELuvVlbl.Text = "v=" + luvc.v.ToString("N4");

            Colourful.xyYColor xyY = a1.ToxyY(xyz);
            CIExyYxlbl.Text = "x=" + xyY.x.ToString("N4");
            CIExyYylbl.Text = "y=" + xyY.y.ToString("N4");
            CIExyYy1lbl.Text = "L=" + xyY.Luminance.ToString("N4");
            Colourful.LChuvColor lchuv = a1.ToLChuv(xyz);

            LCHLlbl.Text = "L=" + lchuv.L.ToString("N4");
            LCHClbl.Text = "C=" + lchuv.C.ToString("N4");
            LCHHlbl.Text = "h=" + lchuv.h.ToString("N4");
            Colourful.LChabColor lchab = a1.ToLChab(xyz);

            LCHabLlbl.Text = "L=" + lchab.L.ToString("N4");
            LCHabClbl.Text = "C=" + lchab.C.ToString("N4");
            LCHabhlbl.Text = "h=" + lchab.h.ToString("N4");
            //Colourful.LMSColor lms = a1.ToLMS(xyz);
            //fchart.LMSLbl.Text = "L=" + lms.L.ToString("N4");
            //fchart.LMSMlbl.Text = "M=" + lms.M.ToString("N4");
            //fchart.LMSSlbl.Text = "S=" + lms.S.ToString("N4");
            Colourful.HunterLabColor hunter = a1.ToHunterLab(xyz);
            hunterLlbl.Text = "L=" + hunter.L.ToString("N4");
            Hunteralbl.Text = "a=" + hunter.a.ToString("N4");
            hunterblbl.Text = "b=" + hunter.b.ToString("N4");

            Color rgb = a.ToRGB(xyz);
            Rlbl.Text = "R=" + rgb.R.ToString();
            Glbl.Text = "G=" + rgb.G.ToString();
            Blbl.Text = "B=" + rgb.B.ToString();
            double  C = 1 - (rgb.R/ 255f);
            double  M = 1 - (rgb.G / 255f);
            double  Y1 = 1 - (rgb.B / 255f);
            double K1 = 1;
            if (C < K1)
                K1 = C;
            if (M < K1)
                K1 = M;
            if (Y1 < K1)
                K1 = Y1;
         if(K1==1)
            {
                C = 0;
                M = 0;
                Y1 = 0;
            }else
            {
                C = (C - K1) / (1 - K1);
                M = (M - K1) / (1 - K1);
                Y1 = (Y1 - K1) / (1 - K1);
            }
            lblC.Text  = "C=" + C.ToString("N4");
            lblM.Text = "M=" + M.ToString("N4");
            lblY.Text = "Y=" + Y1.ToString("N4");
            lblK.Text = "K=" + K1.ToString("N4");

            int match = FindNearestColor(colormach, rgb);





            chart1.Series["palete"].Points.AddXY(XvalColor[match], YvalColor[match]);
            measurment = false;
        }
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

        private void FChart_Load(object sender, EventArgs e)
        {
            rd2.Checked  = true;
            rdA.Checked = true;
            this._lbl_form_text.Text = "Chart";
            IsRun = true;
            chart1.MouseWheel += new MouseEventHandler(this.ChartMOuseWhell);

            chart1.ChartAreas[0].AxisX.LineColor = Color.Red;
            chart1.ChartAreas[0].AxisX.LineColor = Color.Red;
            chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Blue;

           
            chart1.ChartAreas[0].AxisX.InterlacedColor = Color.Brown ;
            chart1.ChartAreas[0].AxisY.InterlacedColor = Color.Brown;
          
            chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.NotSet  ;
           // chart1.ChartAreas[0].AxisX.Interval = 2;
            
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Brown;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Brown;
            this.chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            this.chart1.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
            
            this.chart1.ChartAreas["ChartArea1"].CursorY.IsUserSelectionEnabled = true;
            this.chart1.ChartAreas["ChartArea1"].CursorY.IsUserEnabled = true;
            this.chart1.Legends["Legend1"].Title = "Color Measurement";
            SelectColor();
            t.Tick += T_Tick;
            t.Interval = 500;
          
        }

        private void T_Tick(object sender, EventArgs e)
        {
            colorMeasurment();
          
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
                    
                    viewMinimum = this.chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
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

        private void flatButton1_Click(object sender, EventArgs e)
        {try
            {
                button1.PerformClick();
                lSamlpe1 = Convert.ToDouble((CIELlbl.Text.Remove(0,2)));
                aSamlpe1= Convert.ToDouble((CIEAlbl.Text.Remove(0,2)));
                bSamlpe1 = Convert.ToDouble((CIEBlbl.Text.Remove(0, 2)));
                lblLref.Text = CIELlbl.Text;
                lblARef.Text = CIEAlbl.Text;
                lblBref.Text = CIEBlbl.Text;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void flatButton4_Click(object sender, EventArgs e)
        {
            try
            {
                button1.PerformClick();
                lSamlpe2 = Convert.ToDouble((CIELlbl.Text.Remove(0, 2)));
                aSamlpe2 = Convert.ToDouble((CIEAlbl.Text.Remove(0, 2)));
                bSamlpe2 = Convert.ToDouble((CIEBlbl.Text.Remove(0, 2)));
                lblLSample.Text = CIELlbl.Text;
                lblASample.Text = CIEAlbl.Text;
                lblBSample.Text = CIEBlbl.Text;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      
        private void btnClacDelta_Click(object sender, EventArgs e)
        {
           
            Colourful.LabColor la1 = new LabColor(lSamlpe1, aSamlpe1, bSamlpe1);
            Colourful.LabColor la2 = new LabColor(lSamlpe2, aSamlpe2, bSamlpe2);
            double Deltae76 = DeltaE76(lSamlpe1, aSamlpe1, bSamlpe1, lSamlpe2, aSamlpe2, bSamlpe2);
            lblDeltae76.Text = "ΔE76=" + DeltaE76(lSamlpe1, aSamlpe1, bSamlpe1, lSamlpe2, aSamlpe2, bSamlpe2).ToString("N3");
            lblDeltaE94.Text = "ΔE94=" + DeltaE96(lSamlpe1, aSamlpe1, bSamlpe1, lSamlpe2, aSamlpe2, bSamlpe2).ToString("N3");
            lblDeltaE2000.Text = "ΔE00=" + new Colourful.Difference.CIEDE2000ColorDifference().ComputeDifference(la1, la2).ToString("N3");
            lblDeltaECmc.Text = "ΔECMC=" + new Colourful.Difference.CMCColorDifference(Colourful.Difference.CMCColorDifferenceThreshold.Acceptability).ComputeDifference(la1, la2).ToString("N3");



          
        }



        private double DeltaE76(double l1, double a1, double b1, double l2, double a2, double b2)
        {
            return Math.Sqrt(Math.Pow((l1 - l2), 2) + Math.Pow((a1 - a2), 2) + Math.Pow((b1 - b2), 2));
        }
        private double DeltaE96(double l1, double a1, double b1, double l2, double a2, double b2)
        {
            double DeltaL = l1 - l2;
            double C1 = Math.Sqrt((Math.Pow(a1, 2)) + (Math.Pow(b1, 2)));
            double C2 = Math.Sqrt((Math.Pow(a2, 2)) + (Math.Pow(b2, 2)));
            double DeltaA = a1 - a2;
            double DeltaB = b1 - b2;
            double DeltaC = C1 - C2;
            double DeltaH = Math.Sqrt(Math.Pow(DeltaA, 2) + Math.Pow(DeltaB, 2) - Math.Pow(DeltaC, 2));
            double Sc = 1 + (0.045 * C1);
            double Sh = 1 + (0.014 * C1);
            double Result = Math.Sqrt(Math.Pow(DeltaL, 2) + Math.Pow(DeltaC / Sc, 2) + Math.Pow(DeltaH / Sh, 2));
            
            return Result ;
        }
       private double DeltaE2000(double l1, double a1, double b1, double l2, double a2, double b2)
        {
            double DeltaL = l1 - l2;
            double LBar = (l1 + l2) / 2;
            double C1 = Math.Sqrt((Math.Pow(a1, 2)) + (Math.Pow(b1, 2)));
            double C2 = Math.Sqrt((Math.Pow(a2, 2)) + (Math.Pow(b2, 2)));
         //   double CBar=()

            return 0;
        }
        private void flatButton2_Click(object sender, EventArgs e)
        {

        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
               
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

                                    this.tooltip.Show(string.Concat(new object[] { "X=", point2.XValue.ToString("N4"), ", Y=", point2.YValues[0].ToString("N4") }), this.chart1, location.X, location.Y - 15);
                                lblposition.Text = "X=" + point2.XValue.ToString("N2") + " , " + "Y=" + point2.YValues[0].ToString("N2");
                                //label1.Text = "X=" + point2.XValue.ToString("N2");
                                //label2.Text= "Y=" + point2.YValues[0].ToString("N2");
                                //label3.Text = "Z=" + ( 1-(point2.XValue + point2.YValues[0])).ToString("N2"); 


                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].Area3DStyle.Enable3D = checkBox1.Checked;

        }

        private void FChart_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsRun = false;
         //   Colorm = false;
        }

       
        private void rd2_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
