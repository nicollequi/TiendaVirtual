using Microsoft.EntityFrameworkCore;
using TiendaVirtual.Models;

namespace TiendaVirtual.Data
{
    public class TiendaContext : DbContext
    {
        public TiendaContext(DbContextOptions<TiendaContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, Nombre = "Carlos Ramírez", Correo = "admin@tienda.com", Rol = "Administrador", celular = "3001234567" },
                new Usuario { Id = 2, Nombre = "Laura Gómez", Correo = "cajero@tienda.com", Rol = "Cajero", celular = "3109876543" },
                new Usuario { Id = 3, Nombre = "Andrés Torres", Correo = "bodeguero@tienda.com", Rol = "Bodeguero", celular = "3154561234" },
                new Usuario { Id = 4, Nombre = "María Pérez", Correo = "cliente1@correo.com", Rol = "Cliente", celular = "3201112233" },
                new Usuario { Id = 5, Nombre = "Juan Mora", Correo = "cliente2@correo.com", Rol = "Cliente", celular = "3253334455" }
            );
        }
    }
}