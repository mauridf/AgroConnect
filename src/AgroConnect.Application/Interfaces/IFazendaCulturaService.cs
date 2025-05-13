using AgroConnect.Application.Dtos;

namespace AgroConnect.Application.Interfaces
{
    public interface IFazendaCulturaService
    {
        Task<FazendaCulturaDto> CreateAsync(CreateFazendaCulturaDto dto);
        Task<IEnumerable<FazendaCulturaDto>> GetByFazendaIdAsync(Guid fazendaId);
        Task<decimal> GetAreaTotalUtilizadaAsync(Guid fazendaId);
        Task<FazendaCulturaDto> GetByIdAsync(Guid id);
        Task<FazendaCulturaDto> UpdateAsync(UpdateFazendaCulturaDto dto);
        Task DeleteAsync(Guid id);
    }
}
