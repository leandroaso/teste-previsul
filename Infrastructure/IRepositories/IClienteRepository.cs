using Core.DTOs;
using Core.Endities;

namespace Infrastructure.IRepositories
{
    public interface IClienteRepository
    {
        public Cliente Insert(Cliente cliente);
        Cliente GetBy(int id);
        public void Update(Cliente cliente);
        public void Delete(int id);
        public Paginated<Cliente> GetByFiltro(FiltroPaginated filtro);
    }
}
