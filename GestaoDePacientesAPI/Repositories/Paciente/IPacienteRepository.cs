namespace GestaoDePacientesAPI.Repositories.Paciente;

using GestaoDePacientesAPI.Models;

public interface IPacienteRepository
{
    Task<Paciente?> BuscarPacientePeloId(Guid id);
    Task<bool> BuscarPacientePeloEmail(string email);
    Task<Paciente> CadastrarPaciente(Paciente paciente);
    Task SalvarAlteracoes();
    Task DeletarPacientePeloId(Guid id);
}
