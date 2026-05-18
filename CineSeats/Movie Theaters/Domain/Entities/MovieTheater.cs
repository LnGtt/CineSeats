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
    
    public MovieTheater(string name, List<Room> rooms, EmailVO emailAddress, PasswordVO password)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome do cinema não pode ser vazio.");

        if (rooms == null || rooms.Count == 0)
            throw new ArgumentException("O cinema deve possuir pelo menos uma sala cadastrada.");

        Id = Guid.NewGuid();
        Name = name;
        Rooms = rooms;
        EmailAddress = emailAddress ?? throw new ArgumentNullException(nameof(emailAddress));
        Password = password ?? throw new ArgumentNullException(nameof(password));
    }
}