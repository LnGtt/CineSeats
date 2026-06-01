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
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Scoped Repositories
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
//Scoped Use Cases
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

// configure para as usecase e repository

var postgreConnectionString = builder.Configuration.GetConnectionString("PostgreConnection");
builder.Services.AddDbContext<Context_Post>(options =>
    options.UseNpgsql(postgreConnectionString)
);




var app = builder.Build();

// Configure the HTTP request app aleatorio 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();

namespace CineSeats
{
    record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}