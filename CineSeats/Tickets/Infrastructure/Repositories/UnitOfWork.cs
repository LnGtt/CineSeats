using CineSeats.Infrastructure;
using CineSeats.Tickets.Domain.IRepositories;

namespace CineSeats.Tickets.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly Context_Post _context;

    public UnitOfWork(Context_Post context)
    {
        _context = context;
    }

    public async Task<bool> Commit()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}