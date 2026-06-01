using CineSeats.Catalogue.Application.DTOs.Admin_DTOs;
using CineSeats.Catalogue.Application.IUseCases.Admin_IUseCases;
using CineSeats.Catalogue.Domain.Entities;
using CineSeats.Catalogue.Domain.IRepositories;
using CineSeats.Catalogue.ValueObject;

namespace CineSeats.Catalogue.Application.Use_Cases.Admin_Use_Cases;

public class AddAdminUseCase : IAddAdminUseCase
{
    private readonly IAdminRepository _adminRepository;

    public AddAdminUseCase(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }
    
    public async Task Run(AddAdminRequest addAdminRequest)
    {
        var emailVO = new EmailVO(addAdminRequest.EmailAddress);
        var passwordVO = new PasswordVO(addAdminRequest.Password);
        
        var admin = new Admin(addAdminRequest.Name, emailVO, passwordVO);
        
        await _adminRepository.AddAdmin(admin);
    }
}