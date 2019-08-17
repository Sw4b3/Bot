using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Core.Handlers
{
    public static class ResponseDictionary
    {
        public static Dictionary<int, string> morningGreetings = new Dictionary<int, string>()
        {
            {0,"Goodmorning"},
            {1, "Moring"},
            {2,"Hello"},
        };

        public static Dictionary<int, string> afternoonGreetings = new Dictionary<int, string>()
        {
            {0,"Good afternoon"},
            {1, "Afternoon"},
            {2,"Hello"},
        };

        public static Dictionary<int, string> eveningGreetings = new Dictionary<int, string>()
        {
            {0,"Good evening"},
            {1, "Evening"},
            {2,"Hello"},
        };

        public static Dictionary<int, string> pleasantries = new Dictionary<int, string>()
        {
            {0,"I am well thanks"},
            {1, "I am good"},
            {2,"I am good thank you"},
        };

        public static Dictionary<int, string> time = new Dictionary<int, string>()
        {
            {0,"The time is "},
            {1, "It is"},
            {2, null},
        };

        public static Dictionary<int, string> date = new Dictionary<int, string>()
        {
            {0,"The date is "},
            {1, "It is"},
            {2, null},
        };

    }
}

