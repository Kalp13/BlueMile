using MvvmCross.Forms.Views;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms.Xaml;
using RenewalReminder.Core.ViewModels;

namespace RenewalReminder.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, NoHistory = true, Title = "Renewal Detail", Animated = true)]
    public partial class DetailsPage : MvxContentPage<DetailsViewModel>
    {
        public DetailsPage()
        {
            InitializeComponent();
        }
    }
}
