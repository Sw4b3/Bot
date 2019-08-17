using Bot.Services.Interfaces;
using Bot.Services.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Services
{
    public class WeatherService : IWeatherService
    {
        private WeatherReport _weatherReport;

        private WeatherReport RetrieveWeatherForcast()
        {
            var APIKey = ConfigurationManager.AppSettings["OpenWeatherAPIKey"];
            var URL = ConfigurationManager.AppSettings["OpenWeatherURL"];
            var location = ConfigurationManager.AppSettings["location"];
            var weatherReport = new WeatherReport();

            string url = URL + location + "&APPID=" + APIKey + "&units=metric";

            using (var client = new WebClient())
            {
                var jsonContent = client.DownloadString(url);
                weatherReport = JsonConvert.DeserializeObject<WeatherReport>(jsonContent);
            }

            return weatherReport;
        }

        public WeatherReport GetWeatherForcast()
        {
            _weatherReport = _weatherReport ?? RetrieveWeatherForcast();
            return _weatherReport;
        }

        public double GetTemperature()
        {
            _weatherReport = _weatherReport ?? RetrieveWeatherForcast();
            return _weatherReport.Main.Temp;
        }
    }
}
