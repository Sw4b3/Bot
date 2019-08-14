using Bot.Core.Desktop;
using Bot.Core.Handlers;
using Bot.Core.Interfaces;
using Bot.Core.Models;
using System;
using System.Configuration;
using System.Speech.Recognition;

namespace Bot.Core
{
    public class RecogntionController : IRecogntionController
    {
        static SpeechRecognitionEngine engine = new SpeechRecognitionEngine();
        private readonly INaturalLanguageProcessor _naturalLanguageProcessor;
        private readonly ISpeechController _speechController;
        private readonly IModuleController _moduleController;
        private readonly IPartsOfSpeechHandler _posHandler;
        Grammar shellCommads;
        Grammar grammar;
        Grammar grammarTime;
        Grammar grammarReminder;
        Grammar grammaralphabet;
        Grammar grammarAlphanumeric;
        string word;

        public RecogntionController(INaturalLanguageProcessor naturalLanguageProcessor, ISpeechController speechController, 
            IModuleController moduleController, IPartsOfSpeechHandler posHandler)
        {
            _naturalLanguageProcessor = naturalLanguageProcessor;
            _speechController = speechController;
            _moduleController = moduleController;
            _posHandler = posHandler;
            ReadGrammarFiles();
            // engine.UpdateRecognizerSetting("CFGConfidenceRejectionThreshold", 70);
            engine.LoadGrammarAsync(shellCommads);
            engine.LoadGrammarAsync(grammar);
            //engine.LoadGrammarAsync(grammarAlphanumeric);
            engine.LoadGrammarAsync(grammarTime);
            engine.LoadGrammarAsync(grammarReminder);
            engine.SetInputToDefaultAudioDevice();
            // engine.SpeechRecognitionRejected += _recognizer_SpeechRecognitionRejected;
            StartUp();
        }

        private void ReadGrammarFiles()
        {
            shellCommads = new Grammar(ConfigurationManager.AppSettings["shellCommads"]); ;
            grammar = new Grammar(ConfigurationManager.AppSettings["grammar"]);
            grammarAlphanumeric = new Grammar(ConfigurationManager.AppSettings["grammarAlphanumeric"]);
            grammarTime = new Grammar(".\\Grammar\\grammarTime.xml");
            grammarReminder = new Grammar(".\\Grammar\\grammarReminder.xml");
        }

        public void StartUp()
        {
            _moduleController.SetAIChatlog("Intializing...");
            _speechController.Speak("Intializing");
            engine.RecognizeAsync(RecognizeMode.Multiple);
            engine.SpeechRecognized += engine_speechRecognized;
        }

        private void engine_speechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            var input = e.Result.Text;
            var unitOfSpeech = new UnitOfSpeech()
            {
                Utterance = input,
                Timerstamp = DateTime.Now,
                PartsOfSpeech = new PartsOfSpeech()
                {
                    Words = input.Split(' '),
                    Descriptors = _posHandler.POStagging(input)
                },
            };

            _moduleController.SetUserChatlog(unitOfSpeech.Utterance);
            _naturalLanguageProcessor.CreateQuery(unitOfSpeech);
        }

