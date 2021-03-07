﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BlueMile.Certification.Web.ApiModels
{
    public class UserModel
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }

        public bool IsAdministrator { get; set; }
    }
}