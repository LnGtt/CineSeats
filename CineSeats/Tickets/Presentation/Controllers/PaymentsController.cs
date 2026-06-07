using CineSeats.Tickets.Domain.Entities;
using CineSeats.Tickets.Domain.IServices;
using CineSeats.Tickets.Domain.IRepositories;
using CineSeats.Tickets.Value_Object; 
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
namespace CineSeats.Tickets.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly IOrderRepository _orderRepository;
    private readonly IQrCodeService _qrCodeService;
    
    public PaymentsController(
        IPaymentService paymentService, 
        IOrderRepository orderRepository, 
        IQrCodeService qrCodeService)
    {
        _paymentService = paymentService;
        _orderRepository = orderRepository;
        _qrCodeService = qrCodeService;
    }

    // ==========================================
    // 1. ENDPOINT DE CHECKOUT
    // ==========================================
    [HttpPost("checkout")]
    public async Task<IActionResult> CreateCheckout()
    {
        try
        {
            // Usando seu Value Object real
            var customerEmail = new CustomerEmailVO("cliente@email.com");
            var realOrder = new Order(customerEmail);

            var testSessionId = Guid.NewGuid(); 
            realOrder.AddTicket(testSessionId, "A12", 35.00m);
            realOrder.AddTicket(testSessionId, "A13", 35.00m);

            // Chama o serviço nativo do PagSeguro
            var (paymentUrl, transactionId) = await _paymentService.CreatePaymentAsync(realOrder);

            // Atualiza status local para AwaitingPayment e guarda o ID da transação
            realOrder.SetAsAwaitingPayment(transactionId);

            // PERSISTÊNCIA: Usando o seu método real AddOrder
            await _orderRepository.AddOrder(realOrder);

            return Ok(new
            {
                Message = "Checkout criado e salvo no banco!",
                OrderId = realOrder.Id,
                TransactionId = realOrder.MercadoPagoId,
                StatusAtual = realOrder.Status.ToString(),
                RedirectUrl = paymentUrl
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    // ==========================================
    // 2. ENDPOINT DO WEBHOOK (Notificação do PagSeguro)
    // ==========================================
    [HttpPost("pagseguro-webhook")]
    public async Task<IActionResult> PagSeguroWebhook([FromBody] JsonElement payload)
    {
        try
        {
            // Captura o ID da transação vindo do PagSeguro
            if (!payload.TryGetProperty("id", out var idProp))
            {
                return BadRequest("Payload inválido: ID da transação não encontrado.");
            }
            
            string transactionId = idProp.GetString() ?? "";
            
            // Captura o status do pagamento
            string paymentStatus = "";
            if (payload.TryGetProperty("status", out var statusProp))
            {
                paymentStatus = statusProp.GetString() ?? "";
            }

            // PERSISTÊNCIA: Busca a Ordem usando seu método customizado focado na transação
            var order = await _orderRepository.GetOrderByTransactionId(transactionId);
            if (order == null)
            {
                return NotFound($"Ordem com a transação {transactionId} não encontrada.");
            }

            // Se retornar status de aprovado (PAID ou código 3)
            if (paymentStatus == "PAID" || paymentStatus == "3")
            {
                // Modifica o estado do domínio para Pago
                order.MarkAsPaid();

                // Gera os QR Codes estáveis (QRCoder 1.8.0) para cada Ticket do agregado
                foreach (var ticket in order.Tickets)
                {
                    string qrContent = $"CineSeats-TicketId:{ticket.Id}-Session:{ticket.SessionId}-Seat:{ticket.SeatNumber}";
                    byte[] qrCodeBytes = _qrCodeService.GenerateQrCode(qrContent);
                    string qrCodeBase64 = Convert.ToBase64String(qrCodeBytes);
                    
                    ticket.AssignQrCode(qrCodeBase64);
                }

                // PERSISTÊNCIA: Atualiza a ordem e os tickets no PostgreSQL usando seu UpdateOrder
                await _orderRepository.UpdateOrder(order);
            }
            else if (paymentStatus == "DECLINED" || paymentStatus == "7")
            {
                order.Cancel();
                await _orderRepository.UpdateOrder(order);
            }

            return Ok(new { Message = "Notificação processada com sucesso!" });
        }
        catch (Exception ex)
        {
            return Ok(new { ErrorProcessing = ex.Message });
        }
    }
}