﻿namespace MetalForming.Common
{
    public class Constantes
    {
        public class EstadoPlan
        {
            public const string Pendiente = "Pendiente";
        }

        public class EstadoPrograma
        {
            public const string Programado = "Programado";
            public const string Finalizado = "Finalizado";
            public const string Terminado = "Terminado";
        }

        public class EstadoOrdenVenta
        {
            public const string Pendiente = "Pendiente";
            public const string Programado = "Programado";
            public const string Iniciado = "Iniciado";
        }

        public class EstadoOrdenPoduccion
        {
            public const string PendienteAprobar = "Pendiente Aprobar";
            public const string Rechazado = "Observado";
            public const string Programado = "Programado";
            public const string ReProgramado = "ReProgramado";
            public const string Asignado = "Asignado";
            public const string Conformado = "Conformado";
            public const string Detenido = "Detenido";
            public const string Terminado = "Terminado";
        }
    }
}
