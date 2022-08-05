using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Locations.Views;
using Locations.ViewModels;
using Locations.Services;

namespace Locations
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent();

            NavigationPage mainPage = new NavigationPage(new MainPage());

            var navService = DependencyService.Get<INavService>() as XamarinFormsNavService;

            navService.XamarinFormsNav = mainPage.Navigation;

            navService.RegisterViewMapping(typeof(MainViewModel), typeof(MainPage));

            navService.RegisterViewMapping(typeof(DetailViewModel), typeof(DetailPage));

            navService.RegisterViewMapping(typeof(NewLocationViewModel), typeof(NewLocationPage));

            MainPage = mainPage;
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

