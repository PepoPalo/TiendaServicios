using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Data;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;

namespace TiendaServicios.Api.CarritoCompra.Application
{
    public class Consulta
    {
        public class Execute : IRequest<CarritoDto>
        {
            public int CarritoSesionId { get; set; }
        }

        public class Management : IRequestHandler<Execute, CarritoDto>
        {
            public readonly CarritoContext _context;
            public readonly ILibroService _libroService;

            public Management(CarritoContext context, ILibroService libroService)
            {
                _context = context;
                _libroService = libroService;
            }

            public async Task<CarritoDto> Handle(Execute request, CancellationToken cancellationToken)
            {
                var carritoSesion = await _context.CarritoSesion.FirstOrDefaultAsync(x => x.CarritoSesionId == request.CarritoSesionId);
                var carritoSesionDetalle = await _context.CarritoSesionDetalle.Where(x => x.CarritoSesionId == request.CarritoSesionId).ToListAsync();

                List<CarritoDetalleDto> listaCarritoDetalleDtos = new List<CarritoDetalleDto>();

                foreach(var libro in carritoSesionDetalle)
                {
                    var response = await _libroService.GetLibro(new Guid(libro.ProductoSeleccionado));
                    if (response.result)
                    {
                        var objetoLibro = response.Libro;
                        var carritoDetalle = new CarritoDetalleDto
                        {
                            TituloLibro = objetoLibro.Title,
                            FechaPublicacion = objetoLibro.PublishedDate,
                            LibroId = objetoLibro.LibreriaMaterialId
                        };
                        listaCarritoDetalleDtos.Add(carritoDetalle);
                    }
                }

                var carritoSesionDto = new CarritoDto
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacionSesion = carritoSesion.FechaCreacion,
                    ListaProductos = listaCarritoDetalleDtos
                };

                return carritoSesionDto;
            }
        }
    }
}
