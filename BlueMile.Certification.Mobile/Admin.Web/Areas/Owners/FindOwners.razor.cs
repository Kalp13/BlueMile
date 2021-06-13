using Blazored.LocalStorage;
using BlueMile.Certification.Admin.Services;
using BlueMile.Certification.Admin.Services.Models;
using BlueMile.Certification.Web.ApiModels;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlueMile.Certification.Admin.Web.Areas.Owners
{
    public partial class FindOwners
    {
        public bool IsLoadingPage { get; set; }

        public bool IsPaging { get; set; }

        [Inject] public IOwnerService OwnerService { get; set; }

        public PagedResponseModel<OwnerWebModel> Results { get; set; }

        public FindOwnerModel Filter { get; set; }

        #region Constructor

        public FindOwners()
        {
            
        }

        #endregion

        private async Task LoadData()
        {
            this.Results = await this.OwnerService.FindOwnersAsync(this.Filter);
            this.StateHasChanged();
		}

        private async Task OnPageData(MatPaginatorPageEvent e)
        {
            this.IsPaging = true;

            this.Filter.Page = e.PageIndex + 1;
            this.Filter.PageSize = e.PageSize;
            await this.LoadData();

            this.IsPaging = false;
        }

        protected override async Task OnInitializedAsync()
        {
            this.IsLoadingPage = true;
            this.IsPaging = false;

            await this.LoadData();

            this.IsLoadingPage = false;
        }

        private async Task<string> GetBearerToken()
        {
            string savedToken = await this.localStorage.GetItemAsync<string>("authToken");
            return savedToken;
        }

        private readonly IHttpClientFactory clientFactory;

        private readonly ILocalStorageService localStorage;
    }
}
