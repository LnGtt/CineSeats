namespace CineSeats.Catalogue.Application.DTOs.Admin_DTOs;

public class UpdateAdminPasswordRequest
{
    public Guid Id { get; set; }
    public string Password { get; set; }
}