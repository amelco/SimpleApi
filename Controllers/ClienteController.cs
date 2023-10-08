using Microsoft.AspNetCore.Mvc;
using SimpleApi.Entities;
using SimpleApi.Repositories;

namespace SimpleApi.Controllers
{
    [Controller]
    public class ClienteController : Controller
    {
        [HttpGet]
        [Route("/clientes/total")]
        public IActionResult TotalClientes()
        {
            var repo = new ClienteRepository();
            int count = repo.ObtemNumeroDeClientes();
            return Ok(count);
        }

        [HttpGet]
        [Route("/clientes")]
        public IActionResult ListarClientes()
        {
            var repo = new ClienteRepository();
            var lista = repo.ListarClientes();
            return Ok(lista);
        }

        [HttpPost]
        [Route("/clientes")]
        public IActionResult SalvarCliente([FromBody]Cliente cliente)
        {
            var repo = new ClienteRepository();
            var sucesso = repo.SalvarCliente(cliente);
            if (sucesso)
                return Ok();
            else
                return BadRequest();
        }
    }
}
