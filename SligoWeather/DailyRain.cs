using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sligoRain
{
    internal class DailyRain
    {
        DateOnly _date;
        double _rain;

        public DateOnly DateOfReading {  get {  return _date; } }

        public double Rain { get { return _rain; } } 

        public DailyRain(DateOnly date, double rain)
        {
            this._date = date;
            this._rain = rain;
        }

        public override string ToString()
        {
            return $" {_date} \t{_rain}";
        }
    }
}
