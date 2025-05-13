using AgroConnect.Application.Dtos;
using AgroConnect.Application.Interfaces;
using AgroConnect.Domain.Entities;
using AgroConnect.Domain.ValueObjects;
using AutoMapper;

namespace AgroConnect.Application.Services
{
    public class ProdutorRuralService : IProdutorRuralService
    {
        private readonly IProdutorRuralRepository _repository;
        private readonly IMapper _mapper;

        public ProdutorRuralService(IProdutorRuralRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProdutorRuralDto> CreateAsync(CreateProdutorRuralDto dto)
        {
            var endereco = _mapper.Map<Endereco>(dto.Endereco);
            var produtor = new ProdutorRural(
                dto.Nome,
                dto.CPF,
                dto.Email,
                dto.Telefone,
                endereco);

            await _repository.AddAsync(produtor);
            return _mapper.Map<ProdutorRuralDto>(produtor);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProdutorRuralDto>> GetAllAsync()
        {
            var produtores = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProdutorRuralDto>>(produtores);
        }

        public async Task<ProdutorRuralDto> GetByIdAsync(Guid id)
        {
            var produtor = await _repository.GetByIdAsync(id);
            return _mapper.Map<ProdutorRuralDto>(produtor);
        }

        public async Task<ProdutorRuralDto> GetByCpfAsync(string cpf)
        {
            var produtor = await _repository.GetByCpfAsync(cpf);
            return _mapper.Map<ProdutorRuralDto>(produtor);
        }

        public async Task<IEnumerable<ProdutorRuralDto>> GetByUfAsync(string uf)
        {
            var produtores = await _repository.GetByUfAsync(uf);
            return _mapper.Map<IEnumerable<ProdutorRuralDto>>(produtores);
        }

        public async Task<ProdutorRuralDto> UpdateAsync(UpdateProdutorRuralDto dto)
        {
            var produtorExistente = await _repository.GetByIdAsync(dto.Id);
            if (produtorExistente == null)
                throw new KeyNotFoundException("Produtor rural não encontrado");

            var endereco = _mapper.Map<Endereco>(dto.Endereco);
            produtorExistente.Update(
                dto.Nome,
                dto.CPF,
                dto.Email,
                dto.Telefone,
                endereco);

            await _repository.UpdateAsync(produtorExistente);
            return _mapper.Map<ProdutorRuralDto>(produtorExistente);
        }
    }
}
