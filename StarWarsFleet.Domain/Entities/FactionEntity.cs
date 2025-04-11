namespace StarWarsFleet.Domain.Entities;

public class FactionEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public ICollection<SpaceStationEntity> SpaceStations { get; set; } = new List<SpaceStationEntity>();
}