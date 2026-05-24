namespace CineSeats.Catalogue.Application.DTOs.Movie_DTOs;

public class GetMovieSummaryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string AgeRestriction { get; set; }
    public TimeSpan Duration { get; set; }
    public List<string> Genres { get; set; }
}