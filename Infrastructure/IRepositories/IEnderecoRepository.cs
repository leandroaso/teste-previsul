using Core.Endities;

namespace Infrastructure.IRepositories
{
    public interface IEnderecoRepository
    {
        public Endereco Insert(Endereco endereco);
        void Update(Endereco endereco);
        void Delete(int id);
        Endereco FindBy(int id);
        public IEnumerable<Endereco> FindByClienteId(int id);
    }
}
