using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.api.Autor.Data;
using TiendaServicios.api.Autor.Model;

namespace TiendaServicios.api.Autor.Application
{
    public class Nuevo
    {
        public class Execute : IRequest
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime? Birthday { get; set; }
        }

        public class ValidationExecute : AbstractValidator<Execute>
        {
            public ValidationExecute()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Surname).NotEmpty();
            }
        }

        public class Management : IRequestHandler<Execute>
        {
            public readonly AutorContext _context;

            public Management(AutorContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                AutorLibro autorLibro = new AutorLibro()
                {
                    Name = request.Name,
                    Surname = request.Surname,
                    Birthday = request.Birthday,
                    AutorLibroGuid = Convert.ToString(Guid.NewGuid())
                };

                _context.AutorLibro.Add(autorLibro);
                var value = await _context.SaveChangesAsync();

                if (value > 0) {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el Autor del Libro");
            }
        }
    }
}
