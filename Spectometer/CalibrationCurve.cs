using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Spectometer
{
    [Serializable ]
    class CalibrationCurve
    {
       public  float []abs = new float[200];
       public  float[] cor = new float[200];
    }
}
