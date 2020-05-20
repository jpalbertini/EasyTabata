using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace EasyTabata.Models
{
    public static class Settings
    {
        static string EnableSoundKey = "EnableSound";
        public static bool EnableSound
        {
            get { return Preferences.Get(EnableSoundKey, true); }
            set { Preferences.Set(EnableSoundKey, value); }
        }
    }
}
