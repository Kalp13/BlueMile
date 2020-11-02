using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace RenewalReminder.Core.ViewModels
{
    public class SettingsViewModel : MvxViewModel
    {
        #region Instance Properties

        private IMvxNavigationService NavigationService;

        #endregion


        #region Constructor

        public SettingsViewModel(IMvxNavigationService navigationService)
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
