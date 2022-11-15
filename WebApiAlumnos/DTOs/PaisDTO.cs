namespace WebApiAlumnos.DTOs
{
    public class PaisDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<OpinionesDTO> Opiniones { get; set; }
    }
}
