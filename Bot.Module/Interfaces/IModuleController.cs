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
        void ShowCountdown();
        void HideAll();
        void HideChatlog();
        void HideClock();
        void SetBotChatlog(string text);
        void SetUserChatlog(string text);
        void SetText(string text);
        void StartCountdown(string utterance);
        void StopCoutdown();
    }
}
