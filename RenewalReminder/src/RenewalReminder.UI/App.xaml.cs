using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace RenewalReminder.UI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            try
            {
                if (CrossPermissions.IsSupported)
                {
                    var cameraPermission = CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                    if (cameraPermission.Result != PermissionStatus.Granted)
                    {
                        if (CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera).Result)
                        {
                            var result = CrossPermissions.Current.RequestPermissionsAsync(new Permission[] { Permission.Camera });
                        }
                    }
                }
            }
            catch (Exception exc)
            {

            }
        }
    }
}
