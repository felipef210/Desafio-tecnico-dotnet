namespace GestaoDePacientesAPI.Dtos;

public class EditarPacienteDto
{
    public string Nome { get; set; } = string.Empty;

    public DateOnly DataDeNascimento { get; set; }

    public string Sexo { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string CEP { get; set; } = string.Empty;

    public int Numero { get; set; }
    public string Complemento { get; set; } = string.Empty;
}
