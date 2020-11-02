using BlueMile.Coc.Data;
using Microsoft.EntityFrameworkCore;

namespace BlueMile.Coc.WebApi.Data
{
    public class ImageContext : DbContext
    {
        public DbSet<ImageEntity> ImageEntities { get; set; }

        public ImageContext(DbContextOptions<ImageContext> options) : base(options)
        {

        }
    }
}
