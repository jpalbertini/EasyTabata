using EasyTabata.ViewModels;

namespace EasyTabata.Views;
public partial class SettingsView : ContentPage
{
    SettingsViewModel viewModel;
    public SettingsView()
    {
        BindingContext = viewModel = new SettingsViewModel();
        InitializeComponent();
    }
}