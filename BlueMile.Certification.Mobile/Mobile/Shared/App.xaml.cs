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
        public static IServiceCommunication ApiService { get; set; }

        public static IDataService DataService { get; set; }

        public static Guid OwnerId { get; set; }

        public App()
        {
            this.InitializeComponent();

            DataService = new DataService();

            ApiService = new ServiceCommunication();

            SettingsService.ServiceAddress = @"https://192.168.1.85:5001/api";

            AppCenter.Start("android=b2d64ad6-2d95-4ab9-b1c9-434e3d0ed08f;",
                  typeof(Analytics), typeof(Crashes));

            MainPage = new LoginPage();

            ToastConfig.DefaultActionTextColor = Color.FromHex("#FFFFFF");
            ToastConfig.DefaultBackgroundColor = Color.FromHex("#002EB0");
            ToastConfig.DefaultMessageTextColor = Color.FromHex("#FFFFFF");
            ToastConfig.DefaultDuration = TimeSpan.FromSeconds(4);
            ToastConfig.DefaultPosition = ToastPosition.Bottom;
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
