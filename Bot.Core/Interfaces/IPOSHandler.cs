using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Core.Handlers
{
    public interface IPOSHandler
    {
        void ReadAllGrammarFiles();
        string[] POStagging(string utterance);
        string[] GetConjuctions();
        string GetQuestions();
    }
}
