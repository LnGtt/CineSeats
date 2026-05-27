namespace CineSeats.Infrastructure.Models.ModelsPost;

public class tickets
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public string SeatNumber { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty; // ID do usuário que comprou
    public DateTime PurchasedAt { get; set; }
    public string ReservationCode { get; set; } = string.Empty; // Código do ingresso (QR Code)

    public Session Session { get; set; } = null!;
}