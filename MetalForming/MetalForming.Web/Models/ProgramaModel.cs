﻿using System;
using System.Collections.Generic;
using MetalForming.Web.ProduccionService;

namespace MetalForming.Web.Models
{
    public class ProgramaModel
    {
        public ProgramaModel()
        {
            OrdenesVenta = new List<OrdenVenta>();
        }

        public string Numero { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public int CantidadOV { get; set; }

        public string Estado { get; set; }

        public Plan PlanVigente { get; set; }

        public IList<OrdenVenta> OrdenesVenta { get; set; }

        public IList<Programa> ProgramasAnteriores { get; set; }
    }
}
