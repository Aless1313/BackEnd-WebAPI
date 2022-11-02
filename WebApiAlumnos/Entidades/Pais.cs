using System.ComponentModel.DataAnnotations;
using WebApiAlumnos.Validaciones;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApiAlumnos.Entidades
{
    public class Pais : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        public int Habitantes { get; set; }
        public List<Empresa> empresas { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {
                var primeraLetra = Nombre[0].ToString();

                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayuscula",
                        new String[] { nameof(Nombre) });
                }
            }
        }
    }
}
