using AgroConnect.Application.Dtos;
using AgroConnect.Domain.Enums;

namespace AgroConnect.Application.Interfaces
{
    public interface ICulturaService
    {
        Task<CulturaDto> CreateAsync(CreateCulturaDto dto);
        Task<IEnumerable<CulturaDto>> GetAllAsync();
        Task<IEnumerable<CulturaDto>> GetByCategoriaAsync(CategoriaCultura categoria);
        Task<CulturaDto> GetByIdAsync(Guid id);
        Task<CulturaDto> UpdateAsync(UpdateCulturaDto dto);
        Task DeleteAsync(Guid id);
    }
}
