using CineSeats.Tickets.Domain.Entities;

namespace CineSeats.Tickets.Domain.IServices;

public interface IPaymentService
{
    Task<(string PaymentUrl, string TransactionId)> CreatePaymentAsync(Order order);
}