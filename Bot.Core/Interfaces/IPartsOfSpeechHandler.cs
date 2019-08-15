using Bot.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Core.Handlers
{
    public interface IPartsOfSpeechHandler
    {
        PartsOfSpeech POStagging(string utterance);
        string SearchForConjuction(PartsOfSpeech utterance);
    }
}
