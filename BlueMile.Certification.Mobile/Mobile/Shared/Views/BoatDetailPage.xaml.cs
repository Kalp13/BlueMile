
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlueMile.Certification.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoatDetailPage : ContentPage
    {
        public BoatDetailPage()
        {
            this.InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
        }
    }
}