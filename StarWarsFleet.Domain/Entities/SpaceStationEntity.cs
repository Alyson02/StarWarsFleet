namespace StarWarsFleet.Domain.Entities;

public class SpaceStationEntity : ShipEntity
{
    public string Name { get; set; } = string.Empty;
    public bool IsOperational { get; set; }
    public int Capacity { get; set; }
    public Guid FactionId { get; set; }
    public FactionEntity FactionEntity { get; set; } = new FactionEntity();
    public ICollection<GarageEntity> Garages { get; set; } = new List<GarageEntity>();
}