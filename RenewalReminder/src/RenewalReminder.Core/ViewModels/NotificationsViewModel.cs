using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using RenewalReminder.Core.Models;

namespace RenewalReminder.Core.ViewModels
{
    public class NotificationsViewModel : MvxViewModel
    {
        #region Instance Properties

        private IMvxNavigationService NavigationService;

        public MvxObservableCollection<NotificationModel> Notifications
        {
            get
            {
                return this.notifications;
            }
            set
            {
                SetProperty(ref this.notifications, value);
            }
        }

        #endregion

        #region Constructor

        public NotificationsViewModel(IMvxNavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        #endregion

        #region Instance Methods

        #endregion

        #region Instance Fields
        
        private MvxObservableCollection<NotificationModel> notifications;

        #endregion
    }
}
