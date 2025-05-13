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