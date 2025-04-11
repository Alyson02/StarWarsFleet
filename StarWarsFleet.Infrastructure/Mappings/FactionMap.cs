using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarWarsFleet.Domain.Entities;

namespace StarWarsFleet.Infrastructure.Mappings;

public class FactionMap : IEntityTypeConfiguration<FactionEntity>
{
    public void Configure(EntityTypeBuilder<FactionEntity> builder)
    {
        builder.ToTable("Factions");

        builder.HasKey(f => f.Id);
        builder.Property(f => f.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(f => f.SpaceStations)
            .WithOne(s => s.FactionEntity)
            .HasForeignKey(s => s.FactionId);
    }
} 