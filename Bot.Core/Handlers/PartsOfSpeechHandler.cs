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
     
        public string[] POStagging(string utterance)
        {
            var words = utterance.Split(' ');
            var modelPath = ConfigurationManager.AppSettings["languageModel"];
            var posTagger = new EnglishMaximumEntropyPosTagger(modelPath);
            var unprocessedPos = GetFullPOSString(posTagger.Tag(words));
            var pos = CorrectionByIntent(unprocessedPos);

            return pos;
        }

        //https://www.ling.upenn.edu/courses/Fall_2003/ling001/penn_treebank_pos.html
        private string[] GetFullPOSString(string[] unprocessedPos)
        {
            string[] proccessedPos = unprocessedPos;
            for (int i = 0; i < unprocessedPos.Length; i++)
            {
                {
                    switch (unprocessedPos[i])
                    {
                        case "CC":
                            proccessedPos[i] = "conjunction";
                            break;
                        case "CD":
                            proccessedPos[i] = "cardinal number ";
                            break;
                        case "DT":
                            proccessedPos[i] = "determiner";
                            break;
                        case "EX":
                            proccessedPos[i] = "existential there";
                            break;
                        case "FW":
                            proccessedPos[i] = "foreign word";
                            break;
                        case "IN":
                            proccessedPos[i] = "preposition ";
                            break;
                        case "JJ":
                            proccessedPos[i] = "adjective";
                            break;
                        case "JJR":
                            proccessedPos[i] = "adjective";
                            break;
                        case "JJS":
                            proccessedPos[i] = "adjective";
                            break;
                        case "LS":
                            proccessedPos[i] = "list item marker";
                            break;
                        case "MD":
                            proccessedPos[i] = "modal";
                            break;
                        case "NN":
                            proccessedPos[i] = "noun";
                            break;
                        case "NNS":
                            proccessedPos[i] = "noun";
                            break;
                        case "NNP":
                            proccessedPos[i] = "proper noun";
                            break;
                        case "NNPS":
                            proccessedPos[i] = "proper noun";
                            break;
                        case "PDT":
                            proccessedPos[i] = "predeterminer";
                            break;
                        case "POS":
                            proccessedPos[i] = "possessive ";
                            break;
                        case "PRP":
                            proccessedPos[i] = "pronoun";
                            break;
                        case "PRP$":
                            proccessedPos[i] = "pronoun";
                            break;
                        case "RB":
                            proccessedPos[i] = "adverb";
                            break;
                        case "RBR":
                            proccessedPos[i] = "adverb";
                            break;
                        case "RBS":
                            proccessedPos[i] = "adverb";
                            break;
                        case "RP":
                            proccessedPos[i] = "particle";
                            break;
                        case "SYM":
                            proccessedPos[i] = "symbol";
                            break;
                        case "TO":
                            proccessedPos[i] = "to";
                            break;
                        case "UH":
                            proccessedPos[i] = "interjection";
                            break;
                        case "VB":
                            proccessedPos[i] = "verb";
                            break;
                        case "VBD":
                            proccessedPos[i] = "verb";
                            break;
                        case "VBG":
                            proccessedPos[i] = "verb";
                            break;
                        case "VBN":
                            proccessedPos[i] = "verb";
                            break;
                        case "VBP":
                            proccessedPos[i] = "verb";
                            break;
                        case "VBZ":
                            proccessedPos[i] = "verb";
                            break;
                        case "WDT":
                            proccessedPos[i] = "wh-determiner";
                            break;
                        case "WP":
                            proccessedPos[i] = "wh-pronoun";
                            break;
                        case "WP$":
                            proccessedPos[i] = "wh-pronoun";
                            break;
                        case "WRB":
                            proccessedPos[i] = "wh-adverb";
                            break;
                        default:
                            break;
                    }
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
                else if (unproccessedPos.Length == 1 && (unproccessedPos[i].Contains("noun")|| unproccessedPos[i].Equals("list item marker")))
                {
                    proccessedPos[i] = "interjection";
                }
                if (unproccessedPos.Length == 2 && (unproccessedPos[0].Equals("adjective") || unproccessedPos[1].Equals("noun"))){
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
                if (unprocessedPos.Descriptors[i].Equals("conjunction")) {
                    conjuction = unprocessedPos.Words[i];
                }
            }
            return conjuction;
        }
    }
}
