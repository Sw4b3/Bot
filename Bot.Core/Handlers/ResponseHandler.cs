using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Core.Handlers
{
    public class ResponseHandler
    {
        Random random = new Random();
        int reply;

        public string GetGreetingResponse()
        {
            string user = Environment.UserName;
            DateTime currentTime = DateTime.Now;
            if (currentTime.Hour >= 5 && currentTime.Hour < 12)
            {
                reply = random.Next(0, 3);
                return ResponseDictionary.morningGreetings[reply];
            }
            else if (currentTime.Hour >= 12 && currentTime.Hour < 18)
            {
                reply = random.Next(0, 3);
                return ResponseDictionary.afternoonGreetings[reply];
            }
            else if (currentTime.Hour >= 18 && currentTime.Hour < 24)
            {
                reply = random.Next(0, 3);
                return ResponseDictionary.eveningGreetings[reply];
            }
            else if (currentTime.Hour < 5)
            {
                return ("Hello " + user + ", you are still awake you should go to sleep");
            }
            return null;
        }

        public string GetPleasantryResponse() {
            reply = random.Next(0, 3);
            return ResponseDictionary.pleasantries[reply];
        }

        public string GetTimeResponse(string time)
        {
            reply = random.Next(0, 3);
            return ResponseDictionary.time[reply] + time;
        }

        public string GetDateResponse(string date)
        {
            reply = random.Next(0, 2);
            return ResponseDictionary.time[reply] + date;
        }
    }
}
