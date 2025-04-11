using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarWarsFleet.Domain.Entities;

namespace StarWarsFleet.Infrastructure.Mappings;

public class SpaceStationMap : IEntityTypeConfiguration<SpaceStationEntity>
{
    public void Configure(EntityTypeBuilder<SpaceStationEntity> builder)
    {
        builder.ToTable("SpaceStations");

        builder.Property(s => s.Capacity)
            .IsRequired();

        builder.HasMany(s => s.Garages)
            .WithOne(g => g.SpaceStationEntity)
            .HasForeignKey(g => g.SpaceStationId);
    }
}