using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoDePacientesAPI.Models;

public class Endereco
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string CEP { get; set; } = null!;

    public string? Logradouro { get; set; }

    public int? Numero { get; set; }

    public string? Complemento { get; set; }

    public string? Bairro { get; set; }

    public string? Cidade { get; set; }

    public string? Estado { get; set; }

    [ForeignKey("Paciente")]
    public Guid PacienteId { get; set; }

    public Paciente Paciente { get; set; } = null!;
}
