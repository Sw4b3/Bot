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
        void SetAIChatlog(string text);
        void SetUserChatlog(string text);
        void SetText(string text);
        void StopCoutdown();
    }
}
