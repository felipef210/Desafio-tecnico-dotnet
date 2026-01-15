using GestaoDePacientesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoDePacientesAPI.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Paciente>()
            .HasOne(p => p.Endereco)
            .WithOne(e => e.Paciente)
            .HasForeignKey<Endereco>(e => e.PacienteId);
    }

    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
}
