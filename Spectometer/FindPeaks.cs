using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectometer
{
  public static  class FindPeaks
    {
        public static double[,] findPeaks(double[] x, double Filter)
        {
            int N = x.Length;
            double[,] peaksAndLocations = new double[2, N];

            int j = 0; // counter for peaks and locations

            // case for when data has only one element, it's a peak by default
            if (N == 1)
            {
                peaksAndLocations[0, j] = x[0]; //peak
                peaksAndLocations[1, j] = 0; // location

            }
            else
            {

                // checking if first element is a peak
                if (x[0] > x[1])
                {
                    peaksAndLocations[0, j] = x[0];
                    peaksAndLocations[1, j] = 0;

                    j++;
                }
                List<double> Deflist = new List<double>();
                // case when the peaks lie in the middle of the array data
                for (int i = 1; i < N - 1; i++)
                {

                    if ((x[i] >= x[i - 1]) && (x[i] > x[i + 1]))
                    {
                        // compare element with next element before and after


                        peaksAndLocations[0, j] = x[i]; // peak
                        peaksAndLocations[1, j] = i; // location

                        j++;

                    }
                }

                // check if last element is a peak
                if (x[N - 1] > x[N - 2])
                {
                    peaksAndLocations[0, j] = x[N - 1]; // peak
                    peaksAndLocations[1, j] = N - 1; // location
                    j++;
                }

            }

            // trimming the extra zeros in the peaksAndLocations array  
            int numberOfPeaks = j;
            double[,] actualPeaksAndLocations = new double[2, numberOfPeaks]; // sized to the actual number of peaks and/or locations
            double persent = 0;
            for (int i = 0; i < numberOfPeaks; i++)
            { // up to numberOfPeaks to ignore extra zeros
                //if (i>0)
                //{
                //    double p = ((100 * peaksAndLocations[0, i - 1]) / peaksAndLocations[0, i]) - 100;
                //    persent = Math.Abs(p);
                //}
                if (peaksAndLocations[0, i] > Filter)
                {
                    actualPeaksAndLocations[0, i] = peaksAndLocations[0, i];
                    actualPeaksAndLocations[1, i] = peaksAndLocations[1, i];
                }


            }

            return actualPeaksAndLocations;
        }
    }
}
