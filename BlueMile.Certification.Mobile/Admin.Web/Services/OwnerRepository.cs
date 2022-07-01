using Blazored.LocalStorage;
using BlueMile.Certification.Admin.Contracts;
using BlueMile.Certification.Web.ApiModels;
using System.Net.Http;

namespace BookStore.Website.Services
{
    public class OwnerRepository : BaseRepository<OwnerModel>, IOwnerRepository
    {
        private readonly IHttpClientFactory clientFactory;

        private readonly ILocalStorageService localStorage;

        public OwnerRepository(IHttpClientFactory client, ILocalStorageService localStorage) : base(client, localStorage)
        {
            this.clientFactory = client;
            this.localStorage = localStorage;
        }
    }
}
