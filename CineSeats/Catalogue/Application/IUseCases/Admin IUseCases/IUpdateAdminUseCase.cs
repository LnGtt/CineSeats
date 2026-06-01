using CineSeats.Catalogue.Application.DTOs.Admin_DTOs;

namespace CineSeats.Catalogue.Application.IUseCases.Admin_IUseCases;

public interface IUpdateAdminUseCase
{
    Task Run(UpdateAdminNameRequest updateAdminNameRequest);
    Task Run(UpdateAdminEmailRequest updateAdminEmailRequest);
    Task Run(UpdateAdminPasswordRequest updateAdminPasswordRequest);
}