using System;
using log4net;
using MetalForming.Data.Core;

namespace MetalForming.BusinessLogic.Core
{
    public abstract class BaseLogic
    {
        protected static readonly ILog Logger = LogManager.GetLogger("o&c");

        protected ExceptionLogic ThrowException(Exception exception, string nombreMetodo, params object[] parametros)
        {
            var exceptionData = exception as ExceptionData;

            var parametrosValores = parametros == null ? string.Empty : string.Join("  |  ", parametros);

            if (exceptionData == null)
            {
                Logger.Error(string.Format(@"\nMessage: {0}  \nMethod: {1}  \nParameters: {2}  \nTrace: {3}", exception.Message, nombreMetodo, parametrosValores, exception.StackTrace));

                return new ExceptionLogic(exception.Message, nombreMetodo, parametros);   
            }

            Logger.Error(string.Format(@"\nMessage: {0}  \nMethod: {1}  \nParameters: {2}  \nTrace: {3}", exceptionData, nombreMetodo, parametrosValores, exception.StackTrace));

            return new ExceptionLogic(exceptionData.ToString(), nombreMetodo, parametros);
        }
    }
}
