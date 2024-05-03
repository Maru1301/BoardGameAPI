using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace BoardGame.Models.EFModels
{
    public class AppDbContext(DbContextOptions options) : IdentityDbContext<IdentityUser>(options)
    {
        public DbSet<Member> Members { get; init; }
        public DbSet<Admin> Admins { get; init; }
        public DbSet<Game> Games { get; init; }

        public static AppDbContext Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<AppDbContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Member>().ToCollection("members");
            modelBuilder.Entity<Admin>().ToCollection("admins");
            modelBuilder.Entity<Game>().ToCollection("games");
        }
    }

    public class MongoDBSettings
    {
        public string AtlasURI { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
