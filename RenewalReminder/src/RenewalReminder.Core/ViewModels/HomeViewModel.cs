using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace RenewalReminder.Core.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        #region Instance Properties

        private IMvxNavigationService NavigationService;

        #endregion
        
        #region Constructor

        public HomeViewModel(IMvxNavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        #endregion

        #region Instance Methods

        #endregion

        #region Instance Fields

        #endregion
    }
}
