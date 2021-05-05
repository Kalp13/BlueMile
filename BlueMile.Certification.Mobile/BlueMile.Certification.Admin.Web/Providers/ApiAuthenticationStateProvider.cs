using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlueMile.Certification.Admin.Web.Providers
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorage;

        private readonly JwtSecurityTokenHandler tokenHandler;

        public ApiAuthenticationStateProvider(ILocalStorageService localStorage, JwtSecurityTokenHandler tokenHandler)
        {
            this.localStorage = localStorage;
            this.tokenHandler = tokenHandler;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var savedToken = await this.localStorage.GetItemAsync<string>("authToken");

                if (String.IsNullOrWhiteSpace(savedToken))
                {
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }

                var tokenContent = this.tokenHandler.ReadJwtToken(savedToken);
                //Check time valid
                if ((tokenContent.ValidFrom > DateTime.Now) || (tokenContent.ValidTo < DateTime.Now))
                {
                    await this.localStorage.RemoveItemAsync("authToken");
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }

                //Check 
                var claims = this.ParseClaims(tokenContent);
                var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

                return new AuthenticationState(user);
            }
            catch (Exception)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        public async Task LogggedIn()
        {
            var savedToken = await this.localStorage.GetItemAsync<string>("authToken");
            var tokenContent = this.tokenHandler.ReadJwtToken(savedToken);
            var claims = this.ParseClaims(tokenContent);
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
        }

        public async Task LoggedOut()
        {
            var nobody = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(nobody));
            NotifyAuthenticationStateChanged(authState);
        }

        private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            return claims;
        }
    }
}