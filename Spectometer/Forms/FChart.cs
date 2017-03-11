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

namespace Spectometer.Forms
{
    public partial class FChart : F_Base 
    {
     
        public bool IsRun;
     //   FTimeSpectrum  timeSpectrumFrm1 = new FTimeSpectrum ();
      
       
        private Point mousePoint;
        private Point? prevPosition = null;
        private ToolTip tooltip = new ToolTip();
        public FChart()
        {
            
            InitializeComponent();
        }

        private void FChart_Load(object sender, EventArgs e)
        {
            
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
            this.chart1.Legends["Legend1"].Title = "Time Spectrum";
          
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

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                chart1.ChartAreas[0].Area3DStyle.Enable3D = checkBox1.Checked  ;
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
                                lblposition.Text = "X=" + point2.XValue + " , " + "Y=" + ( int)point2.YValues[0];


                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void FChart_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsRun = false;
        }
    }
}
