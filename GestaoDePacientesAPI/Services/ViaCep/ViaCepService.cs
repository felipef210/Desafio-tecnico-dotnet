using GestaoDePacientesAPI.Dtos;
using GestaoDePacientesAPI.Exceptions;

namespace GestaoDePacientesAPI.Services.ViaCep;

public class ViaCepService : IViaCepService
{
    private readonly HttpClient _httpClient;

    public ViaCepService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ViaCepResponse> BuscarEnderecoPeloCep(string cep)
    {
        bool temLetra = cep.Any(char.IsLetter);

        if (temLetra)
            throw new ParametroInvalidoException("CEP inválido, digite apenas números!");

        var cepLimpo = new string(cep.Where(char.IsDigit).ToArray());

        if (cepLimpo.Length == 0)
            throw new ParametroInvalidoException("CEP não pode ser nulo!");

        if (cepLimpo.Length > 0 && cepLimpo.Length != 8) 
            throw new ParametroInvalidoException("CEP inválido, digite o CEP no formato: 00000-000");

        var response = await _httpClient.GetAsync($"{cepLimpo}/json/");

        if (!response.IsSuccessStatusCode)
            throw new ApiExternaException("Ocorreu um erro com o ViaCep");

        var content = await response.Content.ReadFromJsonAsync<ViaCepResponse>();

        if (content == null || (content.Erro != null && content.Erro.ToString()?.ToLower() == "true"))
            throw new NaoEncontradoException("CEP não encontrado!");

        return content;
    }
}
