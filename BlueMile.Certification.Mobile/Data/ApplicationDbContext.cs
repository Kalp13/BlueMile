using BlueMile.Certification.Data.Models;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlueMile.Certification.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<OwnerModel> Owners { get; set; }

        public DbSet<BoatModel> Boats { get; set; }

        public DbSet<ItemModel> Items { get; set; }

        public DbSet<DataProtectionKey> DataProtectionKeys => throw new NotImplementedException();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
