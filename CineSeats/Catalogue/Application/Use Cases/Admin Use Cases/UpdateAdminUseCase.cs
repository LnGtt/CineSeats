using CineSeats.Catalogue.Application.DTOs.Admin_DTOs;
using CineSeats.Catalogue.Application.IUseCases.Admin_IUseCases;
using CineSeats.Catalogue.Domain.IRepositories;
using CineSeats.Catalogue.ValueObject;

namespace CineSeats.Catalogue.Application.Use_Cases.Admin_Use_Cases;

public class UpdateAdminUseCase : IUpdateAdminUseCase
{
    private readonly IAdminRepository _adminRepository;

    public UpdateAdminUseCase(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }
    
    public async Task Run(UpdateAdminNameRequest updateAdminNameRequest)
    {
        var admin = await _adminRepository.GetAdminById(updateAdminNameRequest.Id)
                    ?? throw new KeyNotFoundException("Admin not found");

        admin.ChangeName(updateAdminNameRequest.Name);

        await _adminRepository.UpdateAdmin(admin);
    }

    public async Task Run(UpdateAdminEmailRequest updateAdminEmailRequest)
    {
        var admin = await _adminRepository.GetAdminById(updateAdminEmailRequest.Id)
                    ?? throw new KeyNotFoundException("Admin not found");

        var newEmail = new EmailVO(updateAdminEmailRequest.Email);
        admin.ChangeEmail(newEmail);

        await _adminRepository.UpdateAdmin(admin);
    }

    public async Task Run(UpdateAdminPasswordRequest updateAdminPasswordRequest)
    {
        var admin = await _adminRepository.GetAdminById(updateAdminPasswordRequest.Id)
                    ?? throw new KeyNotFoundException("Admin not found");

        var newPassword = new PasswordVO(updateAdminPasswordRequest.Password);
        admin.ChangePassword(newPassword);

        await _adminRepository.UpdateAdmin(admin);
    }
}