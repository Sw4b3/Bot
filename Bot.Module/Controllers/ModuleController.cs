using Bot.Module;

namespace Bot.Core.Desktop
{
    public class ModuleController : IModuleController
    {
        private static Chatlog _chatlog = new Chatlog();
        private static Clock _clock = new Clock();
        private static SpeechLog _speechLog = new SpeechLog();
        private static Countdown _countdown = new Countdown();

        public void ShowAll()
        {
            _chatlog.Show();
            _clock.Show();
            _speechLog.Show();
        }

        public void ShowChatlog()
        {
            _chatlog.Show();
        }

        public void ShowClock()
        {
            _clock.Show();
        }

        public void HideAll()
        {
            _clock.Hide();
            _chatlog.Hide();
        }

        public void HideChatlog()
        {
            _chatlog.Hide();
        }

        public void HideClock()
        {
            _clock.Hide();
        }

        public void SetBotChatlog(string text)
        {
            _chatlog.SetBotChatlog(text);
        }

        public void SetUserChatlog(string text)
        {
            _chatlog.SetUserChatlog(text);
        }

        public void SetText(string text)
        {
            _speechLog.SetText(text);
        }

        public void StopCoutdown()
        {
            _countdown.StopCoutdown();
        }

        public void StartCountdown(string utterance)
        {
            _countdown.StartCountdown(utterance);
        }

        public void ShowCountdown()
        {
            _countdown.Show();
        }
    }
}
