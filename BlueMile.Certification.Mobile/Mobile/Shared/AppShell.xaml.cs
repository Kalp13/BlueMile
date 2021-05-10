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
            //Owner Pages
            Routing.RegisterRoute(nameof(OwnerPage), typeof(OwnerPage));

            //Boat Pages
            Routing.RegisterRoute(nameof(BoatsPage), typeof(BoatsPage));
            Routing.RegisterRoute(nameof(BoatDetailPage), typeof(BoatDetailPage));
            Routing.RegisterRoute(nameof(CreateUpdateBoatPage), typeof(CreateUpdateBoatPage));

            //Item Pages
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(RequiredItemsPage), typeof(RequiredItemsPage));
            Routing.RegisterRoute(nameof(EditRequiredItemPage), typeof(EditRequiredItemPage));

            //User Pages
            Routing.RegisterRoute(nameof(RegisterUserPage), typeof(RegisterUserPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        }
    }
}
