using AgroConnect.Domain.Entities;
using AgroConnect.Domain.Enums;

namespace AgroConnect.Application.Interfaces
{
    public interface ICulturaRepository
    {
        Task<Cultura> GetByIdAsync(Guid id);
        Task<IEnumerable<Cultura>> GetAllAsync();
        Task<IEnumerable<Cultura>> GetByCategoriaAsync(CategoriaCultura categoria);
        Task AddAsync(Cultura cultura);
        Task UpdateAsync(Cultura cultura);
        Task DeleteAsync(Guid id);
    }
}
