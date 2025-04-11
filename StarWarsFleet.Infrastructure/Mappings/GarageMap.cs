using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarWarsFleet.Domain.Entities;

namespace StarWarsFleet.Infrastructure.Mappings;

public class GarageMap : IEntityTypeConfiguration<GarageEntity>
{
    public void Configure(EntityTypeBuilder<GarageEntity> builder)
    {
        builder.ToTable("Garages");

        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(g => g.Slots)
            .WithOne(s => s.GarageEntity)
            .HasForeignKey(s => s.GarageId);
    }
}