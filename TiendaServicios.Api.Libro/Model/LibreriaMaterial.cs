﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Libro.Model
{
    public class LibreriaMaterial
    {
        public Guid? LibreriaMaterialId { get; set; }

        public string Title { get; set; }

        public DateTime? PublishedDate { get; set; }

        public Guid? AutorLibro { get; set; }

    }
}
