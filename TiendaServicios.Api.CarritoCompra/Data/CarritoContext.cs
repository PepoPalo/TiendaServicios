using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.CarritoCompra.Model;

namespace TiendaServicios.Api.CarritoCompra.Data
{
    public class CarritoContext : DbContext
    {
        public CarritoContext(DbContextOptions<CarritoContext> options) : base(options) { }

        public DbSet<CarritoSesion> CarritoSesion { get; set; }
        public DbSet<CarritoSesionDetalle> CarritoSesionDetalle { get; set; }
    }
}
