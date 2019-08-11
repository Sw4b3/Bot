using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Core.Interfaces
{
    public interface IRecogntionController
    {
        void StartUp();
        void loadGrammarPOS();
        SpeechRecognitionEngine getInstance();
    }
}
