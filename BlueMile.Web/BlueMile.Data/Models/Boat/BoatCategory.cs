﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlueMile.Data.Models
{
    public enum BoatCategoryEnum : int
    {
        [Display(Name = "Category C", Order = 1)]
        C = 1,

        [Display(Name = "Category D", Order = 2)]
        D = 2,

        [Display(Name = "Category E", Order = 3)]
        E = 3,

        [Display(Name = "Category Z", Order = 4)]
        Z = 4,

        [Display(Name = "Category R", Order = 5)]
        R = 5
    }

    /// <summary>
    /// <c>BoatCategory</c> defines the available categories of boats.
    /// </summary>
    public partial class BoatCategory : BaseEnumTable
    {
        #region Instance Properties

        /// <summary>
        /// Gets or sets the collection of <see cref="Boat"/>s associated 
        /// with the current <see cref="BoatCategory"/>.
        /// </summary>
        public ICollection<Boat> Boats { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new default <see cref="BoatCategory"/>.
        /// </summary>
        public BoatCategory()
        {

        }

        #endregion
    }
}