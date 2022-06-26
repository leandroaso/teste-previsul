using Core.DTOs;
using Core.Endities;

namespace Service.IServices
{
    public interface IClienteService
    {
        Cliente Insert(Cliente cliente);
        Cliente GetBy(int id);
        void Update(Cliente cliente);
        void Delete(int id);
        Paginated<Cliente> FindByFiltro(FiltroPaginated filtro);
    }
}
