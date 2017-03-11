using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Spectometer
{
    [Serializable ]
    class SofwaretProperties
    {
        private  float _AbsorbanceX1 = 0f;
        private float _AbsorbanceX2 = 0f;
        private float _AbsorbanceY1 = 0f;
        private float _AbsorbanceY2 = 0f;
        private float _XmapC1 = 0f;
        private float _XmapC2 = 0f;
        private float _XmapC3 = 0f;
        private float _XmapI = 0f;
        private float _IrradianceX1 = 0f;
        private float _IrradianceX2 = 0f;
        private float _IrradianceY1 = 0f;
        private float _IrradianceY2 = 0f;
        private int _LampType = 0;
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
        private float _RamanX1=0f;
        private float _RamanX2 = 0f;
        private float _RamanY1 = 0f;
        private float _RamanY2 = 0f;

        public float RamanX1
        {
            get { return _RamanX1; }set { _RamanX1 = value; }
        }
        public float RamanX2
        {
            get { return _RamanX2; }
            set { _RamanX2 = value; }
        }
        public float RamanY1
        {
            get { return _RamanY1; }
            set { _RamanY2 = value; }
        }
        public float RamanY2
        {
            get { return _RamanY2; }
            set { _RamanY1 = value; }
        }
        public float AbsorbanceX1
        {
            get
            {
                return _AbsorbanceX1;
            }
            set
            {
                _AbsorbanceX1 = value;
            }
        }

        public float AbsorbanceX2
        {
            get
            {
                return _AbsorbanceX2;
            }
            set
            {
                _AbsorbanceX2 = value;
            }
        }
        public float AbsorbanceY1
        {
            get
            {
                return _AbsorbanceY1;
            }
            set
            {
                _AbsorbanceY1 = value;
            }
        }
        public float AbsorbanceY2
        {
            get
            {
                return _AbsorbanceY2;
            }
            set
            {
                _AbsorbanceY2 = value;
            }
        }
        
        public float XmapC1
        {
            get
            {
                return _XmapC1;
            }
            set
            {
                _XmapC1 = value;
            }
        }

        public float XmapC2
        {
            get
            {
                return _XmapC2;
            }
            set
            {
                _XmapC2 = value;
            }
        }
        public float XmapC3
        {
            get
            {
                return _XmapC3;
            }
            set
            {
                _XmapC3 = value;
            }
        }
        public float XmapI
        {
            get
            {
                return _XmapI ;
            }
            set
            {
                _XmapI = value;
            }
        }

        public float IrradianceX1
        {
            get
            {
                return _IrradianceX1;
            }
            set
            {
                _IrradianceX1 = value;
            }
        }
        public float IrradianceX2
        {
            get
            {
                return _IrradianceX2;
            }
            set
            {
                _IrradianceX2 = value;
            }
        }
        public float IrradianceY1
        {
            get
            {
                return _IrradianceY1;
            }
            set
            {
                _IrradianceY1 = value;
            }
        }
        public float IrradianceY2
        {
            get
            {
                return _IrradianceY2;
            }
            set
            {
                _IrradianceY2 = value;
            }
        }
        public float ScopeX1
        {
            get
            {
                return _ScopeX1;
            }
            set
            {
                _ScopeX1 = value;
            }
        }
        public float ScopeX2
        {
            get
            {
                return _ScopeX2;
            }
            set
            {
                _ScopeX2 = value;
            }
        }
        public float ScopeY1
        {
            get
            {
                return _ScopeY1;
            }
            set
            {
                _ScopeY1 = value;
            }
        }
        public float ScopeY2
        {
            get
            {
                return _ScopeY2;
            }
            set
            {
                _ScopeY2 = value;
            }
        }
        public float TransmittanceX1
        {
            get
            {
                return _TransmittanceX1;
            }
            set
            {
                _TransmittanceX1 = value;
            }
        }
        public float TransmittanceX2
        {
            get
            {
                return _TransmittanceX2;
            }
            set
            {
                _TransmittanceX2 = value;
            }
        }
        public float TransmittanceY1
        {
            get
            {
                return _TransmittanceY1;
            }
            set
            {
                _TransmittanceY1 = value;
            }
        }
        public float TransmittanceY2
        {
            get
            {
                return _TransmittanceY2;
            }
            set
            {
                _TransmittanceY2 = value;
            }
        }

        public float YmapC1
        {
            get
            {
                return _YmapC1;
            }
            set
            {
                _YmapC1 = value;
            }
        }

        public float YmapC2
        {
            get
            {
                return _YmapC2;
            }
            set
            {
                _YmapC2 = value;
            }
        }
        public float YmapC3
        {
            get
            {
                return _YmapC3;
            }
            set
            {
                _YmapC3 = value;
            }
        }
        public float YmapC4
        {
            get
            {
                return _YmapC4;
            }
            set
            {
                _YmapC4 = value;
            }
        }

        public float YmapC5
        {
            get
            {
                return _YmapC5;
            }
            set
            {
                _YmapC5 = value;
            }
        }
        public float YmapC6
        {
            get
            {
                return _YmapC6;
            }
            set
            {
                _YmapC6 = value;
            }
        }
       

        public float YmapC7
        {
            get
            {
                return _YmapC7;
            }
            set
            {
                _YmapC7 = value;
            }
        }
        public float YmapC8
        {
            get
            {
                return _YmapC8;
            }
            set
            {
                _YmapC8= value;
            }
        }
        public float YmapI
        {
            get
            {
                return _YmapI;
            }
            set
            {
                _YmapI = value;
            }
        }

        public int LampType
        {
            get
            {
                return _LampType;
            }
            set
            {
                _LampType = value;
            }
        }

      



    }



}
