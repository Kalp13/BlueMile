using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Helpers;
using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Mobile.Services.InternalServices;
using BlueMile.Certification.Web.ApiClient;
using BlueMile.Certification.Web.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace BlueMile.Certification.Mobile.Services.ExternalServices
{
    public class ServiceCommunication : IServiceCommunication
    {
        #region Instance Properties

        private ICertificationApiClient client;

        #endregion

        #region Constructor

        public ServiceCommunication()
        {
            client = CreateClient();
        }

        #endregion

        #region Login Methods

        /// <inheritdoc/>
        public async Task<bool> RegisterUser(UserRegistrationModel userModel, OwnerMobileModel ownerModel)
        {
            try
            {
                if (this.client == null)
                {
                    this.client = this.CreateClient();
                }

                if (!await this.HasInternetConnectionAsync())
                {
                    throw new WebException("No Internet Connection");
                }

                var ownerId = await this.client.CreateOwner(OwnerModelHelper.ToOwnerModel(ownerModel)).ConfigureAwait(false);
                userModel.OwnerId = ownerId;

                var result = await this.client.RegisterUser(userModel).ConfigureAwait(false);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<UserToken> LogUserIn(UserLoginModel userModel)
        {
            try
            {
                if (this.client == null)
                {
                    this.client = this.CreateClient();
                }

                if (!await this.HasInternetConnectionAsync())
                {
                    return null;
                }

                var token = await this.client.LogUserIn(userModel).ConfigureAwait(false);

                return token;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Owner Methods

        /// <inheritdoc/>
        public async Task<OwnerMobileModel> GetOwnerBySystemId(Guid ownerId)
        {
            try
            {
                if (ownerId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(ownerId));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }

                if (!await this.HasInternetConnectionAsync())
                {
                    throw new WebException("No Internet Connection");
                }

                var result = await this.client.GetOwnerBySystemId(ownerId);
                return OwnerModelHelper.ToOwnerMobileModel(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<OwnerMobileModel> GetOwnerByUsername(string username)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(username))
                {
                    throw new ArgumentNullException(nameof(username));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }

                if (!await this.HasInternetConnectionAsync())
                {
                    throw new WebException("No Internet Connection");
                }

                var result = await this.client.GetOwnerByUsername(username).ConfigureAwait(false);
                return OwnerModelHelper.ToOwnerMobileModel(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Guid> CreateOwner(OwnerMobileModel owner)
        {
            try
            {
                if (owner == null)
                {
                    throw new ArgumentNullException(nameof(owner));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }

                if (!await this.HasInternetConnectionAsync())
                {
                    return Guid.Empty;
                }

                var result = await this.client.CreateOwner(OwnerModelHelper.ToOwnerModel(owner)).ConfigureAwait(false);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Guid> UpdateOwner(OwnerMobileModel owner)
        {
            try
            {
                if (owner == null)
                {
                    throw new ArgumentNullException(nameof(owner));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }

                if (!await this.HasInternetConnectionAsync())
                {
                    return Guid.Empty;
                }

                var result = await this.client.UpdateOwner(OwnerModelHelper.ToOwnerModel(owner)).ConfigureAwait(false);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Boat Methods

        /// <inheritdoc/>
        public async Task<List<BoatMobileModel>> GetBoatsByOwnerId(Guid ownerId)
        {
            try
            {
                if (ownerId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(ownerId));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }

                if (!await this.HasInternetConnectionAsync())
                {
                    throw new WebException("No Internet Connection");
                }

                var result = await this.client.GetBoatsByOwnerId(ownerId).ConfigureAwait(false);
                return result.Select(x => BoatModelHelper.ToMobileModel(x)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<BoatMobileModel> GetBoatById(Guid boatId)
        {
            try
            {
                if (boatId == null || boatId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(boatId));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }

                if (!await this.HasInternetConnectionAsync())
                {
                    throw new WebException("No Internet Connection");
                }

                var result = await this.client.GetBoatById(boatId).ConfigureAwait(false);
                return result != null ? BoatModelHelper.ToMobileModel(result) : null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Guid> CreateBoat(BoatMobileModel boat)
        {
            try
            {
                if (boat == null)
                {
                    throw new ArgumentNullException(nameof(boat));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }

                if (!await this.HasInternetConnectionAsync())
                {
                    return Guid.Empty;
                }

                var result = await this.client.CreateBoat(BoatModelHelper.ToModel(boat));
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Guid> UpdateBoat(BoatMobileModel boat)
        {
            try
            {
                if (boat == null)
                {
                    throw new ArgumentNullException(nameof(boat));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }

                if (!await this.HasInternetConnectionAsync())
                {
                    return Guid.Empty;
                }

                var result = await this.client.UpdateBoat(BoatModelHelper.ToModel(boat)).ConfigureAwait(false);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Item Methods

        /// <inheritdoc/>
        public async Task<List<ItemMobileModel>> GetBoatRequiredItems(Guid boatId)
        {
            var items = new List<ItemMobileModel>();
            try
            {
                if (boatId == null || boatId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(boatId));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }

                if (!await this.HasInternetConnectionAsync())
                {
                    throw new WebException("No Internet Connection");
                }

                var result = await this.client.GetBoatRequiredItems(boatId).ConfigureAwait(false);
                return result.Select(x => ItemModelHelper.ToItemMobileModel(x)).ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return items;
        }

        /// <inheritdoc/>
        public async Task<Guid> CreateItem(ItemMobileModel item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException(nameof(item));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }

                if (!await this.HasInternetConnectionAsync())
                {
                    return Guid.Empty;
                }

                var result = await this.client.CreateItem(ItemModelHelper.ToItemModel(item));
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Guid> UpdateItem(ItemMobileModel item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException(nameof(item));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }

                if (!await this.HasInternetConnectionAsync())
                {
                    return Guid.Empty;
                }

                var result = await this.client.UpdateItem(ItemModelHelper.ToItemModel(item));
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Image Methods

        //public async Task<ImageEntity> GetImageById(Guid imageId)
        //{
        //    try
        //    {
        //        Uri uri = new Uri(String.Format(CultureInfo.InvariantCulture, SettingsService.ItemServiceAddress, String.Empty));
        //        HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        //            return JsonConvert.DeserializeObject<ImageEntity>(content);
        //        }
        //        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            await UserDialogs.Instance.AlertAsync("Could not get image with id - " + imageId, "Image Error").ConfigureAwait(false);
        //            return null;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public async Task<bool> CreateImage(ImageMobileModel image)
        //{
        //    try
        //    {
        //        if (image == null)
        //        {
        //            throw new ArgumentNullException(nameof(image));
        //        }

        //        if (client == null)
        //        {
        //            client = CreateClient();
        //        }

        //        var convertedImage = new ImageEntity()
        //        {
        //            Id = image.Id,
        //            ImageData = File.ReadAllBytes(image.FilePath),
        //            ImageName = image.FileName,
        //            ImagePath = image.FilePath,
        //            UniqueFileName = image.UniqueFileName
        //        };

        //        string json = JsonConvert.SerializeObject(convertedImage);

        //        using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
        //        {
        //            HttpResponseMessage response = null;

        //            Uri uri = new Uri($"{SettingsService.ImageServiceAddress}/create");
        //            if (client.BaseAddress == null)
        //            {
        //                client.BaseAddress = new Uri(SettingsService.ServiceAddress);
        //            }

        //            response = await client.PostAsync(uri, content).ConfigureAwait(false);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                return response.IsSuccessStatusCode;
        //            }
        //            else
        //            {
        //                await UserDialogs.Instance.AlertAsync(String.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}", response.StatusCode, response.ReasonPhrase, response.Headers),
        //                    "Save Image Error").ConfigureAwait(false);
        //                return false;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public async Task<bool> UpdateImage(ImageMobileModel image)
        //{
        //    try
        //    {
        //        if (image == null)
        //        {
        //            throw new ArgumentNullException(nameof(image));
        //        }

        //        if (client == null)
        //        {
        //            client = CreateClient();
        //        }

        //        var convertedImage = new ImageEntity()
        //        {
        //            Id = image.Id,
        //            ImageData = File.ReadAllBytes(image.FilePath),
        //            ImageName = image.FileName,
        //            ImagePath = image.FilePath,
        //            UniqueFileName = image.UniqueFileName
        //        };

        //        string json = JsonConvert.SerializeObject(convertedImage);

        //        using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
        //        {
        //            HttpResponseMessage response = null;

        //            Uri uri = new Uri($"{SettingsService.ImageServiceAddress}/update/{image.Id}");
        //            if (client.BaseAddress == null)
        //            {
        //                client.BaseAddress = new Uri(SettingsService.ServiceAddress);
        //            }

        //            response = await client.PutAsync(uri, content).ConfigureAwait(false);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                return response.IsSuccessStatusCode;
        //            }
        //            else
        //            {
        //                await UserDialogs.Instance.AlertAsync(String.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}", response.StatusCode, response.ReasonPhrase, response.Headers),
        //                    "Update Image Error").ConfigureAwait(false);
        //                return false;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        #endregion

        #region Class Methods

        /// <summary>
        /// Validates that the device has an active internet connection.
        /// </summary>
        /// <returns></returns>
        private async Task<bool> HasInternetConnectionAsync()
        {
            try
            {
                if (Connectivity.ConnectionProfiles.Any(x => x == ConnectionProfile.WiFi || x == ConnectionProfile.Cellular))
                {
                    return Connectivity.NetworkAccess == NetworkAccess.ConstrainedInternet || Connectivity.NetworkAccess == NetworkAccess.Internet;
                }
                else
                {
                    await UserDialogs.Instance.AlertAsync("No Connectivity", "Connectivity Error");
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        private CertificationApiClient CreateClient()
        {
            try
            {
                //if (String.IsNullOrWhiteSpace(SettingsService.UserToken))
                //{
                //    throw new ArgumentNullException(nameof(SettingsService.UserToken), "You have not been authenticated. Please log and back in again.");
                //}

                if (String.IsNullOrWhiteSpace(SettingsService.ServiceAddress))
                {
                    throw new ArgumentNullException(nameof(SettingsService.ServiceAddress), "No valid address saved for API. Please contact support.");
                }

                return new CertificationApiClient(SettingsService.ServiceAddress, SettingsService.UserToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
