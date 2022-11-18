using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using WebApiAlumnos.DTOs;
using WebApiAlumnos.Filtros;
using WebApiAlumnos.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

namespace WebApiAlumnos.Controllers
{
    [ApiController]
    [Route("api/empresas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmpresasController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public EmpresasController(ApplicationDbContext dbContext, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmpresaDTO>>>Get()
        {
            var empresas = await dbContext.Empresas.ToListAsync();
            return mapper.Map<List<EmpresaDTO>>(empresas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmpresaDTO>>Get(int id)
        {
            var empresas = await dbContext.Empresas.Include(empresaDB => empresaDB.EmpresasPais).ThenInclude(empresaPaisDB => empresaPaisDB.Pais).FirstOrDefaultAsync(empresaDB => empresaDB.Id == id);

            if(empresas == null)
            {
                return NotFound();
            }

            return mapper.Map<EmpresaDTOConPais>(empresas);
        }


        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<EmpresaDTO>>> Get([FromRoute]string nombre)
        {
            var empresas = await dbContext.Empresas.Where(empresaDB => empresaDB.Nombre.Contains(nombre)).ToListAsync();
            return mapper.Map<List<EmpresaDTO>>(empresas);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<EmpresaDTO>> Post([FromBody] EmpresaCreacionDTO empresaCreacionDTO)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();

            var email = emailClaim.Value;

            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;

            var existe = await dbContext.Empresas.AnyAsync(x => x.Nombre == empresaCreacionDTO.Nombre);

            if (existe)
            {
                return BadRequest("Empresa existente");
            }

            var empresa = mapper.Map<Empresa>(empresaCreacionDTO);
            dbContext.Add(empresa);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Empresa empresa, int id)
        {
            if(empresa.Id != id)
            {
                return BadRequest("Sin coincidencia");
            }

            dbContext.Update(empresa);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await dbContext.Empresas.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            dbContext.Remove(new Empresa()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
