using BlueMile.Coc.Mobile.Views;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BlueMile.Coc.Mobile
{
    public partial class AppShell : Shell
    {
        Dictionary<string, Type> routes = new Dictionary<string, Type>();

        public Dictionary<string, Type> Routes { get { return routes; } }

        public AppShell()
        {
            this.InitializeComponent();
            this.RegisterRoutes();
            BindingContext = this;
        }

        private void RegisterRoutes()
        {
            routes.Add("boats", typeof(BoatsPage));
            routes.Add("boats/new", typeof(CreateUpdateBoatPage));
            routes.Add("boats/update", typeof(CreateUpdateBoatPage));
            routes.Add("boats/detail", typeof(BoatDetailPage));
            routes.Add("items", typeof(RequiredItemsPage));
            routes.Add("items/new", typeof(NewRequiredItemPage));
            routes.Add("items/detail", typeof(EditRequiredItemPage));
            //routes.Add("items/wizard", typeof())

            foreach (var route in routes)
            {
                Routing.RegisterRoute(route.Key, route.Value);
            }
        }

        void OnNavigating(object sender, ShellNavigatingEventArgs e)
        {
            // Cancel any back navigation
            //if (e.Source == ShellNavigationSource.Pop)
            //{
            //    e.Cancel();
            //}
        }

        void OnNavigated(object sender, ShellNavigatedEventArgs e)
        {
        }
    }
}
