using System.ComponentModel.DataAnnotations;
using WebApiAlumnos.Validaciones;

namespace WebApiAlumnos.DTOs
{
    public class PaisPatchDTO
    {
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo puede tener hasta 250 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
