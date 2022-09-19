using System.ComponentModel.DataAnnotations;


namespace WebApiAlumnos.Entidades
{
    public class Empresa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Nombre { get; set; }

        [Range(0,2022, ErrorMessage = "Año no valido")]
        public int fundacion { get; set; }


        public int PaisId { get; set; }
    }
}
