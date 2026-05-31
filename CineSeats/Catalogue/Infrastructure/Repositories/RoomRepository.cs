using CineSeats.Catalogue.Domain.Entities;
using CineSeats.Catalogue.Domain.IRepositories;
using CineSeats.Infrastructure;

namespace CineSeats.Catalogue.Infrastructure.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly Context_Post _database;
    public RoomRepository(Context_Post database)
    {
        _database = database;
    }
    
    public async Task AddRoom(Room room)
    {
        await _database.Rooms.AddAsync(room);
        await _database.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<Room>> GetRoomsByCinema(Guid cinemaId)
    {
        return await _database.Rooms
            .Where(room => room.CinemaId == cinemaId)
            .ToListAsync();
    }
    
    public async Task<Room> GetRoomById(Guid id, Guid cinemaId)
    {
        return await _database.Rooms
            .FirstOrDefaultAsync(room => room.Id == id && room.CinemaId == cinemaId);
    }
    
    public async Task UpdateRoom(Room room)
    {
        _database.Rooms.Update(room);
        await _database.SaveChangesAsync();
    }

    public async Task DeleteRoom(Room room)
    {
        _database.Rooms.Remove(room);
        await _database.SaveChangesAsync();
    }
}