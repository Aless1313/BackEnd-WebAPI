using Microsoft.EntityFrameworkCore;
using WebApiAlumnos.Entidades;

namespace WebApiAlumnos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Pais> Paises { get; set; }
    }
}
