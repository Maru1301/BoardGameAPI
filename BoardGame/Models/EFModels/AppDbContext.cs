using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace BoardGame.Models.EFModels
{
    public class AppDbContext : DbContext
    {
        public DbSet<Member> Members { get; init; }
        public static AppDbContext Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<AppDbContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Member>().ToCollection("members");
        }
    }
}
