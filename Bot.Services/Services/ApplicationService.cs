using Bot.Services.Interfaces;
using System;
using System.Configuration;
using System.Diagnostics;

namespace Bot.Services
{
    public class ApplicationService : IApplicationService
    {
        public void OpenApplication(string application)
        {
            try
            {
                var path = ConfigurationManager.AppSettings[application];
                Process.Start(path);
            }
            catch (Exception ex)
            {
            }
        }

        public void OpenApplicationWithParamter(string application, string args)
        {
            try
            {
                var path = ConfigurationManager.AppSettings[application];
                Process.Start(path, args);
            }
            catch (Exception ex)
            {
            }
        }

        public void CloseApplication(string application)
        {
            try
            {
                Process[] proc = Process.GetProcessesByName(application);
                proc[0].Kill();

            }
            catch (Exception ex)
            {
            }
        }
    }
}
