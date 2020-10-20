using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver
{
    class Program
    {
        static void Main(string[] args)
        {
          
            Process proc = new Process();

            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Installing EnScix Driver .....");
                System.Threading.Thread.Sleep(200);
                Console.WriteLine("Please Wait .....");

                proc.StartInfo.FileName = "reg.exe";
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = true;

                string path = @"EnScix.reg";
                string command = "import " + path;
                proc.StartInfo.Arguments = command;
                
                proc.Start();

                proc.WaitForExit();
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("driver installed successfully");
                System.Threading.Thread.Sleep(200);
                Console.WriteLine("Please restart the computer");
                System.Threading.Thread.Sleep(200);
                Console.WriteLine("Press any key to exit");



                Console.ReadKey();
            }
            catch (System.Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Error"+ ex.Message);
                Console.ReadKey();
                proc.Dispose();

            }

        }
    }
}
