using System;

namespace MetalForming.Data.Core
{
    [Serializable]
    public class ExceptionData : Exception
    {
        public string Servidor { get; set; }

        public string ConsultaSql { get; set; }

        public ExceptionData(
            string mensaje,
            string servidor,
            string consultaSql) :
            base(mensaje)
        {
            Servidor = servidor;
            ConsultaSql = consultaSql;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} - Error: {2}", Servidor, ConsultaSql, Message);
        }
    }
}