        private void _recognizer_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            if (e.Result.Alternates.Count == 0)
            {
                _speechController.Speak("Speech rejected");
            }
            foreach (RecognizedPhrase r in e.Result.Alternates)
            {
                _speechController.Speak("Speech rejected. Did you mean: " + r.Text);
            }
        }

        private void engine_speechRecognized_startTimer(object sender, SpeechRecognizedEventArgs e)
        {
            var utterance = e.Result.Text;
            try
            {
                if (utterance.Contains("minutes") || utterance.Contains("seconds"))
                {
                    _moduleController.ShowCountdown();
                    _speechController.Speak("Timer added for " + utterance);
                    _moduleController.StartCountdown(utterance);
                    unloadGrammaTimer();
                    engine.SpeechRecognized -= engine_speechRecognized_startTimer;
                }
            }
            catch (Exception)
            {
                _speechController.Speak("Something went wrong");
            }
        }

        private void engine_speechRecognized_spellWord(object sender, SpeechRecognizedEventArgs e)
        {
            var utterance = e.Result.Text;
            if (utterance.Length == 1 && !utterance.Equals("exit"))
            {
                _speechController.Speak(utterance);
                //moduleController.getChatlogInstance().setAISpeechLog(utterance);
                word = word + utterance;
            }
            if (utterance.Equals("confirm"))
            {
                _speechController.Speak("Word is: " + word);
                unloadGrammarAlphabet();
                engine.SpeechRecognized -= engine_speechRecognized_spellWord;
            }
            if (utterance.Equals("cancel"))
            {
                unloadGrammarAlphabet();
                engine.SpeechRecognized -= engine_speechRecognized_spellWord;
            }
            if (utterance.Equals("backspace"))
            {
                word = word.Remove(word.Length - 1);
                _speechController.Speak("Removing last letter");
            }
        }

        private void engine_speechRecognized_setReminder(object sender, SpeechRecognizedEventArgs e)
        {
            string type;
            string date;
            string utterance = e.Result.Text;
            try
            {
                if (utterance.Contains("appointment") || utterance.Contains("birthday"))
                {
                    type = utterance;
                    _speechController.Speak("For when");
                    //moduleController.getReminderInstance().setType(type);
                }
                if (utterance.Contains("January") || utterance.Contains("February") || utterance.Contains("March") || utterance.Contains("April")
                || utterance.Contains("May") || utterance.Contains("June") || utterance.Contains("July") || utterance.Contains("August")
                 || utterance.Contains("September") || utterance.Contains("October") || utterance.Contains("November") || utterance.Contains("December"))
                {
                    date = utterance;
                    //moduleController.getReminderInstance().setDate(date);
                    //moduleController.getReminderInstance().setReminder();
                    unloadGrammarReminder();
                    engine.SpeechRecognized -= engine_speechRecognized_setReminder;
                    type = "";
                    date = "";
                }
            }
            catch (Exception)
            {
                _speechController.Speak("Something went wrong");
            }
        }

        private void engine_speechRecognized_search(object sender, SpeechRecognizedEventArgs e)
        {
            String utterance = e.Result.Text;
            if (utterance == "dot" && !utterance.Equals("exit"))
            {
                _speechController.Speak("dot");
                //moduleController.getWebBrowserInstance().setText(".");
            }
            if (utterance.Length == 1 && !utterance.Equals("exit"))
            {
                _speechController.Speak(utterance);
                //moduleController.getWebBrowserInstance().setText(utterance);
            }
            if (utterance.Equals("confirm"))
            {
                //moduleController.getWebBrowserInstance().navigate();
                unloadGrammarAlphabet();
                engine.SpeechRecognized -= engine_speechRecognized_search;
            }
            if (utterance.Equals("backspace"))
            {
                //moduleController.getWebBrowserInstance().backspace();
                _speechController.Speak("Removing last letter");
            }
        }

        public void loadGrammarPOS()
        {
            try
            {
                engine.LoadGrammarAsync(grammarAlphanumeric);
            }
            catch (Exception)
            {
            }
        }

        public void unloadGrammarPOS()
        {
            try
            {
                engine.UnloadGrammar(grammarAlphanumeric);
            }
            catch (Exception)
            {
            }
        }

        public void loadGrammarReminder()
        {
            try
            {
                engine.SpeechRecognized += engine_speechRecognized_setReminder;
                engine.LoadGrammarAsync(grammarReminder);
            }
            catch (Exception)
            {
            }
        }

        public void unloadGrammarReminder()
        {
            try
            {
                engine.UnloadGrammar(grammarReminder);
            }
            catch (Exception)
            {
            }
        }

        public void loadGrammarAlphabet()
        {
            try
            {
                engine.SpeechRecognized += engine_speechRecognized_spellWord;
                engine.LoadGrammarAsync(grammaralphabet);
            }
            catch (Exception)
            {
            }
        }

        public void loadGrammarAlphabetWeb()
        {
            try
            {
                engine.SpeechRecognized += engine_speechRecognized_search;
                engine.LoadGrammarAsync(grammaralphabet);
            }
            catch (Exception)
            {
            }
        }

        public void unloadGrammarAlphabet()
        {
            try
            {
                engine.UnloadGrammar(grammaralphabet);
            }
            catch (Exception)
            {
            }
        }

        public void loadGrammarTime()
        {
            try
            {
                engine.SpeechRecognized += engine_speechRecognized_startTimer;
                engine.LoadGrammarAsync(grammarTime);
            }
            catch (Exception)
            {
            }
        }

        public void unloadGrammaTimer()
        {
            try
            {
                engine.UnloadGrammar(grammarTime);
            }
            catch (Exception)
            {
            }
        }

        public void loadGrammar()
        {
            try
            {
                engine.LoadGrammarAsync(grammar);
            }
            catch (Exception)
            {
            }
        }

        public void unloadGrammar()
        {
            try
            {
                engine.UnloadGrammar(grammar);
            }
            catch (Exception)
            {
            }
        }

        public SpeechRecognitionEngine getInstance()
        {
            return engine;
        }

        //public NaturalLanguageProcessor getNLPInstance()
        //{
        //    return _naturalLanguageProcessor;
        //}
    }
}

