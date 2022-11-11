using WebApiAlumnos.Entidades;

namespace WebApiAlumnos.DTOs
{
    public class EmpresaDTOConPais : EmpresaDTO
    {
        public List<EmpresaPais> Pais { get; set; }
    }
}
