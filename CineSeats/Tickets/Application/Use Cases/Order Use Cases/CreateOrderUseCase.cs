using CineSeats.Tickets.Application.DTOs.Order_DTOs;
using CineSeats.Tickets.Application.DTOs.Ticket_DTOs;
using CineSeats.Tickets.Application.IUseCases.Integration_IUseCases;
using CineSeats.Tickets.Application.IUseCases.Order_IUseCases;
using CineSeats.Tickets.Domain.Entities;
using CineSeats.Tickets.Domain.IRepositories;
using CineSeats.Tickets.Value_Object;

namespace CineSeats.Tickets.Application.Use_Cases.Order_Use_Cases;

public class CreateOrderUseCase : ICreateOrderUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly ISessionSeatRepository _sessionSeatRepository;
    private readonly ICatalogueService _catalogueService; // Ponte com o outro Bounded Context
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateOrderUseCase(
        IOrderRepository orderRepository, 
        ISessionSeatRepository sessionSeatRepository,
        ICatalogueService catalogueService,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _sessionSeatRepository = sessionSeatRepository;
        _catalogueService = catalogueService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<OrderResponse> Run(CreateOrderRequest request)
    {
        var customerEmail = new CustomerEmailVO(request.CustomerEmail);
        var selectedSeats = await _sessionSeatRepository.GetSpecificSeats(request.SessionId, request.SeatNumbers);
        
        if (selectedSeats.Count() != request.SeatNumbers.Count)
            throw new InvalidOperationException("One or more seats are invalid.");
        
        foreach (var seat in selectedSeats)
        {
            seat.Reserve(TimeSpan.FromMinutes(10));
            await _sessionSeatRepository.UpdateSessionSeat(seat); // Apenas atualiza o tracking do EF Core
        }
        
        var sessionPrice = await _catalogueService.GetSessionPrice(request.SessionId);
        var order = new Order(customerEmail);
        
        foreach (var seat in selectedSeats)
        {
            order.AddTicket(request.SessionId, seat.SeatNumber, sessionPrice);
        }
        
        await _orderRepository.AddOrder(order);
        
        var success = await _unitOfWork.Commit();
        if (!success)
            throw new Exception("Fail to save order on database");
        
        return new OrderResponse
        {
            Id = order.Id,
            CustomerEmail = order.CustomerEmail.EmailAddress,
            CreatedAt = order.CreatedAt,
            Status = order.Status.ToString(),
            TotalPrice = order.TotalPrice,
            Tickets = order.Tickets.Select(t => new TicketResponse
            {
                Id = t.Id,
                SessionId = t.SessionId,
                SeatNumber = t.SeatNumber,
                Price = t.Price,
                QrCodeString = t.QrCodeString
            }).ToList()
        };
    }
}