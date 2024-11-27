using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.api.Autor.Model
{
    public class AutorLibro
    {
        public int AutorLibroId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? Birthday { get; set; }
        public ICollection<GradoAcademico> ListaGradoAcademico { get; set; }
        public string AutorLibroGuid { get; set; }
    }
}
