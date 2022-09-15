namespace WebApiAlumnos.Entidades
{
    public class Pais
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Habitantes { get; set; }
        public List<Empresa> empresas { get; set; }
    }
}
