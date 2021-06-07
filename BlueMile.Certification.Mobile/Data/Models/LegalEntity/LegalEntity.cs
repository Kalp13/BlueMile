using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Data.Models
{
    public class LegalEntity
    {
        #region Instance Properties

        /// <summary>
        /// Gets or sets the main unique identifier of all client users.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Individual"/> associated with the 
        /// current <see cref="LegalEntity"/>.
        /// </summary>
        public IndividualOwner Owner { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="LegalEntityAddress"/>s
        /// associated with the current <see cref="LegalEntity"/>.
        /// </summary>
        public ICollection<LegalEntityAddress> Addresses { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="LegalEntityContactDetail"/>s
        /// associated with the current <see cref="LegalEntity"/>.
        /// </summary>
        public ICollection<LegalEntityContactDetail> ContactDetails { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="LegalEntityDocument"/>s
        /// associated with the current <see cref="LegalEntity"/>.
        /// </summary>
        public ICollection<LegalEntityDocument> Documents { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new default instance of <see cref="LegalEntity"/>.
        /// </summary>
        public LegalEntity()
        {

        }

        #endregion
    }
}
