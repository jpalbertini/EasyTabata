using EasyTabata.Models;

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
