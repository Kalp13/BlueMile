using System;

namespace BlueMile.Certification.Web.ApiModels
{
    public class FindItemsModel : PagedFilterModel
    {
        public string? SearchTerm { get; set; }

        public Guid? ItemId { get; set; }

        #region Constructor

        /// <summary>
        /// Creates a new default instance of <see cref="FindItemsModel"/>.
        /// </summary>
        public FindItemsModel()
        {

        }

        #endregion
    }
}