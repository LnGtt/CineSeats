using CineSeats.Catalogue.Domain.Entities;

namespace CineSeats.Catalogue.Domain.IRepositories;

public interface IRoomRepository
{
    Task AddRoom(Room room);
    Task<IEnumerable<Room>> GetRoomsByCinema(Guid cinemaId);
    Task<Room> GetRoomById(Guid id, Guid cinemaId);
    Task UpdateRoom(Room room);
    Task DeleteRoom(Room room);
}