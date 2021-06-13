using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlueMile.Certification.Web.ApiModels
{
    public class UserLoginModel
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Creates a new default instance of <see cref="UserLoginModel"/>.
        /// </summary>
        public UserLoginModel()
        {

        }
    }

    public class UserRegistrationModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(32, ErrorMessage = "Your Password is invalid. Limited from {2} characters to {1} characters.", MinimumLength = 8)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string ContactNumber { get; set; }

        public Guid OwnerId { get; set; }

        /// <summary>
        /// Creates a new default instance of <see cref="UserRegistrationModel"/>.
        /// </summary>
        public UserRegistrationModel()
        {

        }
    }

    public class UserToken
    {
        public string Username { get; set; }

        public string Token { get; set; }

        public Guid OwnerId { get; set; }

        public string[] Roles { get; set; }
    }
}
