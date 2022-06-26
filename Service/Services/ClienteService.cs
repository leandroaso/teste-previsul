using Core.DTOs;
using Core.Endities;
using Infrastructure.IRepositories;
using Service.IServices;

namespace Service.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly IEnderecoService _enderecoService;

        public ClienteService(IClienteRepository repository, IEnderecoService enderecoService)
        {
            _repository = repository;
            _enderecoService = enderecoService;
        }

        public void Delete(int id)
        {
            var cliente = GetBy(id);

            if (cliente == null) return;

            foreach (var endereco in cliente.Enderecos)
                _enderecoService.Delete(endereco.Id.GetValueOrDefault());

            _repository.Delete(id);
        }

        public Paginated<Cliente> FindByFiltro(FiltroPaginated filtro)
        {
            var paginated = _repository.GetByFiltro(filtro);

            foreach (var cliente in paginated.Content)
                cliente.Enderecos = _enderecoService.FindBy(cliente);

            return paginated;
        }

        public Cliente GetBy(int id)
        {
            var cliente = _repository.GetBy(id);

            if (cliente != null)
                cliente.Enderecos = _enderecoService.FindBy(cliente);

            return cliente;
        }

        public Cliente Insert(Cliente cliente)
        {
            _repository.Insert(cliente);

            foreach (var endereco in cliente.Enderecos)
            {
                endereco.IdCliente = cliente.Id;
                _enderecoService.Insert(endereco);
            }

            return cliente;
        }

        public void Update(Cliente cliente)
        {
            _repository.Update(cliente);

            var enderecosCliente = _enderecoService.FindBy(cliente);
            var enderecosAhRemover = enderecosCliente.Where(ec => !cliente.Enderecos.Where(e => e.Id == ec.Id).Any()).ToList();

            foreach (var endereco in enderecosAhRemover)
            {
                _enderecoService.Delete(endereco.Id.GetValueOrDefault());
            }

            foreach (var endereco in cliente.Enderecos)
            {
                endereco.IdCliente = cliente.Id;

                if (endereco.Id != null)
                    _enderecoService.Update(endereco);
                else
                    _enderecoService.Insert(endereco);
            }
        }
    }
}
