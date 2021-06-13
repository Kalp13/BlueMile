using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Web.ApiModels
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
