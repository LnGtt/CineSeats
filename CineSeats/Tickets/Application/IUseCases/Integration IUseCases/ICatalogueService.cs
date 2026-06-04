namespace CineSeats.Tickets.Application.IUseCases.Integration_IUseCases;

public interface ICatalogueService
{
    Task<decimal> GetSessionPrice(Guid sessionId);
}