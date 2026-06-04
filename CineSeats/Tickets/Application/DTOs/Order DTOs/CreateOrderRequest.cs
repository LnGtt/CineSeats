namespace CineSeats.Tickets.Application.DTOs.Order_DTOs;

public class CreateOrderRequest
{
    public string CustomerEmail { get; set; } 
    public Guid SessionId { get; set; }
    public List<string> SeatNumbers { get; set; } = new();
}