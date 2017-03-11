using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spectometer
{
    [Serializable]
    class Environment
    {
        private  ushort  _average = 0;
        private  float _RE1 = 0f;
        private  ushort _RE2 = 0;
        private UInt32   _integrationTime = 0;
        private ushort _lampBrightness = 0;
        private ushort _smoothing = 0;
        public ushort  Average
        {
            get
            {
                return _average;
            }
            set
            {
                _average = value;
            }
        }
        public float  RE1
        {
            get
            {
                return _RE1 ;
            }
            set
            {
                _RE1  = value;
            }
        }
        public ushort  RE2
        {
            get
            {
                return _RE2;
            }
            set
            {
                _RE2 = value;
            }
        }
        public ushort LampBrightness
        {
            get
            {
                return _lampBrightness;
            }
            set
            {
                _lampBrightness = value;
            }
        }
        public ushort Smoothing
        {
            get
            {
               return  _smoothing;
            }
            set
            {
                _smoothing = value;
            }
        }
    public UInt32   IntegrationTime
        {
            get { return _integrationTime; }
            set { _integrationTime = value; }
        }
    }
}
