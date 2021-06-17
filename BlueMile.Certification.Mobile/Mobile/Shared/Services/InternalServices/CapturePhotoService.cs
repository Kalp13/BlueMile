using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace BlueMile.Certification.Mobile.Services.InternalServices
{
    public class CapturePhotoService
    {
        public static async Task<DocumentMobileModel> CapturePhotoAsync(string photoName)
        {
            try
            {
                if (CrossMedia.IsSupported)
                {
                    var options = new MediaPickerOptions()
                    {
                        Title = "Capture " + photoName,
                    };
                    var image = await MediaPicker.CapturePhotoAsync(options);
                    return new DocumentMobileModel
                    {
                        FilePath = image.FullPath,
                        FileName = photoName + ".jpg",
                        Id = Guid.NewGuid(),
                        FileType = image.ContentType,
                    };
                }
                else
                {
                    await UserDialogs.Instance.AlertAsync("Photo capturing not suppoerted.");
                    return null;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await UserDialogs.Instance.AlertAsync($"You device does not support taking images: {fnsEx.Message}");
                return null;
            }
            catch (PermissionException pEx)
            {
                await UserDialogs.Instance.AlertAsync($"You have not granter the necessary permission for taking images: {pEx.Message}");
                return null;
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.ToString(), exc.Message);
                return null;
            }
        }
    }
}
