using System;
using Xamarin.Forms;
using BlueMile.Coc.Mobile.Services;
using Acr.UserDialogs;

namespace BlueMile.Coc.Mobile
{
    public partial class App : Application
    {
        public static IServiceCommunication ApiService { get; set; }

        public static ISqlDataService DataService { get; set; }

        public static Guid OwnerId { get; set; }
        
        public App()
        {
            this.InitializeComponent();

            DataService = new SqlDataService();

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
