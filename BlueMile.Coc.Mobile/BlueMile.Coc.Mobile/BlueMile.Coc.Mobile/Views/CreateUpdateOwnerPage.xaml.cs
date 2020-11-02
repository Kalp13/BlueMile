
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlueMile.Coc.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateUpdateOwnerPage : ContentPage
    {
        public CreateUpdateOwnerPage()
        {
            this.InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
        }
    }
}