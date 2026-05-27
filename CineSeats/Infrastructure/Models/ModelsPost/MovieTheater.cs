using CineSeats.Movie_Theaters.ValueObject;

namespace CineSeats.Infrastructure.Models.ModelsPost;

public class MovieTheater
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Room> Rooms { get; set; }
    public EmailVO EmailAddress { get; set; } //Value Object
    public PasswordVO Password { get; set; } //Value Object
}