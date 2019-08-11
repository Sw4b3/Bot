using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
using System.Threading;
using Bot.Core.Interfaces;

namespace Bot.Core
{
    public class SpeechController: ISpeechController
    {
        private static SpeechSynthesizer _speechSynthesizer = new SpeechSynthesizer();

        public SpeechController()
        {
            _speechSynthesizer.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
        }

        public void Speak(string utterance)
        {
            Task.Run(() =>
            {
                _speechSynthesizer.Speak(utterance);
            });
        }


    }
}
