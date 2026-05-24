using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using CineSeats.Movie_Theaters.Domain.Entities;

namespace CineSeats.Infrastructure;

public class Context_Post : DbContext
{
    public DbSet<MovieTheater> MovieTheaters { get; set; } 

    public Context_Post(DbContextOptions<Context_Post> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Boa prática incluir a chamada base

        modelBuilder.Entity<MovieTheater>()
            .HasKey(Movie => Movie.Id);
    }
}