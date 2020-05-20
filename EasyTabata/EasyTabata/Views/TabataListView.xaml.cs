using EasyTabata.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        }
    }
}
