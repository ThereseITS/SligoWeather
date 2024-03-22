using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sligoRain;

namespace SligoWeather
{
    internal class RainReportGenerator
    {

        List<DailyRain> rainfall;

        public RainReportGenerator(List<DailyRain> rainfall)
        {
            this.rainfall = rainfall;
        }
        /// <summary>
        /// This method uses the StringBuilder class to build a report (string) which is returned
        /// Doing it this way means that the returned string can be written to the console or a file.
        
        /// </summary>
        /// <returns></returns>
        public string DaysReport()
        {

            StringBuilder stringBuilder = new StringBuilder();

            int[] dayCounts = new int[7];
            double[] dayAmounts = new double[7];
       

// Count the number of times it rained on a particular day, and the total rainfall for that day
 
            foreach (DailyRain dr in rainfall)
            {
                int index = (int) dr.DateOfReading.DayOfWeek;                
                dayAmounts[index] += dr.Rain;
                if(dr.Rain>0) dayCounts[index]++;
            }
            stringBuilder.AppendLine($"\n\n{"Day",-20} {"Times Raining",-20}\t\t{"Amount of Rain",-20}\n");

 // report on rainfall in each day of week.

            for (int i = 0; i < dayCounts.Length; i++)
            {
                stringBuilder.AppendLine($"{Enum.GetName(typeof(DayOfWeek),i),-20}\t\t{dayCounts[i],-20}\t\t{dayAmounts[i],-20:F2}");

            }

            return stringBuilder.ToString();    
        }

        /// <summary>
        /// This method will find which band a metric is in and return the index. It works from the lower boundary.
        /// </summary>
        /// <param name="rain"></param>
        /// <param name="bands"></param>
        /// <returns></returns>
        int GetRainBandIndexL(double rain, int[] bands)
        { 
            int index = 0;

            for(int i = 0;i < bands.Length-1;i++)
            {
                if (rain >= bands[i] && rain < bands[i + 1])
                    return i;
            }
            if( rain>= bands[bands.Length-1])
            {
                return bands.Length-1;
            }

            return -1; 
        }
        /// <summary>
        /// This method uses the StringBuilder class to build a report (string) which is returned
        /// Doing it this way means that the returned string can be written to the console or a file.
        /// </summary>
        /// <param name="bands"> This is an int array specifying the lower boundary of each band for the report</param>
        /// <returns></returns>
       public string BandsReportL(int[] bands)
        {
            StringBuilder stringBuilder = new StringBuilder();
           
            //assumes the bands begin from the lower band


            int[] bandCounts = new int[bands.Length];

            // Count

            foreach( DailyRain dr in rainfall)
            {
                bandCounts[GetRainBandIndexL(dr.Rain,bands)] ++;
            }

            //Report on how many days rainfall in each band.


            Console.WriteLine($"\n\nRain Report\n");


            for (int i = 0; i < bandCounts.Length - 1; i++)
            {
                stringBuilder.AppendLine($"{bands[i]}-{bands[i + 1]}\t{bandCounts[i]}");
            }

            stringBuilder.AppendLine($" > {bands[bandCounts.Length - 1]}\t{bandCounts[bandCounts.Length - 1]}");
   
        
        return stringBuilder.ToString();
        
        }
    }
}
