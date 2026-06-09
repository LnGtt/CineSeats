using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using CineSeats.Tickets.Domain.Entities;
using CineSeats.Tickets.Domain.IServices;
using Microsoft.Extensions.Configuration;

namespace CineSeats.Tickets.Infrastructure.Integration;

public class PagSeguroService : IPaymentService
{
    private readonly HttpClient _httpClient;
    private readonly string _token;
    private readonly string _baseUrl;
    
    // Injetamos o IConfiguration para ler os dados do appsettings.json
    public PagSeguroService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _token = configuration["PagSeguro:Token"] ?? "MOCK_TOKEN";
        _baseUrl = configuration["PagSeguro:Url"] ?? "https://sandbox.api.pagseguro.com";
    }
    
    public async Task<(string PaymentUrl, string TransactionId)> CreatePaymentAsync(Order order)
    {
        
        
        await Task.Delay(500);
        
        string mockTransactionId = $"MOCK_PAGS_{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        string mockPaymentUrl = "https://localhost:3000/sucesso?status=paid";
        
       return (mockPaymentUrl, mockTransactionId);
        
        //  var checkoutRequest = new
        //  {
        //      reference_id = order.Id.ToString(),
        //      customer = new
        //      {
        //          name = "Cliente CineSeats",
        //          email = "cliente@sandbox.pagseguro.com.br", // Mudado para e-mail padrão aceito no Sandbox
        //          tax_id = "12345678909"
        //      },
        //      items = order.Tickets.Select(ticket => new
        //      {
        //          reference_id = ticket.Id.ToString(),
        //          name = $"CineSeats - Assento {ticket.SeatNumber}",
        //          quantity = 1,
        //          unit_amount = (int)(ticket.Price * 100) // Transforma em centavos
        //      }).ToList(),
        //      redirect_url = "https://localhost:3000/sucesso",
        //      notification_urls = new[] { "https://sua-api.loca.lt/api/payments/pagseguro-webhook" }
        //  };
        //
        // // Monta a URL dinamicamente usando a do appsettings
        // var requestUrl = $"{_baseUrl.TrimEnd('/')}/checkouts";
        // var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUrl);
        //
        // // Forma limpa e recomendada pelo .NET para definir o cabeçalho Authorization Bearer
        // requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        // requestMessage.Headers.Add("accept", "application/json");
        //
        // var json = JsonSerializer.Serialize(checkoutRequest);
        // requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
        //
        // var response = await _httpClient.SendAsync(requestMessage);
        //
        // if (!response.IsSuccessStatusCode)
        // {
        //     var errorResponseBody = await response.Content.ReadAsStringAsync();
        //     throw new Exception($"Erro PagSeguro: {errorResponseBody}");
        // }
        //
        // var responseContent = await response.Content.ReadAsStringAsync();
        // using var doc = JsonDocument.Parse(responseContent);
        // var root = doc.RootElement;
        //
        // string transactionId = root.GetProperty("id").GetString() ?? "";
        // string paymentUrl = root.GetProperty("links").EnumerateArray()
        //     .First(l => l.GetProperty("rel").GetString() == "PAY").GetProperty("href").GetString() ?? "";
        //
        // return (paymentUrl, transactionId);
    }
}