using AgroConnect.Domain.Entities;

namespace AgroConnect.Application.Interfaces
{
    public interface IFazendaRepository
    {
        Task<Fazenda> GetByIdAsync(Guid id);
        Task<Fazenda> GetByCnpjAsync(string cnpj);
        Task<IEnumerable<Fazenda>> GetAllAsync();
        Task<IEnumerable<Fazenda>> GetByUfAsync(string uf);
        Task<IEnumerable<Fazenda>> GetByProdutorIdAsync(Guid produtorId);
        Task AddAsync(Fazenda fazenda);
        Task UpdateAsync(Fazenda fazenda);
        Task DeleteAsync(Guid id);
    }
}
