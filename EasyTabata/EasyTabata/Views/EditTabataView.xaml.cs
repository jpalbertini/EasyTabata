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
        public Tabata currentTabata { get; }

        public EditTabataView(Tabata tabata)
        {
            currentTabata = tabata.Clone();

            BindingContext = currentTabata;
            InitializeComponent();
        }
        private async void Save_Clicked(object sender, EventArgs e)
        {
            if (!DependencyService.Get<IDataStore>().UpdateTabataAsync(currentTabata).Result)
                Debug.Fail("Could not update");
            else
                await Navigation.PopAsync();
        }
    }
}