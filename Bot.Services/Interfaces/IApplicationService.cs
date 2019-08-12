using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Services.Interfaces
{
    public interface IApplicationService
    {
        void OpenApplication(string application);
        void OpenApplicationWithParamter(string application, string args);
        void CloseApplication(string application);
    }
}
