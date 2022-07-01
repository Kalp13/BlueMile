using BlueMile.Certification.Web.ApiModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BlueMile.Certification.Admin.Web.Pages.Owner
{
    public partial class FindOwners
    {
        public ObservableCollection<OwnerModel> Owners { get; set; }

        public FindOwners()
        {

        }

        protected override async Task OnInitializedAsync()
        {
            this.Owners = await this.bookRepo.Get(Endpoints.BookssEndpoint);
        }
    }
}
