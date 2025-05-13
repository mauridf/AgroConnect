using AgroConnect.Application.Dtos;
using AgroConnect.Application.Interfaces;
using AgroConnect.Domain.Entities;
using AutoMapper;

namespace AgroConnect.Application.Services
{
    public class FazendaCulturaService : IFazendaCulturaService
    {
        private readonly IFazendaCulturaRepository _repository;
        private readonly IFazendaRepository _fazendaRepository;
        private readonly ICulturaRepository _culturaRepository;
        private readonly IMapper _mapper;

        public FazendaCulturaService(
            IFazendaCulturaRepository repository,
            IFazendaRepository fazendaRepository,
            ICulturaRepository culturaRepository,
            IMapper mapper)
        {
            _repository = repository;
            _fazendaRepository = fazendaRepository;
            _culturaRepository = culturaRepository;
            _mapper = mapper;
        }

        public async Task<FazendaCulturaDto> CreateAsync(CreateFazendaCulturaDto dto)
        {
            // Valida se a fazenda existe
            var fazenda = await _fazendaRepository.GetByIdAsync(dto.FazendaId);
            if (fazenda == null)
                throw new KeyNotFoundException("Fazenda não encontrada");

            // Valida se a cultura existe
            var cultura = await _culturaRepository.GetByIdAsync(dto.CulturaId);
            if (cultura == null)
                throw new KeyNotFoundException("Cultura não encontrada");

            var fazendaCultura = new FazendaCultura(
                dto.FazendaId,
                dto.CulturaId,
                dto.AreaUtilizadaHectares);

            await _repository.AddAsync(fazendaCultura);
            return _mapper.Map<FazendaCulturaDto>(fazendaCultura);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<FazendaCulturaDto> GetByIdAsync(Guid id)
        {
            var fazendaCultura = await _repository.GetByIdAsync(id);
            return _mapper.Map<FazendaCulturaDto>(fazendaCultura);
        }

        public async Task<IEnumerable<FazendaCulturaDto>> GetByFazendaIdAsync(Guid fazendaId)
        {
            var fazendaCulturas = await _repository.GetByFazendaIdAsync(fazendaId);
            return _mapper.Map<IEnumerable<FazendaCulturaDto>>(fazendaCulturas);
        }

        public async Task<decimal> GetAreaTotalUtilizadaAsync(Guid fazendaId)
        {
            return await _repository.GetAreaTotalUtilizadaAsync(fazendaId);
        }

        public async Task<FazendaCulturaDto> UpdateAsync(UpdateFazendaCulturaDto dto)
        {
            var fazendaCulturaExistente = await _repository.GetByIdAsync(dto.Id);
            if (fazendaCulturaExistente == null)
                throw new KeyNotFoundException("Relacionamento Fazenda-Cultura não encontrado");

            // Valida se a fazenda existe
            var fazenda = await _fazendaRepository.GetByIdAsync(dto.FazendaId);
            if (fazenda == null)
                throw new KeyNotFoundException("Fazenda não encontrada");

            // Valida se a cultura existe
            var cultura = await _culturaRepository.GetByIdAsync(dto.CulturaId);
            if (cultura == null)
                throw new KeyNotFoundException("Cultura não encontrada");

            fazendaCulturaExistente.Update(
                dto.FazendaId,
                dto.CulturaId,
                dto.AreaUtilizadaHectares);

            await _repository.UpdateAsync(fazendaCulturaExistente);
            return _mapper.Map<FazendaCulturaDto>(fazendaCulturaExistente);
        }
    }
}
