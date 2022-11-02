using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAlumnos.Entidades;
using WebApiAlumnos.Services;
using WebApiAlumnos.Filtros;

namespace WebApiAlumnos.Controllers
{
    [ApiController]
    [Route("api/paises")]
    public class PaisesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<PaisesController> logger;
        private readonly IWebHostEnvironment env;

        private readonly string nuevosRegistros = "nuevosRegistros.txt";
        private readonly string registrosConsultados = "registrosConsultados.txt";
        public PaisesController(ApplicationDbContext context, IService service,
            ServiceTransient serviceTransient, ServiceScoped serviceScoped,
            ServiceSingleton serviceSingleton, ILogger<PaisesController> logger,
            IWebHostEnvironment env)
        {
            this.dbContext = context;
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
            this.env = env;
        }

        [HttpGet("GUID")]
        [ResponseCache(Duration = 10)]
        [ServiceFilter(typeof(FiltroDeAccion))]
        public ActionResult ObtenerGuid()
        {
            throw new NotImplementedException();
            logger.LogInformation("Durante la ejecucion");
            return Ok(new
            {
                PaisesControllerTransient = serviceTransient.guid,
                ServiceA_Transient = service.GetTransient(),
                PaisesControllerScoped = serviceScoped.guid,
                ServiceA_Scoped = service.GetScoped(),
                PaisesControllerSingleton = serviceSingleton.guid,
                ServiceA_Singleton = service.GetSingleton()
            });
        }


        [HttpGet]
        public async Task<ActionResult<List<Pais>>> GetAll()
        {
            return await dbContext.Paises.Include(x => x.empresas).ToListAsync();
        }


        [HttpGet("/empresas")]
        public async Task<ActionResult<List<Pais>>> GetEmpresas()
        {
            logger.LogInformation("Se obtiene el listado de empresas");
            logger.LogWarning("Mensaje de prueba warning");
            service.EjecutarJob();
            return await dbContext.Paises.Include(x => x.empresas).ToListAsync();

        }

        [HttpPost] 
        public async Task<ActionResult> Post([FromBody] Pais pais)
        {
            dbContext.Add(pais);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Pais pais, int id)
        {
            var exist = await dbContext.Paises.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            if(pais.Id != id)
            {
                return BadRequest("El id no coincide");
               
            }

            dbContext.Update(pais);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Paises.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("Registro no encontrado");
            }

            dbContext.Remove(new Pais() { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
