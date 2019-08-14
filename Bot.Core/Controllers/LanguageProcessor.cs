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
    public class LanguageProcessor : ILanguageProcessor
    {
        private ISpeechController _speechController;
        private IModuleController _moduleController;
        private IApplicationService _applicationService;
        private IDateTimeService _dateTimeService;
        private IInternetService _internetService;
        ResponseHandler responses = new ResponseHandler();
        Stack lastUtterances = new Stack();
        private bool IS_LISTENING = true;

        public LanguageProcessor(ISpeechController speechController, IModuleController moduleController, IApplicationService applicationService, IDateTimeService dateTimeService,
            IInternetService internetService)
        {
            _moduleController = moduleController;
            _speechController = speechController;
            _applicationService = applicationService;
            _dateTimeService = dateTimeService;
            _internetService = internetService;
        }

        public void Check(UnitOfSpeech unitOfSpeech)
        {
            switch (unitOfSpeech.Utterance)
            {
                case "start listening":
                    IS_LISTENING = true;
                    _moduleController.SetAIChatlog("Starting Mic");
                    _speechController.Speak("Starting Mic");
                    break;
                case "stop listening":
                    IS_LISTENING = false;
                    _moduleController.SetAIChatlog("Stopping Mic");
                    _speechController.Speak("Stopping Mic");
                    break;
            };
            if (IS_LISTENING)
            {
                ProccessQuery(unitOfSpeech);
            }
        }

        public void ProccessQuery(UnitOfSpeech unitOfSpeech)
        {
            var response = "";
            switch (unitOfSpeech.Intent)
            {
                case "hello":
                case "hey":
                case "hi":
                    response = responses.Greeting();
                    break;
                case "how are you":
                    response = responses.Pleasantry();
                    break;
                case "what is time":
                    response = responses.GetTime() + _dateTimeService.GetTime();
                    break;
                case "what is date":
                    response = _dateTimeService.GetDate();
                    break;
                case "open file":
                    response = "Opening file";
                    _applicationService.OpenApplicationWithParamter("explorer", "C:/Users/Andrew/");
                    break;
                case "close file":
                    response = "Closing file";
                    _applicationService.CloseApplication("explorer");
                    break;
                case "open steam":
                    response = "Opening steam";
                    _applicationService.OpenApplication("steam");
                    break;
                case "close steam":
                    response = "Closing steam";
                    _applicationService.CloseApplication("steam");
                    break;
                case "open origin":
                    response = "Opening origin";
                    _applicationService.OpenApplication("origin");
                    break;
                case "close origin":
                    response = "Closing origin";
                    _applicationService.CloseApplication("origin");
                    break;
                case "open uplay":
                    response = "Opening U-play";
                    _applicationService.OpenApplication("uplay");
                    break;
                case "close uplay":
                    response = "Closing U-play";
                    _applicationService.CloseApplicationWithParamter("uplay");
                    break;
                case "set timer":
                case "start timer":
                    response = "For how long?";
                    break;
                case "stop timer":
                    _moduleController.StopCoutdown();
                    break;
                #region Deprecated
                //case "what is weather":
                //case "tell weather":
                //case "get weather":
                //    moduleController.getWeatherHandlerInstance().getAllWeather();
                //    break;
                //case "what is temperature":
                //case "get temperature":
                //    response = "The temperature is " + moduleController.getWeatherHandlerInstance().GetTemp();
                //    break;
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
                    if (lastUtterances.Count !=0 && lastUtterances.Peek().ToString().Equals("start timer")
                        || lastUtterances.Peek().ToString().Equals("set timer"))
                    {
                        if (unitOfSpeech.Utterance.Contains("minutes") || unitOfSpeech.Utterance.Contains("seconds"))
                        {
                            _moduleController.ShowCountdown();
                            _speechController.Speak("Timer added for " + unitOfSpeech.Utterance);
                            _moduleController.StartCountdown(unitOfSpeech.Utterance);
                        }
                    }
                    else if (unitOfSpeech.Intent.Contains("search"))
                    {
                        var searchTerm = unitOfSpeech.Intent.Replace("search", "");
                        _internetService.SearchInternet(searchTerm, unitOfSpeech.Entity);
                        response = "Searching for " + searchTerm;
                        break;
                    }
                    break;
            }
            if (response != "")
            {
                _speechController.Speak(response);
                _moduleController.SetText(response);
                _moduleController.SetAIChatlog(response);
            }
            lastUtterances.Push(unitOfSpeech.Utterance);
        }
    }
}
