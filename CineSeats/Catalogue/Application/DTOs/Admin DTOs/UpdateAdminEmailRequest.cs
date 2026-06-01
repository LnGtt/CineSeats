using CineSeats.Catalogue.ValueObject;

namespace CineSeats.Catalogue.Application.DTOs.Admin_DTOs;

public class UpdateAdminEmailRequest
{
    public EmailVO Email { get; set; }
}