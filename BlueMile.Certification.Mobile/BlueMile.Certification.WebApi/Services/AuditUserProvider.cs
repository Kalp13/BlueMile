using BlueMile.Certification.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMile.Certification.WebApi.Services
{
    public class AuditUserProvider : IAuditUserProvider
    {
        public AuditUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            // Validate Parameters.
            if (httpContextAccessor == null)
            {
                throw new ArgumentNullException("httpContextAccessor");
            }

            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUsername()
        {
            return this.httpContextAccessor.HttpContext?.User.Identity.Name;
        }

        private IHttpContextAccessor httpContextAccessor;
    }
}
