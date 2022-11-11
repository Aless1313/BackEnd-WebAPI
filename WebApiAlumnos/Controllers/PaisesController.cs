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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PaisesDTOConEmpresas>> Get(int id)
        {
            var pais = await dbContext.Paises.Include(paisdb => paisdb.EmpresaPais).ThenInclude(empresapaisdb => empresapaisdb.Empresa).FirstOrDefaultAsync(x => x.Id == id);

            pais.EmpresaPais = pais.EmpresaPais.OrderBy(x => x.Id).ToList();
            return mapper.Map<PaisesDTOConEmpresas>(pais);
        }

        [HttpPost]
        public async Task<ActionResult> Post(PaisCreacionDTO paisCreacionDTO)
        {
            if(paisCreacionDTO.EmpresasIds == null)
            {
                return BadRequest("No se puede iniciar");
            }

            var empresaid = await dbContext.Empresas.Where(empresaBD => paisCreacionDTO.EmpresasIds.Contains(empresaBD.Id)).Select(x => x.Id).ToListAsync();

            if(paisCreacionDTO.EmpresasIds.Count != empresaid.Count)
            {
                return BadRequest("No hay empresa");
            }

            var pais = mapper.Map<Pais>(paisCreacionDTO);

            if(pais.EmpresaPais != null)
            {
                for(int i = 0; i < pais.EmpresaPais.Count; i++)
                {
                    pais.EmpresaPais[i].Id = i;
                }
            }

            dbContext.Add(pais);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


    }
}
