namespace CineSeats.Movie_Theaters.Domain.Entities;

public class Room
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public List<int> Seats { get; set; }

    
}