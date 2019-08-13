using Bot.Module;

namespace Bot.Core.Desktop
{
    public class ModuleController: IModuleController
    {
        static Chatlog chatlog = new Chatlog();
        static Clock clock = new Clock();
        static SpeechLog speechLog = new SpeechLog();
        static Countdown countdown = new Countdown();

        private Chatlog GetChatlogInstance()
        {
            return chatlog;
        }

        private Clock GetClockInstance()
        {
            return clock;
        }

        private SpeechLog GetUIInstance()
        {
            return speechLog;
        }

        private Countdown GetCountdownInstance()
        {
            return countdown;
        }

        public void ShowAll()
        {
            GetChatlogInstance().Show();
            GetClockInstance().Show();
            GetUIInstance().Show();
        }

        public void ShowChatlog()
        {
            GetChatlogInstance().Show();
        }

        public void ShowClock()
        {
            GetClockInstance().Show();
        }

        public void HideAll()
        {
            GetClockInstance().Hide();
            GetChatlogInstance().Hide();
        }

        public void HideChatlog()
        {
            GetChatlogInstance().Hide();
        }

        public void HideClock()
        {
            GetClockInstance().Hide();
        }

        public void SetAIChatlog(string text)
        {
            GetChatlogInstance().SetAIChatlog(text);
        }

        public void SetUserChatlog(string text)
        {
            GetChatlogInstance().SetUserChatlog(text);
        }

        public void SetText(string text)
        {
            GetUIInstance().SetText(text);
        }

        public void StopCoutdown()
        {
            GetCountdownInstance().StopCoutdown();
        }
    }
}
