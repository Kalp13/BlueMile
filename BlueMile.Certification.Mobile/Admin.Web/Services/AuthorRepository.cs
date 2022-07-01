using Blazored.LocalStorage;
using BlueMile.Certification.Admin.Contracts;
using BlueMile.Certification.Web.ApiModels;
using System.Net.Http;

namespace BlueMile.Certification.Admin.Services
{
    public class BoatRepository : BaseRepository<BoatModel>, IBoatRepository
    {
        private readonly IHttpClientFactory clientFactory;

        private readonly ILocalStorageService localStorage;

        public BoatRepository(IHttpClientFactory client, ILocalStorageService localStorage) : base(client, localStorage)
        {
            this.clientFactory = client;
            this.localStorage = localStorage;
        }
    }
}
