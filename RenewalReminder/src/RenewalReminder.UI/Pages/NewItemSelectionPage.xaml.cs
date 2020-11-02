using MvvmCross.Forms.Views;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms.Xaml;
using RenewalReminder.Core.ViewModels;

namespace RenewalReminder.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxModalPresentation(NoHistory = true, Title = "Add/Edit", Animated = true)]
    public partial class NewItemSelectionPage : MvxContentPage<NewItemSelectionViewModel>
    {
        public NewItemSelectionPage()
        {
            InitializeComponent();
        }
    }
}
