namespace StarWarsFleet.Domain.Entities;

public class ShipEntity : BaseEntity
{
    public string ModelName { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;

    public DockingSlotEntity DockingSlotEntity { get; set; } = new DockingSlotEntity();
}