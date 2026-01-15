using GestaoDePacientesAPI.Dtos;

namespace GestaoDePacientesAPI.Services.ViaCep;

public interface IViaCepService
{
    Task<ViaCepResponse> BuscarEnderecoPeloCep(string cep);
}
