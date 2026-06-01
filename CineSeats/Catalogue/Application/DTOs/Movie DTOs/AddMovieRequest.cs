namespace CineSeats.Catalogue.Application.DTOs.Movie_DTOs;

public class AddMovieRequest
{
    public string Title { get; set; }
    public int DurationMinutes { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}