using CineSeats.Tickets.Domain.Enums;
using CineSeats.Tickets.Value_Object;


namespace CineSeats.Tickets.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public CustomerEmailVO CustomerEmail { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string? MercadoPagoId { get; private set; } //Id da transação do mercado pago
    public OrderStatus Status { get; private set; }
    
    private readonly List<Ticket> _tickets = new();
    public IReadOnlyCollection<Ticket> Tickets => _tickets.AsReadOnly();
    public decimal TotalPrice => _tickets.Sum(t => t.Price);
    
    protected Order() { }
    
    public Order(CustomerEmailVO customerEmail)
    {
        if (customerEmail == null)
            throw new ArgumentNullException(nameof(customerEmail), "The customer email cannot be null");
        
        Id = Guid.NewGuid();
        CustomerEmail = customerEmail;
        CreatedAt = DateTime.UtcNow;
        Status = OrderStatus.Pending;
    }
    
    public void AddTicket(Guid sessionId, string seatNumber, decimal price)
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Cannot change tickets on pending order");

        var ticket = new Ticket(Id, sessionId, seatNumber, price);
        _tickets.Add(ticket);
    }

    public void SetAsAwaitingPayment(string mercadoPagoId)
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Order status is not 'pending'");
        
        if (!_tickets.Any())
            throw new InvalidOperationException("An order needs at least one ticket");

        MercadoPagoId = mercadoPagoId;
        Status = OrderStatus.AwaitingPayment;
    }

    public void MarkAsPaid()
    {
        if (Status != OrderStatus.AwaitingPayment)
            throw new InvalidOperationException("Only 'AwaitingPayment' Orders can be paid'");

        Status = OrderStatus.Paid;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Paid)
            throw new InvalidOperationException("Cannot cancel paid order");

        Status = OrderStatus.Cancelled;
    }
}