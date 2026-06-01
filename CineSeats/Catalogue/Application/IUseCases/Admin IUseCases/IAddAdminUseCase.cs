using CineSeats.Catalogue.Application.DTOs.Admin_DTOs;

namespace CineSeats.Catalogue.Application.IUseCases.Admin_IUseCases;

public interface IAddAdminUseCase
{
    Task Run(AddAdminRequest addAdminRequest);
}