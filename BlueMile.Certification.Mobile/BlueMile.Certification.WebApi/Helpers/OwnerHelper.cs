using BlueMile.Certification.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMile.Certification.WebApi.Helpers
{
    public static class OwnerHelper
    {
        public static OwnerModel ToCreateOwnerModel(Web.ApiModels.CreateOwnerModel model)
        {
            var owner = new OwnerModel()
            {
                AddressLine1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2,
                AddressLine3 = model.AddressLine3,
                AddressLine4 = model.AddressLine4,
                ContactNumber = model.ContactNumber,
                Country = model.Country,
                Email = model.Email,
                Identification = model.Identification,
                Name = model.Name,
                PostalCode = model.PostalCode,
                Province = model.Province,
                Suburb = model.Suburb,
                Surname = model.Surname,
                Town = model.Town,
                Id = Guid.NewGuid()
            };

            return owner;
        }

        public static OwnerModel ToUpdateOwnerModel(Web.ApiModels.UpdateOwnerModel ownerModel)
        {
            var owner = new OwnerModel()
            {
                AddressLine1 = ownerModel.AddressLine1,
                AddressLine2 = ownerModel.AddressLine2,
                AddressLine3 = ownerModel.AddressLine3,
                AddressLine4 = ownerModel.AddressLine4,
                ContactNumber = ownerModel.ContactNumber,
                Country = ownerModel.Country,
                Email = ownerModel.Email,
                Identification = ownerModel.Identification,
                Name = ownerModel.Name,
                PostalCode = ownerModel.PostalCode,
                Province = ownerModel.Province,
                Suburb = ownerModel.Suburb,
                Surname = ownerModel.Surname,
                Town = ownerModel.Town,
                Id = ownerModel.SystemId,
            };

            return owner;
        }

        public static OwnerModel ToOwnerDataModel(Web.ApiModels.OwnerModel ownerModel)
        {
            var owner = new OwnerModel()
            {
                AddressLine1 = ownerModel.AddressLine1,
                AddressLine2 = ownerModel.AddressLine2,
                AddressLine3 = ownerModel.AddressLine3,
                AddressLine4 = ownerModel.AddressLine4,
                ContactNumber = ownerModel.ContactNumber,
                Country = ownerModel.Country,
                Email = ownerModel.Email,
                Identification = ownerModel.Identification,
                Name = ownerModel.Name,
                PostalCode = ownerModel.PostalCode,
                Province = ownerModel.Province,
                Suburb = ownerModel.Suburb,
                Surname = ownerModel.Surname,
                Town = ownerModel.Town,
                Id = ownerModel.SystemId,
            };

            return owner;
        }

        public static Web.ApiModels.OwnerModel ToApiOwnerModel(OwnerModel ownerEntity)
        {
            var owner = new Web.ApiModels.OwnerModel()
            {
                AddressLine1 = ownerEntity.AddressLine1,
                AddressLine2 = ownerEntity.AddressLine2,
                AddressLine3 = ownerEntity.AddressLine3,
                AddressLine4 = ownerEntity.AddressLine4,
                ContactNumber = ownerEntity.ContactNumber,
                Country = ownerEntity.Country,
                Email = ownerEntity.Email,
                Identification = ownerEntity.Identification,
                Name = ownerEntity.Name,
                PostalCode = ownerEntity.PostalCode,
                Province = ownerEntity.Province,
                Suburb = ownerEntity.Suburb,
                Surname = ownerEntity.Surname,
                Town = ownerEntity.Town,
                SystemId = ownerEntity.Id,
            };

            return owner;
        }
    }
}
