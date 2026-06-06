using CineSeats.Tickets.Domain.IServices;
using QRCoder;
namespace CineSeats.Tickets.Infrastructure.Integration;

public class QrCodeService : IQrCodeService
{
    public byte[] GenerateQrCode(string text)
    {
        // 1. Cria o gerador de dados do QR Code
        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
        
        // 2. Usa a classe específica para transformar os dados em um array de bytes PNG
        using var qrCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeBytes = qrCode.GetGraphic(20); // O número 20 é o tamanho/pixel por módulo

        return qrCodeBytes;
    }
}