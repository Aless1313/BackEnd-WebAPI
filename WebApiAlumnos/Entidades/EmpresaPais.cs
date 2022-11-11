using System.ComponentModel.DataAnnotations;
using WebApiAlumnos.Validaciones;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiAlumnos.Entidades
{
    public class EmpresaPais
    {
        public int Id { get; set; }
        public int PaisId { get; set; }
        public int EmpresaId { get; set; }
        public Pais Pais{ get; set; }
        public Empresa Empresa { get; set; }
    }
}
