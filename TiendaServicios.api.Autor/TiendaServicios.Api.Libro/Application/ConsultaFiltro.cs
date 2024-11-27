using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Data;
using TiendaServicios.Api.Libro.Model;

namespace TiendaServicios.Api.Libro.Application
{
    public class ConsultaFiltro
    {
        public class LibroUnico : IRequest<LibroMaterialDto>
        {
            public Guid? LibroId { get; set; }
        }

        public class Management : IRequestHandler<LibroUnico, LibroMaterialDto>
        {
            private readonly LibreriaContext _context;
            private readonly IMapper _mapper;

            public Management(LibreriaContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<LibroMaterialDto> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await _context.LibreriaMaterial.Where(x => x.LibreriaMaterialId == request.LibroId).FirstOrDefaultAsync();
                if (libro == null)
                {
                    throw new Exception("No se encontró el libro");
                }

                var libroDto = _mapper.Map<LibreriaMaterial, LibroMaterialDto>(libro);

                return libroDto;
            }
        }
    }
}
