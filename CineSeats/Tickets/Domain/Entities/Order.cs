using CineSeats.Tickets.Domain.Enums;

namespace CineSeats.Tickets.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public string CustomerEmail { get; private set; } //Vai virar emailVO depois
    public DateTime CreatedAt { get; private set; }
    public string? MercadoPagoId { get; private set; } //Id da transação do mercado pago
    public OrderStatus Status { get; private set; }
    
    private readonly List<Ticket> _tickets = new();
    public IReadOnlyCollection<Ticket> Tickets => _tickets.AsReadOnly();
    
    protected Order() { }
    
    public Order(string customerEmail)
    {
        //colocar validação de email depois, pois vou ter que reutilizar o emailVO do outro domínio, e pra não quebrar as coisas, vou deixar p depois
        
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