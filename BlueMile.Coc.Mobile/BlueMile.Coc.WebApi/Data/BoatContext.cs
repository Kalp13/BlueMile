using BlueMile.Coc.Data;
using Microsoft.EntityFrameworkCore;

namespace BlueMile.Coc.WebApi.Data
{
    public class BoatContext : DbContext
    {
        public DbSet<BoatEntity> BoatEntities { get; set; }

        public BoatContext(DbContextOptions<BoatContext> options) : base(options)
        {

        }
    }
}
