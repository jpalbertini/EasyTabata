using EasyTabata.Models;
using EasyTabata.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EasyTabata.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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
            if (!DependencyService.Get<IDataStore>().UpdateTabataAsync(CurrentTabata).Result)
                Debug.Fail("Could not update");
            else
                await Navigation.PopAsync();
        }
    }
}