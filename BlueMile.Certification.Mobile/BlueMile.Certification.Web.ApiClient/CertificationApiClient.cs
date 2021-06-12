using BlueMile.Certification.Web.ApiModels;
using BlueMile.Certification.Web.ApiModels.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BlueMile.Certification.Web.ApiClient
{
    public class CertificationApiClient : ICertificationApiClient
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance of <see cref="CertificationApiClient"/>.
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <param name="token"></param>
        public CertificationApiClient(string baseAddress, string token)
        {
            this.baseAddress = baseAddress ?? throw new ArgumentNullException(nameof(baseAddress));
            this.userToken = token ?? throw new ArgumentNullException(nameof(token));
        }

        #endregion

        #region ICertification Implementation

        #region User Methods

        /// <inheritdoc/>
        public async Task<bool> RegisterUser(UserRegistrationModel userModel)
        {
            try
            {
                if (this.client == null)
                {
                    this.client = this.CreateClient();
                }

                var json = JsonConvert.SerializeObject(userModel, typeof(UserRegistrationModel), Formatting.None, null);
                using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = null;

                    Uri uri = new Uri($@"{this.baseAddress}/Users/register");

                    response = await client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
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

                var request = new HttpRequestMessage(HttpMethod.Post, $@"{this.baseAddress}/Users/login");
                request.Content = new StringContent(JsonConvert.SerializeObject(userModel), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    throw new ArgumentException(response.ReasonPhrase);
                }

                var content = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<UserToken>(content);

                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Token);

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
        public async Task<PagedResponseModel<OwnerModel>> FindOwners(FindOwnerModel filterModel)
        {
            try
            {
                if (filterModel == null)
                {
                    throw new ArgumentNullException(nameof(filterModel));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }

                var addressBuilder = new UriBuilder($"{this.baseAddress}/Certification/owner");
                var query = HttpUtility.ParseQueryString(addressBuilder.Query);
                query[nameof(filterModel.OwnerId)] = filterModel.OwnerId.ToString();
                query[nameof(filterModel.SearchTerm)] = filterModel.SearchTerm;

                addressBuilder.Query = query.ToString();

                var authHeader = new AuthenticationHeaderValue("bearer", this.userToken);
                this.client.DefaultRequestHeaders.Authorization = authHeader;

                var request = new HttpRequestMessage(HttpMethod.Get, addressBuilder.ToString());
                request.Headers.Authorization = authHeader;

                var response = await this.client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var owner = JsonConvert.DeserializeObject<PagedResponseModel<OwnerModel>>(content);
                    return owner;
                }

                return null;
            }
            catch (Exception exc)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<OwnerModel> GetOwnerBySystemId(Guid ownerId)
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

                var filterModel = new FindOwnerModel()
                {
                    OwnerId = ownerId,
                    SearchTerm = ""
                };
                var addressBuilder = new UriBuilder($"{this.baseAddress}/Certification/owner");
                var query = HttpUtility.ParseQueryString(addressBuilder.Query);
                query[nameof(filterModel.OwnerId)] = filterModel.OwnerId.ToString();
                query[nameof(filterModel.SearchTerm)] = filterModel.SearchTerm;

                addressBuilder.Query = query.ToString();

                var authHeader = new AuthenticationHeaderValue("bearer", this.userToken);
                this.client.DefaultRequestHeaders.Authorization = authHeader;

                var request = new HttpRequestMessage(HttpMethod.Get, addressBuilder.ToString());
                request.Headers.Authorization = authHeader;

                var response = await this.client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var owner = JsonConvert.DeserializeObject<IEnumerable<OwnerModel>>(content);
                    return owner.FirstOrDefault();
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<OwnerModel> GetOwnerByUsername(string username)
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

                Uri uri = new Uri($@"{this.baseAddress}/Certification/owner/get/{username}");
                HttpResponseMessage response = await this.client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var owner = JsonConvert.DeserializeObject<OwnerModel>(content);
                    return owner;
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Guid> CreateOwner(OwnerModel owner)
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
                string json = JsonConvert.SerializeObject(owner);
                using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = null;

                    Uri uri = new Uri($@"{this.baseAddress}/Certification/owner/create");

                    response = await this.client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var ownerId = JsonConvert.DeserializeObject<Guid>(result);
                        return ownerId;
                    }
                    else
                    {
                        throw new ArgumentException(response.ReasonPhrase);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Guid> UpdateOwner(OwnerModel owner)
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
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this.userToken);

                string json = JsonConvert.SerializeObject(owner);
                using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = null;

                    Uri uri = new Uri($@"{this.baseAddress}/Certification/owner/update/{owner.Id}");

                    response = await this.client.PutAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
                        return result;
                    }
                    else
                    {
                        throw new ArgumentException(response.ReasonPhrase);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Boat Methods

        /// <inheritdoc/>
        public async Task<List<BoatModel>> GetBoatsByOwnerId(Guid ownerId)
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
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this.userToken);

                Uri uri = new Uri($@"{this.baseAddress}/Certification/boat/{ownerId}");
                HttpResponseMessage response = await this.client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var boats = JsonConvert.DeserializeObject<List<BoatModel>>(content);
                    return boats.ToList();
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<BoatModel> GetBoatById(Guid boatId)
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
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this.userToken);

                HttpResponseMessage response = null;

                Uri uri = new Uri($@"{this.baseAddress}/Certification/boat/get/{boatId}");
                if (this.client.BaseAddress == null)
                {
                    this.client.BaseAddress = new Uri(this.baseAddress);
                }

                response = await this.client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<BoatModel>(await response.Content.ReadAsStringAsync());
                    return result;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw new ArgumentException(response.ReasonPhrase);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Guid> CreateBoat(BoatModel boat)
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
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this.userToken);

                string json = JsonConvert.SerializeObject(BoatHelper.ToCreateBoatModel(boat));

                using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = null;

                    Uri uri = new Uri($@"{this.baseAddress}/Certification/boat/create");
                    if (this.client.BaseAddress == null)
                    {
                        this.client.BaseAddress = new Uri(this.baseAddress);
                    }

                    response = await this.client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
                        return result;
                    }
                    else
                    {
                        throw new ArgumentException(response.ReasonPhrase);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Guid> UpdateBoat(BoatModel boat)
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
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this.userToken);

                string json = JsonConvert.SerializeObject(BoatHelper.ToUpdateBoatModel(boat));

                using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = null;

                    Uri uri = new Uri($@"{this.baseAddress}/Certification/boat/update/{boat.Id}");
                    if (this.client.BaseAddress == null)
                    {
                        this.client.BaseAddress = new Uri(this.baseAddress);
                    }

                    response = await this.client.PutAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
                        return result;
                    }
                    else
                    {
                        throw new ArgumentException(response.ReasonPhrase);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Item Methods

        /// <inheritdoc/>
        public async Task<List<ItemModel>> GetBoatRequiredItems(Guid boatId)
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
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this.userToken);

                Uri uri = new Uri($"{this.baseAddress}/Certification/item/{boatId}");
                HttpResponseMessage response = await this.client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var items = JsonConvert.DeserializeObject<List<ItemModel>>(content);
                    return items;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Guid> CreateItem(ItemModel item)
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
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this.userToken);

                string json = JsonConvert.SerializeObject(ItemHelper.ToCreateItemModel(item));

                using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = null;

                    Uri uri = new Uri($"{this.baseAddress}/Certification/item/create");
                    if (this.client.BaseAddress == null)
                    {
                        this.client.BaseAddress = new Uri(this.baseAddress);
                    }

                    response = await client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
                        return result;
                    }
                    else
                    {
                        throw new ArgumentException(response.ReasonPhrase);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Guid> UpdateItem(ItemModel item)
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
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this.userToken);

                string json = JsonConvert.SerializeObject(ItemHelper.ToUpdateItemModel(item));

                using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = null;

                    Uri uri = new Uri($"{this.baseAddress}/Certification/item/update/{item.Id}");
                    if (this.client.BaseAddress == null)
                    {
                        this.client.BaseAddress = new Uri(this.baseAddress);
                    }

                    response = await this.client.PutAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
                        return result;
                    }
                    else
                    {
                        throw new ArgumentException(response.ReasonPhrase);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #endregion

        #region Instance Methods

        private HttpClient CreateClient()
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; },

            };
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            HttpClient client = new HttpClient(httpClientHandler, false);
            client.BaseAddress = new Uri($"{this.baseAddress}");
            client.Timeout = TimeSpan.FromMinutes(30);
            return client;
        }

        #endregion

        private HttpClient client;

        private string baseAddress;

        private string userToken;
    }
}
