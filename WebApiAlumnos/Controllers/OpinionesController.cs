using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAlumnos.DTOs;
using AutoMapper;
using WebApiAlumnos.Entidades;



namespace WebApiAlumnos.Controllers
{
    [ApiController]
    [Route("api/paises/{paisesId:int}/opiniones")]
    public class OpinionesController : ControllerBase
    {

        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public OpinionesController(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<OpinionesDTO>>> Get(int paisId)
        {
            var existe = await dbContext.Paises.AnyAsync(paisDB => paisDB.Id == paisId);

            if (!existe)
            {
                return NotFound();
            }

            var opiniones = await dbContext.Opiniones.Where(opinionesdb => opinionesdb.PaisId == paisId).ToListAsync();

            return mapper.Map<List<OpinionesDTO>>(opiniones);
        }


        [HttpPost]
        public async Task<ActionResult> Post(int paisId, OpinionesCreacionDTO opinionesCreacionDTO)
        {
            var existe = await dbContext.Paises.AnyAsync(paisDB => paisDB.Id == paisId);

            if (!existe)
            {
                return NotFound();
            }

            var opinion = mapper.Map<Opiniones>(opinionesCreacionDTO);
            opinion.PaisId = paisId;
            dbContext.Add(opinion);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }

}

