using Microsoft.AspNetCore.Mvc;
using WebApiAlumnos.Entidades;

namespace WebApiAlumnos.Controllers
{
    [ApiController]
    [Route("api/paises")]
    public class PaisesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Pais>> Get()
        {
            return new List<Pais>()
            {
                new Pais() { Id = 1, Nombre = "Mexico", Habitantes = 12345, Pib = 123 },
                new Pais() { Id = 2, Nombre = "EUA", Habitantes = 456, Pib = 432 },
                new Pais() { Id = 3, Nombre = "Canada", Habitantes = 567, Pib = 88 }
            };
        }
    }
}
