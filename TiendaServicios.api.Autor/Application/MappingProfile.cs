using AutoMapper;
using TiendaServicios.api.Autor.Model;

namespace TiendaServicios.api.Autor.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AutorLibro, AutorDto>();
        }
    }
}
