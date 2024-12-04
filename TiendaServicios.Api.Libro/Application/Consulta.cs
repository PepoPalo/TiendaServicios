using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Data;
using TiendaServicios.Api.Libro.Model;

namespace TiendaServicios.Api.Libro.Application
{
    public class Consulta
    {
        public class Execute : IRequest<List<LibroMaterialDto>> { }

        public class Management : IRequestHandler<Execute, List<LibroMaterialDto>>
        {
            private readonly LibreriaContext _context;
            private readonly IMapper _mapper;

            public Management(LibreriaContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<LibroMaterialDto>> Handle(Execute request, CancellationToken cancellationToken)
            {
                var libros = await _context.LibreriaMaterial.ToListAsync();
                var librosDto = _mapper.Map<List<LibreriaMaterial>, List<LibroMaterialDto>>(libros);

                return librosDto;
            }
        }
    }
}
