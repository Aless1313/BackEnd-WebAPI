using System.ComponentModel.DataAnnotations;
using WebApiAlumnos.Validaciones;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WebApiAlumnos.Entidades
{
    public class Opiniones
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public int PaisId { get; set; }
        public Pais Pais { get; set; }
        public string UsuarioId { get; set; }

        public IdentityUser Usuario { get; set; }
    }
}
