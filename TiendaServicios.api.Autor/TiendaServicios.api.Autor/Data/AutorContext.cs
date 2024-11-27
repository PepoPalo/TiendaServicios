using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.api.Autor.Model;

namespace TiendaServicios.api.Autor.Data
{
    public class AutorContext : DbContext
    {
        public AutorContext(DbContextOptions<AutorContext> options) : base(options) { }

        public DbSet<AutorLibro> AutorLibro { get; set; }
        public DbSet<GradoAcademico> GradoAcademico { get; set; }
    }
}
