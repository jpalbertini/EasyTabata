using EasyTabata.Models;
using System.ComponentModel;

namespace EasyTabata.Views;
public partial class PlayingTabataView : ContentPage
{
    public CurrentPlayingViewModel playingState = null;
    public CurrentPlayingViewModel.State CurrentState
    {
        get { return (CurrentPlayingViewModel.State)GetValue(CurrentStateProperty); }
        set { SetValue(CurrentStateProperty, value); }
    }
    public static readonly BindableProperty CurrentStateProperty = BindableProperty.Create("CurrentState", typeof(CurrentPlayingViewModel.State), typeof(PlayingTabataView));
    public String RemainingTime
    {
        get { return (String)GetValue(RemainingTimeProperty); }
        set { SetValue(RemainingTimeProperty, value); }
    }
    public static readonly BindableProperty RemainingTimeProperty = BindableProperty.Create("RemainingTime", typeof(String), typeof(PlayingTabataView));

    public int RemainingPhaseTime
    {
        get { return (int)GetValue(RemainingPhaseTimeProperty); }
        set { SetValue(RemainingPhaseTimeProperty, value); }
    }
    public static readonly BindableProperty RemainingPhaseTimeProperty = BindableProperty.Create("RemainingPhaseTime", typeof(int), typeof(PlayingTabataView));

    public int CurrentRound
    {
        get { return (int)GetValue(CurrentRoundProperty); }
        set { SetValue(CurrentRoundProperty, value); }
    }
    public static readonly BindableProperty CurrentRoundProperty = BindableProperty.Create("CurrentRound", typeof(int), typeof(PlayingTabataView));
    public int TotalRound
    {
        get { return (int)GetValue(TotalRoundProperty); }
        set { SetValue(TotalRoundProperty, value); }
    }
    public static readonly BindableProperty TotalRoundProperty = BindableProperty.Create("TotalRound", typeof(int), typeof(PlayingTabataView));

    public int CurrentExercise
    {
        get { return (int)GetValue(CurrentExerciseProperty); }
        set { SetValue(CurrentExerciseProperty, value); }
    }
    public static readonly BindableProperty CurrentExerciseProperty = BindableProperty.Create("CurrentExercise", typeof(int), typeof(PlayingTabataView));
    public int TotalExercise
    {
        get { return (int)GetValue(TotalExerciseProperty); }
        set { SetValue(TotalExerciseProperty, value); }
    }
    public static readonly BindableProperty TotalExerciseProperty = BindableProperty.Create("TotalExercise", typeof(int), typeof(PlayingTabataView));

    public double CurrentProgress
    {
        get { return (double)GetValue(CurrentProgressProperty); }
        set { SetValue(CurrentProgressProperty, value); }
    }
    public static readonly BindableProperty CurrentProgressProperty = BindableProperty.Create("CurrentProgress", typeof(double), typeof(PlayingTabataView));

    public bool IsPlaying
    {
        get { return (bool)GetValue(IsPlayingProperty); }
        set { SetValue(IsPlayingProperty, value); }
    }
    public static readonly BindableProperty IsPlayingProperty = BindableProperty.Create("IsPlaying", typeof(bool), typeof(PlayingTabataView));

    public string BgColor
    {
        get { return (string)GetValue(BgColorProperty); }
        set { SetValue(BgColorProperty, value); }
    }
    public static readonly BindableProperty BgColorProperty = BindableProperty.Create("BgColor", typeof(string), typeof(PlayingTabataView));

    public string ProgressColor
    {
        get { return (string)GetValue(ProgressColorProperty); }
        set { SetValue(ProgressColorProperty, value); }
    }
    public static readonly BindableProperty ProgressColorProperty = BindableProperty.Create("ProgressColor", typeof(string), typeof(PlayingTabataView));

    public string BoxColor
    {
        get { return (string)GetValue(BoxColorProperty); }
        set { SetValue(BoxColorProperty, value); }
    }
    public static readonly BindableProperty BoxColorProperty = BindableProperty.Create("BoxColor", typeof(string), typeof(PlayingTabataView));

    public PlayingTabataView()
    {
        BindingContext = this;
        InitializeComponent();
    }
    public PlayingTabataView(Tabata attached)
    {
        BindingContext = this;
        playingState = new CurrentPlayingViewModel(attached);
        playingState.PropertyChanged += onTabataPropertyChanged;

        Title = "Playing: " + attached.Title;

        Sync();
    }

    void onTabataPropertyChanged(object sender, PropertyChangedEventArgs args)
    {
        Sync();
    }

    private void Play_Clicked(object sender, EventArgs e)
    {
        if (playingState.IsPlaying)
            playingState.Pause();
        else
            playingState.Start();
        Sync();
    }

    private void Reset_Clicked(object sender, EventArgs e)
    {
        playingState.Reset();
    }

    private void Sync()
    {
        RemainingTime = playingState.RemainingTime.ToString();
        RemainingPhaseTime = playingState.RemainingPhaseTime.TotalSeconds;
        CurrentProgress = 1.0f - ((playingState.WholePhaseTime.TotalSeconds - playingState.RemainingPhaseTime.TotalSeconds) / (double)playingState.WholePhaseTime.TotalSeconds);
        CurrentState = playingState.CurrentState;
        TotalRound = playingState.attachedTabata.RoundCount;
        CurrentRound = playingState.CurrentRoundNumber;
        TotalExercise = playingState.attachedTabata.ExerciseCount;
        CurrentExercise = playingState.CurrentExerciseNumber;
        IsPlaying = playingState.IsPlaying;

        BoxColor = "LightGray";
        switch (CurrentState)
        {
            case CurrentPlayingViewModel.State.Preparation:
                BgColor = "Beige";
                ProgressColor = "Red";
                break;
            case CurrentPlayingViewModel.State.Work:
                BgColor = "LightSalmon";
                ProgressColor = "Red";
                break;
            case CurrentPlayingViewModel.State.Rest:
                BgColor = "LightCyan";
                ProgressColor = "Blue";
                break;
            case CurrentPlayingViewModel.State.RestBetweenRound:
                BgColor = "Cyan";
                ProgressColor = "Blue";
                break;
            case CurrentPlayingViewModel.State.Finish:
                BgColor = "LightGreen";
                ProgressColor = "Green";
                break;
            default:
                break;
        }
    }
}