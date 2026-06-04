using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using CineSeats.Catalogue.ValueObject;
using CineSeats.Tickets.Domain.Entities;

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
    public DbSet<CineSeats.Tickets.Domain.Entities.Order> Orders { get; set; }
    public DbSet<CineSeats.Tickets.Domain.Entities.Ticket> Tickets { get; set; }
    public DbSet<CineSeats.Tickets.Domain.Entities.SessionSeat> SessionSeats { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // CONFIGURAÇÕES DE TICKETS
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.OwnsOne(o => o.CustomerEmail, email =>
            {
                email
                    .Property(e =>
                        e.EmailAddress) 
                    .HasColumnName("CustomerEmail")
                    .IsRequired();
            });

            entity.Property(o => o.CreatedAt).IsRequired();
            entity.Property(o => o.MercadoPagoId).IsRequired(false);
            entity.Property(o => o.Status).HasConversion<int>().IsRequired();

            entity.HasMany(o => o.Tickets)
                .WithOne()
                .HasForeignKey(t => t.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Navigation(o => o.Tickets).Metadata.SetField("_tickets");
       });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.SessionId).IsRequired();
            entity.Property(t => t.SeatNumber).IsRequired();
            entity.Property(t => t.Price).HasColumnType("decimal(18,2)").IsRequired();
            entity.Property(t => t.QrCodeString).IsRequired(false);
        });
        
        modelBuilder.Entity<SessionSeat>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.SessionId).IsRequired();
            entity.Property(s => s.SeatNumber).IsRequired();
            entity.Property(s => s.Status).HasConversion<int>().IsRequired();
            entity.Property(s => s.ReservedUntil).IsRequired(false);
            
            entity.Property(s => s.Version)
                .IsConcurrencyToken()
                .IsRequired();
            
            // Cria um índice único para garantir que não existam duas linhas pro mesmo assento na mesma sessão
            entity.HasIndex(s => new { s.SessionId, s.SeatNumber }).IsUnique();
        });


// CONFIGURAÇÕES DE CATÁLOGO
        // ==========================================
        modelBuilder.Entity<CineSeats.Catalogue.Domain.Entities.Admin>().HasKey(a => a.Id);
        
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