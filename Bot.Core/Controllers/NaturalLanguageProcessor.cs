using Bot.Core.Handlers;
using Bot.Core.Interfaces;
using Bot.Core.Models;
using System;
using System.Collections;
using System.IO;
using System.Speech.Recognition;

namespace Bot.Core
{
    public class NaturalLanguageProcessor : INaturalLanguageProcessor
    {
        private ISpeechController _speechController;
        private ILanguageProcessor _languageProcessor;
        private IPartsOfSpeechHandler _posHandler;
        public NaturalLanguageProcessor(ISpeechController speechController, ILanguageProcessor languageProcessor, IPartsOfSpeechHandler posHandler)
        {
            _speechController = speechController;
            _languageProcessor = languageProcessor;
            _posHandler = posHandler;
        }

        public void CreateQuery(UnitOfSpeech unitOfSpeech)
        {
            var conjunction = _posHandler.SearchForConjuction(unitOfSpeech.Utterance);
                if (conjunction!="" && unitOfSpeech.Utterance.Contains(conjunction))
                {
                    var querys = unitOfSpeech.Utterance.Split(new string[] { conjunction }, StringSplitOptions.None);
                    var query1 = querys[0].Trim();
                    unitOfSpeech.PartsOfSpeech = _posHandler.POStagging(query1);
                    _languageProcessor.Check(UnderstandIntent(unitOfSpeech));
                    var query2 = querys[1].Trim();
                    unitOfSpeech.PartsOfSpeech = _posHandler.POStagging(query2);
                    _languageProcessor.Check(UnderstandIntent(unitOfSpeech));
            }
           else
            {
                unitOfSpeech.PartsOfSpeech = _posHandler.POStagging(unitOfSpeech.Utterance);
                _languageProcessor.Check(UnderstandIntent(unitOfSpeech));
            }
        }

        public UnitOfSpeech UnderstandIntent(UnitOfSpeech unitOfWork)
        {
            try
            {
                //Action rule
                for (int i = 0; i < unitOfWork.PartsOfSpeech.Length; i++)
                {
                    for (int j = 0; j < unitOfWork.PartsOfSpeech.Length; j++)
                    {
                        if (unitOfWork.PartsOfSpeech.Length <= 1)
                        {

                            if (unitOfWork.PartsOfSpeech[i].Equals("verb"))
                            {

                                unitOfWork.Intent = unitOfWork.Words[i];
                                unitOfWork.Entity = null;
                                break;
                            }
                        }


                        if (unitOfWork.PartsOfSpeech[i].Equals("verb") && unitOfWork.PartsOfSpeech[j].Equals("noun"))
                        {
                            unitOfWork.Intent = unitOfWork.Words[i];
                            unitOfWork.Entity = unitOfWork.Words[j];
                            break;
                        }

                        if (unitOfWork.PartsOfSpeech[i].Equals("verb") && unitOfWork.PartsOfSpeech[j].Equals("verb"))
                        {
                            if (i != j)
                            {
                                unitOfWork.Intent = unitOfWork.Words[j] + " " + unitOfWork.Words[i];
                                unitOfWork.Entity = null;
                                break;
                            }
                        }
                    }
                }

                //noun rule
                for (int i = 0; i < unitOfWork.PartsOfSpeech.Length; i++)
                {
                    for (int j = 0; j < unitOfWork.PartsOfSpeech.Length; j++)
                    {
                        if (unitOfWork.PartsOfSpeech.Length <= 1)
                        {
                            if (unitOfWork.PartsOfSpeech[j].Contains("noun"))
                            {
                                unitOfWork.Intent = unitOfWork.Words[j];
                                unitOfWork.Entity = null;
                                break;
                            }
                        }

                        if (unitOfWork.PartsOfSpeech[i].Equals("adjective") && unitOfWork.PartsOfSpeech[j].Contains("noun"))
                        {
                            unitOfWork.Intent = unitOfWork.Words[i];
                            unitOfWork.Entity = unitOfWork.Words[j];
                            break;
                        }
                    }
                }

                //injection rule
                for (int i = 0; i < unitOfWork.PartsOfSpeech.Length; i++)
                {
                    for (int j = 0; j < unitOfWork.PartsOfSpeech.Length; j++)
                    {

                        if (unitOfWork.PartsOfSpeech[i].Equals("interjection"))
                        {
                            unitOfWork.Intent = unitOfWork.Words[i];
                            break;
                        }
                    }
                }

                //question rule
                for (int k = 0; k < unitOfWork.PartsOfSpeech.Length; k++)
                {
                    if (unitOfWork.PartsOfSpeech[k].Equals("wh-determiner")|| unitOfWork.PartsOfSpeech[k].Equals("wh-pronoun")
                        || unitOfWork.PartsOfSpeech[k].Equals("wh-pronoun") || unitOfWork.PartsOfSpeech[k].Equals("wh-adverb"))
                    {

                        for (int i = 0; i < unitOfWork.PartsOfSpeech.Length; i++)
                        {
                            for (int j = 0; j < unitOfWork.PartsOfSpeech.Length; j++)
                            {
                                if (unitOfWork.PartsOfSpeech[i].Equals("verb") && unitOfWork.PartsOfSpeech[j].Equals("noun"))
                                {
                                    unitOfWork.Intent = unitOfWork.Words[k] + " " + unitOfWork.Words[i];
                                    unitOfWork.Entity = unitOfWork.Words[j];
                                    if (!unitOfWork.Intent.Equals(null) && !unitOfWork.Entity.Equals(null))
                                    {
                                        break;
                                    }
                                }
                                if (unitOfWork.PartsOfSpeech[i].Equals("verb") && unitOfWork.PartsOfSpeech[j].Equals("pronoun"))
                                {
                                    unitOfWork.Intent = unitOfWork.Words[k] + " " + unitOfWork.Words[i];
                                    unitOfWork.Entity = unitOfWork.Words[j];
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

            unitOfWork.Query = unitOfWork.Entity!=null ? unitOfWork.Intent + " " + unitOfWork.Entity: unitOfWork.Intent;
            return unitOfWork;
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
