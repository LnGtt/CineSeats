using CineSeats;
using CineSeats.Catalogue.Application.IUseCases.Movie_IUseCases;
using CineSeats.Catalogue.Application.Use_Cases.Movie_Use_Cases;
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
//Scoped Use Cases
builder.Services.AddScoped<IAddMovieUseCase, AddMovieUseCase>();
builder.Services.AddScoped<IDeleteMovieUseCase, DeleteMovieUseCase>();
builder.Services.AddScoped<IGetMovieDetailUseCase, GetMovieDetailUseCase>();
builder.Services.AddScoped<IListMoviesUseCase, ListMoviesUseCase>();
builder.Services.AddScoped<IUpdateMovieUseCase, UpdateMovieUseCase>();

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