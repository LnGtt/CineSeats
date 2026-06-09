using Microsoft.AspNetCore.Mvc;
using CineSeats.Tickets.Application.IUseCases.SessionSeat_IUseCases; 
using CineSeats.Tickets.Application.IUseCases.Order_IUseCases;   
using CineSeats.Tickets.Application.DTOs.Order_DTOs; 
using CineSeats.Tickets.Application.DTOs.SessionSeat_DTOs;
using CineSeats.Tickets.Application.DTOs; 

namespace CineSeats.Tickets.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    private readonly IGetSessionSeatsUseCase _getSessionSeatsUseCase;
    private readonly ICreateOrderUseCase _createOrderUseCase;

    public TicketController(
        IGetSessionSeatsUseCase getSessionSeatsUseCase,
        ICreateOrderUseCase createOrderUseCase)
    {
        _getSessionSeatsUseCase = getSessionSeatsUseCase;
        _createOrderUseCase = createOrderUseCase;
    }

    // 1. Buscar assentos da sessão
    [HttpGet("session/{sessionId}/seats")]
    public async Task<IActionResult> GetSeatsBySession(Guid sessionId)
    {
        try
        {
            // Nota: Confirme se o método no seu UseCase se chama 'Run', 'Execute' ou 'GetSessionSeats'
            var seats = await _getSessionSeatsUseCase.Run(sessionId);
            
            if (seats == null || !seats.Any())
                return NoContent();

            return Ok(seats);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Erro ao buscar assentos: {ex.Message}" });
        }
    }

    // 2. Criar a Ordem de compra/reserva (Início do fluxo de pagamento)
    [HttpPost("order")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        try
        {
            // Nota: Ajuste o nome do DTO 'CreateOrderRequest' se o seu for diferente
            var orderResponse = await _createOrderUseCase.Run(request);
            
            return Ok(new { orderId = orderResponse.Id, message = "Pedido criado com sucesso! Pronto para o pagamento." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Erro ao criar pedido: {ex.Message}" });
        }
    }
}