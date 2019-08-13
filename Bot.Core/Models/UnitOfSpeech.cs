using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Core.Models
{
    public class UnitOfSpeech
    {
        public string Utterance { get; set; }
        public DateTime Timerstamp { get; set; }
        public string[] Words { get; set; }
        public string[] PartsOfSpeech { get; set; }
        public string Intent { get; set; }
        public string Entity { get; set; }
        public string OptionalEntity { get; set; }
        public string Query { get; set; }
    }
}
