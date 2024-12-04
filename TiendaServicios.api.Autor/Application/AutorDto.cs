using System;

namespace TiendaServicios.api.Autor.Application
{
    public class AutorDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? Birthday { get; set; }
        public string AutorLibroGuid { get; set; }
    }
}
