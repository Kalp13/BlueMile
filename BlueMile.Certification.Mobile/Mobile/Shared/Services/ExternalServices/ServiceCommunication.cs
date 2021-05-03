using Acr.UserDialogs;
using BlueMile.Certification.Mobile.Data.Models;
using BlueMile.Certification.Mobile.Helpers;
using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Mobile.Services.InternalServices;
using BlueMile.Certification.Web.ApiModels;
using BlueMile.Certification.Web.ApiModels.Helper;
using BlueMile.Certification.Web.ApiModels.Static;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BlueMile.Certification.Mobile.Services.ExternalServices
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

        public async Task<bool> RegisterUser(UserRegistrationModel userModel, OwnerMobileModel ownerModel)
        {
            try
            {
                if (this.client == null)
                {
                    this.client = this.CreateClient();
                }

                var ownerId = await this.CreateOwner(OwnerHelper.ToCreateOwnerModel(OwnerModelHelper.ToOwnerModel(ownerModel)));
                userModel.OwnerId = ownerId;

                var request = new HttpRequestMessage(HttpMethod.Post, $@"{SettingsService.UserServiceAddress}/register");
                request.Content = new StringContent(JsonConvert.SerializeObject(userModel), Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = await this.client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    await UserDialogs.Instance.AlertAsync(response.ReasonPhrase, "Registration Failed", "Ok");
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserToken> LogUserIn(UserLoginModel userModel)
        {
            try
            {
                if (this.client == null)
                {
                    this.client = this.CreateClient();
                }

                var request = new HttpRequestMessage(HttpMethod.Post, $@"{SettingsService.UserServiceAddress}/login");
                request.Content = new StringContent(JsonConvert.SerializeObject(userModel), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<UserToken>(content);
                SettingsService.Username = userModel.EmailAddress;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Token);

                return token;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Owner Methods

        public async Task<OwnerMobileModel> GetOwnerBySystemId(Guid ownerId)
        {
            try
            {
                if (ownerId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(ownerId));
                }

                if (client == null)
                {
                    client = CreateClient();
                }

                Uri uri = new Uri($@"{SettingsService.OwnerServiceAddress}/get/{ownerId}");
                HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var owner = JsonConvert.DeserializeObject<OwnerModel>(content);
                    return OwnerModelHelper.ToOwnerMobileModel(owner);
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OwnerMobileModel> GetOwnerByUsername(string username)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(username))
                {
                    throw new ArgumentNullException(nameof(username));
                }

                if (client == null)
                {
                    client = CreateClient();
                }

                Uri uri = new Uri($@"{SettingsService.OwnerServiceAddress}/get/{username}");
                HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var owner = JsonConvert.DeserializeObject<OwnerModel>(content);
                    return OwnerModelHelper.ToOwnerMobileModel(owner);
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Guid> CreateOwner(CreateOwnerModel owner)
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
                string json = JsonConvert.SerializeObject(owner);
                using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = null;

                    Uri uri = new Uri($@"{SettingsService.OwnerServiceAddress}/create");

                    response = await client.PostAsync(uri, content).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return Guid.Parse(result);
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync(String.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}", response.StatusCode, response.ReasonPhrase, response.Headers),
                            "Create Owner Error").ConfigureAwait(false);
                    }
                    return Guid.Empty;
                }
            }
            catch (Exception)
            {
                client.CancelPendingRequests();
                client.Dispose();
                throw;
            }
        }

        public async Task<Guid> UpdateOwner(UpdateOwnerModel owner)
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

                string json = JsonConvert.SerializeObject(owner);
                using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = null;

                    Uri uri = new Uri($@"{SettingsService.OwnerServiceAddress}/update/{owner.SystemId}");
                    
                    response = await client.PostAsync(uri, content).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return Guid.Parse(result);
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync(String.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}", response.StatusCode, response.ReasonPhrase, response.Headers),
                            "Update Owner Error").ConfigureAwait(false);
                    }
                    return Guid.Empty;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Boat Methods

        public async Task<List<BoatMobileModel>> GetBoatsByOwnerId(Guid ownerId)
        {
            try
            {
                if (ownerId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(ownerId));
                }

                if (client == null)
                {
                    client = CreateClient();
                }

                Uri uri = new Uri($@"{SettingsService.BoatServiceAddress}/{ownerId}");
                HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var boats = JsonConvert.DeserializeObject<List<BoatModel>>(content);
                    return boats.Select(x => BoatModelHelper.ToMobileModel(x)).ToList();
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BoatMobileModel> GetBoatById(Guid boatId)
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

                Uri uri = new Uri($@"{SettingsService.BoatServiceAddress}/get/{boatId}");
                if (client.BaseAddress == null)
                {
                    client.BaseAddress = new Uri(SettingsService.ServiceAddress);
                }

                response = await client.GetAsync(uri).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<BoatModel>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                    return BoatModelHelper.ToMobileModel(result);
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

        public async Task<Guid> CreateBoat(BoatMobileModel boat)
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

                string json = JsonConvert.SerializeObject(BoatHelper.ToCreateBoatModel(BoatModelHelper.ToModel(boat)));

                using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = null;

                    Uri uri = new Uri($@"{SettingsService.BoatServiceAddress}/create");
                    if (client.BaseAddress == null)
                    {
                        client.BaseAddress = new Uri(SettingsService.ServiceAddress);
                    }

                    response = await client.PostAsync(uri, content).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return Guid.Parse(result);
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync($"{response.StatusCode}\n{response.ReasonPhrase}\n{response.Headers}",
                            "Create Boat Error").ConfigureAwait(false);
                    }
                    return Guid.Empty;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Guid> UpdateBoat(BoatMobileModel boat)
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

                string json = JsonConvert.SerializeObject(BoatHelper.ToUpdateBoatModel(BoatModelHelper.ToModel(boat)));

                using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = null;

                    Uri uri = new Uri($@"{SettingsService.BoatServiceAddress}/update/{boat.Id}");
                    if (client.BaseAddress == null)
                    {
                        client.BaseAddress = new Uri(SettingsService.ServiceAddress);
                    }

                    response = await client.PutAsync(uri, content).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return Guid.Parse(result);
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync(String.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}", response.StatusCode, response.ReasonPhrase, response.Headers),
                            "Update Boat Error").ConfigureAwait(false);
                    }
                    return Guid.Empty;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Item Methods

        public async Task<List<ItemMobileModel>> GetBoatRequiredItems(Guid boatId)
        {
            var items = new List<ItemMobileModel>();
            try
            {
                Uri uri = new Uri(String.Format(CultureInfo.InvariantCulture, SettingsService.ItemServiceAddress, String.Empty));
                HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    items = JsonConvert.DeserializeObject<List<ItemMobileModel>>(content);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return items;
        }

        public async Task<Guid> CreateItem(ItemMobileModel item)
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

                string json = JsonConvert.SerializeObject(ItemHelper.ToCreateItemModel(ItemModelHelper.ToItemModel(item)));

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
                        var result = await response.Content.ReadAsStringAsync();
                        return Guid.Parse(result);
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync(String.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}", response.StatusCode, response.ReasonPhrase, response.Headers),
                            "Create Item Error").ConfigureAwait(false);
                    }
                    return Guid.Empty;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Guid> UpdateItem(ItemMobileModel item)
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

                string json = JsonConvert.SerializeObject(ItemHelper.ToUpdateItemModel(ItemModelHelper.ToItemModel(item)));

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
                        var result = await response.Content.ReadAsStringAsync();
                        return Guid.Parse(result);
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync(String.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n{2}", response.StatusCode, response.ReasonPhrase, response.Headers),
                            "Update Item Error").ConfigureAwait(false);
                    }
                    return Guid.Empty;
                }
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
        //            UniqueImageName = image.UniqueImageName
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
        //            UniqueImageName = image.UniqueImageName
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

        private HttpClient CreateClient()
        {
            //var authData = String.Format(CultureInfo.InvariantCulture, "{0}:{1}", SettingsService.Username, SettingsService.Password);
            //var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            //var _client = new ;

            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; },

            };
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            HttpClient client = new HttpClient(httpClientHandler, false);
            client.BaseAddress = new Uri($"{SettingsService.ServiceAddress}");
            client.Timeout = TimeSpan.FromMinutes(30);
            return client;

#if DEBUG
            //return new HttpClient(DependencyService.Get<IHttpClientHandlerService>().GetInsecureHandler());
            //client = new HttpClient();
#else
            return new HttpClient();
#endif
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

            /*return _client*/
            ;
        }
    }
}
