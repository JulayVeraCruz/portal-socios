using System.Web.Mvc;
using PortalSocios.Models;
using System.Linq;

namespace PortalSocios.Controllers
{
    public class HomeController : Controller {

        private SociosBD db = new SociosBD();

        public ActionResult Index() {
            return View();
        }

        public ActionResult Sobre() {
            return View();
        }

        public ActionResult Vantagens() {
            return View(db.Beneficios.ToList());
        }

        public ActionResult Quotas() {
            return View(db.Categorias.ToList());
        }
    }
}
