namespace CineSeats.Tickets.Domain.Entities;

public class Ticket
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid SessionId { get; private set; }
    public string SeatNumber { get; private set; } //Método para mudar não deve existir, quebraria concorrência
    public decimal Price { get; private set; } //Por motivos de segurança, não deve haver métodos para mudar preço, o ticket deve ser cancelado por completo e criado outro
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