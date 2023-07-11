using Microsoft.EntityFrameworkCore;
using pablo.API.Models.Entities;

namespace pablo.API.Data
{
    public class PabloDbContext : DbContext
    {
        public PabloDbContext(DbContextOptions options):base(options)
        {

        }
        //DBSet
        public DbSet<Post> Posts { get; set; }
    }
}
 