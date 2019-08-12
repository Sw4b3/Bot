using Bot.Module;

namespace Bot.Core.Desktop
{
    public class ModuleController: IModuleController
    {
        static Chatlog chatlog = new Chatlog();
        static Clock clock = new Clock();
        static TextUI UI = new TextUI();
        static Countdown countdown = new Countdown();

        private Chatlog GetChatlogInstance()
        {
            return chatlog;
        }

        private Clock GetClockInstance()
        {
            return clock;
        }

        private TextUI GetUIInstance()
        {
            return UI;
        }

        private Countdown GetCountdownInstance()
        {
            return countdown;
        }

        public void ShowAll()
        {
            GetChatlogInstance().Show();
            GetClockInstance().Show();
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

        public void SetAISpeechLog(string text)
        {
            GetChatlogInstance().SetAISpeechLog(text);
        }

        public void SetUserSpeechLog(string text)
        {
            GetChatlogInstance().SetUserSpeechLog(text);
        }

        public void StopCoutdown()
        {
            GetCountdownInstance().StopCoutdown();
        }
    }
}
