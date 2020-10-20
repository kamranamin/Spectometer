using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Spectometer
{
    class DataAnalysis
    {
       

        //public int numPixel = DeviceType.NumberOfIndex;
        public  void exportToExcel(float  [] m , string fileName)
        {

            /*Set up work book, work sheets, and excel application*/
            Microsoft.Office.Interop.Excel.Application oexcel = new Microsoft.Office.Interop.Excel.Application();
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                object misValue = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Excel.Workbook obook = oexcel.Workbooks.Add(misValue);
                Microsoft.Office.Interop.Excel.Worksheet osheet = new Microsoft.Office.Interop.Excel.Worksheet();


                //  obook.Worksheets.Add(misValue);

                osheet = (Microsoft.Office.Interop.Excel.Worksheet)obook.Sheets["Sheet1"];
                int colIndex = 0;
                int rowIndex = 1;

               for (int i=1;i < DeviceType.NumberOfIndex;i++)
                { 
                  
                    osheet.Cells[i , rowIndex] = m[i];
                }
             

                osheet.Columns.AutoFit();
             //   string filepath = "C:\\Temp\\Book1" ; 

                //Release and terminate excel

                obook.SaveAs(fileName);
                obook.Close();
                oexcel.Quit();
                releaseObject(osheet);

                releaseObject(obook);

                releaseObject(oexcel);
                GC.Collect();
            }
            catch (Exception ex)
            {
                oexcel.Quit();
              
            }
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {

                obj = null;
                

            }
            finally
            {
                GC.Collect();
            }
        }
        public void SavePacket(float[] Packet, string SavedFileName)
        {
            try
            {
                 

                Packet graph = new Packet();
                graph.packet = new float[DeviceType.NumberOfIndex];
                for (int i = 0; i < DeviceType.NumberOfIndex; i++)
                {
                    graph.packet[i] = Packet[i];
                }

               
               
                var filename = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Spectrometer\\"+SavedFileName);

                using (var serializationStream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite,FileShare.ReadWrite))
                {


                  

                    IFormatter formatter = new BinaryFormatter();
                  //  File.SetAttributes(filename, FileAttributes.Normal);

                 
                    
              

                    formatter.Serialize(serializationStream, graph);
                    serializationStream.Close();
                  
                }
            }
            catch (Exception ex)
            {
                throw ex;
               
            }
           
        

        }
        public float[] SmoothingS(float[] AX, int a)
        {
            float[] numArray = new float[AX.Length ];
            int num = 0;
            for (int i = 0; i < AX.Length ; i++)
            {
                num = 0;
                int num3 = 0;
                while (num3 < a)
                {
                    if ((i + num3) < AX.Length )
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
                numArray[i] = Convert.ToSingle((float)(numArray[i] / ((float)num)));
            }
            return numArray;
        }
        public float []  ReadSavedPacket( string SavedFileName)
        {
            try
            {
                var filename = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Spectrometer\\" + SavedFileName);
                Packet packet = new Packet();
               
                IFormatter formatter = new BinaryFormatter();
                FileStream serializationStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                packet = formatter.Deserialize(serializationStream) as Packet;
                float[] Saved = new float[packet.packet.Length];
              
                



                    for (int i = 0; i < packet.packet.Length; i++)
                    {

                        Saved[i] = packet.packet[i];
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
        public float[] B;
        public float []Irradians(float []data, float[] drak, float[] refrence,float []Xvalue)
        {

            B = new float[DeviceType.NumberOfIndex];
            float[] Irr = new float[DeviceType.NumberOfIndex];
         
            double   k = 1.38 * Math.Pow(10, -23);
            float  T = 3100;
            double c2 = Math.Pow(3 * Math.Pow(10, 8), 2);
          
            double h = 6.266 * Math.Pow(10, -34);
            double Btop = 2 * h * c2;
            double[] ee = new double[DeviceType.NumberOfIndex];
            for (int i = 0; i < Xvalue.Length ; i++)
            {
                double landa = Math.Pow(Xvalue[i] * Math.Pow(10, -9), 5);
                double   bd1 = h*c2 / Xvalue[i] * Math.Pow(10, -9) * k * T;
                double Down = Btop / landa;
                ee[i] = landa * (float)Math.Pow(Math.E, bd1) - 1;
                B[i]= ((float ) Btop)/(float) ee[i] ;
                Irr[i] = B[i] *( (data[i] - drak[i] )/( refrence[i] - drak[i]));
                if (float.IsNaN(Irr[i]) || float.IsInfinity(Irr[i]))
                    Irr[i] = 0;

            }
          
            return Irr; 
        }

        public float [] Transmittance (float [] data,float []drak,float[] refrence)
        {
            float[] tra = new float[DeviceType.NumberOfIndex];
           
            for (int i=0;i < DeviceType.NumberOfIndex; i++)
            {
                tra[i] = (data  [i] - drak[i]) / (refrence  [i] - drak[i])*100;
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
        public float[] Reflectance(float[] data, float[] drak, float[] refrence)
        {
            float[] tra = new float[DeviceType.NumberOfIndex];

            for (int i = 0; i < DeviceType.NumberOfIndex; i++)
            {
                tra[i] = (data[i] - drak[i]) / (refrence[i] - drak[i]) * 100;
                if (Double.IsNaN(tra[0]) || Double.IsInfinity(tra[0]))
                {
                    tra[0] = 0;
                }
                else if (Double.IsNaN(tra[i]) || Double.IsInfinity(tra[i]))
                {
                    tra[i] = tra[i - 1];
                }

            }
            return tra;
        }

        public double[] FirstColumn(string filename)
        {
            Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlsApp == null)
            {
                Console.WriteLine("EXCEL could not be started. Check that your office installation and project references are correct.");
                // return 0;
            }

            //Displays Excel so you can see what is happening
            //xlsApp.Visible = true;

            Workbook wb = xlsApp.Workbooks.Open(filename,
                0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true);
            Sheets sheets = wb.Worksheets;
            Worksheet ws = (Worksheet)sheets.get_Item(1);

            Range firstColumn = ws.UsedRange.Columns[1];
            System.Array myvalues = (System.Array)firstColumn.Cells.Value;

            string[] strArray = myvalues.OfType<double>().Select(o => o.ToString()).ToArray();
            double[] outArray = Array.ConvertAll<string, double>(strArray, Convert.ToDouble);


            return outArray;
        }
        public float[] Absorbance(float[] data, float[] drak, float[] refrence)
        {
            float[] abs = new float[DeviceType.NumberOfIndex];
            float[] k = Transmittance(data, drak, refrence);


            for (int i = 0; i < DeviceType.NumberOfIndex; i++)
            {
                abs[i] = Convert.ToSingle(Math.Log10 ((refrence[i] - drak[i]) / (data[i] - drak[i])));
                if (Double.IsNaN(abs[0]) || Double.IsInfinity(abs[0]))
                {
                    abs[0] = 0;
                }
                else if (Double.IsNaN(abs[i]) || Double.IsInfinity(abs[i]))
                {
                    abs[i] = abs[i - 1];
                }

                }
                return abs;
            
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
        public float [] Refractive (float [] data,float [] Refrence,float [] Dark)
        {
            float[] tra = new float[DeviceType.NumberOfIndex];

            for (int i = 0; i < DeviceType.NumberOfIndex; i++)
            {
                tra[i] = (data[i] - Dark[i]) / (Refrence[i] - Dark[i]) * Convert.ToSingle( Math.Cos(  i/4));
                if (Double.IsNaN(tra[0]) || Double.IsInfinity(tra[0]))
                {
                    tra[0] = 0;
                }
                else if (Double.IsNaN(tra[i]) || Double.IsInfinity(tra[i]))
                {
                    tra[i] = tra[i - 1];
                }

            }
            return tra;
            //float[] RefrectiveData = new float[numPixel];
            //for (int i=0;i<numPixel;i++)
            //{
            //    RefrectiveData[i] = Refrence[i] -Dark[i]* Convert.ToSingle  (Math.Cos(Land[i] ));
            //}
            //return RefrectiveData;
        }
        public float  [] smoothing(float  [] MainData,int Smoothing )
        {
            int b;
            float[] numSmoothing =new float[MainData.Length ];
         float    temp1 = 0;
            for (int m = 0; m < 15 + (Smoothing / 2); m++)
            {
                numSmoothing[m] = MainData[m];
            }
           
            for (int k = 15; k < MainData .Length - Smoothing; k++)
            {

               
                float temp = 0;
                for (int i = 0; i < Smoothing; i++)
                {

                    temp =temp+ MainData [k + i];
                }
                int f = k+ (Smoothing / 2);

                numSmoothing[f ] = temp / Smoothing;
                
              
            }
            return numSmoothing;
        }
        public float [] FirstColumnLa(string filename, int colNo)
        {
            Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlsApp == null)
            {
                Console.WriteLine("EXCEL could not be started. Check that your office installation and project references are correct.");
                // return 0;
            }

            //Displays Excel so you can see what is happening
            //xlsApp.Visible = true;

            Workbook wb = xlsApp.Workbooks.Open(filename,
                0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true);
            Sheets sheets = wb.Worksheets;
            Worksheet ws = (Worksheet)sheets.get_Item(1);

            Range firstColumn = ws.UsedRange.Columns[colNo];
            System.Array myvalues = (System.Array)firstColumn.Cells.Value;

            string[] strArray = myvalues.OfType<double>().Select(o => o.ToString()).ToArray();
            float [] outArray = Array.ConvertAll<string, float >(strArray, Convert.ToSingle);


            return outArray;
        }
    }

}
