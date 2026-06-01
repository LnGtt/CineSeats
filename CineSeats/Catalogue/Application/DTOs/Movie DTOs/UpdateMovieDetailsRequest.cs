namespace CineSeats.Catalogue.Application.DTOs.Movie_DTOs;

public class UpdateMovieDetailsRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int DurationMinutes { get; set; }
}