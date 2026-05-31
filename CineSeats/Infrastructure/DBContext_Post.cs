using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using CineSeats.Catalogue.ValueObject;
namespace CineSeats.Infrastructure;

public class Context_Post : DbContext
{

    public Context_Post(DbContextOptions<Context_Post> options) : base(options)
    {
    }
    
    // DEFINIÇÃO PARA O CATÁLOGO (Catalogue)
    // ==========================================
    public DbSet<CineSeats.Catalogue.Domain.Entities.MovieTheater> MovieTheaters { get; set; } 
    public DbSet<CineSeats.Catalogue.Domain.Entities.Room> Room { get; set; } = null!;
    public DbSet<CineSeats.Catalogue.Domain.Entities.Movie> Movie { get; set; } = null!;
    public DbSet<CineSeats.Catalogue.Domain.Entities.Session> Sessionss { get; set; } = null!;
        
    
    // DEFINIÇÃO PARA OS TICKETS (Tickets)
    // ==========================================
    
    public DbSet<CineSeats.Tickets.Domain.Entities.Session> Sessions { get; set; } = null!;
    public DbSet<CineSeats.Tickets.Domain.Entities.SessionSeat> SessionSeats { get; set; } = null!;
    public DbSet<CineSeats.Tickets.Domain.Entities.tickets> Tickets { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 
        
        // CONFIGURAÇÕES DE TICKETS
        // ==========================================
        modelBuilder.Entity<CineSeats.Tickets.Domain.Entities.Session>().HasKey(s => s.Id);
        modelBuilder.Entity<CineSeats.Tickets.Domain.Entities.SessionSeat>().HasKey(ss => ss.Id);
        modelBuilder.Entity<CineSeats.Tickets.Domain.Entities.tickets>().HasKey(t => t.Id);
        
        // Relacionamento 1 para Muitos: Uma Sessão tem muitos Assentos
        modelBuilder.Entity<CineSeats.Tickets.Domain.Entities.SessionSeat>()
            .HasOne(ss => ss.Session)
            .WithMany(s => s.Seats)
            .HasForeignKey(ss => ss.SessionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<CineSeats.Tickets.Domain.Entities.tickets>()
            .HasIndex(t => new { t.SessionId, t.SeatNumber })
            .IsUnique();
        // CONFIGURAÇÕES DE CATÁLOGO
        // ==========================================
        modelBuilder.Entity<CineSeats.Catalogue.Domain.Entities.MovieTheater>(entity =>
        {
            entity.HasKey(mt => mt.Id);
            
            entity.OwnsOne(mt => mt.EmailAddress, email =>
            {
                email.Property(e => e.EmailAddress) 
                    .HasColumnName("Email")
                    .IsRequired();
            });
            
            entity.OwnsOne(mt => mt.Password, pwd =>
            {
                pwd.Property(p => p.Password) 
                    .HasColumnName("PasswordHash")
                    .IsRequired();
            });
            
            entity.HasMany(mt => mt.Rooms)
                .WithOne() 
                .HasForeignKey("MovieTheaterId") 
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        // // Configuração da Sala do Catálogo
        // modelBuilder.Entity<CineSeats.Catalogue.Domain.Entities.Room>(entity =>
        // {
        //     entity.HasKey(r => r.Id);
        //     
        //     entity.Property(r => r.Seats)
        //         .HasColumnType("integer[]");
        // });

        
    }
}