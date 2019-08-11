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
        string response = "";

        public string Greeting()
        {
            string user = System.Environment.UserName;
            System.DateTime currentTime = System.DateTime.Now;
            if (currentTime.Hour >= 5 && currentTime.Hour < 12)
            {
                reply = random.Next(0, 2);
                switch (reply)
                {
                    case 0:
                        return ("Goodmorning " + user);
                    case 1:
                           return ("Goodmorning " + user);
                    case 2:
                        return ("Hello " + user);
                }      
            }
            if (currentTime.Hour >= 12 && currentTime.Hour < 18)
            {
                reply = random.Next(0, 3);
                switch (reply)
                {
                    case 0:
                        return ("Good afternoon " + user);
                    case 1:
                        return ("Good afternoon ");
                    case 2:
                        return ("Hello " + user);
                }
            }
            if (currentTime.Hour >= 18 && currentTime.Hour < 24)
            {
                reply = random.Next(0, 3);
                switch (reply)
                {
                    case 0:
                        return ("Good evening " + user);
                    case 1:
                        return ("Good evening ");
                    case 2:
                        return ("Hello " + user);
                }
            }
            if (currentTime.Hour < 5)
            {
                return ("Hello " + user + ", you are still awake you should go to sleep, it's getting late");
            }
            return response;
        }

        public string Pleasantry() {
            reply = random.Next(0, 3);
            switch (reply)
            {
                case 0:
                    return ("I am well thanks");
                case 1:
                    return ("I am good");
                case 2:
                    return ("Im good thank you");
            }
            return response;
        }

        public string GetTime()
        {
            reply = random.Next(0, 3);
            switch (reply)
            {
                case 0:
                    return null;
                case 1:
                    return ("The time is ");
                case 2:
                    return ("It is ");
            }
            return response;
        }

        public string GetDate(string date)
        {
            reply = random.Next(0, 2);
            switch (reply)
            {
                case 0:
                    return ("The date is " + date);
                case 1:
                    return ("It is " + date);
            }
            return response;
        }

        public string RepeatedUtterance()
        {
            reply = random.Next(0, 5);
            switch (reply)
            {
                case 0:
                case 1:
                case 2:
                    return (null);
                case 3:
                    return ("You just said that");
            }
            return response;
        }
    }
}
