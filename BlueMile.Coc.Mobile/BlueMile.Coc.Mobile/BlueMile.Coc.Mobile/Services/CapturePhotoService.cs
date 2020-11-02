using Acr.UserDialogs;
using BlueMile.Coc.Mobile.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Threading.Tasks;

namespace BlueMile.Coc.Mobile.Services
{
    public static class CapturePhotoService
    {
        public static async Task<ImageModel> CapturePhotoAsync(string photoName)
        {
            try
            {
                if (CrossMedia.IsSupported)
                {
                    StoreCameraMediaOptions cameraOptions = new StoreCameraMediaOptions
                    {
                        DefaultCamera = CameraDevice.Rear,
                        SaveToAlbum = true,
                        PhotoSize = PhotoSize.Full,
                        Directory = "Auto360",
                        Name = photoName + ".jpg",
                        AllowCropping = false,
                        CompressionQuality = 100,
                    };
                    var image = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()).ConfigureAwait(false);
                    return new ImageModel
                    {
                        FilePath = image.Path,
                        FileName = photoName + ".jpg"
                    };
                }
                else
                {
                    await UserDialogs.Instance.AlertAsync("Photo capturing not suppoerted.").ConfigureAwait(false);
                    return null;
                }
            }
            catch (Exception exc)
            {
                await UserDialogs.Instance.AlertAsync(exc.ToString(), exc.Message).ConfigureAwait(false);
                return null;
            }
        }
    }
}
