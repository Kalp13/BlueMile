using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Services;
using BlueMile.Certification.Mobile.Services.ExternalServices;
using BlueMile.Certification.Mobile.Services.InternalServices;
using BlueMile.Certification.Mobile.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlueMile.Certification.Mobile
{
    public partial class App : Application
    {
        public static IServiceCommunication ApiService { get; set; }

        public static IDataService DataService { get; set; }

        public static Guid OwnerId { get; set; }

        public App()
        {
            InitializeComponent();

            DataService = new DataService();

            ApiService = new ServiceCommunication();

            SettingsService.ServiceAddress = @"https://192.168.1.86:55999/BMCocApi";

            MainPage = new AppShell();

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
