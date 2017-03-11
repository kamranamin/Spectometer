namespace Spectometer
{
    using FTD2XX_NET;
    using System;
    using System.Threading;

    internal class Interface
    {
        private FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;
        private FTDI myFtdiDevice = new FTDI();

        public void CloseDevice()
        {
            if (this.myFtdiDevice.IsOpen)
            {
                this.myFtdiDevice.Close();
            }
        }

        public int OpenDeveice(string description, uint BaudRate)
        {
            uint devcount = 0;
            this.ftStatus = this.myFtdiDevice.GetNumberOfDevices(ref devcount);
            if (this.ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return -1;
            }
            if (devcount == 0)
            {
                return -2;
            }
            if (!this.myFtdiDevice.IsOpen)
            {
                this.ftStatus = this.myFtdiDevice.OpenByDescription(description);
            }
            else
            {
                return -3;
            }
            if (this.ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return -4;
            }
            this.ftStatus = this.myFtdiDevice.SetBaudRate(BaudRate);
            if (this.ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return -5;
            }
            this.ftStatus = this.myFtdiDevice.SetDataCharacteristics(8, 0, 0);
            if (this.ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return -6;
            }
            this.ftStatus = this.myFtdiDevice.SetFlowControl(0x100, 0x11, 0x13);
            if (this.ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return -7;
            }
            this.ftStatus = this.myFtdiDevice.SetTimeouts(0x1388, 0);
            if (this.ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return -8;
            }
            return 1;
        }
        
        public int Read(ref byte[] ReadBuffer)
        {
            uint rxQueue = 0;
            this.ftStatus = this.myFtdiDevice.GetRxBytesAvailable(ref rxQueue);
            if (this.ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return -1;
            }
            uint numBytesRead = 0;
            if (rxQueue > 0)
            {
                this.ftStatus = this.myFtdiDevice.Read(ReadBuffer, rxQueue, ref numBytesRead);
                if (this.ftStatus != FTDI.FT_STATUS.FT_OK)
                {
                    return -2;
                }
                return Convert.ToInt32(numBytesRead);
            }
            if (rxQueue < 0)
            {
                return -10;
            }
            return 0;
        }

        public int ReadPacket(ref byte[] Packet, uint PacketLength)
        {
            uint rxQueue = 0;
            uint numBytesRead = 0;
            do
            {
                this.ftStatus = this.myFtdiDevice.GetRxBytesAvailable(ref rxQueue);
                if (this.ftStatus != FTDI.FT_STATUS.FT_OK)
                {
                    return -1;
                }
                Thread.Sleep(2);
            }
            while (rxQueue < PacketLength);
            this.ftStatus = this.myFtdiDevice.Read(Packet, PacketLength, ref numBytesRead);
            if (this.ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return -2;
            }
            return Convert.ToInt32(numBytesRead);
        }

        public int Write(byte[] WriteBuffer, int dataToWriteLength)
        {
            uint numBytesWritten = 0;
            this.ftStatus = this.myFtdiDevice.Write(WriteBuffer, dataToWriteLength, ref numBytesWritten);
            if (this.ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return -1;
            }
            return Convert.ToInt32(numBytesWritten);
        }
    }
}

