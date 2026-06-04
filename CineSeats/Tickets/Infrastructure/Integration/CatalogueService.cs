using CineSeats.Infrastructure;
using CineSeats.Tickets.Application.IUseCases.Integration_IUseCases;
using Microsoft.EntityFrameworkCore;

namespace CineSeats.Tickets.Infrastructure.Integration;

public class CatalogueService : ICatalogueService
{
    private readonly Context_Post _context;

    public CatalogueService(Context_Post context)
    {
        _context = context;
    }
    
    public async Task<decimal> GetSessionPrice(Guid sessionId)
    {
        var session = await _context.SessionsCatalogo
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == sessionId);

        if (session == null)
            throw new Exception("The requested session was not found in catalogue");

        return session.TicketPrice; 
    }
}