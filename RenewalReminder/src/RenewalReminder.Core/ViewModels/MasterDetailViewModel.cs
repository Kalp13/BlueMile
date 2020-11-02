using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace RenewalReminder.Core.ViewModels
{
    public class MasterDetailViewModel : MvxViewModel
    {
        #region Instance Properties

        private IMvxNavigationService NavigationService;

        #endregion

        #region Constructor

        public MasterDetailViewModel(IMvxNavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        #endregion

        #region Class Methods
        
        public override async void ViewAppearing()
        {
            base.ViewAppearing();
            await NavigationService.Navigate<MenuViewModel>();
            await NavigationService.Navigate<HomeViewModel>();
        }

        #endregion

        #region Instance Mthods

        #endregion

        #region Instance Fields

        #endregion
    }
}
