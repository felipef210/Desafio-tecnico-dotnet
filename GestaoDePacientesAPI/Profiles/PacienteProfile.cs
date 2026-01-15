using AutoMapper;
using GestaoDePacientesAPI.Dtos;
using GestaoDePacientesAPI.Models;

namespace GestaoDePacientesAPI.Profiles;

public class PacienteProfile : Profile
{
    public PacienteProfile()
    {
        CreateMap<CadastrarPacienteDto, Paciente>();
      
        CreateMap<Paciente, PacienteDto>()
            .ForMember(dest => dest.CEP, opt => opt.MapFrom(src => src.Endereco.CEP))
            .ForMember(dest => dest.Logradouro, opt => opt.MapFrom(src => src.Endereco.Logradouro))
            .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.Endereco.Numero))
            .ForMember(dest => dest.Complemento, opt => opt.MapFrom(src => src.Endereco.Complemento))
            .ForMember(dest => dest.Bairro, opt => opt.MapFrom(src => src.Endereco.Bairro))
            .ForMember(dest => dest.Cidade, opt => opt.MapFrom(src => src.Endereco.Cidade))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Endereco.Estado));

        CreateMap<EditarPacienteDto, Paciente>();
    }
}
