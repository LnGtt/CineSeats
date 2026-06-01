using CineSeats.Catalogue.ValueObject;

namespace CineSeats.Catalogue.Application.DTOs.Admin_DTOs;

public class AddAdminRequest
{
    public string Name { get; private set; }
    public EmailVO EmailAddress { get; private set; }
    public PasswordVO Password { get; private set; }
}