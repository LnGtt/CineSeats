namespace CineSeats.Infrastructure.Models.ModelsPost;

public class Movie
{
    public Guid Id { get; private set; }
    public Guid CinemaId { get; private set; }
    public string Name { get; private set; }
    public string AgeRestriction { get; private set; }
    public string Synopsis { get; private set; }
    public string Director { get; private set; }
    public string Producer { get; private set; }
    public TimeSpan Duration { get; private set; }
}