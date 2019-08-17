using Bot.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Services.Interfaces
{
    public interface IWeatherService
    {
        WeatherReport GetWeatherForcast();
        double GetTemperature();
    }
}
