using WebApiAlumnos.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace WebApiAlumnos.DTOs
{
    public class PaisCreacionDTO
    {
        [PrimeraLetraMayuscula]
        [StringLength(maximumLength: 250)]
        public string Nombre { get; set; }
        public List<int> EmpresasIds { get; set; }
    }
}
