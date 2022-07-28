namespace EasyTabata;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        DependencyService.Register<Services.MockDataStore>();

        MainPage = new Views.TabataListView();
    }
}
