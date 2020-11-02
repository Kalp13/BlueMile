using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using RenewalReminder.Core.Models;

namespace RenewalReminder.Core.ViewModels
{
    public class DetailsViewModel : MvxViewModel
    {
        #region Instance Properties

        private IMvxNavigationService NavigationService;

        public RenewalModel DetailedItem
        {
            get
            {
                return this.detailedItem;
            }
            set
            {
                SetProperty(ref this.detailedItem, value);
            }
        }

        #endregion

        #region Constructor

        public DetailsViewModel(IMvxNavigationService navigationService, long itemId)
        {
            NavigationService = navigationService;
            if (itemId == null || itemId == 0)
            {
                this.DetailedItem = new RenewalModel();
            }
            else
            {
                //Get the selected detail item.
            }
        }

        #endregion

        #region Instance Methods

        #endregion

        #region Instance Fields

        private RenewalModel detailedItem;

        #endregion
    }
}
