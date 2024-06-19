using Codepulse_API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Codepulse_API.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Clategory> Categories { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
    }
}
