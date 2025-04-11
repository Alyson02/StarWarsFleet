using System.Reflection;
using Microsoft.EntityFrameworkCore;
using StarWarsFleet.Domain.Entities;
using StarWarsFleet.Infrastructure.Mappings;

namespace StarWarsFleet.Infrastructure.Data;

public class StarWarsDbContext : DbContext
{
    public StarWarsDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<DockingSlotEntity> DockingSlots { get; set; }
    public DbSet<FactionEntity> Factions { get; set; }
    public DbSet<GarageEntity> Garages { get; set; }
    public DbSet<ShipEntity> Ships { get; set; }
    public DbSet<SpaceStationEntity> SpaceStations { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StarWarsDbContext).Assembly);
    }
}