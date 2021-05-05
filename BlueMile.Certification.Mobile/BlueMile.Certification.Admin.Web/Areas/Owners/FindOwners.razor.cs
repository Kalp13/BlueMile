using BlueMile.Certification.Admin.Services;
using BlueMile.Certification.Admin.Services.Models;
using BlueMile.Certification.Web.ApiModels;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMile.Certification.Admin.Web.Areas.Owners
{
    public partial class FindOwners
	{
		public bool IsLoadingPage { get; set; }

		public bool IsPaging { get; set; }

		[Inject] public IOwnerService OwnerService { get; set; }

		public PagedResponseModel<OwnerModel> Results { get; set; }

		public FindOwnersRequestModel Filter { get; set; }

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
	}
}
