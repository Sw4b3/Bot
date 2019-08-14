using Bot.Core.Handlers;
using Bot.Core.Interfaces;
using Bot.Core.Models;
using System;
using System.Collections;
using System.IO;
using System.Speech.Recognition;
using System.Linq;

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
            var conjunction = _posHandler.SearchForConjuction(unitOfSpeech.PartsOfSpeech);
            if (conjunction != "" && unitOfSpeech.Utterance.Contains(conjunction))
            {
                var querys = unitOfSpeech.Utterance.Split(new string[] { conjunction }, StringSplitOptions.None);
                var query1 = querys[0].Trim();
                var queryPos = SplitQuery(query1, unitOfSpeech.PartsOfSpeech);
                unitOfSpeech.PartsOfSpeech = queryPos.query1Pos;
                _languageProcessor.Check(UnderstandIntent(unitOfSpeech));
                unitOfSpeech.PartsOfSpeech = queryPos.query2Pos;
                _languageProcessor.Check(UnderstandIntent(unitOfSpeech));
            }
            else
            {
                _languageProcessor.Check(UnderstandIntent(unitOfSpeech));
            }
        }

        public (PartsOfSpeech query1Pos, PartsOfSpeech query2Pos) SplitQuery(string query, PartsOfSpeech partsOfSpeech)
        {
            var query1Pos = new PartsOfSpeech();
            var query2Pos = new PartsOfSpeech();
            var words = query.Split(' ');
            var wordCount = words.Length;
            query1Pos.Descriptors = partsOfSpeech.Descriptors.Take(wordCount).ToArray();
            query1Pos.Words = partsOfSpeech.Words.Take(wordCount).ToArray();
            query2Pos.Descriptors = partsOfSpeech.Descriptors.Skip(wordCount+1).ToArray();
            query2Pos.Words = partsOfSpeech.Words.Skip(wordCount+1).ToArray();
            return (query1Pos: query1Pos, query2Pos: query2Pos);
        }

        public UnitOfSpeech UnderstandIntent(UnitOfSpeech unitOfWork)
        {
            var workCount = unitOfWork.PartsOfSpeech.Descriptors.Length;
            try
            {
                //Action rule
                for (int i = 0; i < workCount; i++)
                {
                    for (int j = 0; j < workCount; j++)
                    {
                        if (workCount <= 1)
                        {

                            if (unitOfWork.PartsOfSpeech.Descriptors[i].Equals("verb"))
                            {
                                unitOfWork.Intent = unitOfWork.PartsOfSpeech.Words[i];
                                break;
                            }
                        }

                        if (workCount >= 4 && unitOfWork.PartsOfSpeech.Descriptors[i].Equals("verb") && unitOfWork.PartsOfSpeech.Descriptors[j].Equals("noun")
                            && (j <= workCount - 2 && unitOfWork.PartsOfSpeech.Descriptors[j + 2].Equals("noun")))
                        {
                            unitOfWork.Intent = unitOfWork.PartsOfSpeech.Words[i] + " " + unitOfWork.PartsOfSpeech.Words[j];
                            unitOfWork.Entity = unitOfWork.PartsOfSpeech.Words[j + 2];
                            break;

                        }
                        else if (unitOfWork.PartsOfSpeech.Descriptors[i].Equals("verb") && unitOfWork.PartsOfSpeech.Descriptors[j].Equals("noun"))
                        {
                            unitOfWork.Intent = unitOfWork.PartsOfSpeech.Words[i] + " " + unitOfWork.PartsOfSpeech.Words[j];
                            break;
                        }

                        if (unitOfWork.PartsOfSpeech.Descriptors[i].Equals("verb") && unitOfWork.PartsOfSpeech.Descriptors[j].Equals("verb"))
                        {
                            if (i != j)
                            {
                                unitOfWork.Intent = unitOfWork.PartsOfSpeech.Words[j] + " " + unitOfWork.PartsOfSpeech.Words[i];
                                break;
                            }
                        }
                    }
                }

                //noun rule
                for (int i = 0; i < workCount; i++)
                {
                    for (int j = 0; j < workCount; j++)
                    {
                        if (workCount <= 1)
                        {
                            if (unitOfWork.PartsOfSpeech.Descriptors[j].Contains("noun"))
                            {
                                unitOfWork.Intent = unitOfWork.PartsOfSpeech.Words[j];
                                break;
                            }
                        }

                        if (unitOfWork.PartsOfSpeech.Descriptors[i].Equals("adjective") && unitOfWork.PartsOfSpeech.Descriptors[j].Contains("noun"))
                        {
                            unitOfWork.Intent = unitOfWork.PartsOfSpeech.Words[i] + " " + unitOfWork.PartsOfSpeech.Words[j];
                            break;
                        }
                    }
                }

                //injection rule
                for (int i = 0; i < workCount; i++)
                {
                    for (int j = 0; j < workCount; j++)
                    {

                        if (unitOfWork.PartsOfSpeech.Descriptors[i].Equals("interjection"))
                        {
                            unitOfWork.Intent = unitOfWork.PartsOfSpeech.Words[i];
                            break;
                        }
                    }
                }

                //question rule
                for (int k = 0; k < workCount; k++)
                {
                    if (unitOfWork.PartsOfSpeech.Descriptors[k].Equals("wh-determiner") || unitOfWork.PartsOfSpeech.Descriptors[k].Equals("wh-pronoun")
                        || unitOfWork.PartsOfSpeech.Descriptors[k].Equals("wh-pronoun") || unitOfWork.PartsOfSpeech.Descriptors[k].Equals("wh-adverb"))
                    {

                        for (int i = 0; i < workCount; i++)
                        {
                            for (int j = 0; j < workCount; j++)
                            {
                                if (unitOfWork.PartsOfSpeech.Descriptors[i].Equals("verb") && unitOfWork.PartsOfSpeech.Descriptors[j].Equals("noun"))
                                {
                                    unitOfWork.Intent = unitOfWork.PartsOfSpeech.Words[k] + " " + unitOfWork.PartsOfSpeech.Words[i] + " " + unitOfWork.PartsOfSpeech.Words[j];
                                    if (!unitOfWork.Intent.Equals(null))
                                    {
                                        break;
                                    }
                                }
                                if (unitOfWork.PartsOfSpeech.Descriptors[i].Equals("verb") && unitOfWork.PartsOfSpeech.Descriptors[j].Equals("pronoun"))
                                {
                                    unitOfWork.Intent = unitOfWork.PartsOfSpeech.Words[k] + " " + unitOfWork.PartsOfSpeech.Words[i] + " " + unitOfWork.PartsOfSpeech.Words[j];
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return unitOfWork;
        }

        private void VoicePOSDebug(string[] PartsOfSpeech, string[] words)
        {
            for (int i = 0; i < words.Length; i++)
            {
                _speechController.Speak(words[i] + " is a " + PartsOfSpeech[i]);
            }
        }

    }
}
