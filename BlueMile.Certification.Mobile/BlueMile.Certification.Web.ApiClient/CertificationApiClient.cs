using BlueMile.Certification.Web.ApiModels;
using BlueMile.Certification.Web.ApiModels.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BlueMile.Certification.Web.ApiClient
{
    public class CertificationApiClient : ICertificationApiClient
    {
        #region Constructor

        public CertificationApiClient(string baseAddress, string token)
        {
            this.baseAddress = baseAddress ?? throw new ArgumentNullException(nameof(baseAddress));
            this.userToken = token ?? throw new ArgumentNullException(nameof(token));
        }

        #endregion

        #region ICertification Implementation

        public Task<bool> RegisterUser(UserRegistrationModel userModel, OwnerModel ownerModel)
        {
            throw new NotImplementedException();
        }

        public Task<UserToken> LogUserIn(UserLoginModel userModel)
        {
            throw new NotImplementedException();
        }

        public Task<OwnerModel> GetOwnerBySystemId(Guid ownerId)
        {
            throw new NotImplementedException();
        }

        public Task<OwnerModel> GetOwnerByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateOwner(CreateOwnerModel owner)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateOwner(UpdateOwnerModel owner)
        {
            throw new NotImplementedException();
        }

        public Task<List<BoatModel>> GetBoatsByOwnerId(Guid ownerId)
        {
            throw new NotImplementedException();
        }

        public async Task<BoatModel> GetBoatById(Guid boatId)
        {
            try
            {
                var client = this.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this.userToken);

                HttpResponseMessage response = null;

                Uri uri = new Uri($@"{this.baseAddress}/get/{boatId}");

                response = await client.GetAsync(uri).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<BoatModel>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                    return new BoatModel();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Guid> CreateBoat(BoatModel boat)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateBoat(BoatModel boat)
        {
            throw new NotImplementedException();
        }

        public Task<List<ItemModel>> GetBoatRequiredItems(Guid boatId)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateItem(ItemModel item)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateItem(ItemModel item)
        {
            throw new NotImplementedException();
        }

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
