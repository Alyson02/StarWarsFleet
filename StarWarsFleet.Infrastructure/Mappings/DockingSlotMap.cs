using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarWarsFleet.Domain.Entities;

namespace StarWarsFleet.Infrastructure.Mappings;

public class DockingSlotMap : IEntityTypeConfiguration<DockingSlotEntity>
{
    public void Configure(EntityTypeBuilder<DockingSlotEntity> builder)
    {
        builder.ToTable("DockingSlots");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.SlotNumber)
            .IsRequired();

        builder.HasOne(d => d.ShipEntity)
            .WithOne(s => s.DockingSlotEntity)
            .HasForeignKey<DockingSlotEntity>(d => d.ShipId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}