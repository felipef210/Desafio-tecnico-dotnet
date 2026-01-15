using System.ComponentModel.DataAnnotations;

namespace GestaoDePacientesAPI.Models;

public class Paciente
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Você deve preencher {0}")]
    public string Nome { get; set; } = null!;

    [Required(ErrorMessage = "Você deve preencher {0}")]
    public DateOnly DataDeNascimento { get; set; }

    [Required(ErrorMessage = "Você deve preencher {0}")]
    public string Sexo { get; set; } = null!;

    [Phone]
    public string? Telefone { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    public Endereco Endereco { get; set; } = null!;
}
