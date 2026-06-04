namespace CineSeats.Tickets.Domain.IServices;

public interface IEmailService
{
    /// <summary>
    /// Envia o e-mail com o QR Code do ingresso embutido.
    /// </summary>
    Task SendTicketEmailAsync(string toEmail, string customerName, string qrCodeBase64, string seatNumber);
}