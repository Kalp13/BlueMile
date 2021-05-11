using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Web.ApiModels
{
    public class BoatModel
    {
        /// <summary>
        /// Gets or sets the primary unique identifier of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public Guid SystemId { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the owner of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the <c>Name</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the <c>RegisteredNumber</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public string RegisteredNumber { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the cateogry of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public int BoatCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the <c>BoyancyCertificateNumber</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public string BoyancyCertificateNumber { get; set; }

        /// <summary>
        /// Gets or sets the <c>BoyancyCertificateImagePath</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public ImageModel BoyancyCertificateImage { get; set; }

        /// <summary>
        /// Gets or sets the flag to state if this a Jetski or not.
        /// </summary>
        public bool IsJetski { get; set; }

        /// <summary>
        /// Gets or sets the <c>TubbiesCertificateNumber</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public string TubbiesCertificateNumber { get; set; }

        /// <summary>
        /// Gets or sets the <c>TubbiesCertificateImagePath</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public ImageModel TubbiesCertificateImage { get; set; }

        public bool IsSynced { get; set; }
    }

    public class CreateBoatModel
    {
        /// <summary>
        /// Gets or sets the foreign key to the owner of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the <c>Name</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the <c>RegisteredNumber</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public string RegisteredNumber { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the cateogry of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public int BoatCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the <c>BoyancyCertificateNumber</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public string BoyancyCertificateNumber { get; set; }

        /// <summary>
        /// Gets or sets the <c>BoyancyCertificateImagePath</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public ImageModel BoyancyCertificateImage { get; set; }

        /// <summary>
        /// Gets or sets the flag to state if this a Jetski or not.
        /// </summary>
        public bool IsJetski { get; set; }

        /// <summary>
        /// Gets or sets the <c>TubbiesCertificateNumber</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public string TubbiesCertificateNumber { get; set; }

        /// <summary>
        /// Gets or sets the <c>TubbiesCertificateImagePath</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public ImageModel TubbiesCertificateImage { get; set; }
    }

    public class UpdateBoatModel
    {
        /// <summary>
        /// Gets or sets the primary unique identifier of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public Guid SystemId { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the owner of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the <c>Name</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the <c>RegisteredNumber</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public string RegisteredNumber { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the cateogry of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public int BoatCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the <c>BoyancyCertificateNumber</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public string BoyancyCertificateNumber { get; set; }

        /// <summary>
        /// Gets or sets the <c>BoyancyCertificateImagePath</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public ImageModel BoyancyCertificateImage { get; set; }

        /// <summary>
        /// Gets or sets the flag to state if this a Jetski or not.
        /// </summary>
        public bool IsJetski { get; set; }

        /// <summary>
        /// Gets or sets the <c>TubbiesCertificateNumber</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public string TubbiesCertificateNumber { get; set; }

        /// <summary>
        /// Gets or sets the <c>TubbiesCertificateImagePath</c> of this <see cref="BoatMobileEntity"/>.
        /// </summary>
        public ImageModel TubbiesCertificateImage { get; set; }
    }
}
