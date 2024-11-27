using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.api.Autor.Data;
using TiendaServicios.api.Autor.Model;

namespace TiendaServicios.api.Autor.Application
{
    public class ConsultaFiltro
    {
        public class AutorUnico : IRequest<AutorDto>
        {
            public string AutorGuid { get; set; }
        }

        public class Management : IRequestHandler<AutorUnico, AutorDto>
        {
            private readonly AutorContext _context;
            private readonly IMapper _mapper;
            public Management(AutorContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<AutorDto> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await _context.AutorLibro.Where(x => x.AutorLibroGuid == request.AutorGuid).FirstOrDefaultAsync();
                if (autor == null)
                {
                    throw new Exception("No se encontró el autor.");
                }
                var autorDto = _mapper.Map<AutorLibro, AutorDto>(autor);

                return autorDto;
            }
        }
    }
}
