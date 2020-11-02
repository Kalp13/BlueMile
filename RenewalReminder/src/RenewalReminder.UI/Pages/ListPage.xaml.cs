using MvvmCross.Forms.Views;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms.Xaml;
using RenewalReminder.Core.ViewModels;

namespace RenewalReminder.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, NoHistory = true, Title = "Renewals Loaded", Animated = true)]
    public partial class ListPage : MvxContentPage<ListViewModel>
    {
        public ListPage()
        {
            InitializeComponent();
        }
    }
}
