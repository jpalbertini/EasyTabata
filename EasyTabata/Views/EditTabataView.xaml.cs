using EasyTabata.Models;
using EasyTabata.Services;
using System.Diagnostics;

namespace EasyTabata.Views;
public partial class EditTabataView : ContentPage
{
    public Tabata CurrentTabata { get; }

    public Duration TemporaryLength
    {
        get { return (Duration)GetValue(TemporaryLengthProperty); }
        set { SetValue(TemporaryLengthProperty, value); }
    }
    public static readonly BindableProperty TemporaryLengthProperty = BindableProperty.Create("TemporaryLength", typeof(Duration), typeof(EditTabataView));

    public EditTabataView(Tabata tabata)
    {
        CurrentTabata = tabata.Clone();
        TemporaryLength = CurrentTabata.CompleteLength;

        CurrentTabata.PropertyChanged += CurrentTabata_PropertyChanged;

        BindingContext = CurrentTabata;
        InitializeComponent();
    }

    private void CurrentTabata_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "CompleteLength")
            TemporaryLength = (sender as Tabata).CompleteLength;
    }

    private async void Save_Clicked(object sender, EventArgs e)
    {
        bool result = false;
        if (CurrentTabata.ID == Guid.Empty)
            result = DependencyService.Get<IDataStore>().AddTabataAsync(CurrentTabata).Result;
        else
            result = DependencyService.Get<IDataStore>().UpdateTabataAsync(CurrentTabata).Result;

        if (result)
            await Navigation.PopAsync();
        else
            Debug.Fail("Could not update");
    }
}