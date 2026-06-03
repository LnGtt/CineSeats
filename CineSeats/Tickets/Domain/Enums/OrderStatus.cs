namespace CineSeats.Tickets.Domain.Enums;

public enum OrderStatus
{
    Pending = 0,         // O cliente está escolhendo os lugares
    AwaitingPayment = 1, // O cliente foi para o Mercado Pago
    Paid = 2,            // O webhook do Mercado Pago confirmou
    Cancelled = 3       // O tempo expirou ou o pagamento foi recusado
}