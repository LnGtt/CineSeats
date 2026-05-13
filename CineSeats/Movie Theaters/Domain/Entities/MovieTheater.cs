namespace CineSeats.Movie_Theaters.Domain.Entities;
using CineSeats.Movie_Theaters.Domain.Entities;
using CineSeats.Movie_Theaters.ValueObject;


public class MovieTheater
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Room> Rooms { get; set; }
    public EmailVO EmailAddress { get; set; } //Value Object
    public PasswordVO Password { get; set; } //Value Object

    public MovieTheater(string name, List<Room> rooms)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException("name");
        }
        
        if (rooms == null) 
        {
            throw new ArgumentNullException("rooms");
        }
        this.Name = name;
        this.Rooms = rooms;
    }
}