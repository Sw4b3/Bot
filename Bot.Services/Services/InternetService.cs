using Bot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Services
{
    public class InternetService : IInternetService
    {
        public void SearchInternet(string searchTerm, string optionParameter)
        {
            try
            {
                var appliction = optionParameter != null ? optionParameter + "Search" : "googleSearch";
                var path = ConfigurationManager.AppSettings[appliction];
                Process.Start(path + searchTerm);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
