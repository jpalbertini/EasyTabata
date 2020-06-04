using Xamarin.Forms;
using EasyTabata.Views;
using EasyTabata.Services;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

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
            AppCenter.Start("android=9d12b6bd-1aad-49f6-b720-254f959b3c38;" + "uwp=61e0de10-0c19-48e6-8959-43458c05bbed;" + "ios=ecbf269e-c12d-4ce2-ad4d-98d8389566fa", typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
