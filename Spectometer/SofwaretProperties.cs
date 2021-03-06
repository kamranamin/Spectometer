﻿using System;
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
        private float _XmapC4 = 0f;
        private float _XmapC5 = 0f;
        private float _IrradianceX1 = 0f;
        private float _IrradianceX2 = 0f;
        private float _IrradianceY1 = 0f;
        private float _IrradianceY2 = 0f;
        private bool _tangestan = false;
        private bool _Dutrium = false;
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
        private int _BaseLine = 0;
        private int _FluorescenceX1 = 0;
        private int _FluorescenceX2 = 0;
        private int _FluorescenceY1 = 0;
        private int _FluorescenceY2 = 0;
        private bool  _enablebaseline = false;
        private bool _nm;
        private bool _cm;
        private bool _hideColorBar;
        private bool _NanoLed;
        private int _gain;
        private int _Offset;
        public int Gain { get { return _gain; } set { _gain = value; } }
        public int Offset { get {return _Offset; } set { _Offset = value; } }
        public bool EnableBaseLine { get { return _enablebaseline; } set { _enablebaseline = value; } }
        public int FluorescenceX1 { get { return _FluorescenceX1; }set { _FluorescenceX1 = value; } }
        public int FluorescenceX2 { get { return _FluorescenceX2; } set { _FluorescenceX2 = value; } }
        public int FluorescenceY1 { get { return _FluorescenceY1; } set { _FluorescenceY1 = value; } }
        public int FluorescenceY2 { get { return _FluorescenceY2; } set { _FluorescenceY2 = value; } }
        public bool Tngestan { get { return _tangestan; }set { _tangestan = value; } }
        public bool Dutrium { get { return _Dutrium; }set { _Dutrium = value; } }
        public bool NanoLed { get { return _NanoLed; } set { _NanoLed = value; } }
      
        public bool HideColorbar
        {
            get
            {
                return _hideColorBar;
            }
            set
            {
                _hideColorBar = value;
            }
        }
          public bool XvalCM { get { return _cm; }set { _cm = value; }}
        public bool XvalNM
        {
            get
            {
                return _nm;
            }
            set
            {
                _nm = value;
            }
        }
        public float RamanX1
        {
            get { return _RamanX1; }set { _RamanX1 = value; }
        }
        public int BaseLine
        {
            get { return _BaseLine; }set { _BaseLine = value; }
        }
        public float RamanX2
        {
            get { return _RamanX2; }
            set { _RamanX2 = value; }
        }
        public float RamanY1
        {
            get { return _RamanY1; }
            set { _RamanY1 = value; }
        }
        public float RamanY2
        {
            get { return _RamanY2; }
            set { _RamanY2 = value; }
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
        public float XmapC4
        {
            get
            {
                return _XmapC4;
            }
            set
            {
                _XmapC4 = value;
            }
        }
        public float XmapC5
        {
            get
            {
                return _XmapC5;
            }
            set
            {
                _XmapC5 = value;
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

      

      



    }



}
