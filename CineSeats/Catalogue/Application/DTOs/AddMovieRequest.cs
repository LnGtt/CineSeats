namespace CineSeats.Catalogue.Application.DTOs;

public class AddMovieRequest
{
    public string Name { get; set; }
    public List<string> Genres { get; set; }
    public string AgeRestriction { get; set; }
    public string Synopsis { get; set; }
    public Dictionary<string, string> Cast { get; set; }
    public string Director { get; set; }
    public string Producer { get; set; }
    public TimeSpan Duration { get; set; }
}