using sligoRain;
//
// This version uses a class for rain data, and reads, parses, validates and creates a list of rain data objects from the file.
// A report generator class is used to generate reports on the data.This might work with other positive metrics like rainfall e.g.% cloud cover.
// Which alterations would be needed to report on continuous data, such as temperature?
//
namespace SligoWeather
{
        internal class Program
        {
            static void Main(string[] args)
            {
            string path = "../../../sligoRainfall.csv";
            string input = "";

            DateOnly date;
            double rain;

            List<DailyRain> rainfall = new List<DailyRain>();

            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    while ((input = sr.ReadLine()) != null)
                    {
                        string[] fields = input.Split(',');

                        if ((fields.Length == 2)                        // check that the data is valid.
                            && DateOnly.TryParse(fields[0], out date) 
                            && double.TryParse(fields[1], out rain) 
                            && rain >= 0)
                        {
                            DailyRain dr = new DailyRain(date, rain);
                            rainfall.Add(dr);
                        }
                        else
                        {
                            Console.WriteLine("Invalid record: " + input);
                        }

                    }
                }

                RainReportGenerator reporter = new RainReportGenerator(rainfall);

                int[] bands = { 0, 2,4, 6, 8, 10, 12,14};

                Console.WriteLine(reporter.DaysReport());
                Console.WriteLine(reporter.BandsReportL(bands));
                
            }
        }
    }

}

