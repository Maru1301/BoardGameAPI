using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BoardGame.Models.EFModels
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Member> Members { get; init; }
        public DbSet<Admin> Admins { get; init; }
        public DbSet<Game> Games { get; init; }
        public DbSet<Character> Characters { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Get all types from the specified namespace
            var entityTypes = Assembly.GetExecutingAssembly()
                                       .GetTypes()
                                       .Where(t => t.Namespace == "BoardGame.Models.EFModels" && t.IsClass);

            // Dynamically configure entities
            //foreach (var entityType in entityTypes)
            //{
            //    modelBuilder.Entity(entityType);
            //}
            modelBuilder.Entity<Member>();
            modelBuilder.Entity<Admin>();
            modelBuilder.Entity<Game>();
            modelBuilder.Entity<Character>();
        }
    }

    public class MongoDBSettings
    {
        public string AtlasURI { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
