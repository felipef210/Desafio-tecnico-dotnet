using System.Text.RegularExpressions;
using AutoMapper;
using GestaoDePacientesAPI.Exceptions;
using GestaoDePacientesAPI.Repositories.Paciente;


namespace GestaoDePacientesAPI.Services.Paciente;

using GestaoDePacientesAPI.Dtos;
using GestaoDePacientesAPI.Models;
using GestaoDePacientesAPI.Services.ViaCep;

public class PacienteService : IPacienteService
{
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IMapper _mapper;
    private readonly IViaCepService _viaCepService;

    public PacienteService(IPacienteRepository pacienteRepository, IMapper mapper, IViaCepService viaCepService)
    {
        _pacienteRepository = pacienteRepository;
        _mapper = mapper;
        _viaCepService = viaCepService;
    }

    public async Task<PacienteDto> BuscarPacientePeloId(Guid id)
    {
        var paciente = await _pacienteRepository.BuscarPacientePeloId(id);

        if (paciente == null)
            throw new NaoEncontradoException("Paciente não encontrado!");

        var pacienteDto = _mapper.Map<PacienteDto>(paciente);

        return pacienteDto;
    }

    public async Task<PacienteDto> CadastrarPaciente(CadastrarPacienteDto cadastrarPacienteDto)
    {
        bool emailJaExiste = await _pacienteRepository.BuscarPacientePeloEmail(cadastrarPacienteDto.Email);

        if (emailJaExiste && !string.IsNullOrWhiteSpace(cadastrarPacienteDto.Email))
            throw new ParametroInvalidoException("Este e-mail já está cadastrado por outro paciente!");   

        ValidarCadastroEdicao(cadastrarPacienteDto.Nome, cadastrarPacienteDto.DataDeNascimento, cadastrarPacienteDto.Sexo.ToLower(), cadastrarPacienteDto.Telefone, cadastrarPacienteDto.Email);

        var dadosEndereco = await _viaCepService.BuscarEnderecoPeloCep(cadastrarPacienteDto.CEP);

        var paciente = _mapper.Map<Paciente>(cadastrarPacienteDto);

        paciente.Endereco = _mapper.Map<Endereco>(dadosEndereco);

        paciente.Endereco.Numero = cadastrarPacienteDto.Numero;
        paciente.Endereco.Complemento = cadastrarPacienteDto.Complemento;

        paciente.Nome = CapitalizeFullName(paciente.Nome);
        paciente.Sexo = CapitalizeFirstLetter(paciente.Sexo);

        if (!string.IsNullOrEmpty(paciente.Telefone))
            paciente.Telefone = FormatarTelefone(paciente.Telefone);

        var pacienteDto = _mapper.Map<PacienteDto>(await _pacienteRepository.CadastrarPaciente(paciente));
        return pacienteDto;
    }

    public async Task DeletarPacientePeloId(Guid id)
    {
        var pacienteExiste = await _pacienteRepository.BuscarPacientePeloId(id);

        if (pacienteExiste == null)
            throw new NaoEncontradoException("Paciente não encontrado!");

        await _pacienteRepository.DeletarPacientePeloId(id);
    }

