namespace CineSeats.Tickets.Domain.IRepositories;

public interface IUnitOfWork
{
    Task<bool> Commit();
}