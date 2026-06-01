using CineSeats.Catalogue.Application.DTOs.Session_DTOs;

namespace CineSeats.Catalogue.Application.IUseCases.Session_IUseCases;

public interface IAddSessionUseCase
{
    Task Run(AddSessionRequest request);
}