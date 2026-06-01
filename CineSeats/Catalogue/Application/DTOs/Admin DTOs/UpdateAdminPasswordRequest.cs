using CineSeats.Catalogue.ValueObject;

namespace CineSeats.Catalogue.Application.DTOs.Admin_DTOs;

public class UpdateAdminPasswordRequest
{
    public PasswordVO Password { get; set; }
}