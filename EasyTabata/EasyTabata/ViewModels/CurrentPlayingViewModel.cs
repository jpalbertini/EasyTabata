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
        public Duration WholeTime { get; private set; }
        public Duration RemainingTime { get; private set; }
        public Duration WholePhaseTime { get; private set; }
        public Duration RemainingPhaseTime { get; private set; }
        public int CurrentRoundNumber { get; private set; }
        public int CurrentExerciseNumber { get; private set; }

        public Tabata attachedTabata = null;
        private Timer playTimer;

        public bool IsPlaying { get { return playTimer.Enabled; } }

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
            RemainingPhaseTime = new Duration();
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
            RemainingPhaseTime.RemoveSeconds(1);
            RemainingTime.RemoveSeconds(1);

            switch (CurrentState)
            {
                case State.Preparation:
                    if (RemainingPhaseTime.TotalSeconds == 0)
                    {
                        CurrentRoundNumber = 1;
                        CurrentExerciseNumber = 1;
                        CurrentState = State.Work;
                        RemainingPhaseTime.CopyFrom(attachedTabata.WorkLength);
                        WholePhaseTime.CopyFrom(attachedTabata.WorkLength);
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(StateChangedEventName));
                    }
                    else if (RemainingPhaseTime.TotalSeconds == 3)
                        AudioHelper.PlayStarting();
                    break;
                case State.Work:
                    if (RemainingPhaseTime.TotalSeconds == 0)
                    {
                        AudioHelper.PlayFinishBell();
                        if ((CurrentRoundNumber) == attachedTabata.RoundCount)
                        {
                            RemainingPhaseTime.CopyFrom(attachedTabata.RestBetweenRoundLength);
                            WholePhaseTime.CopyFrom(attachedTabata.RestBetweenRoundLength);
                            CurrentState = State.RestBetweenRound;
                        }
                        else if ((CurrentExerciseNumber) == attachedTabata.ExerciseCount)
                        {
                            CurrentState = State.Finish;
                            playTimer.Stop();
                        }
                        else
                        {
                            CurrentState = State.Rest;
                            RemainingPhaseTime.CopyFrom(attachedTabata.RestLength);
                            WholePhaseTime.CopyFrom(attachedTabata.RestLength);
                        }

                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(StateChangedEventName));
                    }

                    if (RemainingPhaseTime.TotalSeconds < 5)
                        AudioHelper.PlayAlmostFinishBell();

                    break;
                case State.Rest:
                    if (RemainingPhaseTime.TotalSeconds == 0)
                    {
                        CurrentRoundNumber++;
                        RemainingPhaseTime.CopyFrom(attachedTabata.WorkLength);
                        WholePhaseTime.CopyFrom(attachedTabata.WorkLength);
                        CurrentState = State.Work;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(StateChangedEventName));
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(RoundChangedEventName));
                    }
                    break;
                case State.RestBetweenRound:
                    if (RemainingPhaseTime.TotalSeconds == 0)
                    {
                        RemainingPhaseTime.CopyFrom(attachedTabata.WorkLength);
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
            RemainingTime.CopyFrom(attachedTabata.CompleteLength);
            RemainingPhaseTime.CopyFrom(attachedTabata.PreparationLength);
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
            if (!IsPlaying)
                playTimer.Start();
        }
        public void Pause()
        {
            if (IsPlaying)
                playTimer.Stop();
        }
    }
}
