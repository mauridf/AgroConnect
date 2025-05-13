using AgroConnect.Application.Dtos;
using AgroConnect.Application.Interfaces;
using AgroConnect.Domain.Entities;
using AgroConnect.Domain.Enums;
using AutoMapper;

namespace AgroConnect.Application.Services
{
    public class CulturaService : ICulturaService
    {
        private readonly ICulturaRepository _repository;
        private readonly IMapper _mapper;

        public CulturaService(ICulturaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CulturaDto> CreateAsync(CreateCulturaDto dto)
        {
            var cultura = new Cultura(
                dto.Nome,
                dto.Categoria,
                dto.TempoColheita,
                dto.ExigenciaClimatica,
                dto.Detalhes);

            await _repository.AddAsync(cultura);
            return _mapper.Map<CulturaDto>(cultura);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CulturaDto>> GetAllAsync()
        {
            var culturas = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<CulturaDto>>(culturas);
        }

        public async Task<CulturaDto> GetByIdAsync(Guid id)
        {
            var cultura = await _repository.GetByIdAsync(id);
            return _mapper.Map<CulturaDto>(cultura);
        }

        public async Task<IEnumerable<CulturaDto>> GetByCategoriaAsync(CategoriaCultura categoria)
        {
            var culturas = await _repository.GetByCategoriaAsync(categoria);
            return _mapper.Map<IEnumerable<CulturaDto>>(culturas);
        }

        public async Task<CulturaDto> UpdateAsync(UpdateCulturaDto dto)
        {
            var culturaExistente = await _repository.GetByIdAsync(dto.Id);
            if (culturaExistente == null)
                throw new KeyNotFoundException("Cultura não encontrada");

            culturaExistente.Atualizar(
                dto.Nome,
                dto.Categoria,
                dto.TempoColheita,
                dto.ExigenciaClimatica,
                dto.Detalhes);

            await _repository.UpdateAsync(culturaExistente);
            return _mapper.Map<CulturaDto>(culturaExistente);
        }
    }
}
