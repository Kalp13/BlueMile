
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlueMile.Certification.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewRequiredItemPage : ContentPage
    {
        public NewRequiredItemPage()
        {
            this.InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
        }
    }
}