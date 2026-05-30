using CineSeats.Catalogue.Domain.Entities;

namespace CineSeats.Catalogue.Domain.IRepositories;

public interface IAdminRepository
{
    Task AddAdmin(Admin admin);
    Task UpdateAdmin(Admin admin);
}