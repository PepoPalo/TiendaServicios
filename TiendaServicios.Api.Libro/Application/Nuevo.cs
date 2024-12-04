using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Data;
using TiendaServicios.Api.Libro.Model;

namespace TiendaServicios.Api.Libro.Application
{
    public class Nuevo
    {
        public class Execute : IRequest
        {
            public string Title { get; set; }
            public DateTime? PublishedDate { get; set; }
            public Guid? AutorLIbro { get; set;  }
        }

        public class ExecuteValidation : AbstractValidator<Execute>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.PublishedDate).NotEmpty();
                RuleFor(x => x.AutorLIbro).NotEmpty();
            }
        }

        public class Management : IRequestHandler<Execute>
        {
            private readonly LibreriaContext _context;

            public Management(LibreriaContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var libro = new LibreriaMaterial
                {
                    Title = request.Title,
                    PublishedDate = request.PublishedDate,
                    AutorLibro = request.AutorLIbro
                };

                _context.LibreriaMaterial.Add(libro);

                var value = await _context.SaveChangesAsync();

                if (value > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo guardar el libro");
            }
        }
    }
}
