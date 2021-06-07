using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlueMile.Certification.Data.Models
{
    public enum ItemTypeEnum
    {
        [Display(Name = "Hand Held Flare", Order = 1)]
        HandHeldFlare = 1,

        [Display(Name = "Hand Held Flare", Order = 2)]
        LifeJacket,

        [Display(Name = "Hand Held Flare", Order = 3)]
        SmokeFlare,

        [Display(Name = "Hand Held Flare", Order = 4)]
        Anchor,

        [Display(Name = "Hand Held Flare", Order = 5)]
        AnchorRope,

        [Display(Name = "Hand Held Flare", Order = 6)]
        DrogueAnchor,

        [Display(Name = "Hand Held Flare", Order = 7)]
        CapsizeBottleWith2mLaneyard,

        [Display(Name = "Hand Held Flare", Order = 8)]
        ParachuteFlare,

        [Display(Name = "Hand Held Flare", Order = 9)]
        WaterproofTorch,

        [Display(Name = "Hand Held Flare", Order = 10)]
        SpaceBlanket,

        [Display(Name = "Hand Held Flare", Order = 11)]
        IdSheet,

        [Display(Name = "Hand Held Flare", Order = 12)]
        HandheldSpotlight,

        [Display(Name = "Hand Held Flare", Order = 13)]
        FirstAidKit,

        [Display(Name = "Hand Held Flare", Order = 14)]
        RadarReflector,

        [Display(Name = "Hand Held Flare", Order = 15)]
        VhfRadio,

        [Display(Name = "Hand Held Flare", Order = 16)]
        MagneticCompass,

        [Display(Name = "Hand Held Flare", Order = 17)]
        FireExtinguisher,

        [Display(Name = "Hand Held Flare", Order = 18)]
        OarOrPaddle,

        [Display(Name = "Hand Held Flare", Order = 19)]
        FittedGrabline,

        [Display(Name = "Hand Held Flare", Order = 20)]
        CodeFlag,

        [Display(Name = "Hand Held Flare", Order = 21)]
        FogHorn,

        [Display(Name = "Hand Held Flare", Order = 22)]
        TowRope
    }

    /// <summary>
    /// <c>ItemType</c> defines the available types of items for a boat.
    /// </summary>
    public partial class ItemType : BaseEnumTable
    {
        #region Instance Properties

        /// <summary>
        /// Gets or sets the collection of <see cref="Item"/>s associated 
        /// with the current <see cref="ItemType"/>.
        /// </summary>
        public ICollection<Item> Items { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new default <see cref="ItemType"/>.
        /// </summary>
        public ItemType()
        {

        }

        #endregion
    }
}
