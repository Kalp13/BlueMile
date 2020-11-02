using Android.App;
using MvvmCross.Forms.Platforms.Android.Core;
using ZXing.Mobile;

#if DEBUG
[assembly: Application(Debuggable = true)]
#else
[assembly: Application(Debuggable = false)]
#endif

namespace RenewalReminder.Droid
{
    public class Setup : MvxFormsAndroidSetup<Core.App, UI.App>
    {
        
    }
}
