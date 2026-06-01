namespace CineSeats.Catalogue.Application.DTOs.Movie_DTOs;

public class UpdateMovieScheduleRequest
{
    public Guid Id { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}