﻿using Xamarin.Forms;
using EasyTabata.Views;
using EasyTabata.Services;

namespace EasyTabata
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);

            DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new TabataListView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
