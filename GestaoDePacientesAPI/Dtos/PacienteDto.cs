namespace GestaoDePacientesAPI.Dtos;

public class PacienteDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    public DateOnly DataDeNascimento { get; set; }

    public string Sexo { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string CEP { get; set; } = string.Empty;

    public string Logradouro { get; set; } = string.Empty;

    public int Numero { get; set; }

    public string Complemento { get; set; } = string.Empty;

    public string Bairro { get; set; } = string.Empty;

    public string Cidade { get; set; } = string.Empty;

    public string Estado { get; set; } = string.Empty;
}
