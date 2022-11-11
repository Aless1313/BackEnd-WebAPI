using System.ComponentModel.DataAnnotations;
using WebApiAlumnos.Validaciones;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiAlumnos.Entidades
{
    public class Pais 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        public List<Opiniones> Opiniones{ get; set; }
        public List<EmpresaPais> EmpresaPais { get; set; }

    
    }
}
