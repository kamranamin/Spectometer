using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectometer
{
    [Serializable ]
    class CieData
    {
        public float[] A = new float[110];
        public float[] D65 = new float[110];
        public float[] X2 = new float[110];
        public float[] Y2 = new float[110];
        public float[] Z2 = new float[110];
        public float[] X10 = new float[110];
        public float[] Y10 = new float[110];
        public float[] Z10 = new float[110];
    }
}
