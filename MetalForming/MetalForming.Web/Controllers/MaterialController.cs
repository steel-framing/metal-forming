using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MetalForming.Common;
using MetalForming.Web.Core;
using MetalForming.Web.Models;
using MetalForming.Web.MantenimientoService;

namespace MetalForming.Web.Controllers
{
    public class MaterialController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult BuscarMaterial(string codigo, string descripcion, int tipo, int min, int max, int estado)
        {
            var response = new JsonResponse();
            try
            {
                IList<Material> materiales;
                using (var service = new MantenimientoServiceClient())
                {
                    materiales = service.ListarMateriales(codigo, descripcion, tipo, min, max, estado);
                }

                response.Data = materiales;
                response.Success = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;

                LogError(ex);
            }
            return Json(response);
        }

        [HttpPost]
        public JsonResult GuardarMaterial(Material model)
        {
            var response = new JsonResponse();
            try
            {
                using (var service = new MantenimientoServiceClient())
                {
                    if (model.Id == 0)
                    {
                        response.Data = service.GuardarMaterial(model);
                    }
                    else
                    {
                        response.Data = service.ActualizarMaterial(model);
                    }
                }

                response.Success = true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                response.Message = ex.Message;
            }
            return Json(response);
        }

        [HttpPost]
        public JsonResult EliminarMaterial(int id)
        {
            var response = new JsonResponse();
            try
            {
                using (var service = new MantenimientoServiceClient())
                {
                    response.Data = service.EliminarMaterial(id);
                }

                response.Success = true;
            }
            catch (Exception ex)
            {
                LogError(ex);
                response.Message = ex.Message;
            }
            return Json(response);
        }
	}
}