namespace CineSeats.Tickets.Application.DTOs.SessionSeat_DTOs;

public class SessionSeatResponse
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public string SeatNumber { get; set; }
    
    // IMPORTANTE: Transforme o Enum num texto fácil para o Front-end ler
    // O front-end fará um if: se for "Available" (Verde), "Reserved" (Amarelo), "Sold" (Cinza)
    public string Status { get; set; }
}