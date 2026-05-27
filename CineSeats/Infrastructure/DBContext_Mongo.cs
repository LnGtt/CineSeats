using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using CineSeats.Catalogue.Domain.Entities;

namespace CineSeats.Infrastructure;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using CineSeats.Movie_Theaters.Domain.Entities;

public class Context_Mongo
{
    private readonly IMongoDatabase _database;

    public Context_Mongo(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoConnection");
        var client = new MongoClient(connectionString);
        
        var databaseName = configuration.GetSection("MongoSettings:DatabaseName").Value ?? "CineSeatsCatalog";
        _database = client.GetDatabase(databaseName);
    }
    
    public IMongoCollection<Movie> Movies => _database.GetCollection<Movie>("Movies");
    
    public IMongoCollection<MovieTheater> MovieTheaters => _database.GetCollection<MovieTheater>("MovieTheaters");
}