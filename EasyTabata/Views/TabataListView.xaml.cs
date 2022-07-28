using EasyTabata.Models;
using EasyTabata.ViewModels;

namespace EasyTabata.Views;
public partial class TabataListView : ContentPage
{
    TabataListViewModel viewModel;

    public TabataListView()
    {
        BindingContext = viewModel = new TabataListViewModel();
        Appearing += OnAppearing;

        InitializeComponent();
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

    private async void Add_Clicked(object sender, EventArgs e)
    {
        var newTabata = viewModel.AddTabata();
        var secondPage = new EditTabataView(newTabata);
        await Navigation.PushAsync(secondPage);
    }
}