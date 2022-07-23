using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMile.Data.Models
{
    /// <summary>
    /// <c>LegalEntityRelationship</c> defines a relationship between two 
    /// <see cref="LegalEntity"/>(s).
    /// </summary>
    public class LegalEntityRelationship : IBaseDbEntity
	{
		#region Instance Properties

		/// <summary>
		/// Gets or sets the unique identifier for the current relationship.
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// Gets or sets the unique <see cref="LegalEntity"/> identifier for the parent 
		/// <see cref="LegalEntity"/>.
		/// </summary>
		public long ParentId { get; set; }

		/// <summary>
		/// Gets or sets the parent <see cref="LegalEntity"/>.
		/// </summary>
		public LegalEntity Parent { get; set; }

		/// <summary>
		/// Gets or sets the unique <see cref="LegalEntity"/> identifier for the child 
		/// <see cref="LegalEntity"/>.
		/// </summary>
		public long ChildId { get; set; }

		/// <summary>
		/// Gets or sets the child <see cref="LegalEntity"/>.
		/// </summary>
		public LegalEntity Child { get; set; }

		/// <summary>
		/// Gets or sets the unique <see cref="LegalEntityRelationshipType"/> identifier
		/// for the current <see cref="LegalEntityRelationship"/>.
		/// </summary>
		public int RelationshipTypeId { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="LegalEntityRelationshipType"/>
		/// for the current <see cref="LegalEntityRelationship"/>.
		/// </summary>
		public LegalEntityRelationshipType RelationshipType { get; set; }

		/// <summary>
		/// Gets or sets the unique <see cref="LegalEntityRelationshipState"/> identifier
		/// for the current <see cref="LegalEntityRelationship"/>.
		/// </summary>
		public int? ApprovalStateId { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="LegalEntityRelationshipState"/>
		/// or the current <see cref="LegalEntityRelationship"/>.
		/// </summary>
		public LegalEntityRelationshipState ApprovalState { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new default <see cref="LegalEntityRelationship"/>.
		/// </summary>
		public LegalEntityRelationship()
		{

		}

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
	}
}
