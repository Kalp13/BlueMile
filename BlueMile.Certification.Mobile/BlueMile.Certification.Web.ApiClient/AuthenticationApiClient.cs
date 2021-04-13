using BlueMile.Certification.Web.ApiModels;
using BlueMile.Certification.Web.ApiModels.Static;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlueMile.Certification.Web.ApiClient
{
    public class AuthenticationApiClient : IAuthenticationApiClient
    {

        public AuthenticationApiClient()
        {
            
        }

        public async Task<bool> Login(UserLoginModel user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Endpoints.LoginEndpoint);
            request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var client = this.httpClient.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var content = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<TokenResponseModel>(content);

            //Store Token
            await this.localStorage.SetItemAsync<string>("authToken", token.Token);

            //Change auth state of app
            await ((ApiAuthenticationStateProvider)this.authentication).LogggedIn();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Token);

            return true;
        }

        public async Task LogOut()
        {
            await this.localStorage.RemoveItemAsync("authToken");
            await ((ApiAuthenticationStateProvider)this.authentication).LoggedOut();
        }

        public async Task<bool> Register(UserRegistrationModel user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Endpoints.RegisterEndpoint);
            request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var client = this.httpClient.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            return response.IsSuccessStatusCode;
        }
    }
}
