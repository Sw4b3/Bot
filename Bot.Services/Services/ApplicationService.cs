using Bot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Services
{
    public class ApplicationService : IApplicationService
    {
        public void OpenSteam()
        {
            try
            {
                Process.Start("D:/Steam/Steam.exe");
            }
            catch (Exception ex)
            {
            }
        }

        public void CloseSteam()
        {
            try
            {
                Process[] proc = Process.GetProcessesByName("steam");
                proc[0].Kill();

            }
            catch (Exception ex)
            {
            }
        }

        public void OpenFileExplorer()
        {
            try
            {
                Process.Start("explorer.exe", "C:/Users/Andrew/");
            }
            catch (Exception ex)
            {
            }
        }

        public void CloseFileExplorer()
        {
            try
            {
                Process[] proc = Process.GetProcessesByName("explorer");
                proc[0].Kill();

            }
            catch (Exception ex)
            {
            }
        }

    }
}
