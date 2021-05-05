namespace BlueMile.Certification.Admin.Services.Models
{
    public class PagedFilterModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public PagedFilterModel()
        {
            this.Page = 1;
            this.PageSize = 25;
        }
    }
}