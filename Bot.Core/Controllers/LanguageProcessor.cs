using Bot.Core.Desktop;
using Bot.Core.Handlers;
using Bot.Core.Interfaces;
using System;
using System.Diagnostics;
using System.IO;

namespace Bot.Core
{
    public class LanguageProcessor : ILanguageProcessor
    {
        ISpeechController _speechController;
        IModuleController _moduleController;
        ResponseHandler responses = new ResponseHandler();
        Process process;
        string response;
        private bool IS_LISTENING = true;

        public LanguageProcessor(ISpeechController speechController, IModuleController moduleController)
        {
            _moduleController = moduleController;
            _speechController = speechController;
        }

        public void Check(string utterance)
        {
            switch (utterance)
            {
                case "start listening":
                    IS_LISTENING = true;
                    _moduleController.SetAISpeechLog("Starting Mic");
                    _speechController.Speak("Starting Mic");
                    break;
                case "stop listening":
                    IS_LISTENING = false;
                    _moduleController.SetAISpeechLog("Stopping Mic");
                    _speechController.Speak("Stopping Mic");
                    break;
            };
            if (IS_LISTENING)
            {
                Query(utterance);
            }
        }

        public void Query(string intent)
        {
            response = "";
            switch (intent)
            {
                case "hello":
                case "hey":
                case "hi":
                    response = (responses.Greeting());
                    break;
                case "how are you":
                    response = (responses.Pleasantry());
                    break;
                case "what is time":
                    response = (responses.GetTime() + DateTime.Now.ToString("h:mm tt"));
                    break;
                case "what is date":
                    response = (DateTime.Now.ToString("dd MMM", new System.Globalization.CultureInfo("en-US")));
                    break;
                //case "what is weather":
                //case "tell weather":
                //case "get weather":
                //    moduleController.getWeatherHandlerInstance().getAllWeather();
                //    break;
                //case "what is temperature":
                //case "get temperature":
                //    response = "The temperature is " + moduleController.getWeatherHandlerInstance().GetTemp();
                //    break;
                case "play audio":
                    response = ("Playing audio");
                    break;
                case "stop audio":
                    response = ("Stopping Audio");
                    break;
                //case "start timer":
                //    response = ("For how long?");
                //    _recognitionController.loadGrammarTime();
                //    break;
                //case "stop timer":
                //    _moduleController.StopCoutdown();
                //    break;
                case "open file":
                    response = ("Opening file");
                    process = Process.Start("explorer.exe", "C:/Users/Andrew/");
                    process.WaitForExit();
                    break;
                //case "start mediaplayer":
                //case "open mediaplayer":
                //    moduleController.createMediaPlayer();
                //    break;
                //case "stop mediaplayer":
                //case "close mediaplayer":
                //    moduleController.getMediaPlayerInstance().Dispose();
                //    break;
                //case "stop media":
                //case "pause media":
                //    moduleController.getMediaPlayerInstance().pauseMedia();
                //    break;
                //case "start media":
                //case "play media":
                //case "resume media":
                //    moduleController.getMediaPlayerInstance().resumeMedia();
                //    break;
                //case "fullscreen mediaplayer":
                //    moduleController.getMediaPlayerInstance().fullscreen();
                //    break;
                //case "exit fullscreen":
                //    moduleController.getMediaPlayerInstance().exitFullscreen();
                //    break;
                //case "maximize mediaplayer":
                //    moduleController.getMediaPlayerInstance().WindowState = FormWindowState.Maximized;
                //    break;
                //case "minimize mediaplayer":
                //    moduleController.getMediaPlayerInstance().WindowState = FormWindowState.Minimized;
                //    break;
                //case "open youtube":
                //    moduleController.createWebBrowser();
                //    moduleController.getWebBrowserInstance().NavigateToPage("https://www.youtube.com/");
                //    break;
                //case "open web":
                //case "open browser":
                //    moduleController.createWebBrowser();
                //    moduleController.getWebBrowserInstance().NavigateToPage("https://www.google.com/");
                //    break;
                //case "maximize browser":
                //    moduleController.getWebBrowserInstance().WindowState = FormWindowState.Maximized;
                //    break;
                //case "minimize browser":
                //    moduleController.getWebBrowserInstance().WindowState = FormWindowState.Minimized;
                //    break;
                //case "close browser":
                //    moduleController.getWebBrowserInstance().Dispose();
                //    break;
                //case "fullscreen browser":
                //    moduleController.getWebBrowserInstance().fullscreen();
                //    break;
                case "search":
                    response = ("Where do you want to go");
                    //_recognitionController.loadGrammarAlphabetWeb();
                    break;
                case "add appointment":
                case "set appointment":
                case "add reminder":
                case "set reminder":
                    response = ("What is your reminder");
                    //_recognitionController.loadGrammarReminder();
                    break;
                case "have appointments":
                case "get appointment":
                //case "get reminder":
                //    response = ("Getting reminder");
                //    moduleController.getReminderInstance().getReminder();
                //    break;
                case "spell word":
                    response = ("Start spellinng you word");
                    //_recognitionController.loadGrammarAlphabet();
                    break;
                    //case "untagged words":
                    //    _recognitionController.getNLPInstance().taggUntagged();
                    //    break;
            }
            if (response != "")
            {
                _speechController.Speak(response);
                //_moduleController.setText(response);
                _moduleController.SetAISpeechLog(response);
            }
        }
    }
}
