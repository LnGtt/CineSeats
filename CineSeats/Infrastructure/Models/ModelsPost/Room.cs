namespace CineSeats.Infrastructure.Models.ModelsPost;

public class Room
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public List<int> Seats { get; set; }
}