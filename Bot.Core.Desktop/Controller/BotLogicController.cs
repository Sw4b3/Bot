using Bot.Core.Desktop;
using Bot.Core.Handlers;
using Bot.Core.Interfaces;
using Bot.Core.Models;
using Bot.Services.Interfaces;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace Bot.Core
{
    public class BotLogicController : IBotLogicController
    {
        private ISpeechController _speechController;
        private IModuleController _moduleController;
        private IApplicationService _applicationService;
        private IDateTimeService _dateTimeService;
        private IInternetService _internetService;
        private IWeatherService _weatherService;
        ResponseHandler responses = new ResponseHandler();
        Stack lastUtterances = new Stack();
        private bool IS_LISTENING = true;

        public BotLogicController(ISpeechController speechController, IModuleController moduleController, IApplicationService applicationService, IDateTimeService dateTimeService,
            IInternetService internetService, IWeatherService weatherService)
        {
            _moduleController = moduleController;
            _speechController = speechController;
            _applicationService = applicationService;
            _dateTimeService = dateTimeService;
            _internetService = internetService;
            _weatherService = weatherService;
        }

        public bool IsListening(Query query)
        {
            switch (query.Intent)
            {
                case "start listening":
                    _moduleController.SetBotChatlog("Starting Mic");
                    _speechController.Speak("Starting Mic");
                    IS_LISTENING = true;
                    break;
                case "stop listening":
                    _moduleController.SetBotChatlog("Stopping Mic");
                    _speechController.Speak("Stopping Mic");
                    IS_LISTENING = false;
                    break;
            }
            return IS_LISTENING;
        }

        public void ProccessQuery(Query query)
        {
            if (IsListening(query))
            {
                var response = "";
                switch (query.Intent)
                {
                    case "hello":
                    case "hey":
                    case "hi":
                        response = responses.GetGreetingResponse();
                        break;
                    case "how are you":
                        response = responses.GetPleasantryResponse();
                        break;
                    case "what is time":
                        response = responses.GetTimeResponse(_dateTimeService.GetTime());
                        break;
                    case "what is date":
                        response = responses.GetDateResponse(_dateTimeService.GetDate());
                        break;
                    case "open file":
                        response = "Opening file";
                        _applicationService.OpenApplicationWithParamter("explorer", "C:/Users/Andrew/");
                        break;
                    case "close file":
                        response = "Closing file";
                        _applicationService.CloseApplication("explorer");
                        break;
                    case "set timer":
                    case "start timer":
                        response = "For how long?";
                        break;
                    case "stop timer":
                        _moduleController.StopCoutdown();
                        break;
                    case "what is weather":
                    case "tell weather":
                    case "get weather":
                        response = responses.GetWeatherResponseResponse(_weatherService.GetWeatherForcast());
                        break;
                    case "what is temperature":
                    case "get temperature":
                        response = responses.GetWeatherResponse(_weatherService.GetTemperature());
                        break;
                    #region Deprecated
                    //case "play audio":
                    //    response = "Playing audio";
                    //    break;
                    //case "stop audio":
                    //    response = "Stopping Audio";
                    //    break;
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
                    //case "search ":
                    //    //_recognitionController.loadGrammarAlphabetWeb();
                    //    break;
                    //case "add appointment":
                    //case "set appointment":
                    //case "add reminder":
                    //case "set reminder":
                    //    response = ("What is your reminder");
                    //    //_recognitionController.loadGrammarReminder();
                    //    break;
                    //case "have appointments":
                    //case "get appointment":
                    ////case "get reminder":
                    ////    response = ("Getting reminder");
                    ////    moduleController.getReminderInstance().getReminder();
                    ////    break;
                    //case "spell word":
                    //    response = ("Start spellinng you word");
                    //    //_recognitionController.loadGrammarAlphabet();
                    //    break;
                    #endregion
                    case null:
                        break;
                    default:
                        if (lastUtterances.Count != 0 && (lastUtterances.Peek().ToString().Equals("start timer")
                            || lastUtterances.Peek().ToString().Equals("set timer")))
                        {
                            if (query.Intent.Contains("minute") || query.Intent.Contains("second"))
                            {
                                _moduleController.ShowCountdown();
                                _speechController.Speak("Timer added for " + query.Intent);
                                _moduleController.StartCountdown(query.Intent);
                            }
                        }
                        else if (query.Intent.Contains("open"))
                        {
                            response = "Opening " + query.Entity;
                            _applicationService.OpenApplication(query.Entity);
                            break;
                        }
                        else if (query.Intent.Contains("close"))
                        {
                            response = "Opening " + query.Entity;
                            _applicationService.OpenApplication(query.Entity);
                            break;
                        }
                        else if (query.Intent.Contains("search"))
                        {
                            var searchTerm = query.Intent.Replace("search", "");
                            _internetService.SearchInternet(searchTerm, query.Entity);
                            response = "Searching for " + searchTerm;
                            break;
                        }
                        break;
                }
                if (response != "")
                {
                    _speechController.Speak(response);
                    _moduleController.SetText(response);
                    _moduleController.SetBotChatlog(response);
                }
                lastUtterances.Push(query.Intent);
            }
        }
    }
}
