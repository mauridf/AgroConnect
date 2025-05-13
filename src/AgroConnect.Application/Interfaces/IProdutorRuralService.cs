using AgroConnect.Application.Dtos;

namespace AgroConnect.Application.Interfaces
{
    public interface IProdutorRuralService
    {
        Task<ProdutorRuralDto> CreateAsync(CreateProdutorRuralDto dto);
        Task<IEnumerable<ProdutorRuralDto>> GetAllAsync();
        Task<IEnumerable<ProdutorRuralDto>> GetByUfAsync(string uf);
        Task<ProdutorRuralDto> GetByIdAsync(Guid id);
        Task<ProdutorRuralDto> GetByCpfAsync(string cpf);
        Task<ProdutorRuralDto> UpdateAsync(UpdateProdutorRuralDto dto);
        Task DeleteAsync(Guid id);
    }
}
