using BlueMile.Certification.Mobile.Services;
using BlueMile.Certification.Mobile.Views;
using System;
using Xamarin.Forms;

namespace BlueMile.Certification.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Owner Pages
            Routing.RegisterRoute(Constants.ownersRoute, typeof(OwnersPage));
            Routing.RegisterRoute(Constants.ownerDetailRoute, typeof(OwnerPage));
            Routing.RegisterRoute(Constants.ownerEditRoute, typeof(CreateUpdateOwnerPage));

            //Boat Pages
            Routing.RegisterRoute(Constants.boatsRoute, typeof(BoatsPage));
            Routing.RegisterRoute(Constants.boatDetailRoute, typeof(BoatDetailPage));
            Routing.RegisterRoute(Constants.boatEditRoute, typeof(CreateUpdateBoatPage));

            //Item Pages
            Routing.RegisterRoute(Constants.itemsRoute, typeof(RequiredItemsPage));
            Routing.RegisterRoute(Constants.itemDetailRoute, typeof(ItemDetailPage));
            Routing.RegisterRoute(Constants.itemNewRoute, typeof(NewItemPage));
            Routing.RegisterRoute(Constants.itemEditRoute, typeof(EditRequiredItemPage));

            //User Pages
            Routing.RegisterRoute(nameof(RegisterUserPage), typeof(RegisterUserPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));

            //Extra Routes
            Routing.RegisterRoute(Constants.settingsRoute, typeof(SettingsPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
