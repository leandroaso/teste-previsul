using Core.DTOs;
using Core.Endities;

namespace Service.IServices
{
    public interface IEnderecoService
    {
        Endereco Insert(Endereco endereco);
        void Update(Endereco endereco);
        void Delete(int id);
        IEnumerable<Endereco> FindBy(Cliente cliente);
        Endereco FindBy(int id);
    }
}
