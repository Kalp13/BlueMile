using BlueMile.Certification.Mobile.Models;
using BlueMile.Certification.Web.ApiModels;
using System;

namespace BlueMile.Certification.Mobile.Helpers
{
    public class ImageModelHelper
    {
        public static DocumentMobileModel ToImageMobileModel(DocumentModel document)
        {
            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var imageModel = new DocumentMobileModel()
            {
                FilePath = document.FilePath,
                FileName = document.FileName,
                FileType = document.DocumentType,
                SystemId = document.SystemId,
                Id = document.SystemId,
                UniqueImageName = document.UniqueImageName,
                FileContent = document.FileContent
            };

            return imageModel;
        }

        public static DocumentModel ToImageModel(DocumentMobileModel document)
        {
            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var imageModel = new DocumentModel()
            {
                FilePath = document.FilePath,
                FileName = document.FileName,
                DocumentType = document.FileType,
                SystemId = document.Id,
                UniqueImageName = document.UniqueImageName,
                FileContent = document.FileContent
            };

            return imageModel;
        }
    }
}
