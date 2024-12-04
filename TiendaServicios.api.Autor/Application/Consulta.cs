using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.api.Autor.Data;
using TiendaServicios.api.Autor.Model;

namespace TiendaServicios.api.Autor.Application
{
    public class Consulta
    {
        public class ListaAutor : IRequest<List<AutorDto>> { }

        public class Management : IRequestHandler<ListaAutor, List<AutorDto>>
        {
            private readonly AutorContext _context;
            private readonly IMapper _mapper;
            public Management(AutorContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<AutorDto>> Handle(ListaAutor request, CancellationToken cancellationToken)
            {
                var autores = await _context.AutorLibro.ToListAsync();
                var autoresDto = _mapper.Map<List<AutorLibro>, List<AutorDto>>(autores);

                return autoresDto;
            }
        }
    }
}
