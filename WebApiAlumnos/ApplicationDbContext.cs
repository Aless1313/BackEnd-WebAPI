using Microsoft.EntityFrameworkCore;
using WebApiAlumnos.Entidades;

namespace WebApiAlumnos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmpresaPais>().HasKey(al => new { al.EmpresaId, al.PaisId });


        }

        public DbSet<Pais> Paises { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Opiniones> Opiniones { get; set; }
        public DbSet<EmpresaPais> EmpresaPais { get; set; }
    }
}
