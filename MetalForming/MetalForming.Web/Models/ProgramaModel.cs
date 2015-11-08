using System;
using System.Collections.Generic;
using MetalForming.Web.ProduccionService;

namespace MetalForming.Web.Models
{
    public class ProgramaModel
    {
        public ProgramaModel()
        {
            OrdenesVenta = new List<OrdenVenta>();
            ProgramasAnteriores = new List<Programa>();
        }

        public int Id { get; set; }

        public string Numero { get; set; }

        public DateTime FechaInicio { get; set; }

        public string FechaInicioStr { get; set; }

        public DateTime FechaFin { get; set; }

        public string FechaFinStr { get; set; }

        public int CantidadOV { get; set; }

        public string Estado { get; set; }

        public int IdPlan { get; set; }

        public Plan PlanVigente { get; set; }

        public IList<OrdenVenta> OrdenesVenta { get; set; }

        public IList<Programa> ProgramasAnteriores { get; set; }
    }
}
