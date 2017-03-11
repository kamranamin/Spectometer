using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Spectometer
{
    class DataAnalysis
    {
        public void SavePacket(float[] Packet, string FileName)
        {
            try
            {
                 

                Packet graph = new Packet();
                for (int i = 0; i < 0x826; i++)
                {
                    graph.packet[i] = Packet[i];
                }
                IFormatter formatter = new BinaryFormatter();
                FileStream serializationStream = new FileStream(FileName, FileMode.Create, FileAccess.Write);
                formatter.Serialize(serializationStream, graph);
                serializationStream.Close();
            }
            catch (SerializationException exSerialization)
            {
                throw exSerialization;
            }
            catch (IOException IoException)
            {

                throw IoException;

            }

        }
        public float []  ReadSavedPacket( string FileName)
        {
            try
            {
                float[] Saved = new float[2090];
                Packet packet = new Packet();
                IFormatter formatter = new BinaryFormatter();
                FileStream serializationStream = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                packet = formatter.Deserialize(serializationStream) as Packet;
                for (int i = 0; i < 0x826; i++)
                {
                    Saved [i] = packet.packet[i];
                }
                return Saved;
            }
            catch (SerializationException exSerialization)
            {
                throw exSerialization;
            }
            catch (IOException IoException)
            {
                throw  IoException;
            }
           
        }
        public float [] Transmittance (float [] data,float []drak,float[] refrence)
        {
            float[] tra = new float[2090];
           
            for (int i=0;i<2090; i++)
            {
                tra[i] = ((data[i] - drak[i]) / (refrence[i] - drak[i]))*100;
                if (Double.IsNaN(tra[0])|| Double.IsInfinity(tra[0]))
                    {
                    tra[0] = 0;
                }
              else   if (Double.IsNaN( tra[i]) || Double.IsInfinity(tra[i]))
                {
                    tra[i] = tra[i - 1];
                }
              
            }
            return tra;
        }
        public float[] Absorbance(float[] data, float[] drak, float[] refrence)
        {
            float[] abs = new float[2090];

            for (int i = 0; i < 2090; i++)
            {
                abs[i] = - Convert.ToSingle ( Math.Log( ((data[i] - drak[i]) / (refrence[i] - drak[i]))));
                if (Double.IsNaN(abs[0]) || Double.IsInfinity(abs[0]))
                {
                    abs[0] = 0;
                }
                else if (Double.IsNaN(abs[i]) || Double.IsInfinity(abs[i]))
                {
                    abs[i] = abs[i - 1];
                }

            }
            return  abs;
        }
        public float [] RamanData(float [] Intensity)
        {
            float [] RamanValue = new float [Intensity.Length];
            for (int i = 0; i < Intensity.Length; i++)
            {
            

                RamanValue[i] =  Convert.ToSingle ( ((1f / 532f) - (1f / Intensity[i])) * Math.Pow(10, 7));
                if (Double.IsNaN(RamanValue [0]) || Double.IsInfinity(RamanValue[0]))
                {
                    RamanValue[0] = 0;
                }
                if (Double.IsNaN(RamanValue [i]) || Double.IsInfinity(RamanValue [i]))
                {
                    RamanValue[i] =  RamanValue [i - 1];
                }



            }


            return RamanValue;

        }
        public float [] Refractive (float [] Land,float [] Refrence)
        {
            float[] RefrectiveData = new float[2090];
            for (int i=0;i<2090;i++)
            {
                RefrectiveData[i] = Refrence[i] * Convert.ToSingle  (Math.Cos(Land[i] / 4));
            }
            return RefrectiveData;
        }
    }

}
