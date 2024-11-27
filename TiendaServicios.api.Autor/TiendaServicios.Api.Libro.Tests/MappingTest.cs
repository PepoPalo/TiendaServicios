using AutoMapper;
using TiendaServicios.Api.Libro.Application;
using TiendaServicios.Api.Libro.Model;

namespace TiendaServicios.Api.Libro.Tests
{
    public class MappingTest : Profile
    {
        public MappingTest()
        {
            CreateMap<LibreriaMaterial, LibroMaterialDto>();
        }
    }
}