    public async Task<PacienteDto> EditarPacientePeloId(EditarPacienteDto editarPacienteDto, Guid id)
    {
        var pacienteExiste = await _pacienteRepository.BuscarPacientePeloId(id);

        if (pacienteExiste == null)
            throw new NaoEncontradoException("Paciente não encontrado!");

        bool emailJaExiste = await _pacienteRepository.BuscarPacientePeloEmail(editarPacienteDto.Email);

        if (emailJaExiste && !string.IsNullOrWhiteSpace(editarPacienteDto.Email) && pacienteExiste.Email != editarPacienteDto.Email)
            throw new ParametroInvalidoException("Este e-mail já está cadastrado por outro paciente!");

        ValidarCadastroEdicao(editarPacienteDto.Nome, editarPacienteDto.DataDeNascimento, editarPacienteDto.Sexo.ToLower(), editarPacienteDto.Telefone, editarPacienteDto.Email);

        if (editarPacienteDto.CEP != pacienteExiste.Endereco.CEP)
        {
            var dadosEndereco = await _viaCepService.BuscarEnderecoPeloCep(editarPacienteDto.CEP);
            _mapper.Map(dadosEndereco, pacienteExiste.Endereco);
        }

        _mapper.Map(editarPacienteDto, pacienteExiste);

        pacienteExiste.Endereco.Numero = editarPacienteDto.Numero;
        pacienteExiste.Endereco.Complemento = editarPacienteDto.Complemento;

        pacienteExiste.Nome = CapitalizeFullName(pacienteExiste.Nome);
        pacienteExiste.Sexo = CapitalizeFirstLetter(pacienteExiste.Sexo);

        if (!string.IsNullOrEmpty(pacienteExiste.Telefone))
            pacienteExiste.Telefone = FormatarTelefone(pacienteExiste.Telefone);
        
        await _pacienteRepository.SalvarAlteracoes();

        return _mapper.Map<PacienteDto>(pacienteExiste);
    }

    private bool ValidarCadastroEdicao(string nome, DateOnly dataNascimento, string sexo, string telefone, string email)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ParametroInvalidoException("O nome do paciente é obrigatório!");

        nome = nome.Trim();

        if (nome.Length > 150)
            throw new ParametroInvalidoException("O nome é muito longo. Máximo de 150 caracteres.");

        if (dataNascimento > DateOnly.FromDateTime(DateTime.Now))
            throw new ParametroInvalidoException("A data de nascimento deve ser anterior ou igual ao dia de hoje!");

        if (string.IsNullOrWhiteSpace(sexo))
            throw new ParametroInvalidoException("O sexo do paciente é obrigatório!");

        sexo = sexo.Trim();

        if (sexo != "masculino" && sexo != "feminino" && sexo != "outro")
            throw new ParametroInvalidoException("Insira o sexo masculino, feminíno ou outro");

        if (!string.IsNullOrEmpty(telefone.Trim()))
        {
            bool temLetra = telefone.Any(char.IsLetter);

            if (temLetra)
                throw new ParametroInvalidoException("O número de telefone deve conter apenas números!");

            if (telefone.Count() < 10)
                throw new ParametroInvalidoException("Número de telefone inválido!");
        }

        if (!IsFullName(nome))
            throw new ParametroInvalidoException("Insira seu nome completo!");

        if (!string.IsNullOrEmpty(email.Trim()))
            if (!IsEmailValid(email))
                throw new ParametroInvalidoException("E-mail no formato incorreto!");

        return true;
    }

    private bool IsEmailValid(string email)
    {
        Regex regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,63}$");
        return regex.IsMatch(email);
    }

    private bool IsFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            return false;

        Regex nomeRegex = new Regex(@"^[A-Za-zÀ-ÖØ-öø-ÿ' ]+$");

        if (!nomeRegex.IsMatch(fullName))
            return false;

        string[] separatedName = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return separatedName.Length > 1;
    }

    private string CapitalizeFullName(string fullName)
    {
        string[] lowercaseWords = { "da", "de", "do", "dos", "das" };

        return string.Join(" ", fullName
            .ToLower()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select((word, index) =>
            {
                // Sempre mantém a primeira letra do primeiro nome maiuscula, mesmo sendo uma preposição.
                if (index == 0 || !lowercaseWords.Contains(word))
                    return char.ToUpper(word[0]) + word.Substring(1);
                else
                    return word;
            }));
    }

    private string CapitalizeFirstLetter(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            return word;

        return char.ToUpper(word[0]) + word.Substring(1).ToLower();
    }

    private string FormatarTelefone(string telefone)
    {
        telefone = new string(telefone.Where(char.IsDigit).ToArray());

        if (telefone.Length == 11)
            return Regex.Replace(telefone, @"(\d{2})(\d{5})(\d{4})", "($1) $2-$3");

        if (telefone.Length == 10)
            return Regex.Replace(telefone, @"(\d{2})(\d{4})(\d{4})", "($1) $2-$3");

        return telefone;
    }
}
