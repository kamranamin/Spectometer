using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Spectometer
{
    [Serializable ]
   public  class SPoint
    {
        /// <summary>
        /// //
        /// </summary>
        public int PointColor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double XValue { get; set; }
        public double YValue { get; set; }
        public string Name { get; set; }
        public Color color { get; set; }

        public SPoint() { }

        public SPoint(int c, double xv, double yv, string n,Color col)
        {
            PointColor = c; XValue = xv; YValue = yv; Name = n;color  = col;
        }

        static public SPoint FromDataPoint(DataPoint dp)
        {
            return new SPoint(dp.Color.ToArgb(), dp.XValue, dp.YValues[0], dp.Color.Name ,dp.Color );
        }

        static public DataPoint FromSPoint(SPoint sp)
        {
            DataPoint dp = new DataPoint(sp.XValue, sp.YValue);
            dp.Color = Color.FromArgb(sp.PointColor);
            dp.Name = Color.FromArgb(sp.PointColor).Name ;
            dp.Color = sp.color;
            return dp;
        }
    }
}




