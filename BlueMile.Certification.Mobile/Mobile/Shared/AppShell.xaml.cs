using BlueMile.Certification.Mobile.ViewModels;
using BlueMile.Certification.Mobile.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BlueMile.Certification.Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(OwnerPage), typeof(OwnerPage));
            Routing.RegisterRoute(nameof(BoatsPage), typeof(BoatsPage));
            Routing.RegisterRoute(nameof(RequiredItemsPage), typeof(RequiredItemsPage));
            Routing.RegisterRoute(nameof(BoatDetailPage), typeof(BoatDetailPage));
            Routing.RegisterRoute(nameof(RegisterUserPage), typeof(RegisterUserPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
