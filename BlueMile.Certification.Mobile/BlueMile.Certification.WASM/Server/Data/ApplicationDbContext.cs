using BlueMile.Certification.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlueMile.Certification.WASM.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<OwnerModel> Owners { get; set; }

        public DbSet<BoatModel> Boats { get; set; }

        public DbSet<ItemModel> Items { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
