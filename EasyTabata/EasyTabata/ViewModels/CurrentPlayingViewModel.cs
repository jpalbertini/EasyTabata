using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;
using Xamarin.Forms;

namespace EasyTabata.Models
{
    public class CurrentPlayingViewModel: INotifyPropertyChanged
    {
        public static String StateChangedEventName = "State";
        public static String TimeChangedEventName = "Time";
        public static String RoundChangedEventName = "Round";
        public static String ExerciceChangedEventName = "Exercice";

        public event PropertyChangedEventHandler PropertyChanged;

        public State CurrentState { get; private set; }
        public Duration WholePhaseTime { get; private set; }
        public Duration RemainingTime { get; private set; }
        public int CurrentRoundNumber { get; private set; }
        public int CurrentExerciseNumber { get; private set; }

        private Tabata attachedTabata = null;
        private Timer playTimer;

        public enum State
        {
            Preparation,
            Work,
            Rest,
            RestBetweenRound,
            Finish
        }

        public CurrentPlayingViewModel(Tabata attached)
        {
            RemainingTime = new Duration();
            WholePhaseTime = new Duration();
            attachedTabata = attached;
            playTimer = new Timer(1000);
            playTimer.Elapsed += TimerTick;
            playTimer.AutoReset = true;
            Reset();
        }

        private void TimerTick(Object source, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() => Update());
        }

        private void Update()
        {
            RemainingTime.RemoveSeconds(1);

            switch (CurrentState)
            {
                case State.Preparation:
                    if (RemainingTime.TotalSeconds == 0)
                    {
                        CurrentState = State.Work;
                        RemainingTime.CopyFrom(attachedTabata.WorkLength);
                        WholePhaseTime.CopyFrom(attachedTabata.WorkLength);
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(StateChangedEventName));
                    }
                    break;
                case State.Work:
                    if (RemainingTime.TotalSeconds == 0)
                    {
                        if ((CurrentRoundNumber + 1) == attachedTabata.RoundCount)
                        {
                            RemainingTime.CopyFrom(attachedTabata.RestBetweenRoundLength);
                            WholePhaseTime.CopyFrom(attachedTabata.RestBetweenRoundLength);
                            CurrentState = State.RestBetweenRound;
                        }
                        else if ((CurrentExerciseNumber + 1) == attachedTabata.ExerciseCount)
                        {
                            CurrentState = State.Finish;
                            playTimer.Stop();
                        }
                        else
                        {
                            CurrentState = State.Rest;
                            RemainingTime.CopyFrom(attachedTabata.RestLength);
                            WholePhaseTime.CopyFrom(attachedTabata.RestLength);
                        }

                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(StateChangedEventName));
                    }
                    break;
                case State.Rest:
                    if (RemainingTime.TotalSeconds == 0)
                    {
                        CurrentRoundNumber++;
                        RemainingTime.CopyFrom(attachedTabata.WorkLength);
                        WholePhaseTime.CopyFrom(attachedTabata.WorkLength);
                        CurrentState = State.Work;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(StateChangedEventName));
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(RoundChangedEventName));
                    }
                    break;
                case State.RestBetweenRound:
                    if (RemainingTime.TotalSeconds == 0)
                    {
                        RemainingTime.CopyFrom(attachedTabata.WorkLength);
                        WholePhaseTime.CopyFrom(attachedTabata.WorkLength);
                        CurrentExerciseNumber++;
                        CurrentState = State.Work;

                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(StateChangedEventName));
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(ExerciceChangedEventName));
                    }
                    break;
                default:
                    Debug.Assert(true, $"Should not be here for {CurrentState}");
                    break;
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(TimeChangedEventName));
        }

        public void Reset()
        {
            RemainingTime.CopyFrom(attachedTabata.PreparationLength);
            WholePhaseTime.CopyFrom(attachedTabata.PreparationLength);
            CurrentState = State.Preparation;
            CurrentRoundNumber = 0;
            CurrentExerciseNumber = 0;
            playTimer.Stop();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(StateChangedEventName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(RoundChangedEventName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(ExerciceChangedEventName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(TimeChangedEventName));
        }

        public void Start()
        {
            playTimer.Start();
        }
        public void Pause()
        {
            playTimer.Stop();
        }
    }
}
