namespace StarWarsFleet.Domain.Entities;

public class GarageEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public Guid SpaceStationId { get; set; }
    public SpaceStationEntity SpaceStationEntity { get; set; } = new SpaceStationEntity();

    public ICollection<DockingSlotEntity> Slots { get; set; } = new List<DockingSlotEntity>();
}