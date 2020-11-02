
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlueMile.Coc.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateUpdateBoatPage : ContentPage
    {
        public CreateUpdateBoatPage()
        {
            this.InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
        }
    }
}