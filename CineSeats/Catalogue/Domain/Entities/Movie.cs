namespace CineSeats.Catalogue.Domain.Entities;

public class Movie
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<string> Genres { get; set; }
    public int AgeRestriction { get; set; }
    public string Synopsis { get; set; }
    public Dictionary<string, string> Cast { get; set; } //Nome ator: nome personagem
    public string Director { get; set; }
    public string Producer { get; set; }
    public TimeSpan Duration { get; set; }
}