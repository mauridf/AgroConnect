using AgroConnect.Domain.Entities;

namespace AgroConnect.Application.Interfaces
{
    public interface IFazendaCulturaRepository
    {
        Task<FazendaCultura> GetByIdAsync(Guid id);
        Task<IEnumerable<FazendaCultura>> GetByFazendaIdAsync(Guid fazendaId);
        Task<decimal> GetAreaTotalUtilizadaAsync(Guid fazendaId);
        Task AddAsync(FazendaCultura fazendaCultura);
        Task UpdateAsync(FazendaCultura fazendaCultura);
        Task DeleteAsync(Guid id);
    }
}
