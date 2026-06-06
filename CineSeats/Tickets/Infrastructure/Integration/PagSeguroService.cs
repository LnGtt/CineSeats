using System.Text;
using System.Text.Json;
using CineSeats.Tickets.Domain.Entities;
using CineSeats.Tickets.Domain.IServices;

namespace CineSeats.Tickets.Infrastructure.Integration;

public class PagSeguroService : IPaymentService
{
    private readonly HttpClient _httpClient;
    private const string Token = "SEU-TOKEN-PAGSEGURO-AQUI";
    
    public PagSeguroService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<(string PaymentUrl, string TransactionId)> CreatePaymentAsync(Order order)
    {
        var checkoutRequest = new
        {
            reference_id = order.Id.ToString(),
            customer = new
            {
                name = "Cliente CineSeats",
                email = "cliente@email.com",
                tax_id = "12345678909"
            },
            items = order.Tickets.Select(ticket => new
            {
                reference_id = ticket.Id.ToString(),
                name = $"CineSeats - Assento {ticket.SeatNumber}",
                quantity = 1,
                unit_amount = (int)(ticket.Price * 100) // Transforma em centavos
            }).ToList(),
            redirect_url = "https://localhost:3000/sucesso",
            notification_urls = new[] { "https://sua-api.loca.lt/api/payments/pagseguro-webhook" }
        };

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://sandbox.api.pagseguro.com/checkouts");
        requestMessage.Headers.Add("Authorization", $"Bearer {Token}");
        requestMessage.Headers.Add("accept", "application/json");
        
        var json = JsonSerializer.Serialize(checkoutRequest);
        requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(requestMessage);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Falha ao gerar checkout no PagSeguro");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(responseContent);
        var root = doc.RootElement;

        string transactionId = root.GetProperty("id").GetString() ?? "";
        string paymentUrl = root.GetProperty("links").EnumerateArray()
            .First(l => l.GetProperty("rel").GetString() == "PAY").GetProperty("href").GetString() ?? "";

        return (paymentUrl, transactionId);
    }
    
    
}