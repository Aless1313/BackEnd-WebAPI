using Microsoft.AspNetCore.Mvc;
using WebApiAlumnos.Entidades;
using Microsoft.EntityFrameworkCore;


namespace WebApiAlumnos.Controllers
{
    [ApiController]
    [Route("api/empresas")]
    public class EmpresasController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<EmpresasController> log;

        public EmpresasController(ApplicationDbContext dbContext, ILogger<EmpresasController> log)
        {
            this.dbContext = dbContext;
            this.log = log;
        }

        [HttpGet]
        public async Task<ActionResult<List<Empresa>>> GetAll()
        {
            log.LogInformation("Obteniendo listado");
            return await dbContext.Empresas.ToListAsync();
        }



        [HttpGet("{nombre}")]
        public async Task<ActionResult<Empresa>> GetEmpresa(String nombre)
        {
            var empresa = await dbContext.Empresas.FirstOrDefaultAsync(x => x.Nombre == nombre);

            if(empresa == null)
            {
                return NotFound();
            }

            log.LogInformation("La empresa es " + nombre);
            return empresa;
        }
        

        [HttpPost] 
        public async Task<ActionResult> Post([FromBody] Empresa empresa)
        {
            var exist = await dbContext.Empresas.AnyAsync(x => x.Id == empresa.PaisId);

            if (!exist)
            {
                return BadRequest($"No existe pais relacionado al id");
            }

            dbContext.Add(empresa);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Empresa empresa, int id)
        {
            var exist = await dbContext.Empresas.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("Empresa no encontrada");
            }

            if(empresa.Id != id)
            {
                return BadRequest("Empresa sin id coincidente");
            }
            dbContext.Update(empresa);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Empresas.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound("Registro no encontrado");
            }

            dbContext.Remove(new Empresa() { Id = id });

            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
