using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using RenewalReminder.Core.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RenewalReminder.Core.ViewModels
{
    public class NewItemSelectionViewModel : MvxViewModel
    {
        #region Instance Properties

        private IMvxNavigationService NavigationService;

        public string Barcode
        {
            get
            {
                return this.barcode;
            }
            set
            {
                SetProperty(ref this.barcode, value);
            }
        }

        public IMvxAsyncCommand ScanCommand
        {
            get;
            private set;
        }

        public IMvxAsyncCommand ManualCommand
        {
            get;
            private set;
        }

        #endregion

        #region Constructor

        public NewItemSelectionViewModel(IMvxNavigationService navigationService)
        {
            this.NavigationService = navigationService;

            this.InitCommands();
        }

        #endregion

        #region Instance Methods

        private void InitCommands()
        {
            this.ScanCommand = new MvxAsyncCommand(() => this.ScanBarcode(), () => this.CanUseScanner().Result);
        }

        private async Task ScanBarcode()
        {
            IScanService scanService = DependencyService.Get<IScanService>();
            
            try
            {
                var barcode = await scanService.ScanBarcodeAsync("License Disk");

            }
            catch (OperationCanceledException)
            {
                
            }
            catch (Exception exc)
            {
                
            }
        }

        private Task<bool> CanUseScanner()
        {
            TaskCompletionSource<bool> completionSource = new TaskCompletionSource<bool>();
            completionSource.SetResult(CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera).Result == PermissionStatus.Granted);
            return completionSource.Task;
        }
        #endregion

        #region Instance Fields

        private string barcode;

        #endregion
    }
}
