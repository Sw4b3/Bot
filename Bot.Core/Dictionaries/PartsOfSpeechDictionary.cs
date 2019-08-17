using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Core.Handlers
{
    public static class PartsOfSpeechDictionary
    {
        //https://www.ling.upenn.edu/courses/Fall_2003/ling001/penn_treebank_pos.html
        public static Dictionary<string, string> dict = new Dictionary<string, string>()
                                            {
                                                {"CC","conjunction"},
                                                { "CD", "cardinal number"},
                                                {"DT","determiner"},
                                                {"EX","existential there"},
                                                { "FW", "foreign word"},
                                                {"IN","preposition"},
                                                {"JJ","adjective"},
                                                { "JJR", "adjective"},
                                                {"JJS","adjective"},
                                                {"LS","list item marker"},
                                                { "MD", "modal"},
                                                {"NN","noun"},
                                                {"NNR","noun"},
                                                { "NNS", "noun"},
                                                {"NNP","proper noun"},
                                                { "NNPS", "proper noun"},
                                                {"PDT","predeterminer"},
                                                {"POS","possessive"},
                                                { "PRP", "pronoun"},
                                                {"PRP$","pronoun"},
                                                {"RB","adverb"},
                                                { "RBR", "adverb"},
                                                {"RBS","adverb"},
                                                {"PR","particle"},
                                                { "SYM", "symbol"},
                                                {"TO","to"},
                                                {"UH","interjection"},
                                                { "VB", "verb"},
                                                {"VBD","verb"},
                                                { "VBG", "verb"},
                                                {"VBN","verb"},
                                                {"VBP","verb"},
                                                { "VBZ", "verb"},
                                                {"WDT","wh-determiner"},
                                                {"WP","wh-pronoun"},
                                                { "WP$", "wh-pronoun"},
                                                {"WRB","wh-adverb"}
                                            };

    }
}

