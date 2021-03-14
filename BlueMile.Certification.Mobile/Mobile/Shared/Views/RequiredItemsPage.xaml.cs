
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlueMile.Coc.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RequiredItemsPage : ContentPage
    {
        public RequiredItemsPage()
        {
            this.InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
        }
    }
}