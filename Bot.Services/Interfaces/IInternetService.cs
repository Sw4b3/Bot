using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Services.Interfaces
{
    public interface IInternetService
    {
        void SearchInternet(string searchTerm, string optionParameter);
    }
}
