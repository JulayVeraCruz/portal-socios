using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalSocios.Controllers {
    public class ErrosController : Controller {

        // GET: Erros
        // erro 404
        public ActionResult NotFound() {
            return View();
        }

        // erro de serviço indisponível
        public ActionResult Indisponivel() {
            return View();
        }

        // erro de acesso restrito
        public ActionResult Restrito() {
            return View();
        }
    }
}