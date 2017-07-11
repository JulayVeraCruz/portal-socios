using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalSocios.Controllers {
    public class ErrosController : Controller {

        /// <summary>
        /// Mostra a página de erro, para o erro 404
        /// GET: Erros
        /// </summary>
        public ActionResult NotFound() {
            return View();
        }

        /// <summary>
        /// Mostra a página de erro, para serviço indisponível
        /// GET: Erros
        /// </summary>
        public ActionResult Indisponivel() {
            return View();
        }

        /// <summary>
        /// Mostra a página de erro, para acesso restrito
        /// GET: Erros
        /// </summary>
        public ActionResult Restrito() {
            return View();
        }
    }
}