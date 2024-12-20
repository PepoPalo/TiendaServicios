﻿using System;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.RemoteModel;

namespace TiendaServicios.Api.CarritoCompra.RemoteInterface
{
    public interface ILibroService
    {
        Task<(bool result, LibroRemote Libro, string ErrorMessage)> GetLibro(Guid LibroId);
    }
}
