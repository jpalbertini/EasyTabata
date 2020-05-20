using EasyTabata.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyTabata.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsView : ContentPage
    {
        SettingsViewModel viewModel;
        public SettingsView()
        {
            InitializeComponent();
            BindingContext = viewModel = new SettingsViewModel();
        }
    }
}