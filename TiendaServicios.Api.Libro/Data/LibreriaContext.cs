using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Model;

namespace TiendaServicios.Api.Libro.Data
{
    public class LibreriaContext : DbContext
    {
        public LibreriaContext() { }

        public LibreriaContext(DbContextOptions<LibreriaContext> options) : base(options) { }

        public virtual DbSet<LibreriaMaterial> LibreriaMaterial { get; set; }

    }
}
