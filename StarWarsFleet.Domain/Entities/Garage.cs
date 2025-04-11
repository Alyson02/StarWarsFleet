namespace StarWarsFleet.Domain.Entities;

public class Garage
{
    public string Name { get; set; } = string.Empty;

    public int SpaceStationId { get; set; }
    public SpaceStation SpaceStation { get; set; }

    public ICollection<DockingSlot> Slots { get; set; } = new List<DockingSlot>();
}