using CineSeats.Tickets.Application.DTOs.Order_DTOs;

namespace CineSeats.Tickets.Application.IUseCases.Order_IUseCases;

public interface ICreateOrderUseCase
{
    Task<OrderResponse> Run(CreateOrderRequest request);
}