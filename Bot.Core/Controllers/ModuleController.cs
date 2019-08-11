using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualAssistant
{
    class ModuleController
    {
        static WeatherHandler weatherHandler = new WeatherHandler();
        static Reminders reminders = new Reminders();
        static Chatlog chatlog = new Chatlog();
        static Clock clock = new Clock();
        static TextUI UI = new TextUI();
        static Performance performance = new Performance();
        static MediaPlayer player;
        static WebBrowser browser;
        static Countdown countdown;

        public WeatherHandler getWeatherHandlerInstance()
        {
            return weatherHandler;
        }

        public Reminders getReminderInstance()
        {
            return reminders;
        }

        public MediaPlayer getMediaPlayerInstance()
        {
            return player;
        }

        public WebBrowser getWebBrowserInstance()
        {
            return browser;
        }

        public Countdown getCountdownInstance()
        {
            return countdown;
        }

        public Chatlog getChatlogInstance()
        {
            return chatlog;
        }

        public void createMediaPlayer()
        {
            player = new MediaPlayer();
            player.Show();
        }

        public void createWebBrowser()
        {
            browser = new WebBrowser();
            browser.Show();
        }

        public void createCountdown()
        {
            countdown = new Countdown();
            countdown.Show();
        }


        public Clock getClockInstance()
        {
            return clock;
        }

        public TextUI getUIInstance()
        {
            return UI;
        }

        public void showAll()
        {
            performance.Show();
            getChatlogInstance().Show();
            getClockInstance().Show();
        }

        public void hideAll()
        {
            performance.Hide();
            getClockInstance().Hide();
            getChatlogInstance().Hide();
        }
    }
}
