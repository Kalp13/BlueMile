using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using RenewalReminder.Core.Models;
using RenewalReminder.Core.Services;
using Xamarin.Forms;

namespace RenewalReminder.Core.ViewModels
{
    public class ListViewModel : MvxViewModel
    {
        #region Instance Properties

        private IMvxNavigationService NavigationService;

        public MvxObservableCollection<RenewalModel> RenewalItems
        {
            get
            {
                return this.renewalItems;
            }
            set
            {
                SetProperty(ref this.renewalItems, value);
            }
        }

        public IMvxAsyncCommand AddItemCommand
        {
            get;
            private set;
        }
        
        #endregion

        #region Constructor

        public ListViewModel(IMvxNavigationService navigationService)
        {
            NavigationService = navigationService;

            this.InitCommands();
        }

        #endregion

        #region Instance Methods

        private void InitCommands()
        {
            this.AddItemCommand = new MvxAsyncCommand(() => this.AddNewRenewalItemAsync());
        }

        private async Task AddNewRenewalItemAsync()
        {
            try
            {
                var choice = await UserDialogs.Instance.ActionSheetAsync("Add Item", "Cancel", "Delete", null, new string[] { "Scan Barcode", "Manually Enter Data" });
                switch (choice)
                {
                    case ("Scan Barcode"):
                        if (Mvx.IoCProvider.CanResolve<IScanService>())
                        {
                            UserDialogs.Instance.ShowLoading("Scanning...");
                            Mvx.IoCProvider.TryResolve<IScanService>(out IScanService scanner);
                            var barcode = await scanner.ScanBarcodeAsync("License Disk");
                            UserDialogs.Instance.HideLoading();
                            await this.SaveLicensdeDisk(barcode);
                        }
                        break;
                    case ("Manually Enter Data"):
                        break;
                    default: await UserDialogs.Instance.AlertAsync("Invalid choice - " + choice);
                        break;
                };
            }
            catch (Exception exc)
            {
                UserDialogs.Instance.Alert(exc.Message, "Add Error");
            }
        }

        private async Task SaveLicensdeDisk(string barcode)
        {
            var barcodeValues = barcode.Trim('%').Split('%');
            var name = await UserDialogs.Instance.PromptAsync(new PromptConfig()
            {
                CancelText = "Cancel",
                InputType = InputType.Name,
                IsCancellable = true,
                Message = "What would you like to name this item?",
                OkText = "Save",
                Placeholder = "Name",
                Title = "Item Name"
            });
            this.RenewalItems.Add(new RenewalModel()
            {
                CreatedOn = DateTime.Now,
                ExpiryDate = DateTime.Now.AddYears(1),
                Name = name.Text
            });
        }

        #endregion

        #region Instance Fields

        private MvxObservableCollection<RenewalModel> renewalItems;

        #endregion
    }
}
