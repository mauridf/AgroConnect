using AgroConnect.Application.Dtos;

namespace AgroConnect.Application.Interfaces
{
    public interface IFazendaService
    {
        Task<FazendaDto> CreateAsync(CreateFazendaDto dto);
        Task<IEnumerable<FazendaDto>> GetAllAsync();
        Task<IEnumerable<FazendaDto>> GetByUfAsync(string uf);
        Task<IEnumerable<FazendaDto>> GetByProdutorIdAsync(Guid produtorId);
        Task<FazendaDto> GetByIdAsync(Guid id);
        Task<FazendaDto> GetByCnpjAsync(string cnpj);
        Task<FazendaDto> UpdateAsync(UpdateFazendaDto dto);
        Task DeleteAsync(Guid id);
    }
}
