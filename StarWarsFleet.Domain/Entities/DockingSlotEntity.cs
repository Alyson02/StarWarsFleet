namespace StarWarsFleet.Domain.Entities;

public class DockingSlotEntity : BaseEntity
{
    public int SlotNumber { get; set; }

    public Guid GarageId { get; set; }
    public GarageEntity GarageEntity { get; set; } = new GarageEntity();

    public Guid? ShipId { get; set; }
    public ShipEntity ShipEntity { get; set; } = new ShipEntity();
}