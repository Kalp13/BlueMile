using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BlueMile.Coc.Mobile.Models
{
    /// <summary>
    /// Represents all the necessary properties for images used.
    /// </summary>
    public class ImageModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for this image item.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of this image file item.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the unique image name of this <see cref="ImageModel"/>
        /// </summary>
        public string UniqueImageName { get; set; }

        /// <summary>
        /// Gets or sets the full path of this image item.
        /// </summary>
        public string FilePath { get; set; }
        
        /// <summary>
        /// Gets the image with the <c>FilePath</c> from the local storage.
        /// </summary>
        public ImageSource FileImage
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(this.FilePath))
                {
                    return ImageSource.FromFile(this.FilePath);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Creates a new default instnace of the <see cref="ImageModel"/> object.
        /// </summary>
        public ImageModel()
        {
            this.FileName = String.Empty;
            this.FilePath = String.Empty;
        }
    }
}
