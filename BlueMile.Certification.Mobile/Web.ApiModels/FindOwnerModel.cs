using System;

namespace BlueMile.Certification.Web.ApiModels
{
    public class FindOwnerModel
    {
        public string? SearchTerm { get; set; }

        public Guid? OwnerId { get; set; }
    }
}