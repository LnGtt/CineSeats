namespace CineSeats.Tickets.Domain.IServices;

public interface IQrCodeService
{
    /// <summary>
    /// Transforma um texto (ex: ID do Ticket) em uma imagem QR Code no formato Base64.
    /// </summary>
    byte[] GenerateQrCode(string text);
}