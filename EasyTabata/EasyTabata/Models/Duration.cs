using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

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
        public void Reset()
        {
            Minutes = 0;
            Seconds = 0;
        }
        public void AddSeconds(int seconds)
        {
            int total = TotalSeconds + seconds;
            Minutes = total / 60;
            Seconds = total % 60;
        }
        public void RemoveSeconds(int seconds)
        {
            int total = TotalSeconds - seconds;
            Minutes = total / 60;
            Seconds = total % 60;
        }

        static public Duration FromSeconds(int seconds)
        {
            return new Duration(seconds / 60, seconds % 60);
        }
        static public Duration FromString(string str)
        {
            int min = 0, secs = 0;
            var splits = str.Split(':');
            if (splits.Length > 0)
                min = int.Parse(splits[0]);
            if (splits.Length > 1)
                secs = int.Parse(splits[1]);

            return new Duration(min, secs);
        }
        public static Duration operator +(Duration lhs, Duration rhs)
        {
            return Duration.FromSeconds(lhs.TotalSeconds + rhs.TotalSeconds);
        }
        public static Duration operator -(Duration lhs, Duration rhs)
        {
            return Duration.FromSeconds(lhs.TotalSeconds - rhs.TotalSeconds);
        }
        public static bool operator >(Duration lhs, Duration rhs)
        {
            return lhs.TotalSeconds > rhs.TotalSeconds;
        }
        public static bool operator <(Duration lhs, Duration rhs)
        {
            return lhs.TotalSeconds < rhs.TotalSeconds;
        }
        public static bool operator ==(Duration lhs, Duration rhs)
        {
            return lhs.TotalSeconds == rhs.TotalSeconds;
        }
        public static bool operator !=(Duration lhs, Duration rhs)
        {
            return lhs.TotalSeconds != rhs.TotalSeconds;
        }

        public override string ToString()
        {
            return String.Format("{0} mins {1} secs", Minutes, Seconds);
        }

        public void CopyFrom(Duration other)
        {
            Minutes = other.Minutes;
            Seconds = other.Seconds;
        }
    }
}
