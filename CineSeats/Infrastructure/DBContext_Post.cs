using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using CineSeats.Movie_Theaters.Domain.Entities;
using CineSeats.Tickets.Domain.Entities;

namespace CineSeats.Infrastructure;

public class Context_Post : DbContext
{

    public Context_Post(DbContextOptions<Context_Post> options) : base(options)
    {
    }
    
    // Definiçao pras tabelas no PostgreSQL para os MovieTheaters
    public DbSet<MovieTheater> MovieTheaters { get; set; } 
    
    // Definiçao pras tabelas no PostgreSQL para ROOm
    public DbSet<Room> Rooms { get; set; } = null!;
    
    // Definiçao pras tabelas no PostgreSQL para os Tickts
    public DbSet<Session> Sessions { get; set; } = null!;
    public DbSet<SessionSeat> SessionSeats { get; set; } = null!;
    public DbSet<tickets> Tickets { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 
        
        // chave primária para Session
        modelBuilder.Entity<Session>().HasKey(s => s.Id);
        // Configuração para SessionSeat
        modelBuilder.Entity<SessionSeat>().HasKey(ss => ss.Id);
        // Configuração para Ticktes
        modelBuilder.Entity<tickets>().HasKey(t => t.Id);
        
        // Relacionamento 1 para Muitos: Uma Sessão tem muitos Assentos
        modelBuilder.Entity<SessionSeat>()
            .HasOne(ss => ss.Session)
            .WithMany(s => s.Seats)
            .HasForeignKey(ss => ss.SessionId)
            .OnDelete(DeleteBehavior.Cascade); // Se deletar a sessão, deleta os status das cadeiras

       
        
        // garante proteçao para que nao aja dois ingresso
        modelBuilder.Entity<tickets>()
            .HasIndex(t => new { t.SessionId, t.SeatNumber })
            .IsUnique();

        modelBuilder.Entity<MovieTheater>(entity =>
        {
            entity.HasKey(mt => mt.Id);
            
            // Mapeia o Value Object EmailVO para virar uma coluna comum na tabela MovieTheater
            // Assumindo que a propriedade interna de EmailVO que guarda a string se chame "Address" ou "Value"
            entity.OwnsOne(mt => mt.EmailAddress, email =>
            {
                email.Property<string>(e => e.EmailAddress) // <-- troque "Address" pelo nome da propriedade string dentro do seu EmailVO
                    .HasColumnName("Email")
                    .IsRequired();
            });
            
            entity.OwnsOne(mt => mt.Password, pwd =>
            {
                pwd.Property<string>(p => p.Password) // <-- troque "Hash" pelo nome da propriedade string dentro do seu PasswordVO
                    .HasColumnName("PasswordHash")
                    .IsRequired();
            });
            
            entity.HasMany(mt => mt.Rooms)
                .WithOne() // Se a sua classe Room não tiver a propriedade "MovieTheater", deixe vazio
                .HasForeignKey("MovieTheaterId") // Cria a Shadow Foreign Key no banco
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(r => r.Id);
            
            entity.Property(r => r.Seats)
                .HasColumnType("integer[]");
        });
    }
}