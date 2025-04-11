namespace StarWarsFleet.Domain.Entities;

public class DockingSlot
{
    public int SlotNumber { get; set; }

    public int GarageId { get; set; }
    public Garage Garage { get; set; }

    public int? ShipId { get; set; }
    public Ship Ship { get; set; }
}