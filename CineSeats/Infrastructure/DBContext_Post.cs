using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using CineSeats.Infrastructure.Models.ModelsPost;
// using CineSeats.Movie_Theaters.Domain.Entities;
// using CineSeats.Tickets.Domain.Entities;
// using CineSeats.Catalogue.Domain.Entities;

namespace CineSeats.Infrastructure;

public class Context_Post : DbContext
{

    public Context_Post(DbContextOptions<Context_Post> options) : base(options)
    {
    }
    
    // Definiçao para os MovieTheaters
    public DbSet<MovieTheater> MovieTheaters { get; set; } 
    public DbSet<Room> Rooms { get; set; } = null!;
    
    // Definiçao para os Tickts
    public DbSet<Session> Sessions { get; set; } = null!;
    public DbSet<SessionSeat> SessionSeats { get; set; } = null!;
    public DbSet<tickets> Tickets { get; set; } = null!;
    
    //Definição para catalogo
    public DbSet<Room> RoomCatalogo { get; set; } = null!;
    public DbSet<Movie> Movies { get; set; } = null!;
    public DbSet<Session> SessionsCatalogo { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 
        
        // chave primária para tickts
        modelBuilder.Entity<Session>().HasKey(s => s.Id);
        modelBuilder.Entity<SessionSeat>().HasKey(ss => ss.Id);
        modelBuilder.Entity<tickets>().HasKey(t => t.Id);
        
        // Relacionamento 1 para Muitos: Uma Sessão tem muitos Assentos
        modelBuilder.Entity<SessionSeat>()
            .HasOne(ss => ss.Session)
            .WithMany(s => s.Seats)
            .HasForeignKey(ss => ss.SessionId)
            .OnDelete(DeleteBehavior.Cascade); // Se deletar a sessão, deleta os status das cadeiras
        
        
        modelBuilder.Entity<tickets>()
            .HasIndex(t => new { t.SessionId, t.SeatNumber })
            .IsUnique();
        

        modelBuilder.Entity<MovieTheater>(entity =>
        {
            entity.HasKey(mt => mt.Id);
            
            entity.OwnsOne(mt => mt.EmailAddress, email =>
            {
                email.Property<string>(e => e.EmailAddress) 
                    .HasColumnName("Email")
                    .IsRequired();
            });
            
            entity.OwnsOne(mt => mt.Password, pwd =>
            {
                pwd.Property<string>(p => p.Password) 
                    .HasColumnName("PasswordHash")
                    .IsRequired();
            });
            
            entity.HasMany(mt => mt.Rooms)
                .WithOne() 
                .HasForeignKey("MovieTheaterId") 
                .OnDelete(DeleteBehavior.Cascade);
        });
        // 1 filme para uma sala , uma sala vai ter varias sessoes, 1 filme tem varias sessoes  
        
        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(r => r.Id);
            
            entity.Property(r => r.Seats)
                .HasColumnType("integer[]");
        });
    }
}