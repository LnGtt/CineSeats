namespace CineSeats.Catalogue.Application.DTOs.Movie_DTOs;

public class UpdateMovieScheduleRequest
{
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}