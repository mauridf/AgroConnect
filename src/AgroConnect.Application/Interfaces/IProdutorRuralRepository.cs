using AgroConnect.Domain.Entities;

namespace AgroConnect.Application.Interfaces
{
    public interface IProdutorRuralRepository
    {
        Task<ProdutorRural> GetByIdAsync(Guid id);
        Task<ProdutorRural> GetByCpfAsync(string cpf);
        Task<IEnumerable<ProdutorRural>> GetAllAsync();
        Task<IEnumerable<ProdutorRural>> GetByUfAsync(string uf);
        Task AddAsync(ProdutorRural produtor);
        Task UpdateAsync(ProdutorRural produtor);
        Task DeleteAsync(Guid id);
    }
}
