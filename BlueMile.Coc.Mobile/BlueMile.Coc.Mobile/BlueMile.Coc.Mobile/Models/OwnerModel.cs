using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace BlueMile.Coc.Mobile.Models
{
    public class OwnerModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string CellNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string VhfOperatorsLicense { get; set; }

        public string SkippersLicenseNumber { get; set; }

        public ImageModel SkippersLicenseImage { get; set; }

        public string IdentificationNumber { get; set; }

        public ImageModel IdentificationDocument { get; set; }

        public ImageModel IcasaPopPhoto { get; set; }

        public bool IsSynced { get; set; }

        #region Constructor

        public OwnerModel()
        {
            this.IcasaPopPhoto = new ImageModel();
            this.IdentificationDocument = new ImageModel();
            this.SkippersLicenseImage = new ImageModel();
        }

        #endregion

        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "Name: {0} {1}\n" +
                                 "Cell: {2}\n" +
                                 "Email: {3}\n" +
                                 "Address: {4}\n" +
                                 "Identification Number: {5}\n" +
                                 "VHF License: {6}\n" +
                                 "Skippers License: {7}\n",
                this.Name, this.Surname, this.CellNumber, this.Email, this.Address, this.IdentificationNumber, this.VhfOperatorsLicense, this.SkippersLicenseNumber);
        }
    }
}
