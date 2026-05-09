// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using apiatas.Models;
namespace apiatas.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Ata> Atas => Set<Ata>();
    public DbSet<Departamento> Departamentos => Set<Departamento>();
    public DbSet<Pessoa> Pessoas => Set<Pessoa>();
    public DbSet<AtaParticipante> AtaParticipantes => Set<AtaParticipante>();
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AtaParticipante>()
            .HasKey(ap => new { ap.AtaId, ap.PessoaId });

        modelBuilder.Entity<AtaParticipante>()
            .HasOne(ap => ap.Ata)
            .WithMany(a => a.Participantes)
            .HasForeignKey(ap => ap.AtaId);

        modelBuilder.Entity<AtaParticipante>()
            .HasOne(ap => ap.Pessoa)
            .WithMany(p => p.AtasParticipadas)
            .HasForeignKey(ap => ap.PessoaId);
    }
}