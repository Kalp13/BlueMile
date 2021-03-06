﻿using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlueMile.Certification.Mobile.Services.InternalServices
{
    public class CapturePhotoService
    {
        public static async Task<ImageMobileModel> CapturePhotoAsync(string photoName)
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
                    return new ImageMobileModel
                    {
                        FilePath = image.Path,
                        ImageName = photoName + ".jpg"
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
