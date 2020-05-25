using EasyTabata.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyTabata.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayingTabataView : ContentPage
    {
        public CurrentPlayingViewModel playingState = null;
        public CurrentPlayingViewModel.State CurrentState
        {
            get { return (CurrentPlayingViewModel.State)GetValue(CurrentStateProperty); }
            set
            {
                SetValue(CurrentStateProperty, value);
                Console.WriteLine($"Setting state to {value}");
            }
        }
        public static readonly BindableProperty CurrentStateProperty = BindableProperty.Create("CurrentState", typeof(CurrentPlayingViewModel.State), typeof(PlayingTabataView));

        public int RemainingTime
        {
            get { return (int)GetValue(CurrentTimeProperty); }
            set
            {
                SetValue(CurrentTimeProperty, value);
                Console.WriteLine($"Setting time to {value}");
            }
        }
        public static readonly BindableProperty CurrentTimeProperty = BindableProperty.Create("RemainingTime", typeof(int), typeof(PlayingTabataView));

        public int CurrentRound
        {
            get { return (int)GetValue(CurrentRoundProperty); }
            set { SetValue(CurrentRoundProperty, value); }
        }
        public static readonly BindableProperty CurrentRoundProperty = BindableProperty.Create("CurrentRound", typeof(int), typeof(PlayingTabataView));

        public int CurrentExercise
        {
            get { return (int)GetValue(CurrentExerciseProperty); }
            set { SetValue(CurrentExerciseProperty, value); }
        }
        public static readonly BindableProperty CurrentExerciseProperty = BindableProperty.Create("CurrentExercise", typeof(int), typeof(PlayingTabataView));

        public PlayingTabataView()
        {
            BindingContext = this;
            InitializeComponent();
        }
        public PlayingTabataView(Tabata attached)
        {
            BindingContext = this;
            InitializeComponent();
            playingState = new CurrentPlayingViewModel(attached);
            playingState.PropertyChanged += onTabataPropertyChanged;

            Sync();
        }

        void onTabataPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            Sync();
        }

        private void Play_Clicked(object sender, EventArgs e)
        {
            playingState.Start();
        }

        private void Stop_Clicked(object sender, EventArgs e)
        {
            playingState.Pause();
        }

        private void Reset_Clicked(object sender, EventArgs e)
        {
            playingState.Reset();
        }

        private void Sync()
        {
            RemainingTime = playingState.RemainingTime.TotalSeconds;
            CurrentState = playingState.CurrentState;
            CurrentRound = playingState.CurrentRoundNumber;
            CurrentExercise = playingState.CurrentExerciseNumber;
        }
    }
}