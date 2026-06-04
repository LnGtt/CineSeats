namespace CineSeats.Tickets.Application.DTOs.Ticket_DTOs;

public class TicketResponse
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public string SeatNumber { get; set; }
    public decimal Price { get; set; }
    public string? QrCodeString { get; set; }
}