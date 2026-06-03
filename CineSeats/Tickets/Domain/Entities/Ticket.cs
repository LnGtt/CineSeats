namespace CineSeats.Tickets.Domain.Entities;

public class Ticket
{
    /*public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public string SeatNumber { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty; // ID do usuário que comprou
    public DateTime PurchasedAt { get; set; }
    public string ReservationCode { get; set; } = string.Empty; // Código do ingresso (QR Code)

    public Session Session { get; set; } = null!;*/
    
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid SessionId { get; private set; }
    public string SeatNumber { get; private set; }
    public decimal Price { get; private set; }
    public string? QrCodeString { get; private set; }
    
    protected Ticket() { }
    
    internal Ticket(Guid orderId, Guid sessionId, string seatNumber, decimal price)
    {
        if (orderId == Guid.Empty)
            throw new ArgumentException("Order id cannot be empty");
        
        if (sessionId == Guid.Empty)
            throw new ArgumentException("Session id cannot be empty");
        
        if (string.IsNullOrWhiteSpace(seatNumber))
            throw new ArgumentException("SeatNumber cannot be null or whitespace");
        
        if (price <= 0)
            throw new ArgumentException("Price cannot be zero or negative");
        
        Id = Guid.NewGuid();
        OrderId = orderId;
        SessionId = sessionId;
        SeatNumber = seatNumber;
        Price = price;
    }
    
    public void AssignQrCode(string qrCode)
    {
        if (string.IsNullOrWhiteSpace(qrCode))
            throw new ArgumentException("QrCode string cannot be null or whitespace");

        QrCodeString = qrCode;
    }
}