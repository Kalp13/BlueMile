using BlueMile.Certification.Data.Models;
using BlueMile.Certification.Web.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMile.Certification.WebApi.Helpers
{
    public static class DocumentHelper
    {
        public static Document ToCreateDocumentModel(DocumentModel model)
        {
            var doc = new Document()
            {
                CreatedBy = "test",
                CreatedOn = DateTime.Now,
                DocumentName = model.FileName,
                UniqueFileName = model.UniqueImageName
            };

            return doc;
        }
    }
}
