using System;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using RenewalReminder.Core.Models;
using Xamarin.Forms;

namespace RenewalReminder.Core.ViewModels
{
    public class MenuViewModel : MvxViewModel
    {
        #region Instance Properties

        public MvxObservableCollection<string> MenuItemList
        {
            get
            {
                return this.menuItemList;
            }
            set
            {
                SetProperty(ref this.menuItemList, value);
            }
        }

        public IMvxAsyncCommand<string> NavigateCommand
        {
            get
            {
                this.navigateCommand = this.navigateCommand ?? new MvxAsyncCommand<string>(ShowDetailsPageAsync);
                return this.navigateCommand;
            }
        }

        #endregion

        private IMvxNavigationService NavigationService;

        #region Constructor

        public MenuViewModel(IMvxNavigationService navigationService)
        {
            NavigationService = navigationService;
            this.MenuItemList = new MvxObservableCollection<string>(Enum.GetNames(typeof(MenuItems)).ToList());
        }

        #endregion

        #region Instance Methods

        private async Task ShowDetailsPageAsync(string param)
        {
            var item = Enum.Parse(typeof(MenuItems), param);
            switch (item)
            {
                case (MenuItems.Home):
                    await this.NavigationService.Navigate<HomeViewModel>();
                    break;
                case (MenuItems.Renewals):
                    await this.NavigationService.Navigate<ListViewModel>();
                    break;
                case (MenuItems.Notifications):
                    await this.NavigationService.Navigate<NotificationsViewModel>();
                    break;
                case (MenuItems.Settings):
                    await this.NavigationService.Navigate<SettingsViewModel>();
                    break;
                default:
                    break;
            }

            if (Application.Current.MainPage is MasterDetailPage masterDetailPage)
            {
                masterDetailPage.IsPresented = false;
            }
            else if (Application.Current.MainPage is NavigationPage navigationPage &&
                        navigationPage.CurrentPage is MasterDetailPage nestedMasterDetailPage)
            {
                nestedMasterDetailPage.IsPresented = false;
            }
        }

        #endregion

        #region Instance Fields

        private MvxObservableCollection<string> menuItemList;

        public IMvxAsyncCommand<string> navigateCommand;

        #endregion
    }
}
