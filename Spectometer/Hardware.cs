using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spectometer
{
    [Serializable ]
    class Hardware
    {
        public byte[] HardwareData = new byte[0x10];
    }
}
