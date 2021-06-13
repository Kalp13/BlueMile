using BlueMile.Certification.Admin.Services;
using BlueMile.Certification.Web.ApiModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMile.Certification.Admin.Web.Areas.Owners
{
    public partial class OwnerDetail
    {
        [Parameter]
        public string Id { get; set; }

        [Inject] public IOwnerService OwnerService { get; set; }

        private OwnerModel Model = new OwnerModel();

        public bool IsLoading { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await this.LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            this.IsLoading = true;
            Guid id = Guid.Parse(this.Id);
            this.Model = await this.OwnerService.FindOwnerBySystemIdAsync(id);
            this.IsLoading = false;
        }

        private void BackToList()
        {
            this.navigationManager.NavigateTo("/authors");
        }
    }
}
