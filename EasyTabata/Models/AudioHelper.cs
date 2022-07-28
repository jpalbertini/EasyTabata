using System.Reflection;

namespace EasyTabata.Models
{
    public class AudioHelper
    {
        private static void InternalPlay(string sound)
        {
            if (!Settings.EnableSound)
                return;

            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream audioStream = assembly.GetManifestResourceStream("EasyTabata.Ressources.Sounds." + sound);
            //var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            //player.Load(audioStream);
            //player.Play();
        }

        public static void PlayStarting()
        {
            InternalPlay("Starting.wav");
        }

        public static void PlayAlmostFinishBell()
        {
            InternalPlay("AlmostFinish.wav");
        }
        public static void PlayFinishBell()
        {
            InternalPlay("Finish.mp3");
        }
    }
}
