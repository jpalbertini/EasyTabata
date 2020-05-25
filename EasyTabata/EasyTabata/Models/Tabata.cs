using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace EasyTabata.Models
{
    public class Tabata: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Guid ID { get; set; }
        public String Title { get; set; }

        private Duration _PreparationLength = new Duration();
        public Duration PreparationLength
        {
            get { return _PreparationLength; }
            set 
            {
                _PreparationLength = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CompleteLength"));
            } 
        }

        private Duration _WorkLength = new Duration();
        public Duration WorkLength 
        {
            get { return _WorkLength; }
            set { _WorkLength = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CompleteLength")); }
        }

        private Duration _RestLength = new Duration();
        public Duration RestLength
        {
            get { return _RestLength; }
            set { _RestLength = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CompleteLength")); }
        }

        private Duration _RestBetweenExercicesLength = new Duration();
        public Duration RestBetweenExercicesLength
        {
            get { return _RestBetweenExercicesLength; }
            set { _RestBetweenExercicesLength = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CompleteLength")); }
        }

        private int _RoundCount;
        public int RoundCount
        {
            get { return _RoundCount; }
            set { _RoundCount = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CompleteLength")); }
        }

        private int _ExerciseCount;
        public int ExerciseCount
        {
            get { return _ExerciseCount; }
            set { _ExerciseCount = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CompleteLength")); }
        }

        public String CompleteLength
        { 
            get {
                var exerciseLength = (ExerciseCount * WorkLength.TotalSeconds) + ((ExerciseCount - 1) * RestLength.TotalSeconds);
                var duration = Duration.FromSeconds(exerciseLength * RoundCount);
                return (PreparationLength + duration).ToString();
            } 
        }

        public Tabata()
        {
            ID = Guid.Empty;
            PreparationLength = Duration.FromSeconds(30);
            RestBetweenExercicesLength = Duration.FromSeconds(30);
            RoundCount = 8;
            ExerciseCount = 8;
            WorkLength = Duration.FromSeconds(20);
            RestLength = Duration.FromSeconds(10);
        }

        public Tabata Clone()
        {
            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
            return JsonConvert.DeserializeObject<Tabata>(JsonConvert.SerializeObject(this), deserializeSettings);
        }
    }
}
