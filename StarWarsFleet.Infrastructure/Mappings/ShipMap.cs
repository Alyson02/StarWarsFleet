using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarWarsFleet.Domain.Entities;

namespace StarWarsFleet.Infrastructure.Mappings;

public class ShipMap : IEntityTypeConfiguration<ShipEntity>
{
    public void Configure(EntityTypeBuilder<ShipEntity> builder)
    {
        builder.ToTable("Ships");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.ModelName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Type)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(s => s.DockingSlotEntity)
            .WithOne(ds => ds.ShipEntity)
            .HasForeignKey<DockingSlotEntity>(ds => ds.ShipId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}