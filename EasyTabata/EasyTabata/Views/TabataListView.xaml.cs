using EasyTabata.Models;
using EasyTabata.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyTabata.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabataListView : ContentPage
    {
        TabataListViewModel viewModel;

        public TabataListView()
        {
            InitializeComponent();
            BindingContext = viewModel = new TabataListViewModel();

            Appearing += OnAppearing;
        }

        private async void EditionButton_Clicked(object sender, EventArgs e)
        {
            var tabata = (sender as BindableObject).BindingContext as Tabata;
            var secondPage = new EditTabataView(tabata);
            await Navigation.PushAsync(secondPage);
        }

        public void OnAppearing(object sender, EventArgs e)
        {
            viewModel.updateItems();

        }
    }
}
