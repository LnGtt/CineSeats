using CineSeats.Catalogue.Domain.Entities;
using CineSeats.Catalogue.Domain.IRepositories;
using CineSeats.Infrastructure;

namespace CineSeats.Catalogue.Infrastructure.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly Context_Post _context;

    public AdminRepository(Context_Post context)
    {
        _context = context;
    }
    
    public async Task AddAdmin(Admin admin)
    {
        await _context.Admin.AddAsync(admin);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAdmin(Admin admin)
    {
        _context.Admin.Update(admin);
        await _context.SaveChangesAsync();
    }
}