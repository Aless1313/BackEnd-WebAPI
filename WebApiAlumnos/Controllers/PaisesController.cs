using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using WebApiAlumnos.Entidades;

namespace WebApiAlumnos.Controllers
{
    [ApiController]
    [Route("api/paises")]
    public class PaisesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public PaisesController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pais>>> Get()
        {
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
