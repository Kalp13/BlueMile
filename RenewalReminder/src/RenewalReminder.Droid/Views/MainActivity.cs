
using Acr.UserDialogs;
using Android.App;
using Android.OS;
using Android.Runtime;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Views;
using MvvmCross.Platforms.Android;
using Plugin.Permissions;
using RenewalReminder.Core.Services;
using RenewalReminder.Core.ViewModels.Main;
using RenewalReminder.Droid.Views;
using ZXing.Mobile;

namespace RenewalReminder.Droid
{
    [Activity(
        Theme = "@style/AppTheme")]
    public class MainActivity : MvxFormsAppCompatActivity<MainViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            MobileBarcodeScanner.Initialize(this.Application);
            UserDialogs.Init(() => Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity);
            Mvx.IoCProvider.RegisterType<IScanService, ScanService_Droid>();

            base.OnCreate(bundle);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
