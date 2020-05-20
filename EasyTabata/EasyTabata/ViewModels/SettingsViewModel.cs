using EasyTabata.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTabata.ViewModels
{
    public class SettingsViewModel
    {
        public bool EnableSound
        {
            get { return Settings.EnableSound; }
            set { Settings.EnableSound = value; }
        }
    }
}
