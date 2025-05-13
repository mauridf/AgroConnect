using AgroConnect.Application.Dtos;
using AgroConnect.Application.Interfaces;
using AgroConnect.Domain.Entities;
using AgroConnect.Domain.ValueObjects;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AgroConnect.Application.Services
{
    public class FazendaService : IFazendaService
    {
        private readonly IFazendaRepository _repository;
        private readonly IProdutorRuralRepository _produtorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<FazendaService> _logger;

        public FazendaService(
            IFazendaRepository repository,
            IProdutorRuralRepository produtorRepository,
            IMapper mapper,
            ILogger<FazendaService> logger)
        {
            _repository = repository;
            _produtorRepository = produtorRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<FazendaDto> CreateAsync(CreateFazendaDto dto)
        {
            var produtor = await _produtorRepository.GetByIdAsync(dto.ProdutorId);
            if (produtor == null)
                throw new KeyNotFoundException("Produtor rural não encontrado");

            Endereco endereco;

            if (dto.UsarEnderecoProdutor)
            {
                // Aviso se endereço foi fornecido mas será ignorado
                if (dto.Endereco != null)
                {
                    _logger.LogWarning("Endereço fornecido será ignorado pois UsarEnderecoProdutor=true para a fazenda {NomeFazenda}", dto.Nome);
                }

                // Usa o endereço do produtor
                endereco = new Endereco
                {
                    Logradouro = produtor.Endereco.Logradouro,
                    Cidade = produtor.Endereco.Cidade,
                    UF = produtor.Endereco.UF,
                    CEP = produtor.Endereco.CEP
                };
            }
            else
            {
                // Valida se o endereço foi fornecido
                if (dto.Endereco == null)
                    throw new ArgumentException("Endereço da fazenda é obrigatório quando não usar endereço do produtor");

                endereco = _mapper.Map<Endereco>(dto.Endereco);
            }

            var fazenda = new Fazenda(
                dto.ProdutorId,
                dto.Nome,
                dto.CNPJ,
                endereco,
                dto.AreaTotalHectares,
                dto.AreaAgricultavelHectares,
                dto.AreaVegetacaoHectares,
                dto.AreaConstruidaHectares);

            await _repository.AddAsync(fazenda);
            return _mapper.Map<FazendaDto>(fazenda);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<FazendaDto>> GetAllAsync()
        {
            var fazendas = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<FazendaDto>>(fazendas);
        }

        public async Task<FazendaDto> GetByIdAsync(Guid id)
        {
            var fazenda = await _repository.GetByIdAsync(id);
            return _mapper.Map<FazendaDto>(fazenda);
        }

        public async Task<FazendaDto> GetByCnpjAsync(string cnpj)
        {
            var fazenda = await _repository.GetByCnpjAsync(cnpj);
            return _mapper.Map<FazendaDto>(fazenda);
        }

        public async Task<IEnumerable<FazendaDto>> GetByUfAsync(string uf)
        {
            var fazendas = await _repository.GetByUfAsync(uf);
            return _mapper.Map<IEnumerable<FazendaDto>>(fazendas);
        }

        public async Task<IEnumerable<FazendaDto>> GetByProdutorIdAsync(Guid produtorId)
        {
            var fazendas = await _repository.GetByProdutorIdAsync(produtorId);
            return _mapper.Map<IEnumerable<FazendaDto>>(fazendas);
        }

        public async Task<FazendaDto> UpdateAsync(UpdateFazendaDto dto)
        {
            var fazendaExistente = await _repository.GetByIdAsync(dto.Id);
            if (fazendaExistente == null)
                throw new KeyNotFoundException("Fazenda não encontrada");

            var produtor = await _produtorRepository.GetByIdAsync(dto.ProdutorId);
            if (produtor == null)
                throw new KeyNotFoundException("Produtor rural não encontrado");

            // Nova lógica para endereço
            Endereco endereco;
            if (dto.UsarEnderecoProdutor)
            {
                endereco = new Endereco
                {
                    Logradouro = produtor.Endereco.Logradouro,
                    Cidade = produtor.Endereco.Cidade,
                    UF = produtor.Endereco.UF,
                    CEP = produtor.Endereco.CEP
                };
            }
            else
            {
                if (dto.Endereco == null)
                    throw new ArgumentException("Endereço da fazenda é obrigatório quando não usar endereço do produtor");

                endereco = _mapper.Map<Endereco>(dto.Endereco);
            }

            fazendaExistente.Update(
                dto.ProdutorId,
                dto.Nome,
                dto.CNPJ,
                endereco,
                dto.AreaTotalHectares,
                dto.AreaAgricultavelHectares,
                dto.AreaVegetacaoHectares,
                dto.AreaConstruidaHectares);

            await _repository.UpdateAsync(fazendaExistente);
            return _mapper.Map<FazendaDto>(fazendaExistente);
        }
    }
}
