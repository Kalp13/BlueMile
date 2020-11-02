using Acr.UserDialogs;
using BlueMile.Coc.Data;
using BlueMile.Coc.Mobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BlueMile.Coc.Mobile.Services
{
    public class ServiceCommunication : IServiceCommunication
    {
        #region Instance Properties

        HttpClient client;

        #endregion

        #region Constructor

        public ServiceCommunication()
        {
            client = CreateClient();
        }

        #endregion

        #region Login Methods

        public Task<bool> RegisterUser(UserEntity user)
        {
            throw new NotImplementedException();
        }

        public async Task<UserEntity> LogUserIn(string username, string password)
        {
            try
            {
                var authData = String.Format(CultureInfo.InvariantCulture, "{0}:{1}", username, password);
                var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

                if (client == null)
                {
                    client = CreateClient();
                }

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

                HttpResponseMessage response = null;

                Uri uri = new Uri(String.Format(CultureInfo.InvariantCulture, $"{SettingsService.OwnerServiceAddress}/get/{boatId}", String.Empty));
                if (client.BaseAddress == null)
                {
                    client.BaseAddress = new Uri(SettingsService.ServiceAddress);
                }

                response = await client.GetAsync(uri).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<UserEntity>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                    return result;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    await UserDialogs.Instance.AlertAsync(String.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}", response.StatusCode, response.ReasonPhrase, response.Headers),
                        "Create Boat Error").ConfigureAwait(false);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Owner Methods

        public Task<OwnerEntity> GetOwnerByDetails(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<OwnerEntity> CreateOwner(OwnerModel owner)
        {
            try
            {
                if (owner == null)
                {
                    throw new ArgumentNullException(nameof(owner));
                }

                if (client == null)
                {
                    client = CreateClient();
                }

                var convertedOwner = new OwnerEntity()
                {
                    Address = owner.Address,
                    CellNumber = owner.CellNumber,
                    Email = owner.Email,
                    IcasaPopPhotoId = owner.IcasaPopPhoto.Id,
                    IdentificationDocumentId = owner.IdentificationDocument.Id,
                    IdentificationNumber = owner.IdentificationNumber,
                    Name = owner.Name,
                    Id = owner.Id,
                    SkippersLicenseImageId = owner.SkippersLicenseImage.Id,
                    SkippersLicenseNumber = owner.SkippersLicenseNumber,
                    Surname = owner.Surname,
                    VhfOperatorsLicense = owner.VhfOperatorsLicense,
                    IsSynced = owner.IsSynced
                };
                string json = JsonConvert.SerializeObject(convertedOwner);
                using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = null;

                    Uri uri = new Uri($"{SettingsService.OwnerServiceAddress}/create");
                    if (client.BaseAddress == null)
                    {
                        client.BaseAddress = new Uri(SettingsService.ServiceAddress);
                    }

                    response = await client.PostAsync(uri, content).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<OwnerEntity>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                        return result;
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync(String.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}", response.StatusCode, response.ReasonPhrase, response.Headers), 
                            "Create Owner Error").ConfigureAwait(false);
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                client.CancelPendingRequests();
                client.Dispose();
                throw;
            }
        }

        public async Task<OwnerEntity> UpdateOwner(OwnerModel owner)
        {
            try
            {
                if (owner == null)
                {
                    throw new ArgumentNullException(nameof(owner));
                }

                if (client == null)
                {
                    client = CreateClient();
                }

                var convertedOwner = new OwnerEntity()
                {
                    Address = owner.Address,
                    CellNumber = owner.CellNumber,
                    Email = owner.Email,
                    IcasaPopPhotoId = owner.IcasaPopPhoto.Id,
                    IdentificationDocumentId = owner.IdentificationDocument.Id,
                    IdentificationNumber = owner.IdentificationNumber,
                    Name = owner.Name,
                    Id = owner.Id,
                    SkippersLicenseImageId = owner.SkippersLicenseImage.Id,
                    SkippersLicenseNumber = owner.SkippersLicenseNumber,
                    Surname = owner.Surname,
                    VhfOperatorsLicense = owner.VhfOperatorsLicense,
                    IsSynced = owner.IsSynced
                };
                string json = JsonConvert.SerializeObject(convertedOwner);
                using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = null;

                    Uri uri = new Uri($"{SettingsService.OwnerServiceAddress}/update/{owner.Id}");
                    if (client.BaseAddress == null)
                    {
                        client.BaseAddress = new Uri(SettingsService.ServiceAddress);
                    }

                    response = await client.PostAsync(uri, content).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<OwnerEntity>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                        return result;
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync(String.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}", response.StatusCode, response.ReasonPhrase, response.Headers),
                            "Update Owner Error").ConfigureAwait(false);
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Boat Methods

        public async Task<List<BoatEntity>> GetBoatsByOwnerId(Guid ownerId)
        {
            var boats = new List<BoatEntity>();
            try
            {
                Uri uri = new Uri(String.Format(CultureInfo.InvariantCulture, SettingsService.BoatServiceAddress, String.Empty));
                HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    boats = JsonConvert.DeserializeObject<List<BoatEntity>>(content);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return boats;
        }

        public async Task<BoatEntity> GetBoatById(Guid boatId)
        {
            try
            {
                if (boatId == null || boatId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(boatId));
                }

                if (client == null)
                {
                    client = CreateClient();
                }

                HttpResponseMessage response = null;

                Uri uri = new Uri(String.Format(CultureInfo.InvariantCulture, $"{SettingsService.BoatServiceAddress}/get/{boatId}", String.Empty));
                if (client.BaseAddress == null)
                {
                    client.BaseAddress = new Uri(SettingsService.ServiceAddress);
                }

                response = await client.GetAsync(uri).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<BoatEntity>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                    return result;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    await UserDialogs.Instance.AlertAsync(String.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}", response.StatusCode, response.ReasonPhrase, response.Headers),
                        "Create Boat Error").ConfigureAwait(false);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BoatEntity> CreateBoat(BoatModel boat)
        {
            try
            {
                if (boat == null)
                {
                    throw new ArgumentNullException(nameof(boat));
                }

                if (client == null)
                {
                    client = CreateClient();
                }

                var convertedBoat = new BoatEntity()
                {
                    BoyancyCertificateImageId = boat.BoyancyCertificateImage.Id,
                    BoyancyCertificateNumber = boat.BoyancyCertificateNumber,
                    CategoryId = boat.CategoryId,
                    Id = boat.Id,
                    IsJetski = boat.IsJetski,
                    Name = boat.Name,
                    OwnerId = boat.OwnerId,
                    RegisteredNumber = boat.RegisteredNumber,
                    TubbiesCertificateImageId = boat.TubbiesCertificateImage.Id,
                    TubbiesCertificateNumber = boat.TubbiesCertificateNumber
                };

                string json = JsonConvert.SerializeObject(convertedBoat);

                using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = null;

                    Uri uri = new Uri($"{SettingsService.BoatServiceAddress}/create");
                    if (client.BaseAddress == null)
                    {
                        client.BaseAddress = new Uri(SettingsService.ServiceAddress);
                    }

                    response = await client.PostAsync(uri, content).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<BoatEntity>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                        return result;
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync(String.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}", response.StatusCode, response.ReasonPhrase, response.Headers),
                            "Create Boat Error").ConfigureAwait(false);
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BoatEntity> UpdateBoat(BoatModel boat)
        {
            try
            {
                if (boat == null)
                {
                    throw new ArgumentNullException(nameof(boat));
                }

                if (client == null)
                {
                    client = CreateClient();
                }

                var convertedBoat = new BoatEntity()
                {
                    BoyancyCertificateImageId = boat.BoyancyCertificateImage.Id,
                    BoyancyCertificateNumber = boat.BoyancyCertificateNumber,
                    CategoryId = boat.CategoryId,
                    Id = boat.Id,
                    IsJetski = boat.IsJetski,
                    Name = boat.Name,
                    OwnerId = boat.OwnerId,
                    RegisteredNumber = boat.RegisteredNumber,
                    TubbiesCertificateImageId = boat.TubbiesCertificateImage.Id,
                    TubbiesCertificateNumber = boat.TubbiesCertificateNumber
                };

                string json = JsonConvert.SerializeObject(convertedBoat);

                using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = null;

                    Uri uri = new Uri($"{SettingsService.BoatServiceAddress}/update/{boat.Id}");
                    if (client.BaseAddress == null)
                    {
                        client.BaseAddress = new Uri(SettingsService.ServiceAddress);
                    }

                    response = await client.PutAsync(uri, content).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<BoatEntity>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                        return result;
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync(String.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}", response.StatusCode, response.ReasonPhrase, response.Headers),
                            "Update Boat Error").ConfigureAwait(false);
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Item Methods

        public async Task<List<RequiredItemEntity>> GetBoatRequiredItems(Guid boatId)
        {
            var items = new List<RequiredItemEntity>();
            try
            {
                Uri uri = new Uri(String.Format(CultureInfo.InvariantCulture, SettingsService.ItemServiceAddress, String.Empty));
                HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    items = JsonConvert.DeserializeObject<List<RequiredItemEntity>>(content);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return items;
        }

        public async Task<RequiredItemEntity> CreateItem(RequiredItemModel item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException(nameof(item));
                }

                if (client == null)
                {
                    client = CreateClient();
                }

                var convertedItem = new RequiredItemEntity()
                {
                    BoatId = item.BoatId,
                    CapturedDate = item.CapturedDate,
                    Description = item.Description,
                    ExpiryDate = item.ExpiryDate,
                    Id = item.Id,
                    ItemImageId = item.ItemImage.Id,
                    ItemTypeId = item.ItemTypeId,
                    SerialNumber = item.SerialNumber
                };

                string json = JsonConvert.SerializeObject(convertedItem, Formatting.Indented);

                using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = null;

                    Uri uri = new Uri($"{SettingsService.ItemServiceAddress}/create");
                    if (client.BaseAddress == null)
                    {
                        client.BaseAddress = new Uri(SettingsService.ServiceAddress);
                    }

                    response = await client.PostAsync(uri, content).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<RequiredItemEntity>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                        return result;
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync(String.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}", response.StatusCode, response.ReasonPhrase, response.Headers),
                            "Create Item Error").ConfigureAwait(false);
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RequiredItemEntity> UpdateItem(RequiredItemModel item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException(nameof(item));
                }

                if (client == null)
                {
                    client = CreateClient();
                }

                var convertedItem = new RequiredItemEntity()
                {
                    BoatId = item.BoatId,
                    CapturedDate = item.CapturedDate,
                    Description = item.Description,
                    ExpiryDate = item.ExpiryDate,
                    Id = item.Id,
                    ItemImageId = item.ItemImage.Id,
                    ItemTypeId = item.ItemTypeId,
                    SerialNumber = item.SerialNumber
                };

                string json = JsonConvert.SerializeObject(convertedItem, Formatting.Indented);

                using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = null;

                    Uri uri = new Uri($"{SettingsService.ItemServiceAddress}/update/{item.Id}");
                    if (client.BaseAddress == null)
                    {
                        client.BaseAddress = new Uri(SettingsService.ServiceAddress);
                    }

                    response = await client.PutAsync(uri, content).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<RequiredItemEntity>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                        return result;
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync(String.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}", response.StatusCode, response.ReasonPhrase, response.Headers),
                            "Update Item Error").ConfigureAwait(false);
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Image Methods

        public async Task<ImageEntity> GetImageById(Guid imageId)
        {
            try
            {
                Uri uri = new Uri(String.Format(CultureInfo.InvariantCulture, SettingsService.ItemServiceAddress, String.Empty));
                HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<ImageEntity>(content);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    await UserDialogs.Instance.AlertAsync("Could not get image with id - " + imageId, "Image Error").ConfigureAwait(false);
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CreateImage(ImageModel image)
        {
            try
            {
                if (image == null)
                {
                    throw new ArgumentNullException(nameof(image));
                }

                if (client == null)
                {
                    client = CreateClient();
                }

                var convertedImage = new ImageEntity()
                {
                    Id = image.Id,
                    ImageData = File.ReadAllBytes(image.FilePath),
                    ImageName = image.FileName,
                    ImagePath = image.FilePath,
                    UniqueImageName = image.UniqueImageName
                };

                string json = JsonConvert.SerializeObject(convertedImage);

                using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = null;

                    Uri uri = new Uri($"{SettingsService.ImageServiceAddress}/create");
                    if (client.BaseAddress == null)
                    {
                        client.BaseAddress = new Uri(SettingsService.ServiceAddress);
                    }

                    response = await client.PostAsync(uri, content).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        return response.IsSuccessStatusCode;
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync(String.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}", response.StatusCode, response.ReasonPhrase, response.Headers),
                            "Save Image Error").ConfigureAwait(false);
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateImage(ImageModel image)
        {
            try
            {
                if (image == null)
                {
                    throw new ArgumentNullException(nameof(image));
                }

                if (client == null)
                {
                    client = CreateClient();
                }

                var convertedImage = new ImageEntity()
                {
                    Id = image.Id,
                    ImageData = File.ReadAllBytes(image.FilePath),
                    ImageName = image.FileName,
                    ImagePath = image.FilePath,
                    UniqueImageName = image.UniqueImageName
                };

                string json = JsonConvert.SerializeObject(convertedImage);

                using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = null;

                    Uri uri = new Uri($"{SettingsService.ImageServiceAddress}/update/{image.Id}");
                    if (client.BaseAddress == null)
                    {
                        client.BaseAddress = new Uri(SettingsService.ServiceAddress);
                    }

                    response = await client.PutAsync(uri, content).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        return response.IsSuccessStatusCode;
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync(String.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}", response.StatusCode, response.ReasonPhrase, response.Headers),
                            "Update Image Error").ConfigureAwait(false);
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        private static HttpClient CreateClient()
        {
            //var authData = String.Format(CultureInfo.InvariantCulture, "{0}:{1}", SettingsService.Username, SettingsService.Password);
            //var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            //var _client = new ;

#if DEBUG
            return new HttpClient(DependencyService.Get<IHttpClientHandlerService>().GetInsecureHandler());
            //client = new HttpClient();
#else
            return new HttpClient();
#endif
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

            /*return _client*/;
        }
    }
}
