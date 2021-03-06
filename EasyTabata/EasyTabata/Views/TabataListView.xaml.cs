﻿using EasyTabata.Models;
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

        private async void Settings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsView());
        }

        public void OnAppearing(object sender, EventArgs e)
        {
            viewModel.updateItems();

        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var tabata = e.SelectedItem as Tabata;
            var secondPage = new PlayingTabataView(tabata);
            await Navigation.PushAsync(secondPage);
        }
    }
}
