using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Core.Desktop
{
    public interface IModuleController
    {
        void ShowAll();
        void ShowChatlog();
        void ShowClock();
        void HideAll();
        void HideChatlog();
        void HideClock();
        void SetAISpeechLog(string text);
        void SetUserSpeechLog(string text);
        void StopCoutdown();
    }
}
