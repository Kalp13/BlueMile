using MvvmCross.Forms.Views;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms.Xaml;
using RenewalReminder.Core.ViewModels;

namespace RenewalReminder.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, NoHistory = true, Title = "Notifications", Animated = true)]
    public partial class NotificationsPage : MvxContentPage<NotificationsViewModel>
    {
        public NotificationsPage()
        {
            InitializeComponent();
        }
    }
}
