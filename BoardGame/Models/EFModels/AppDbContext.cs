using Microsoft.EntityFrameworkCore;

namespace BoardGame.Models.EFModels
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }

    }
}
