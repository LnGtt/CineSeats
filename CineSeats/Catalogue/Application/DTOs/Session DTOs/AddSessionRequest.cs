namespace CineSeats.Catalogue.Application.DTOs.Session_DTOs;

public class AddSessionRequest
{
    public Guid MovieId { get; set; }
    public Guid RoomId { get; set; }
    public string Description { get; set; }
    public TimeOnly? StartTime { get; set; }
    public decimal TicketPrice { get; set; }
}