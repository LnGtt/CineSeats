using CineSeats.Catalogue.Domain.Entities;
namespace CineSeats.Catalogue.Application.DTOs.MovieTheaters_DTOs;

public class CreateMovieTheaterRequest
{
    public string Name { get; set; }
    public List<Room> Rooms { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}