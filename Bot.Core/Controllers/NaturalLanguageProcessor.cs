using Bot.Core.Handlers;
using Bot.Core.Interfaces;
using Bot.Core.Models;
using System;
using System.Collections;
using System.IO;
using System.Speech.Recognition;
using System.Linq;
using System.Collections.Generic;

namespace Bot.Core
{
    public class NaturalLanguageProcessor : INaturalLanguageProcessor
    {
        private IPartsOfSpeechHandler _posHandler;

        public NaturalLanguageProcessor(IPartsOfSpeechHandler posHandler)
        {
            _posHandler = posHandler;
        }

        public UnitOfSpeech RecognizeIntent(UnitOfSpeech unitOfSpeech)
        {
            var utterance = unitOfSpeech.Utterance;
            unitOfSpeech.Queries = new List<Query>();

            unitOfSpeech.PartsOfSpeech = _posHandler.POStagging(utterance);

            var conjunction = _posHandler.SearchForConjuction(unitOfSpeech.PartsOfSpeech);
            if (conjunction != "" && unitOfSpeech.Utterance.Contains(conjunction))
            {
                var querys = unitOfSpeech.Utterance.Split(new string[] { conjunction }, StringSplitOptions.None);
                var query1 = querys[0].Trim();
                var queryPos = SplitQuery(query1, unitOfSpeech.PartsOfSpeech);

                unitOfSpeech.PartsOfSpeech = queryPos.query1Pos;
                unitOfSpeech.Queries.Add(UnderstandIntent(unitOfSpeech));

                unitOfSpeech.PartsOfSpeech = queryPos.query2Pos;
                unitOfSpeech.Queries.Add(UnderstandIntent(unitOfSpeech));
            }
            else
            {
                unitOfSpeech.Queries.Add(UnderstandIntent(unitOfSpeech));
            }

            return unitOfSpeech;
        }

        private (PartsOfSpeech query1Pos, PartsOfSpeech query2Pos) SplitQuery(string query, PartsOfSpeech partsOfSpeech)
        {
            var query1Pos = new PartsOfSpeech();
            var query2Pos = new PartsOfSpeech();
            var words = query.Split(' ');
            var wordCount = words.Length;
            query1Pos.Descriptors = partsOfSpeech.Descriptors.Take(wordCount).ToArray();
            query1Pos.Words = partsOfSpeech.Words.Take(wordCount).ToArray();
            query2Pos.Descriptors = partsOfSpeech.Descriptors.Skip(wordCount + 1).ToArray();
            query2Pos.Words = partsOfSpeech.Words.Skip(wordCount + 1).ToArray();
            return (query1Pos: query1Pos, query2Pos: query2Pos);
        }

        private Query UnderstandIntent(UnitOfSpeech unitOfWork)
        {
            var query = new Query();
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
                                query.Intent = unitOfWork.PartsOfSpeech.Words[i];
                                break;
                            }
                        }

                        if (workCount >= 4 && unitOfWork.PartsOfSpeech.Descriptors[i].Equals("verb") && unitOfWork.PartsOfSpeech.Descriptors[j].Equals("noun")
                            && (j <= workCount - 2 && unitOfWork.PartsOfSpeech.Descriptors[j + 2].Equals("noun")))
                        {
                            query.Intent = unitOfWork.PartsOfSpeech.Words[i] + " " + unitOfWork.PartsOfSpeech.Words[j];
                            query.Entity = unitOfWork.PartsOfSpeech.Words[j + 2];
                            break;

                        }
                        else if (unitOfWork.PartsOfSpeech.Descriptors[i].Equals("verb") && unitOfWork.PartsOfSpeech.Descriptors[j].Equals("noun"))
                        {
                            query.Intent = unitOfWork.PartsOfSpeech.Words[i] + " " + unitOfWork.PartsOfSpeech.Words[j];
                            break;
                        }

                        if (unitOfWork.PartsOfSpeech.Descriptors[i].Equals("verb") && unitOfWork.PartsOfSpeech.Descriptors[j].Equals("verb"))
                        {
                            if (i != j)
                            {
                                query.Intent = unitOfWork.PartsOfSpeech.Words[j] + " " + unitOfWork.PartsOfSpeech.Words[i];
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
                                query.Intent = unitOfWork.PartsOfSpeech.Words[j];
                                break;
                            }
                        }

                        if (unitOfWork.PartsOfSpeech.Descriptors[i].Equals("adjective") && unitOfWork.PartsOfSpeech.Descriptors[j].Contains("noun"))
                        {
                            query.Intent = unitOfWork.PartsOfSpeech.Words[i] + " " + unitOfWork.PartsOfSpeech.Words[j];
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
                            query.Intent = unitOfWork.PartsOfSpeech.Words[i];
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
                                    query.Intent = unitOfWork.PartsOfSpeech.Words[k] + " " + unitOfWork.PartsOfSpeech.Words[i] + " " + unitOfWork.PartsOfSpeech.Words[j];
                                    if (!query.Intent.Equals(null))
                                    {
                                        break;
                                    }
                                }
                                if (unitOfWork.PartsOfSpeech.Descriptors[i].Equals("verb") && unitOfWork.PartsOfSpeech.Descriptors[j].Equals("pronoun"))
                                {
                                    query.Intent = unitOfWork.PartsOfSpeech.Words[k] + " " + unitOfWork.PartsOfSpeech.Words[i] + " " + unitOfWork.PartsOfSpeech.Words[j];
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
            return query;
        }

    }
}
