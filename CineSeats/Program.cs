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


app.Run();

namespace CineSeats
{
    
}