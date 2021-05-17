using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Services.ExternalServices;
using BlueMile.Certification.Mobile.Services.InternalServices;
using System;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using BlueMile.Certification.Mobile.Views;

namespace BlueMile.Certification.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();

            SettingsService.ServiceAddress = @"https://192.168.1.85:5001/api";

            AppCenter.Start("android=b2d64ad6-2d95-4ab9-b1c9-434e3d0ed08f;",
                  typeof(Analytics), typeof(Crashes));

            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                MainPage = new LoginPage();
            });

            ToastConfig.DefaultActionTextColor = Color.FromHex("#FFFFFF");
            ToastConfig.DefaultBackgroundColor = Color.FromHex("#002EB0");
            ToastConfig.DefaultMessageTextColor = Color.FromHex("#FFFFFF");
            ToastConfig.DefaultDuration = TimeSpan.FromSeconds(2);
            ToastConfig.DefaultPosition = ToastPosition.Bottom;

            IDataService dataService = new DataService();
        }

        protected override void OnStart()
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                MainPage = new LoginPage();
            });
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
