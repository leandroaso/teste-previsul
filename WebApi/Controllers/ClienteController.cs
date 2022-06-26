using Core.DTOs;
using Core.Endities;
using Microsoft.AspNetCore.Mvc;
using Service.IServices;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClienteController(IClienteService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult<Cliente> Insert([FromBody] Cliente cliente)
        {
            return Ok(_service.Insert(cliente));
        }

        [HttpPut]
        public ActionResult Update([FromBody] Cliente cliente)
        {
            _service.Update(cliente);
            return Ok();
        }

        [HttpDelete]
        public void Delete([FromQuery] int id)
        {
            _service.Delete(id);
        }

        [HttpGet]
        public Cliente FindBy([FromQuery] int id)
        {
            return _service.GetBy(id);
        }

        [HttpGet("paginated")]
        public Paginated<Cliente> FindByFiltro([FromQuery] FiltroPaginated filtro)
        {
            return _service.FindByFiltro(filtro);
        }
    }
}
