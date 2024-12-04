using System;

namespace TiendaServicios.Api.Libro.Application
{
    public class LibroMaterialDto
    {
        public Guid? LibreriaMaterialId { get; set; }

        public string Title { get; set; }

        public DateTime? PublishedDate { get; set; }

        public Guid? AutorLibro { get; set; }
}
}
