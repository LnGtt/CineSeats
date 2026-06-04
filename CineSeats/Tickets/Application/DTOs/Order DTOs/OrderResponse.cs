using CineSeats.Tickets.Application.DTOs.Ticket_DTOs;

namespace CineSeats.Tickets.Application.DTOs.Order_DTOs;

public class OrderResponse
{
    public Guid Id { get; set; }
    public string CustomerEmail { get; set; } 
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } 
    public string? MercadoPagoId { get; set; }
    public decimal TotalPrice { get; set; } 
    public List<TicketResponse> Tickets { get; set; } = new();
}