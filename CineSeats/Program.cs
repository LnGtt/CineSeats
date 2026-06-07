using CineSeats;
using CineSeats.Catalogue.Application.IUseCases.Admin_IUseCases;
using CineSeats.Catalogue.Application.IUseCases.Movie_IUseCases;
using CineSeats.Catalogue.Application.IUseCases.Room_IUseCases;
using CineSeats.Catalogue.Application.IUseCases.Session_IUseCases;
using CineSeats.Catalogue.Application.Use_Cases.Admin_Use_Cases;
using CineSeats.Catalogue.Application.Use_Cases.Movie_Use_Cases;
using CineSeats.Catalogue.Application.Use_Cases.Room_Use_Cases;
using CineSeats.Catalogue.Application.Use_Cases.Session_Use_Cases;
using CineSeats.Catalogue.Domain.IRepositories;
using CineSeats.Catalogue.Infrastructure.Repositories;
using CineSeats.Infrastructure;
using CineSeats.Tickets.Application.IUseCases.Integration_IUseCases;
using CineSeats.Tickets.Application.IUseCases.Order_IUseCases;
using CineSeats.Tickets.Application.IUseCases.SessionSeat_IUseCases;
using CineSeats.Tickets.Application.Use_Cases.Order_Use_Cases;
using CineSeats.Tickets.Application.Use_Cases.SessionSeat_Use_Cases;
using CineSeats.Tickets.Domain.IRepositories;
using CineSeats.Tickets.Infrastructure.Integration;
using CineSeats.Tickets.Infrastructure.Repositories;
using CineSeats.Tickets.Domain.IServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//INJEÇÃO DE DEPENDÊNCIA REPOSITORIES --------------------------------
//CATALOGUE===========================================================
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
//TICKETS=============================================================
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ISessionSeatRepository, SessionSeatRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//--------------------------------------------------------------------
//INJEÇÃO DE DEPENDÊNCIA USE CASES -----------------------------------
//CATALOGUE===========================================================
builder.Services.AddScoped<IAddAdminUseCase, AddAdminUseCase>();
builder.Services.AddScoped<IUpdateAdminUseCase, UpdateAdminUseCase>();
builder.Services.AddScoped<IAddMovieUseCase, AddMovieUseCase>();
builder.Services.AddScoped<IDeleteMovieUseCase, DeleteMovieUseCase>();
builder.Services.AddScoped<IGetMovieOrMoviesUseCase, GetMovieOrMoviesUseCase>();
builder.Services.AddScoped<IUpdateMovieUseCase, UpdateMovieUseCase>();
builder.Services.AddScoped<IAddRoomUseCase, AddRoomUseCase>();
builder.Services.AddScoped<IDeleteRoomUseCase, DeleteRoomUseCase>();
builder.Services.AddScoped<IGetRoomOrRoomsUseCase, GetRoomOrRoomsUseCase>();
builder.Services.AddScoped<IUpdateRoomUseCase, UpdateRoomUseCase>();
builder.Services.AddScoped<IAddSessionUseCase, AddSessionUseCase>();
builder.Services.AddScoped<IDeleteSessionUseCase, DeleteSessionUseCase>();
builder.Services.AddScoped<IGetSessionOrSessionsUseCase, GetSessionOrSessionsUseCase>();
builder.Services.AddScoped<IUpdateSessionUseCase, UpdateSessionUseCase>();
//TICKETS=============================================================
builder.Services.AddScoped<ICatalogueService, CatalogueService>();
builder.Services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();
builder.Services.AddScoped<IGetSessionSeatsUseCase, GetSessionSeatsUseCase>();
//--------------------------------------------------------------------
//pagSeguro
builder.Services.AddHttpClient<IPaymentService, PagSeguroService>();
builder.Services.AddScoped<IQrCodeService, QrCodeService>();

//builder.Services.AddControllers(); Parece que é importante???????

var postgreConnectionString = builder.Configuration.GetConnectionString("PostgreConnection");
builder.Services.AddDbContext<Context_Post>(options =>
    options.UseNpgsql(postgreConnectionString)
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request app aleatorio 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

//app.MapControllers(); ????

app.Run();

namespace CineSeats
{
    
}