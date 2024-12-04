using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Data;
using TiendaServicios.Api.CarritoCompra.Model;

namespace TiendaServicios.Api.CarritoCompra.Application
{
    public class Nuevo
    {
        public class Execute : IRequest
        {
            public DateTime CreateSessionDate { get; set; }

            public List<string> ListProduct { get; set; }
        }

        public class Management : IRequestHandler<Execute>
        {
            public readonly CarritoContext _context;

            public Management(CarritoContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = request.CreateSessionDate
                };

                _context.CarritoSesion.Add(carritoSesion);
                var value = await _context.SaveChangesAsync();
                if (value == 0)
                {
                    throw new Exception("Errores en la inserción del carrito de compras.");
                }

                int id = carritoSesion.CarritoSesionId;

                foreach(var obj in request.ListProduct)
                {
                    var detalleSesion = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id,
                        ProductoSeleccionado = obj
                    };

                    _context.CarritoSesionDetalle.Add(detalleSesion);
                }

                value = await _context.SaveChangesAsync();

                if (value > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el detalle del carrito de compras.");
            }
        }
    }
}
