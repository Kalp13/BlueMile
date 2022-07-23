namespace BlueMile.Data.Models
{
    public class CertificationRequest : IBaseDbEntity
    {
        #region Instance Properties

        /// <summary>
        /// Gets or sets the unique identifier of the <see cref="CertificationRequest"/>.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the boat associated with this image.
        /// </summary>
        public Guid BoatId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Boat"/> for the current <see cref="CertificationRequest"/>.
        /// </summary>
        public Boat Boat { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the <see cref="RequestStateEnum"/>.
        /// </summary>
        public int RequestStateId { get; set; }

        /// <summary>
        /// Gets or sets the request state of this request.
        /// </summary>
        public RequestState RequestState { get; set; }

        /// <summary>
        /// Gets or sets when this request was made.
        /// </summary>
        public DateTime? RequestedOn { get; set; }

        /// <summary>
        /// Gets or sets when this request was rejected.
        /// </summary>
        public DateTime? RejectedOn { get; set; }

        /// <summary>
        /// Gets or sets when this request was approved.
        /// </summary>
        public DateTime? ApprovedOn { get; set; }

        /// <summary>
        /// Gets or sets when this request was completed.
        /// </summary>
        public DateTime? CompletedOn { get; set; }

        #endregion

        #region IBaseDbEntity Implementation

        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }

        /// <inheritdoc/>
        public DateTime ModifiedOn { get; set; }

        /// <inheritdoc/>
        public string CreatedBy { get; set; }

        /// <inheritdoc/>
        public string ModifiedBy { get; set; }

        /// <inheritdoc/>
        public bool IsActive { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new default instance of <see cref="CertificationRequest"/>.
        /// </summary>
        public CertificationRequest()
        {

        }

        #endregion
    }
}
