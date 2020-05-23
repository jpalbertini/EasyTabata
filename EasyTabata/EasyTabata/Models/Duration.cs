using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTabata.Models
{
    public class Duration
    {
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public int TotalSeconds { get { return Minutes * 60 + Seconds; } }
        public Duration()
        {
        }
        public Duration(int minutes, int seconds)
        {
            Minutes = minutes;
            Seconds = seconds;
        }

        static public Duration FromSeconds(int seconds)
        {
            return new Duration(seconds / 60, seconds % 60);
        }
        public static Duration operator +(Duration lhs, Duration rhs)
        {
            return Duration.FromSeconds(lhs.TotalSeconds + rhs.TotalSeconds);
        }

        public override string ToString()
        {
            return $"{Minutes}:{Seconds}";
        }
    }
}
