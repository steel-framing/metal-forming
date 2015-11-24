namespace MetalForming.Common
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
            public const string PendienteAsignar = "Pendiente Asignar";
            public const string Asignado = "Asignado";
            public const string Programado = "Programado";
            public const string ReservadoStock = "ReservadoStock";
            public const string Conformado = "Conformado";
            public const string Detenido = "Detenido";
            public const string Producido = "Producido";
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

        public class EstadoProcesoMaquina
        {
            public const string Pendiente = "Pendiente";
            public const string EnProceso = "En Proceso";
            public const string Terminado = "Terminado";
        }

        public class MotivosDeParada
        {
            public const string Motivo1 = "Configuración de maquina";
            public const string Motivo2 = "Problema eléctrico o electrónico";
            public const string Motivo3 = "Problema hidráulico";
            public const string Motivo4 = "Problema mecánico";
            public const string Motivo5 = "Regulación de Maquina";
        }
    }
}
