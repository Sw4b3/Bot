using Bot.Core.Handlers;
using Bot.Core.Interfaces;
using System;
using System.Collections;
using System.IO;
using System.Speech.Recognition;

namespace Bot.Core
{
    public class NaturalLanguageProcessor : INaturalLanguageProcessor
    {
        ISpeechController _speechController;
        ILanguageProcessor _languageProcessor;
        IPOSHandler _posHandler;
        private bool mulitquery;


        public NaturalLanguageProcessor(ISpeechController speechController, ILanguageProcessor languageProcessor, IPOSHandler posHandler)
        {
            _speechController = speechController;
            _languageProcessor = languageProcessor;
            _posHandler = posHandler;
            _posHandler.ReadAllGrammarFiles();
        }

        public void CreateQuery(string utterance)
        {
            var words = utterance.Split(' ');
            var conjunctions = _posHandler.GetConjuctions();
            mulitquery = false;

            for (int i = 0; i < conjunctions.Length; i++)
            {
                if (utterance.Contains(conjunctions[i]))
                {
                    var querys = utterance.Split(new string[] { conjunctions[i] }, StringSplitOptions.None);
                    var query1 = querys[0];
                    var pos = _posHandler.POStagging(query1);
                    _languageProcessor.Check(UnderstandIntent(pos, words).Trim());
                    var query2 = querys[1];
                    pos = _posHandler.POStagging(query2);
                    _languageProcessor.Check(UnderstandIntent(pos, words).Trim());
                    mulitquery = true;
                }
            }
            if (!mulitquery)
            {
                var pos = _posHandler.POStagging(utterance);
                _languageProcessor.Check(UnderstandIntent(pos, words).Trim());
            }
        }

        public string UnderstandIntent(string[] PartsOfSpeech, string[] words)
        {
            string intent = null;
            string entity = null;
            var question = _posHandler.GetQuestions();
     
            try
            {
                for (int i = 0; i < PartsOfSpeech.Length; i++)
                {
                    intent = intent + PartsOfSpeech[i] + " ";
                }
                // MessageBox.Show("Debug: "+intent);


                //Action rule
                for (int i = 0; i < PartsOfSpeech.Length; i++)
                {
                    for (int j = 0; j < PartsOfSpeech.Length; j++)
                    {
                        if (PartsOfSpeech.Length <= 1)
                        {

                            if (PartsOfSpeech[i].Equals("verb"))
                            {

                                intent = words[i];
                                entity = null;
                                break;
                            }
                        }


                        if (PartsOfSpeech[i].Equals("verb") && PartsOfSpeech[j].Equals("noun"))
                        {
                            intent = words[i];
                            entity = words[j];
                            break;
                        }

                        if (PartsOfSpeech[i].Equals("verb") && PartsOfSpeech[j].Equals("verb"))
                        {
                            if (i != j)
                            {
                                intent = words[j] + " " + words[i];
                                entity = null;
                                break;
                            }
                        }
                    }
                }

                //noun rule
                for (int i = 0; i < PartsOfSpeech.Length; i++)
                {
                    for (int j = 0; j < PartsOfSpeech.Length; j++)
                    {
                        if (PartsOfSpeech.Length <= 1)
                        {
                            if (PartsOfSpeech[j].Equals("noun"))
                            {
                                intent = words[j];
                                entity = null;
                                break;
                            }
                        }

                        if (PartsOfSpeech[i].Equals("adjective") && PartsOfSpeech[j].Equals("noun"))
                        {
                            intent = words[i];
                            entity = words[j];
                            break;
                        }
                    }
                }

                //injection rule
                for (int i = 0; i < PartsOfSpeech.Length; i++)
                {
                    for (int j = 0; j < PartsOfSpeech.Length; j++)
                    {

                        if (PartsOfSpeech[i].Equals("interjection"))
                        {
                            intent = words[i];
                            break;
                        }
                    }
                }

                //question rule
                for (int k = 0; k < words.Length; k++)
                {
                    if (words[k].Equals(question))
                    {

                        for (int i = 0; i < PartsOfSpeech.Length; i++)
                        {
                            for (int j = 0; j < PartsOfSpeech.Length; j++)
                            {
                                if (PartsOfSpeech[i].Equals("verb") && PartsOfSpeech[j].Equals("noun"))
                                {
                                    intent = question + " " + words[i];
                                    entity = words[j];
                                    if (!intent.Equals(null) && !entity.Equals(null))
                                    {
                                        break;
                                    }
                                }
                                if (PartsOfSpeech[i].Equals("verb") && PartsOfSpeech[j].Equals("pronoun"))
                                {
                                    intent = question + " " + words[i];
                                    entity = words[j];
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            var query = intent + " " + entity;
            return query;
        }

        //public void taggUntagged()
        //{
        //    if (!untaggedWord.Count.Equals(0))
        //    {
        //        _speechController.Speak("There are " + untaggedWord.Count + " untagged words");
        //        _speechController.Speak("What part of speech is " + untaggedWord[index]);
        //        _recogntionController.loadGrammarPOS();
        //        _recogntionController.getInstance().SpeechRecognized += (object sender, SpeechRecognizedEventArgs e) =>
        //         {
        //             string utterance = e.Result.Text;
        //             switch (utterance)
        //             {
        //                 case "adjective":
        //                 case "adverb":
        //                 case "conjunction":
        //                 case "determiner":
        //                 case "injection":
        //                 case "noun":
        //                 case "number":
        //                 case "prepostion":
        //                 case "pronoun":
        //                 case "verb":
        //                     POS = utterance;
        //                     _speechController.Speak(untaggedWord[index] + " is a " + POS);
        //                     using (StreamWriter sw = File.AppendText(".\\Grammar\\Parts of Speech\\" + POS + "s.txt"))
        //                     {
        //                         sw.WriteLine(untaggedWord[index] + "\n");
        //                     }
        //                     POS = "";
        //                     untaggedWord.RemoveAt(0);
        //                     _recogntionController.loadGrammarPOS();
        //                     break;
        //             }
        //         };
        //    }
        //    else
        //    {
        //        _speechController.Speak("There are no untagged words");
        //    }
        //}

        private void VoicePOSDebug(string[] PartsOfSpeech, string[] words)
        {
            for (int i = 0; i < words.Length; i++)
            {
                _speechController.Speak(words[i] + " is a " + PartsOfSpeech[i]);
            }
        }

    }
}
