namespace CineSeats.Infrastructure.Models.ModelsPost;

public class SessionSeat
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public string SeatNumber { get; set; } = string.Empty; 
    public string Status { get; set; } = "Available"; 
    public DateTime? ReservedUntil { get; set; }


    public Session Session { get; set; } = null;
}