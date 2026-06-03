namespace CineSeats.Tickets.Domain.Enums;

public enum SeatStatus
{
    Available = 0, //Livre para compra
    Reserved = 1,  //Cliente está no Mercado Pago digitando o cartão
    Sold = 2       //Pagamento confirmado
}