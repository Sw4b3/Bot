using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Services.Models
{
    public class WeatherReport
    {
        public List<Weather> Weather { get; set; }
        public Temperature Main { get; set; }
        public string Name { get; set; }
    }

    public class Weather
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class Temperature
    {
        public double Temp { get; set; }
        public int Humidity { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
    }
}
