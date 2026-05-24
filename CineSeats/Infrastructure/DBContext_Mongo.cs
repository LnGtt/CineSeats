using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using CineSeats.Catalogue.Domain.Entities;

namespace CineSeats.Infrastructure;

public class Context_Mongo : DbContext
{
    public DbSet<Movie> Movies;
    public DbSet<Room> Rooms;

    public Context_Mongo(DbContextOptions<Context_Mongo> options) : base(options)
    {
    }

        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 

        modelBuilder.Entity<Movie>()
            .HasKey(M => M.Id);
    }
}