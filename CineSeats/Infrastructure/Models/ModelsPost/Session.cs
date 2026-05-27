namespace CineSeats.Infrastructure.Models.ModelsPost;

public class Session
{
    public Guid Id { get; set; }
    public string RoomId { get; set; } = string.Empty; 
    public DateTime StartTime { get; set; }
    public decimal Price { get; set; }

    // Relacionamento: Uma sessão tem muitas poltronas com status
    public ICollection<SessionSeat> Seats { get; set; } = new List<SessionSeat>();
}