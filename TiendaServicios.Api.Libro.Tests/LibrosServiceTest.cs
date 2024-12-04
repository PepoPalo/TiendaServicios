using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TiendaServicios.Api.Libro.Application;
using TiendaServicios.Api.Libro.Data;
using TiendaServicios.Api.Libro.Model;
using Xunit;

namespace TiendaServicios.Api.Libro.Tests
{
    public class LibrosServiceTest
    {
        private IEnumerable<LibreriaMaterial> ObtenerDataPrueba()
        {
            A.Configure<LibreriaMaterial>()
                .Fill(x => x.Title).AsArticleTitle()
                .Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid(); });

            var lista = A.ListOf<LibreriaMaterial>(30);
            lista[0].LibreriaMaterialId = Guid.Empty;

            return lista;
        }

        private Mock<LibreriaContext> CrearContext()
        {
            var dataPrueba = ObtenerDataPrueba().AsQueryable();

            var dbSet = new Mock<DbSet<LibreriaMaterial>>();
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());

            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<LibreriaMaterial>(dataPrueba.GetEnumerator()));

            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibreriaMaterial>(dataPrueba.Provider));

            var context = new Mock<LibreriaContext>();
            context.Setup(x => x.LibreriaMaterial).Returns(dbSet.Object);
            return context;
        }

        [Fact]
        public async void GetLibroPorId()
        {
            var mockContext = CrearContext();
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });
            var mockMapper = mapConfig.CreateMapper();

            var request = new ConsultaFiltro.LibroUnico();
            request.LibroId = Guid.Empty;

            var management = new ConsultaFiltro.Management(mockContext.Object, mockMapper);

            var libro = await management.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(libro);
            Assert.True(libro.LibreriaMaterialId == Guid.Empty);
        }

        [Fact]
        public async void GetLibros()
        {
            //System.Diagnostics.Debugger.Launch();
            //1ro: Emular el context
            var mockContext = CrearContext();

            //2do: Emular el mapper
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mockMapper = mapConfig.CreateMapper();

            //3ro: Instanciar la clase manejador y pasarle los mocks creados
            Consulta.Management management = new Consulta.Management(mockContext.Object, mockMapper);

            Consulta.Execute request = new Consulta.Execute();

            var lista = await management.Handle(request, new System.Threading.CancellationToken());

            Assert.True(lista.Any());
        }

        [Fact]
        public async void GuardarLibro()
        {
            var options = new DbContextOptionsBuilder<LibreriaContext>()
                .UseInMemoryDatabase(databaseName: "BaseDatosLibro")
                .Options;

            var context = new LibreriaContext(options);

            var request = new Nuevo.Execute();
            request.Title = "Libro de Microservice";
            request.AutorLIbro = Guid.Empty;
            request.PublishedDate = DateTime.Now;

            var management = new Nuevo.Management(context);

            var nuevolibro = await management.Handle(request, new System.Threading.CancellationToken());

            Assert.True(nuevolibro != null);
        }
    }
}
