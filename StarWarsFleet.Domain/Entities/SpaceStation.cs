namespace StarWarsFleet.Domain.Entities;

public class SpaceStation : Base
{
    public string Name { get; set; } = String.Empty;
    public bool IsOperational { get; set; }
    public int Capacity { get; set; }
    public int FactionId { get; set; }
    public Faction Faction { get; set; } = new Faction();
    public ICollection<Garage> Garages { get; set; } = new List<Garage>();
}