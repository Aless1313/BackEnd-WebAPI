using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAlumnos.Entidades;
using Microsoft.AspNetCore.Http;
using WebApiAlumnos.DTOs;
using AutoMapper;

namespace WebApiAlumnos.Controllers
{
    [ApiController]
    [Route("api/paises")]
    public class PaisesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public PaisesController(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpGet("/listadoPaises")]
        public async Task<ActionResult<List<Pais>>> GetAll()
        {
            return await dbContext.Paises.ToListAsync();
        }
        [HttpGet("{id:int}", Name = "obtenerPais")]
        public async Task<ActionResult<PaisesDTOConEmpresas>> GetById(int id)
        {
            var paises = await dbContext.Paises
                .Include(empresaDB => empresaDB.EmpresaPais)
                .ThenInclude(empresaPaisDB => empresaPaisDB.Empresa)
                .Include(servicioDB => servicioDB.Opiniones)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (paises == null)
            {
                return NotFound();
            }

            paises.EmpresaPais = paises.EmpresaPais.OrderBy(x => x.Orden).ToList();

            return mapper.Map<PaisesDTOConEmpresas>(paises);
        }


        [HttpPost]
        public async Task<ActionResult> Post(PaisCreacionDTO paisCreacionDTO)
        {
            if (paisCreacionDTO.EmpresasIds == null)
            {
                return BadRequest("No se puede iniciar");
            }

            var empresaid = await dbContext.Empresas.Where(empresaBD => paisCreacionDTO.EmpresasIds.Contains(empresaBD.Id)).Select(x => x.Id).ToListAsync();

            if (paisCreacionDTO.EmpresasIds.Count != empresaid.Count)
            {
                return BadRequest("No hay empresa");
            }

            var pais = mapper.Map<Pais>(paisCreacionDTO);



            dbContext.Add(pais);
            await dbContext.SaveChangesAsync();
            var paisDTO = mapper.Map<PaisDTO>(pais);

            return CreatedAtRoute("obtenerPais", new { id = pais.Id }, paisDTO);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(PaisCreacionDTO paisDto, int id)
        {
            var exist = await dbContext.Paises.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            var pais = mapper.Map<Pais>(paisDto);
            pais.Id = id;

            dbContext.Update(pais);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await dbContext.Paises.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            dbContext.Remove(new Pais()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }



    }
}
