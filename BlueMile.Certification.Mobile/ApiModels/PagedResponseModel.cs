using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Web.ApiModels
{
    public class PagedResponseModel<T>
    {
        public T[] Items { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public long Total { get; set; }

        public PagedResponseModel()
        {
            this.Page = 1;
            this.PageSize = 25;
            this.Items = Array.Empty<T>();
        }
    }
}
