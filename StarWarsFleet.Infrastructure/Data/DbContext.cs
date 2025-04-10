using Microsoft.EntityFrameworkCore;
using StarWarsFleet.Domain.Entities;

namespace StarWarsFleet.Infrastructure.Data;

public class StarWarsDbContext : DbContext
{
    public StarWarsDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<SpaceStation> SpaceStations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure your entity mappings here
        // For example:
        // modelBuilder.Entity<YourEntity>().ToTable("YourTableName");
    }
}