using CineSeats.Catalogue.Domain.Entities;

namespace CineSeats.Catalogue.Domain.IRepositories;

public interface IRoomRepository
{
    Task AddRoom(Room room);
    Task<IEnumerable<Room>> GetRooms();
    Task<Room> GetRoomById(Guid id);
    Task UpdateRoom(Room room);
    Task DeleteRoom(Room room);
}