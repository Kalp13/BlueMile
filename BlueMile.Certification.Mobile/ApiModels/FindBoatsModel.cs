using System;

namespace BlueMile.Certification.Web.ApiModels
{
    public class FindBoatsModel : PagedFilterModel
    {
        public string? SearchTerm { get; set; }

        public Guid? BoatId { get; set; }

        #region Constructor

        /// <summary>
        /// Creates a new default instance of <see cref="FindBoatsModel"/>.
        /// </summary>
        public FindBoatsModel()
        {

        }

        #endregion
    }
}