using CineSeats.Tickets.Domain.Enums;

namespace CineSeats.Tickets.Domain.Entities;

public class SessionSeat
{
    public Guid Id { get; private set; }
    public Guid SessionId { get; private set; } // Referência APENAS pelo ID para o Catálogo (teremos uma interface ICatalogueService)
    public string SeatNumber { get; private set; }
    public SeatStatus Status { get; private set; }
    public DateTime? ReservedUntil { get; private set; } // Expiração da reserva (10 minutos)
    
    // Propriedade para Controle de Concorrência (EF Core), Optimistic Concurrency. Substitui o Redis
    public Guid Version { get; private set; }
    
    protected SessionSeat() { }
    
    //public Session Session { get; set; } = null!;

    public SessionSeat(Guid sessionId, string seatNumber)
    {
        if (sessionId == Guid.Empty)
            throw new ArgumentException("SessionId cannot be empty");
            
        if (string.IsNullOrWhiteSpace(seatNumber))
            throw new ArgumentException("SeatNumber cannot be null or empty");

        Id = Guid.NewGuid();
        SessionId = sessionId;
        SeatNumber = seatNumber;
        Status = SeatStatus.Available;
        Version = Guid.NewGuid();
    }
    
    public void Reserve(TimeSpan duration)
    {
        if (Status == SeatStatus.Sold)
            throw new InvalidOperationException("The chosen seat is already sold");
        
        if (Status == SeatStatus.Reserved && ReservedUntil > DateTime.UtcNow)
            throw new InvalidOperationException("The chosen seat is temporarily reserved");
        
        Status = SeatStatus.Reserved;
        ReservedUntil = DateTime.UtcNow.Add(duration);
        
        // Sempre que alterar o estado, atualiza a versão para o EF Core saber que mudou
        Version = Guid.NewGuid(); 
    }

    public void ConfirmSale()
    {
        if (Status == SeatStatus.Sold)
            throw new InvalidOperationException("The seat is already sold");

        Status = SeatStatus.Sold;
        ReservedUntil = null; // Limpa o tempo, não precisa mais
        Version = Guid.NewGuid();
    }

    public void ReleaseIfExpired()
    {
        if (Status == SeatStatus.Reserved && ReservedUntil <= DateTime.UtcNow)
        {
            Status = SeatStatus.Available;
            ReservedUntil = null;
            Version = Guid.NewGuid();
        }
    }
}