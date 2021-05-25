using System;

namespace BlueMile.Certification.Web.ApiModels
{
    public class FindOwnerModel : PagedFilterModel
    {
        public string? SearchTerm { get; set; }

        public Guid? OwnerId { get; set; }

        #region Constructor

        /// <summary>
        /// Creates a new default instance of <see cref="FindOwnerModel"/>.
        /// </summary>
        public FindOwnerModel()
        {

        }

        #endregion
    }
}