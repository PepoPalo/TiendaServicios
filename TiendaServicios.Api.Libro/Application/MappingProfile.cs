using AutoMapper;
using TiendaServicios.Api.Libro.Model;

namespace TiendaServicios.Api.Libro.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LibreriaMaterial, LibroMaterialDto>();
        }
    }
}
