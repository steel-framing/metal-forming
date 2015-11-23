using System;
using System.Net;
using System.Web.Mvc;
using log4net;

namespace MetalForming.Web.Core
{
    [HandleError]
    public class BaseController : Controller
    {
        #region Propiedades

        private static readonly ILog Logger = LogManager.GetLogger("o&c");

        #endregion

        #region Constructor

        #endregion

        #region Overrides

        protected override void OnException(ExceptionContext filterContext)
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var controllerName = filterContext.RouteData.Values["controller"];
            var actionName = filterContext.RouteData.Values["action"];

            Logger.Error(string.Format("Controlador:{0}  Action:{1}  Mensaje:{2}", controllerName, actionName, GetExceptionMessage(filterContext.Exception)));

            filterContext.Result = View("Error");
        }

        protected override void HandleUnknownAction(string actionName)
        {
            RedirectToAction("Index").ExecuteResult(ControllerContext);
        }

        #endregion

        #region Métodos

        protected void LogError(Exception exception)
        {
            Logger.Error(string.Format("Mensaje: {0} Trace: {1}", exception.Message, exception.StackTrace));
        }

        private string GetExceptionMessage(Exception ex)
        {
            if (ex.InnerException == null)
                return ex.Message;
            string errorMessage = string.Format("{0}<br/>{1}", ex.Message, GetExceptionMessage(ex.InnerException));

            return errorMessage;
        }

        #endregion
    }
}
