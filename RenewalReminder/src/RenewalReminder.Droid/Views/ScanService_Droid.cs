using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RenewalReminder.Core.Services;
using RenewalReminder.Droid.Views;
using ZXing.Mobile;

namespace RenewalReminder.Droid.Views
{
    public class ScanService_Droid : IScanService
    {
        private Android.Widget.Button toggleFlashButton;
        private Android.Widget.TextView instructionView;

        private MobileBarcodeScanner BarcodeScanner { get; set; }
        private MobileBarcodeScanningOptions ScanningOptions { get; set; }
        public ScanService_Droid()
        {

            this.BarcodeScanner = new MobileBarcodeScanner()
            {
                UseCustomOverlay = true,
            };
            var myCustomLayout = LayoutInflater.FromContext(Android.App.Application.Context).Inflate(Resource.Layout.ScannerLayout, null);
            this.BarcodeScanner.CustomOverlay = myCustomLayout;
            instructionView = myCustomLayout.FindViewById<TextView>(Resource.Id.instructionView);
            instructionView.SetTextColor(Android.Graphics.Color.WhiteSmoke);
            instructionView.SetTextSize(Android.Util.ComplexUnitType.Pt, 10);
            myCustomLayout.Clickable = true;
            myCustomLayout.Click += MyCustomLayout_Click;
            toggleFlashButton = myCustomLayout.FindViewById<global::Android.Widget.Button>(Resource.Id.toggleFlashButton);
            toggleFlashButton.Click += ToggleFlashButtonTapped;

            ScanningOptions = new MobileBarcodeScanningOptions()
            {
                AutoRotate = false,
                DisableAutofocus = false,
                CameraResolutionSelector = HandleCameraResolutionSelectorDelegate,
                DelayBetweenAnalyzingFrames = 500,
                InitialDelayBeforeAnalyzingFrames = 1000,
                TryHarder = true,
                UseCode39ExtendedMode = true,
                UseNativeScanning = true,
                TryInverted = true,
            };
        }

        private void MyCustomLayout_Click(object sender, EventArgs e)
        {
            this.BarcodeScanner.PauseAnalysis();
            this.BarcodeScanner.AutoFocus();
            this.BarcodeScanner.ResumeAnalysis();
        }

        private void ToggleFlashButtonTapped(object sender, EventArgs e)
        {
            this.BarcodeScanner.ToggleTorch();
        }

        private void AutofocusButton_Click(object sender, EventArgs e)
        {
            this.BarcodeScanner.AutoFocus();
        }

        public async Task<string> ScanBarcodeAsync(string barcodeName)
        {
            try
            {
                instructionView.Text = String.Format(CultureInfo.InvariantCulture, "Please scan the barcode " + barcodeName);
                ZXing.Result result = await this.BarcodeScanner.Scan(this.ScanningOptions);

                if (result != null)
                {
                    return result.Text;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private static CameraResolution HandleCameraResolutionSelectorDelegate(List<CameraResolution> availableResolutions)
        {
            if ((availableResolutions == null) || (availableResolutions.Count < 1))
            {
                return new CameraResolution()
                {
                    Width = 800,
                    Height = 600,
                };
            }

            if (availableResolutions.FirstOrDefault().Width > availableResolutions.LastOrDefault().Width)
            {
                return availableResolutions.FirstOrDefault();
            }
            else
            {
                return availableResolutions.LastOrDefault();
            }
        }
    }
}
