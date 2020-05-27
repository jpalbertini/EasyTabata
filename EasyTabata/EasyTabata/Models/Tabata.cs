using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace EasyTabata.Models
{
    public class Tabata
    {
        public Guid ID { get; set; }
        public String Title { get; set; }

        public Duration PreparationLength { get; set; }
        public Duration WorkLength { get; set; }
        public Duration RestLength { get; set; }
        public Duration RestBetweenRoundLength { get; set; }
        public int RoundCount { get; set; }
        public int ExerciseCount { get; set; }

        public Duration CompleteLength
        { 
            get {
                var exerciseLength = (RoundCount * WorkLength.TotalSeconds) + ((RoundCount - 1) * RestLength.TotalSeconds);
                var duration = Duration.FromSeconds(exerciseLength * ExerciseCount) + Duration.FromSeconds(RestBetweenRoundLength.TotalSeconds * (ExerciseCount - 1));
                return (PreparationLength + duration);
            }
        }

        public Tabata()
        {
            ID = Guid.Empty;
            PreparationLength = Duration.FromSeconds(30);
            RestBetweenRoundLength = Duration.FromSeconds(60);
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
