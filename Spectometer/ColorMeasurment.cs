using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectometer
{
    [Serializable]

    class ColorMeasurment
    {
        public double []X = new double [100] ;
        public double[] Y = new double[100];
        public double[] Z = new double[100];
    }
}
