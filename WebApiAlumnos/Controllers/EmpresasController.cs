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

        public EmpresasController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Empresa>>> GetAll()
        {
            return await dbContext.Empresas.ToListAsync();
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
