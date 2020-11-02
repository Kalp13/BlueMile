using BlueMile.Coc.Data;
using Microsoft.EntityFrameworkCore;

namespace BlueMile.Coc.WebApi.Data
{
    public class RequiredItemContext : DbContext
    {
        public DbSet<RequiredItemEntity> RequiredItems { get; set; }

        public RequiredItemContext(DbContextOptions<RequiredItemContext> options) : base(options)
        {

        }
    }
}
