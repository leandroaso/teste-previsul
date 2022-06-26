using Core.Endities;
using Infrastructure.IRepositories;
using Service.IServices;

namespace Service.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _repository;

        public EnderecoService(IEnderecoRepository repository)
        {
            _repository = repository;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public Endereco FindBy(int id)
        {
            return _repository.FindBy(id);
        }

        public IEnumerable<Endereco> FindBy(Cliente cliente)
        {
            return _repository.FindByClienteId(cliente.Id);
        }

        public Endereco Insert(Endereco endereco)
        {
            return _repository.Insert(endereco);
        }

        public void Update(Endereco endereco)
        {
            _repository.Update(endereco);
        }
    }
}
