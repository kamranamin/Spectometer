using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectometer
{
    [Serializable ]
    class ChartData
    {
        public List<double > DataSerisList = new List<double >();
        public int SeriesTypes = 0;
    }
}
