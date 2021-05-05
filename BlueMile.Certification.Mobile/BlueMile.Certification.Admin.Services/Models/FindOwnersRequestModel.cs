namespace BlueMile.Certification.Admin.Services.Models
{
    public class FindOwnersRequestModel : PagedFilterModel
    {
        public string SearchTerm { get; set; }

        public FindOwnersRequestModel()
        {

        }
    }
}