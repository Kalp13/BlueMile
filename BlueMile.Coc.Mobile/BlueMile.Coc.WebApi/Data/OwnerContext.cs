using Microsoft.EntityFrameworkCore;
using BlueMile.Coc.Data;

namespace BlueMile.Coc.WebApi.Data
{
    public class OwnerContext : DbContext
    {
        public DbSet<OwnerEntity> OwnerEntities { get; set; }

        public OwnerContext(DbContextOptions<OwnerContext> options) : base(options)
        {

        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<OwnerEntity>().ToTable(nameof(OwnerEntity));
        //}
    }
}
