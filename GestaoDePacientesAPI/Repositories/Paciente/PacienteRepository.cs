
namespace GestaoDePacientesAPI.Repositories.Paciente;

using GestaoDePacientesAPI.Data;
using GestaoDePacientesAPI.Models;
using Microsoft.EntityFrameworkCore;

public class PacienteRepository : IPacienteRepository
{
    private readonly Context _context;

    public PacienteRepository(Context context)
    {
        _context = context;
    }

    public async Task<Paciente?> BuscarPacientePeloId(Guid id)
    {
        return await _context.Pacientes
             .Include(p => p.Endereco)
             .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<bool> BuscarPacientePeloEmail(string email)
    {
        return await _context.Pacientes.AnyAsync(p => p.Email == email);
    }

    public async Task<Paciente> CadastrarPaciente(Paciente paciente)
    {
        _context.Pacientes.Add(paciente);
        await _context.SaveChangesAsync();
        return paciente;
    }

    public async Task DeletarPacientePeloId(Guid id)
    {
        var paciente = await BuscarPacientePeloId(id);
        _context.Remove(paciente!);
        await _context.SaveChangesAsync();
    }

    public async Task SalvarAlteracoes()
    {
        await _context.SaveChangesAsync();
    }
}
