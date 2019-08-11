using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Core.Handlers
{
    public class POSHandler: IPOSHandler
    {
        string verb;
        string noun;
        string pronoun;
        string question;
        string[] words;
        string[] PartsOfSpeech;
        string[] adjectives;
        string[] adverbs;
        string[] conjunctions;
        string[] determiners;
        string[] interjections;
        string[] nouns;
        string[] numbers;
        string[] prepostions;
        string[] pronouns;
        string[] verbs;
        string[] questionWords = { "have", "how", "what", "when", "why", "where" };
        ArrayList untaggedWord = new ArrayList();

        public void ReadAllGrammarFiles()
        {
            adverbs = File.ReadAllLines(".\\Grammar\\Parts of Speech\\adverbs.txt");
            adjectives = File.ReadAllLines(".\\Grammar\\Parts of Speech\\adjectives.txt");
            conjunctions = File.ReadAllLines(".\\Grammar\\Parts of Speech\\conjunctions.txt");
            determiners = File.ReadAllLines(".\\Grammar\\Parts of Speech\\determiners.txt");
            interjections = File.ReadAllLines(".\\Grammar\\Parts of Speech\\Interjections.txt");
            nouns = File.ReadAllLines(".\\Grammar\\Parts of Speech\\nouns.txt");
            numbers = File.ReadAllLines(".\\Grammar\\Parts of Speech\\numbers.txt");
            prepostions = File.ReadAllLines(".\\Grammar\\Parts of Speech\\prepostions.txt");
            pronouns = File.ReadAllLines(".\\Grammar\\Parts of Speech\\pronouns.txt");
            verbs = File.ReadAllLines(".\\Grammar\\Parts of Speech\\verbs.txt");
        }

        public string[] POStagging(string utterance)
        {
            words = utterance.Split(' ');
            PartsOfSpeech = new string[words.Length];

            for (int i = 0; i < words.Length; i++)
            {
                for (int j = 0; j < adjectives.Length; j++)
                {
                    if (words[i].Equals(adjectives[j]))
                    {
                        PartsOfSpeech[i] = "adjective";
                    }
                }

                for (int j = 0; j < adverbs.Length; j++)
                {
                    if (words[i].Equals(adverbs[j]))
                    {
                        PartsOfSpeech[i] = "adverb";
                    }
                }

                for (int j = 0; j < conjunctions.Length; j++)
                {
                    if (words[i].Equals(conjunctions[j]))
                    {
                        PartsOfSpeech[i] = "conjunction";
                    }
                }

                for (int j = 0; j < determiners.Length; j++)
                {
                    if (words[i].Equals(determiners[j]))
                    {
                        PartsOfSpeech[i] = "determiner";
                    }
                }

                for (int j = 0; j < interjections.Length; j++)
                {
                    if (words[i].Equals(interjections[j]))
                    {
                        PartsOfSpeech[i] = "interjection";
                    }
                }

                for (int j = 0; j < nouns.Length; j++)
                {
                    if (words[i].Equals(nouns[j]))
                    {
                        PartsOfSpeech[i] = "noun";
                    }
                }

                for (int j = 0; j < pronouns.Length; j++)
                {
                    if (words[i].Equals(pronouns[j]))
                    {
                        PartsOfSpeech[i] = "pronoun";
                    }
                }

                for (int j = 0; j < prepostions.Length; j++)
                {
                    if (words[i].Equals(prepostions[j]))
                    {
                        PartsOfSpeech[i] = "prepostion";
                    }
                }

                for (int j = 0; j < verbs.Length; j++)
                {
                    if (words[i].Equals(verbs[j]))
                    {
                        PartsOfSpeech[i] = "verb";
                    }
                }

                for (int j = 0; j < questionWords.Length; j++)
                {
                    if (words[i].Equals(questionWords[j]))
                    {
                        question = words[i];
                    }
                }

                if (PartsOfSpeech[i] == null)
                {
                    untaggedWord.Add(words[i]);
                }
            }
            return PartsOfSpeech;
        }

        public string[] GetConjuctions() {
            return conjunctions;
        }

        public string GetQuestions()
        {
            return question;
        }

        public string GetInterjection()
        {
            string interjection = null;

            for (int i = 0; i < PartsOfSpeech.Length; i++)
            {
                if (PartsOfSpeech[i].Equals("interjection"))
                {
                    interjection = words[i];
                }
            }
            return interjection;
        }

        public string GetNoun()
        {
            noun = null;

            for (int i = 0; i < PartsOfSpeech.Length; i++)
            {
                if (PartsOfSpeech[i].Equals("noun"))
                {
                    noun = words[i];
                }
            }
            return noun;
        }

        public string GetPronoun()
        {
            pronoun = null;

            for (int i = 0; i < PartsOfSpeech.Length; i++)
            {
                if (PartsOfSpeech[i].Equals("pronoun"))
                {
                    pronoun = words[i];
                }
            }
            return noun;
        }

        public string GetVerb()
        {
            verb = null;

            for (int i = 0; i < PartsOfSpeech.Length; i++)
            {
                if (PartsOfSpeech[i].Equals("verb"))
                {
                    verb = words[i];
                }
            }
            return verb;
        }
    }
}
