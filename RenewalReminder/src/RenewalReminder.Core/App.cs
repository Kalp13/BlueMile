using MvvmCross.ViewModels;
using RenewalReminder.Core.Services;
using RenewalReminder.Core.ViewModels;

namespace RenewalReminder.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<MasterDetailViewModel>();
        }
    }
}
