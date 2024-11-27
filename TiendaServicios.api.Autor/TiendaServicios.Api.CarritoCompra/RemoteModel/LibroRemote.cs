using System;

namespace TiendaServicios.Api.CarritoCompra.RemoteModel
{
    public class LibroRemote
    {
        public Guid? LibreriaMaterialId { get; set; }
        public string Title { get; set; }
        public DateTime? PublishedDate { get; set; }
        public Guid? AutorLibro { get; set; }
    }
}
