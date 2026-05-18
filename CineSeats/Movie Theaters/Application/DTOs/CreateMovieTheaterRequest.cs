using CineSeats.Movie_Theaters.Domain.Entities;

namespace CineSeats.Movie_Theaters.Application.DTOs;

public class CreateMovieTheaterRequest
{
    public string Name { get; set; }
    public List<Room> Rooms { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}