using System.Web.Mvc;
using PortalSocios.Models;
using System.Linq;

namespace PortalSocios.Controllers {
    public class HomeController : Controller {

        // cria um novo objeto que representa a BD
        private SociosBD db = new SociosBD();

        /// <summary>
        /// Mostra a VIEW da página inicial
        /// </summary>
        public ActionResult Index() {
            return View();
        }

        /// <summary>
        /// Mostra a VIEW da página 'Sobre'
        /// </summary>
        public ActionResult Sobre() {
            return View();
        }

        /// <summary>
        /// Mostra a VIEW da página 'Quotas'
        /// </summary>
        public ActionResult Quotas() {
            return View(db.Categorias.ToList());
        }

        /// <summary>
        /// Mostra a VIEW da página 'Vantagens'
        /// </summary>
        public ActionResult Vantagens() {
            return View(db.Beneficios.ToList());
        }
    }
}
