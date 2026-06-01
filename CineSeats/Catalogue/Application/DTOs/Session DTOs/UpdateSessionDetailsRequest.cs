namespace CineSeats.Catalogue.Application.DTOs.Session_DTOs;

public class UpdateSessionDetailsRequest
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public decimal TicketPrice { get; set; }
}