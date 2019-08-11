using Bot.Services.Interfaces;
using System;

namespace Bot.Services
{
    public class DateTimeService: IDateTimeService
    {
        public string GetDate() {
            return DateTime.Now.ToString("dd MMM", new System.Globalization.CultureInfo("en-US"));
        }

        public string GetTime()
        {
            return DateTime.Now.ToString("h:mm tt");
        }
    }
}
