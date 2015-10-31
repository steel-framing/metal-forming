using System.Web.Mvc;
using MetalForming.Web.Core;

namespace MetalForming.Web.Controllers
{
    public class InicioController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}