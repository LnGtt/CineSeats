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
        // Pega a string de conexão configurada no appsettings.json
        var connectionString = configuration.GetConnectionString("MongoConnection");
        var client = new MongoClient(connectionString);
        
        // Pega o nome do banco de dados do appsettings
        var databaseName = configuration.GetSection("MongoSettings:DatabaseName").Value ?? "CineSeatsCatalog";
        _database = client.GetDatabase(databaseName);
    }

    // Em vez de DbSet, o Mongo usa IMongoCollection
    public IMongoCollection<Movie> Movies => _database.GetCollection<Movie>("Movies");
    
    // Você já pode deixar aqui a coleção para o seu caso de uso de cinemas
    public IMongoCollection<MovieTheater> MovieTheaters => _database.GetCollection<MovieTheater>("MovieTheaters");
}