using Bot.Core.Desktop;
using Bot.Core.Handlers;
using Bot.Core.Interfaces;
using Bot.Core.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Speech.Recognition;

namespace Bot.Core
{
    public class RecogntionController : IRecogntionController
    {
        private static SpeechRecognitionEngine engine = new SpeechRecognitionEngine();
        private readonly INaturalLanguageProcessor _naturalLanguageProcessor;
        private readonly ISpeechController _speechController;
        private readonly IModuleController _moduleController;
        private readonly IBotLogicController _logicController;
        Grammar shellCommads;
        Grammar grammar;

        public RecogntionController(INaturalLanguageProcessor naturalLanguageProcessor, ISpeechController speechController,
            IModuleController moduleController, IBotLogicController logicController)
        {
            _naturalLanguageProcessor = naturalLanguageProcessor;
            _speechController = speechController;
            _moduleController = moduleController;
            _logicController = logicController;
            ReadGrammarFiles();
            // engine.UpdateRecognizerSetting("CFGConfidenceRejectionThreshold", 70);
            engine.LoadGrammarCompleted += _LoadGrammarCompleted;
            engine.LoadGrammarAsync(shellCommads);
            engine.LoadGrammarAsync(grammar);
            engine.SetInputToDefaultAudioDevice();
            // engine.SpeechRecognitionRejected += _recognizer_SpeechRecognitionRejected;
            StartUp();
        }

        private void ReadGrammarFiles()
        {
            shellCommads = new Grammar(ConfigurationManager.AppSettings["shellCommads"]);
            shellCommads.Name = "shellCommads";
            grammar = new Grammar(ConfigurationManager.AppSettings["grammar"]);
            grammar.Name = "grammar";
        }

        public void StartUp()
        {
            _moduleController.SetBotChatlog("Intializing...");
            _speechController.Speak("Intializing");
            engine.RecognizeAsync(RecognizeMode.Multiple);
            engine.SpeechRecognized += Engine_speechRecognized;
        }

        private void Engine_speechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            var input = e.Result.Text;
            var unitOfSpeech = new UnitOfSpeech()
            {
                Utterance = input,
                Timerstamp = DateTime.Now,
            };

            CreateQuery(unitOfSpeech);
        }

        private void CreateQuery(UnitOfSpeech unitOfSpeech)
        {
            _moduleController.SetUserChatlog(unitOfSpeech.Utterance);
            unitOfSpeech = _naturalLanguageProcessor.RecognizeIntent(unitOfSpeech);

            foreach (var query in unitOfSpeech.Queries)
            {
                _logicController.ProccessQuery(query);
            }
        }

        private void _SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
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

        private void _LoadGrammarCompleted(object sender, LoadGrammarCompletedEventArgs e)
        {
            if (!e.Grammar.Name.Equals("shellCommads"))
            {
                _moduleController.SetBotChatlog("Initialization complete...");
                _speechController.Speak("Initialization complete...");
            }
        }

        public SpeechRecognitionEngine getInstance()
        {
            return engine;
        }

        #region Deprecated
        //private void engine_speechRecognized_spellWord(object sender, SpeechRecognizedEventArgs e)
        //{
        //    var utterance = e.Result.Text;
        //    if (utterance.Length == 1 && !utterance.Equals("exit"))
        //    {
        //        _speechController.Speak(utterance);
        //        //moduleController.getChatlogInstance().setAISpeechLog(utterance);
        //        word = word + utterance;
        //    }
        //    if (utterance.Equals("confirm"))
        //    {
        //        _speechController.Speak("Word is: " + word);
        //        unloadGrammarAlphabet();
        //        engine.SpeechRecognized -= engine_speechRecognized_spellWord;
        //    }
        //    if (utterance.Equals("cancel"))
        //    {
        //        unloadGrammarAlphabet();
        //        engine.SpeechRecognized -= engine_speechRecognized_spellWord;
        //    }
        //    if (utterance.Equals("backspace"))
        //    {
        //        word = word.Remove(word.Length - 1);
        //        _speechController.Speak("Removing last letter");
        //    }
        //}

        //private void engine_speechRecognized_setReminder(object sender, SpeechRecognizedEventArgs e)
        //{
        //    string type;
        //    string date;
        //    string utterance = e.Result.Text;
        //    try
        //    {
        //        if (utterance.Contains("appointment") || utterance.Contains("birthday"))
        //        {
        //            type = utterance;
        //            _speechController.Speak("For when");
        //            //moduleController.getReminderInstance().setType(type);
        //        }
        //        if (utterance.Contains("January") || utterance.Contains("February") || utterance.Contains("March") || utterance.Contains("April")
        //        || utterance.Contains("May") || utterance.Contains("June") || utterance.Contains("July") || utterance.Contains("August")
        //         || utterance.Contains("September") || utterance.Contains("October") || utterance.Contains("November") || utterance.Contains("December"))
        //        {
        //            date = utterance;
        //            //moduleController.getReminderInstance().setDate(date);
        //            //moduleController.getReminderInstance().setReminder();
        //            unloadGrammarReminder();
        //            engine.SpeechRecognized -= engine_speechRecognized_setReminder;
        //            type = "";
        //            date = "";
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        _speechController.Speak("Something went wrong");
        //    }
        //}

        //private void engine_speechRecognized_search(object sender, SpeechRecognizedEventArgs e)
        //{
        //    String utterance = e.Result.Text;
        //    if (utterance == "dot" && !utterance.Equals("exit"))
        //    {
        //        _speechController.Speak("dot");
        //        //moduleController.getWebBrowserInstance().setText(".");
        //    }
        //    if (utterance.Length == 1 && !utterance.Equals("exit"))
        //    {
        //        _speechController.Speak(utterance);
        //        //moduleController.getWebBrowserInstance().setText(utterance);
        //    }
        //    if (utterance.Equals("confirm"))
        //    {
        //        //moduleController.getWebBrowserInstance().navigate();
        //        unloadGrammarAlphabet();
        //        engine.SpeechRecognized -= engine_speechRecognized_search;
        //    }
        //    if (utterance.Equals("backspace"))
        //    {
        //        //moduleController.getWebBrowserInstance().backspace();
        //        _speechController.Speak("Removing last letter");
        //    }
        //}
        #endregion


    }
}

