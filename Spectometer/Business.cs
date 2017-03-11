namespace Spectometer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    internal class Business
    {
        public static byte[] Hardwaredata = new byte[0x10];
        private Interface interface1 = new Interface();

        public int Absorbance(ushort[] Scope, uint ScopeLength, ushort[] Dark, ushort[] Reference, ref float[] absorbance)
        {
            for (int i = 0; i < ScopeLength; i++)
            {
                double num = Scope[i] - Dark[i];
                double num2 = Reference[i] - Dark[i];
                if (num2 == 0.0)
                {
                    num2 = 1E-11;
                }
                double a = num / num2;
                if (a > 0.0)
                {
                    absorbance[i] = (float) (-1.0 * Math.Log(a, 10.0));
                }
                else if (i == 0)
                {
                    absorbance[i] = 0f;
                }
                else
                {
                    absorbance[i] = absorbance[i - 1];
                }
            }
            return 1;
        }

        public int Average(ushort[,] Packets, uint PacketLength, uint average, ref ushort[] AveragedPacket)
        {
            double num = 0.0;
            for (int i = 0; i < PacketLength; i++)
            {
                num = 0.0;
                for (int j = 0; j < average; j++)
                {
                    num += Packets[j, i];
                }
                num /= (double) average;
                AveragedPacket[i] = Convert.ToUInt16(num);
            }
            return 1;
        }

        public int CalCurve(List<float> AX, List<float> AY, int n, ref float m, ref float b, ref float Rtwo)
        {
            int num6;
            Rtwo = 0f;
            m = 0f;
            b = 0f;
            float num = 0f;
            float num2 = 0f;
            float num3 = 0f;
            float num4 = 0f;
            float num5 = 0f;
            n = AX.Count;
            for (num6 = 0; num6 < n; num6++)
            {
                num += AX[num6] * AY[num6];
            }
            for (num6 = 0; num6 < n; num6++)
            {
                num4 += AX[num6];
            }
            for (num6 = 0; num6 < n; num6++)
            {
                num5 += AY[num6];
            }
            for (num6 = 0; num6 < n; num6++)
            {
                num2 = num4 * num5;
            }
            for (num6 = 0; num6 < n; num6++)
            {
                num3 += AX[num6] * AX[num6];
            }
            m = ((n * num) - num2) / ((n * num3) - (num4 * num4));
            b = ((num3 * num5) - (num * num4)) / ((n * num3) - (num4 * num4));
            float num7 = 0f;
            for (num6 = 0; num6 < n; num6++)
            {
                num7 += AY[num6] * AY[num6];
            }
            Rtwo = (float) (((double) ((n * num) - (num4 * num5))) / Math.Sqrt((double) (((n * num3) - (num4 * num4)) * ((n * num7) - (num5 * num5)))));
            Rtwo *= Rtwo;
            return 1;
        }

        public void CloseDevice()
        {
            this.interface1.CloseDevice();
        }

        public float[] Derivate(float[] AY, float[] AX)
        {
            float[] numArray = new float[AX.Length];
            try
            {
                for (int i = 1; i < (AX.Length - 1); i++)
                {
                    numArray[i] = (AY[i + 1] - AY[i - 1]) / (AX[i + 1] - AX[i - 1]);
                }
                numArray[0] = numArray[1];
                numArray[AX.Length - 1] = numArray[AX.Length - 2];
            }
            catch (Exception)
            {
                return numArray;
            }
            return numArray;
        }

        public byte[] GetHardwareData() => 
            Hardwaredata;

        public int HardwareAthenticate(ref byte[] RetByte)
        {
            int num = 0;
            byte[] writeBuffer = new byte[4];
            Random random = new Random();
            byte num2 = Convert.ToByte(random.Next(0, 5));
            Random random2 = new Random();
            byte num3 = Convert.ToByte(random2.Next(0, 0x7f));
            byte num4 = Convert.ToByte(random2.Next(0, 0x7f));
            byte num5 = 0;
            int num6 = 0;
            switch (num2)
            {
                case 0:
                    num5 = Convert.ToByte((int) (num3 & num4));
                    break;

                case 1:
                    num5 = Convert.ToByte((int) (num3 | num4));
                    break;

                case 2:
                    num5 = Convert.ToByte((int) (num3 ^ num4));
                    break;

                case 3:
                    num6 = Convert.ToInt16(~(num3 & num4));
                    if (num6 < 0)
                    {
                        num6 = (0xff + num6) + 1;
                    }
                    num5 = Convert.ToByte(num6);
                    break;

                case 4:
                    num6 = Convert.ToInt16(~(num3 | num4));
                    if (num6 < 0)
                    {
                        num6 = (0xff + num6) + 1;
                    }
                    num5 = Convert.ToByte(num6);
                    break;

                case 5:
                    num6 = Convert.ToInt16(~(num3 ^ num4));
                    if (num6 < 0)
                    {
                        num6 = (0xff + num6) + 1;
                    }
                    num5 = Convert.ToByte(num6);
                    break;

                case 6:
                    num5 = 0;
                    break;

                case 7:
                    num5 = 0;
                    break;

                default:
                    num5 = 0;
                    break;
            }
            writeBuffer[0] = 4 ;
            writeBuffer[1] = num2;
            writeBuffer[2] = num3;
            writeBuffer[3] = num4;
            if (this.interface1.Write(writeBuffer, 4) > 0)
            {
                num = this.interface1.ReadPacket(ref RetByte, 1);
                if (RetByte[0] == num5)
                {
                    return 1;
                }
            }
            return -1;
        }

        public int LampBrightness(byte Brightness0, byte Brightness1)
        {
            byte[] writeBuffer = new byte[] { 0x35, Brightness0, Brightness1, 0 };
            return this.interface1.Write(writeBuffer, 4);
        }

        public int MapFormulaX(int PacketLength, ref float[] PacketX, float I, float C1, float C2, float C3)
        {
            for (int i = 0; i < PacketLength; i++)
            {
                PacketX[i] = ((I + (C1 * i)) + ((C2 * i) * i)) + (((C3 * i) * i) * i);
            }
            return 1;
        }

        public int MapFormulaY(int PacketLength, ref float[] PacketY, float I, float C1, float C2, float C3, float C4, float C5, float C6, float C7, float C8)
        {
            try
            {
                for (int i = 0; i < PacketLength; i++)
                {
                    float num = PacketY[i] * PacketY[i];
                    float num2 = num * PacketY[i];
                    float num3 = num2 * PacketY[i];
                    float num4 = num3 * PacketY[i];
                    float num5 = num4 * PacketY[i];
                    float num6 = num5 * PacketY[i];
                    float num7 = num6 * PacketY[i];
                    float num9 = (((((((I + (C1 * PacketY[i])) + (C2 * num)) + (C3 * num2)) + (C4 * num3)) + (C5 * num4)) + (C6 * num5)) + (C7 * num6)) + (C8 * num7);
                }
            }
            catch
            {
                return -1;
            }
            return 1;
        }

        public int MergeByteArray(ref byte[] first, ref int m, byte[] second, int n)
        {
            for (int i = 0; i < n; i++)
            {
                first[m + i] = second[i];
            }
            m += n;
            return 1;
        }

        public int OpenDevice(string Description, uint Baudrate) => 
            this.interface1.OpenDeveice(Description, Baudrate);

        public int Read(ref byte[] ReadBuffer) => 
            this.interface1.Read(ref ReadBuffer);

        public int ReadHardwareFile()
        {
            try
            {
                Hardware hardware = new Hardware();
                IFormatter formatter = new BinaryFormatter();
                FileStream serializationStream = new FileStream("HardwareSetup.dat", FileMode.Open, FileAccess.Read);
                hardware = formatter.Deserialize(serializationStream) as Hardware;
                for (int i = 0; i < 0x10; i++)
                {
                    Hardwaredata[i] = hardware.HardwareData[i];
                }
            }
            catch (SerializationException)
            {
                return -1;
            }
            catch (IOException)
            {
                return -2;
            }
            return 1;
        }

        public int ReadHardwareSetup(ref byte[] Setup)
        {
            byte[] writeBuffer = new byte[] { 50, 0 };
            if (this.interface1.Write(writeBuffer, 2) <= 0)
            {
                return -1;
            }
            return this.interface1.ReadPacket(ref Setup, 0x10);
        }

        public int ReadPacket(ref byte[] ReadPacket) => 
            this.interface1.ReadPacket(ref ReadPacket, 0x1054);

        public int ReadSavedPacket(ref float [] Packet, string FileName)
        {
            try
            {
                Packet packet = new Packet();
                IFormatter formatter = new BinaryFormatter();
                FileStream serializationStream = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                packet = formatter.Deserialize(serializationStream) as Packet;
                for (int i = 0; i < 0x826; i++)
                {
                    Packet[i] = packet.packet[i];
                }
            }
            catch (SerializationException)
            {
                return -1;
            }
            catch (IOException)
            {
                return -2;
            }
            return 1;
        }

        public float[] ReversePacket(float[] AX)
        {
            float[] numArray = new float[AX.Length];
            try
            {
                for (int i = 0; i < AX.Length; i++)
                {
                    numArray[i] = AX[(AX.Length - i) - 1];
                }
            }
            catch (Exception)
            {
            }
            return numArray;
        }

        public int SaveHardwareFile(byte[] data)
        {
            try
            {
                Hardware graph = new Hardware();
                for (int i = 0; i < 0x10; i++)
                {
                    graph.HardwareData[i] = data[i];
                }
                IFormatter formatter = new BinaryFormatter();
                FileStream serializationStream = new FileStream("HardwareSetup.dat", FileMode.Create, FileAccess.Write);
                formatter.Serialize(serializationStream, graph);
                serializationStream.Close();
            }
            catch (SerializationException)
            {
                return -1;
            }
            catch (IOException)
            {
                return -2;
            }
            return 1;
        }

        public int SavePacket(ushort[] Packet, string FileName)
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
            catch (SerializationException)
            {
                return -1;
            }
            catch (IOException)
            {
                return -2;
            }
            return 1;
        }

        public int SendCommand(byte Command)
        {
            byte[] writeBuffer = new byte[] { 0x34, Command, 0 };
            return this.interface1.Write(writeBuffer, 3);
        }

        public void SetHardwareData(byte[] data)
        {
            Hardwaredata[0] = data[0];
            Hardwaredata[1] = data[1];
            Hardwaredata[2] = data[2];
            Hardwaredata[3] = data[3];
            Hardwaredata[4] = data[4];
            Hardwaredata[5] = data[5];
            Hardwaredata[6] = data[6];
            Hardwaredata[7] = data[7];
            Hardwaredata[8] = data[8];
            Hardwaredata[9] = data[9];
            Hardwaredata[10] = data[10];
            Hardwaredata[11] = data[11];
            Hardwaredata[12] = data[12];
            Hardwaredata[13] = data[13];
            Hardwaredata[14] = data[14];
            Hardwaredata[15] = data[15];
        }

        public float[] SmoothingSingle(float[] AX, int a, int c)
        {
            float[] numArray = new float[c];
            int num = 0;
            for (int i = 0; i < c; i++)
            {
                num = 0;
                int num3 = 0;
                while (num3 < a)
                {
                    if ((i + num3) < c)
                    {
                        numArray[i] = AX[i + num3] + numArray[i];
                        num++;
                    }
                    num3++;
                }
                for (num3 = 0; num3 < a; num3++)
                {
                    if ((i - num3) > 0)
                    {
                        numArray[i] = AX[i - num3] + numArray[i];
                        num++;
                    }
                }
                numArray[i] = Convert.ToSingle((float) (numArray[i] / ((float) num)));
            }
            return numArray;
        }

        public float[] SmothingSinglesingle2(float[] AX, int a, int c, int time)
        {
            float[] numArray = new float[c];
            if ((a / 2) == 0)
            {
                a++;
            }
            int num = 0;
            for (int i = 0; i < time; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    num = 0;
                    int num4 = 0;
                    while (num4 < a)
                    {
                        if ((j + num4) < c)
                        {
                            numArray[j] = AX[j + num4] + numArray[j];
                            num++;
                        }
                        num4++;
                    }
                    for (num4 = 0; num4 < a; num4++)
                    {
                        if ((j - num4) > 0)
                        {
                            numArray[j] = AX[j - num4] + numArray[j];
                            num++;
                        }
                    }
                    numArray[j] = Convert.ToSingle((float) (numArray[j] / ((float) num)));
                }
            }
            return numArray;
        }

        public int Transmittance(ushort[] Scope, uint ScopeLength, ushort[] Dark, ushort[] Reference, ref float[] transmittance)
        {
            for (int i = 0; i < ScopeLength; i++)
            {
                double num = Scope[i] - Dark[i];
                double num2 = Reference[i] - Dark[i];
                if (num2 == 0.0)
                {
                    num2 = 0.001;
                }
                double num3 = num / num2;
                if (((num >= 0.0) && (num2 >= 0.0)) && (num3 > 0.0))
                {
                    transmittance[i] = (float) (100.0 * num3);
                }
                else if (i == 0)
                {
                    transmittance[i] = 0f;
                }
                else
                {
                    transmittance[i] = transmittance[i - 1];
                }
            }
            return 1;
        }

        public int TrustPacket(byte[] Packet, ref ushort PacketLength, ref ushort[] NewPacket, ref ushort NewPacketLength)
        {
            uint num = 0;
            uint num2 = 0;
            uint num3 = 0;
            if ((Packet[0] != 0xff) && (Packet[1] != 0xff))
            {
                return -1;
            }
            NewPacketLength = Convert.ToUInt16((int) ((PacketLength - 8) / 2));
            for (int i = 1; i < NewPacketLength; i++)
            {
                num2 = Packet[i * 2];
                num = Packet[(i * 2) + 1];
                num = num << 8;
                num3 = num + num2;
                num3 &= 0xfffc;
                NewPacket[i - 1] = Convert.ToUInt16(num3);
            }
            return NewPacketLength;
        }

        public int TrustPacket16Bit(byte[] Packet, ref ushort PacketLength, ref ushort[] NewPacket, ref ushort NewPacketLength)
        {
            uint num = 0;
            uint num2 = 0;
            uint num3 = 0;
            if ((Packet[0] != 0xff) && (Packet[1] != 0xff))
            {
                return -1;
            }
            NewPacketLength = Convert.ToUInt16((int) ((PacketLength - 8) / 2));
            for (int i = 1; i < NewPacketLength; i++)
            {
                num2 = Packet[i * 2];
                num = Packet[(i * 2) + 1];
                num = num << 8;
                num3 = num + num2;
                num3 &= 0xffff;
                NewPacket[i - 1] = Convert.ToUInt16(num3);
            }
            return NewPacketLength;
        }

        public int WriteHardwareSetup()
        {
            byte[] writeBuffer = new byte[] { 
                0x33, Hardwaredata[0], Hardwaredata[1], Hardwaredata[2], Hardwaredata[3], Hardwaredata[4], Hardwaredata[5], Hardwaredata[6], Hardwaredata[7], Hardwaredata[8], Hardwaredata[9], Hardwaredata[10], Hardwaredata[11], Hardwaredata[12], Hardwaredata[13], Hardwaredata[14],
                Hardwaredata[15], 0
            };
            return this.interface1.Write(writeBuffer, 0x12);
        }

        public int WriteRequestPacket(byte NumberOfFrame, uint IntegratiomTime)
        {
            byte[] writeBuffer = new byte[7];
            writeBuffer[0] = 0x31;
            writeBuffer[1] = NumberOfFrame;
            if (IntegratiomTime <= 19.6608)
            {
                ushort num2 = Convert.ToUInt16((double) (65536.0 - (((double) IntegratiomTime) / 0.0003)));
                writeBuffer[5] = Convert.ToByte((int) (num2 & 0xff));
                writeBuffer[4] = Convert.ToByte((int) ((num2 & 0xff00) >> 8));
                writeBuffer[3] = 1;
                writeBuffer[2] = 0;
            }
            else
            {
                uint num3 = Convert.ToUInt16((double) (((double) IntegratiomTime) / 19.6608));
                uint num4 = Convert.ToUInt16((double) (((double) IntegratiomTime) % 19.6608));
                writeBuffer[5] = Convert.ToByte((uint) (num4 & 0xff));
                writeBuffer[4] = Convert.ToByte((uint) ((num4 & 0xff00) >> 8));
                writeBuffer[3] = Convert.ToByte((uint) (num3 & 0xff));
                writeBuffer[2] = Convert.ToByte((uint) ((num3 & 0xff00) >> 8));
            }
            writeBuffer[6] = 0;
            return this.interface1.Write(writeBuffer, 7);
        }
    }
}

