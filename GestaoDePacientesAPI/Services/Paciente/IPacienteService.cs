namespace GestaoDePacientesAPI.Services.Paciente;

using GestaoDePacientesAPI.Dtos;

public interface IPacienteService
{
    Task<PacienteDto> BuscarPacientePeloId(Guid id);
    Task<PacienteDto> CadastrarPaciente(CadastrarPacienteDto cadastrarPacienteDto);
    Task<PacienteDto> EditarPacientePeloId(EditarPacienteDto editarPacienteDto, Guid id);
    Task DeletarPacientePeloId(Guid id);
}
