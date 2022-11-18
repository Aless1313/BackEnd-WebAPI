using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAlumnos.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApiAlumnos.Entidades;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.AspNetCore.Identity;

namespace WebApiAlumnos.Controllers
{
    [ApiController]
    [Route("api/paises/{paisesId:int}/opiniones")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [AllowAnonymous]
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


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(OpinionesCreacionDTO opinionesDto, int id)
        {
            var exist = await dbContext.Opiniones.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            var opinion = mapper.Map<Opiniones>(opinionesDto);
            opinion.Id = id;

            dbContext.Update(opinion);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await dbContext.Opiniones.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            dbContext.Remove(new Opiniones()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }

}

