using System.ComponentModel.DataAnnotations;
using WebApiAlumnos.Validaciones;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiAlumnos.Entidades
{
    public class Empresa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        [Range(0, 2022, ErrorMessage = "Año no valido")]
        public int fundacion { get; set; }

        public List<EmpresaPais> EmpresasPais { get; set; }
    }
}
