using Blazored.LocalStorage;
using BlueMile.Certification.Admin.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BlueMile.Certification.Admin.Services
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IHttpClientFactory clientFactory;

        private readonly ILocalStorageService localStorage;

        public BaseRepository(IHttpClientFactory factory, ILocalStorageService localStorage)
        {
            this.clientFactory = factory;
            this.localStorage = localStorage;
        }

        public async Task<bool> Create(string url, T newEntity)
        {
            //var request = new HttpRequestMessage(HttpMethod.Post, url);

            if (newEntity == null)
            {
                return false;
            }

            var serialized = JsonConvert.SerializeObject(newEntity);
            var content = new StringContent(serialized);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var client = this.clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());

            var response = await client.PostAsync(url, content);
            //HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Delete(string url, int id)
        {
            if (id <= 0)
            {
                return false;
            }

            var request = new HttpRequestMessage(HttpMethod.Delete, 
                            string.Format(CultureInfo.InvariantCulture, "{0}/{1}",url, id));

            var client = this.clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        public async Task<T> Get(string url, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                            string.Format(CultureInfo.InvariantCulture, "{0}/{1}", url, id));

            var client = this.clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());

            HttpResponseMessage response = await client.SendAsync(request);
            
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }

            return null;
        }

        public async Task<IList<T>> Get(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var client = this.clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<T>>(content);
            }



            return null;
        }

        public async Task<bool> Update(string url, T updatedEntity, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, 
                                string.Format(CultureInfo.InvariantCulture, "{0}/{1}", url, id));

            if (updatedEntity == null)
            {
                return false;
            }

            request.Content = new StringContent(JsonConvert.SerializeObject(updatedEntity), 
                                                Encoding.UTF8, 
                                                "application/json");

            var client = this.clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());

            HttpResponseMessage response = await client.SendAsync(request);

            
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        private async Task<string> GetBearerToken()
        {
            var savedToken = await this.localStorage.GetItemAsync<string>("authToken");
            return savedToken;
        }
    }
}
