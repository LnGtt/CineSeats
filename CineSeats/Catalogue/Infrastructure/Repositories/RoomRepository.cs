using CineSeats.Catalogue.Domain.Entities;
using CineSeats.Catalogue.Domain.IRepositories;
using CineSeats.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CineSeats.Catalogue.Infrastructure.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly Context_Post _context;
    public RoomRepository(Context_Post context)
    {
        _context = context;
    }
    
    public async Task AddRoom(Room room)
    {
        await _context.Room.AddAsync(room);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Room>> GetRooms()
    {
        return await _context.Room.ToListAsync();
    }

    public async Task<Room> GetRoomById(Guid id)
    {
        return await _context.Room.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task UpdateRoom(Room room)
    {
        _context.Room.Update(room);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRoom(Room room)
    {
        _context.Room.Remove(room);
        await _context.SaveChangesAsync();
    }
}