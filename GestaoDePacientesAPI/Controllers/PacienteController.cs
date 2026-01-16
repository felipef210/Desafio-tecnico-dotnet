using GestaoDePacientesAPI.Dtos;
using GestaoDePacientesAPI.Services.Paciente;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDePacientesAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class PacienteController : ControllerBase
{
    private readonly IPacienteService _pacienteService;

    public PacienteController(IPacienteService pacienteService)
    {
        _pacienteService = pacienteService;
    }

    /// <summary>
    /// Cadastra o paciente do banco de dados.
    /// </summary>
    /// <param name="cadastrarPacienteDto">Objeto com os campos necessários para o cadastro de um paciente.</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso a criação seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PacienteDto))]
    public async Task<IActionResult> CadastrarPaciente([FromBody] CadastrarPacienteDto cadastrarPacienteDto)
    {
        var pacienteDto = await _pacienteService.CadastrarPaciente(cadastrarPacienteDto);
        return CreatedAtRoute("BuscarPacientePorId", new { id = pacienteDto.Id }, pacienteDto);
    }

    /// <summary>
    /// Consulta um paciente do banco de dados pelo seu identificador único (Id).
    /// </summary>
    /// <param name="id">Identificador do paciente desejado para consulta.</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a consulta seja feita com sucesso.</response>
    [HttpGet("{id}", Name = "BuscarPacientePorId")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PacienteDto))]
    public async Task<IActionResult> BuscarPacientePorId(Guid id)
    {
        var pacienteDto = await _pacienteService.BuscarPacientePeloId(id);
        return Ok(pacienteDto);
    }

    /// <summary>
    /// Edita um paciente do banco de dados pelo seu identificador único (Id).
    /// </summary>
    /// <param name="id">Identificador do paciente desejado para edição.</param>
    /// <param name="editarPacienteDto">Objeto com os campos necessários para edição de cadastro de um paciente</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a modificação seja feita com sucesso.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PacienteDto))]
    public async Task<IActionResult> EditarPacientePorId([FromBody] EditarPacienteDto editarPacienteDto, Guid id)
    {
        var pacienteDto = await _pacienteService.EditarPacientePeloId(editarPacienteDto, id);
        return Ok(pacienteDto);
    }

    /// <summary>
    /// Deleta um paciente do banco de dados pelo seu identificador único (Id).
    /// </summary>
    /// <param name="id">Identificador do paciente desejado para remoção.</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a remoçao seja feita com sucesso.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeletarPacientePorId(Guid id)
    {
        await _pacienteService.DeletarPacientePeloId(id);
        return NoContent();
    }
}
