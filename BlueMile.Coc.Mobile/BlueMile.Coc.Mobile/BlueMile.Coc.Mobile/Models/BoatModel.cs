using BlueMile.Coc.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BlueMile.Coc.Mobile.Models
{
    public class BoatModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }

        public string Name { get; set; }

        public string RegisteredNumber { get; set; }
        
        public CategoryStaticEntity CategoryId { get; set; }

        public string BoyancyCertificateNumber { get; set; }

        public ImageModel BoyancyCertificateImage { get; set; }

        public bool IsJetski { get; set; }

        public string TubbiesCertificateNumber { get; set; }

        public ImageModel TubbiesCertificateImage { get; set; }

        #region Constructor

        public BoatModel()
        {
            this.BoyancyCertificateImage = new ImageModel();
            this.TubbiesCertificateImage = new ImageModel();
        }
        #endregion
    }
}
