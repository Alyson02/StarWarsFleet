namespace StarWarsFleet.Domain.Entities;

public class SpaceStation
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Faction { get; set; }
    public int CrewCapacity { get; set; }
    public bool IsOperational { get; set; }
}