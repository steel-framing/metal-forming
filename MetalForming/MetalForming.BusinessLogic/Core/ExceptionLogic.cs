using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalForming.BusinessLogic.Core
{
    [Serializable]
    public class ExceptionLogic : Exception
    {
        private string _mensaje;

        private string Metodo { get; set; }

        private object[] Parametros { get; set; }

        public override string Message
        {
            get { return GenerateMesage(); }
        }

        public ExceptionLogic(
            string mensaje,
            string metodo,
            params object[] parametros) :
            base(mensaje)
        {
            Metodo = metodo;
            Parametros = parametros;
            _mensaje = mensaje;
        }

        private string GenerateMesage()
        {
            if (Parametros == null)
            {
                return string.Format("{0} - Mensaje Error: {1}", Metodo, _mensaje);
            }
            return string.Format("{0} - {1} - Mensaje Error: {2}", Metodo, string.Join("  |  ", Parametros), _mensaje);
        }

        public override string ToString()
        {
            return GenerateMesage();
        }
    }
}
