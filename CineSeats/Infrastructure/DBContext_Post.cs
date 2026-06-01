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
    public DbSet<CineSeats.Catalogue.Domain.Entities.Room> Room { get; set; } = null!;
    public DbSet<CineSeats.Catalogue.Domain.Entities.Movie> Movie { get; set; } = null!;
    public DbSet<CineSeats.Catalogue.Domain.Entities.Session> SessionsCatalogo { get; set; } = null!;
    public DbSet<CineSeats.Catalogue.Domain.Entities.Admin> Admin { get; set; } = null!;
        
    
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
        
        modelBuilder.Entity<CineSeats.Catalogue.Domain.Entities.Admin>().HasKey(a => a.Id);
        
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
        modelBuilder.Entity<CineSeats.Catalogue.Domain.Entities.Admin>(entity =>
        {
            entity.HasKey(a => a.Id);
    
            
            entity.OwnsOne(a => a.EmailAddress, email =>
            {
                
                email.Property(e => e.EmailAddress)  
                    .HasColumnName("AdminEmail")
                    .IsRequired();
            });
            
            entity.OwnsOne(a => a.Password, pwd =>
            {
                
                pwd.Property(p => p.Password)  
                    .HasColumnName("AdminPasswordHash")
                    .IsRequired();
                //.OnDelete(DeleteBehavior.Cascade);
            });   
        });
        modelBuilder.Entity<CineSeats.Catalogue.Domain.Entities.Session>(entity =>
        {
            entity.HasKey(s => s.Id);
            
            entity.Property(s => s.TicketPrice)
                .HasPrecision(18, 2); 
        });
        modelBuilder.Entity<CineSeats.Catalogue.Domain.Entities.Room>(entity =>
        {
            entity.HasKey(r => r.Id);
    
            entity.Property(r => r.RoomNumber)
                .IsRequired();

            
            entity.OwnsMany(r => r.Layout, builder =>
            {
                builder.ToJson(); 
            });
        });
    }
}