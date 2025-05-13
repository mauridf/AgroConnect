using AgroConnect.Application.Dtos;
using AgroConnect.Domain.Entities;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AgroConnect.API.Extensions
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            // Usuário
            CreateMap<Usuario, UsuarioDto>();
            CreateMap<UsuarioRegisterDto, Usuario>();

            // Produtor Rural
            CreateMap<ProdutorRural, ProdutorRuralDto>();
            CreateMap<CreateProdutorRuralDto, ProdutorRural>();
            CreateMap<UpdateProdutorRuralDto, ProdutorRural>();
            CreateMap<ProdutorRural, ProdutorRuralSummaryDto>()
                .ForMember(dest => dest.UF, opt => opt.MapFrom(src => src.Endereco.UF))
                .ForMember(dest => dest.TotalFazendas, opt => opt.MapFrom(src => src.Fazendas.Count));

            // Fazenda
            CreateMap<Fazenda, FazendaDto>();
            CreateMap<CreateFazendaDto, Fazenda>();
            CreateMap<UpdateFazendaDto, Fazenda>();

            // Cultura
            CreateMap<Cultura, CulturaDto>();
            CreateMap<CreateCulturaDto, Cultura>();
            CreateMap<UpdateCulturaDto, Cultura>();

            // FazendaCultura
            CreateMap<FazendaCultura, FazendaCulturaDto>();
            CreateMap<CreateFazendaCulturaDto, FazendaCultura>();
            CreateMap<UpdateFazendaCulturaDto, FazendaCultura>();
        }
    }
}