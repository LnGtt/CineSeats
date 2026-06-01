namespace CineSeats.Catalogue.Application.DTOs.Session_DTOs;

public class GetSessionsResponse
{
    public Guid Id { get; set; }
    public Guid MovieId { get; set; }
    public Guid RoomId { get; set; }
    public string Description { get; set; }
    public TimeOnly? StartTime { get; set; }
    public decimal TicketPrice { get; set; }
}