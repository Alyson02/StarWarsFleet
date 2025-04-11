namespace StarWarsFleet.Domain.Entities;

public class Ship
{
    public string ModelName { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;

    public DockingSlot DockingSlot { get; set; }
}