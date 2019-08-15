using Bot.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenNLP.Tools.PosTagger;
using System.Configuration;

namespace Bot.Core.Handlers
{
    public class PartsOfSpeechHandler : IPartsOfSpeechHandler
    {
        public PartsOfSpeech POStagging(string utterance)
        {
            var words = utterance.Split(' ');
            var modelPath = ConfigurationManager.AppSettings["languageModel"];

            var posTagger = new EnglishMaximumEntropyPosTagger(modelPath);
            var unprocessedPos = GetFullPOSString(posTagger.Tag(words));
            var pos = CorrectionByIntent(unprocessedPos);
            
            return new PartsOfSpeech()
            {
                Words = words,
                Descriptors = pos,
            };
        }

        private string[] GetFullPOSString(string[] unprocessedPos)
        {
            string[] proccessedPos = unprocessedPos;
            for (int i = 0; i < unprocessedPos.Length; i++)
            {
                {
                    proccessedPos[i] = PartsOfSpeechDictionary.dict[unprocessedPos[i]];
                }
            }
            return proccessedPos;
        }

        private string[] CorrectionByIntent(string[] unproccessedPos)
        {
            string[] proccessedPos = unproccessedPos;
            for (int i = 0; i < unproccessedPos.Length; i++)
            {
                if (unproccessedPos.Length == 1 && unproccessedPos[i].Contains("noun"))
                {
                    proccessedPos[i] = "verb";
                }
                else if (unproccessedPos.Length == 1 && (unproccessedPos[i].Contains("noun") || unproccessedPos[i].Equals("list item marker")))
                {
                    proccessedPos[i] = "interjection";
                }
                if (unproccessedPos.Length == 2 && (unproccessedPos[0].Equals("adjective") || unproccessedPos[1].Equals("noun")))
                {
                    proccessedPos[0] = "verb";
                }
                else if (unproccessedPos.Length == 2 && (unproccessedPos[0].Equals("adverb") || unproccessedPos[1].Equals("verb")))
                {
                    proccessedPos[0] = "verb";
                    proccessedPos[1] = "noun";
                }
                if (unproccessedPos.Length >= 3 && unproccessedPos[0].Equals("noun"))
                {
                    proccessedPos[0] = "verb";
                }

            }
            return proccessedPos;
        }

        public string SearchForConjuction(PartsOfSpeech unprocessedPos)
        {
            var conjuction = "";
            for (int i = 0; i < unprocessedPos.Descriptors.Length; i++)
            {
                if (unprocessedPos.Descriptors[i].Equals("conjunction"))
                {
                    conjuction = unprocessedPos.Words[i];
                }
            }
            return conjuction;
        }
    }
}
