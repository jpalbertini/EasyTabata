using Newtonsoft.Json;
using System.ComponentModel;

namespace EasyTabata.Models
{
    public class Tabata : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Guid ID { get; set; }
        public String Title { get; set; }
        public Duration PreparationLength { get; set; }
        public Duration WorkLength { get; set; }
        public Duration RestLength { get; set; }
        public Duration RestBetweenRoundLength { get; set; }
        public int RoundCount { get; set; }
        public int ExerciseCount { get; set; }
        public Duration CompleteLength { get; private set; }

        public Tabata()
        {
            ID = Guid.Empty;
            PreparationLength = Duration.FromSeconds(30);
            RestBetweenRoundLength = Duration.FromSeconds(60);
            RoundCount = 8;
            ExerciseCount = 8;
            WorkLength = Duration.FromSeconds(20);
            RestLength = Duration.FromSeconds(10);
            UpdateDuration();
        }


        public Tabata Clone()
        {
            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
            return JsonConvert.DeserializeObject<Tabata>(JsonConvert.SerializeObject(this), deserializeSettings);
        }

        public void UpdateDuration()
        {
            var exerciseLength = (RoundCount * WorkLength.TotalSeconds) + ((RoundCount - 1) * RestLength.TotalSeconds);
            var duration = Duration.FromSeconds(exerciseLength * ExerciseCount) + Duration.FromSeconds(RestBetweenRoundLength.TotalSeconds * (ExerciseCount - 1));
            CompleteLength = PreparationLength + duration;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CompleteLength"));
        }
    }
}
