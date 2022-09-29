using System.ComponentModel.DataAnnotations;
using WebApiAlumnos.Validaciones;

namespace WebApiAlumnos.Entidades
{
    public class Pais
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }


        public int Habitantes { get; set; }
        public List<Empresa> empresas { get; set; }
    }
}
