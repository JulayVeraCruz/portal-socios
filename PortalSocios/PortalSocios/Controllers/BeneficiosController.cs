using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PortalSocios.Models;
using System;

namespace PortalSocios.Controllers {
    [Authorize(Roles = "Administrador, Funcionario")]
    public class BeneficiosController : Controller {

        // cria um novo objeto que representa a BD
        private SociosBD db = new SociosBD();

        /// <summary>
        /// Mostra a VIEW da lista de benefícios
        /// GET: Beneficios
        /// </summary>
        /// <param name="pesquisar"></param>
        public ActionResult Index(string pesquisar) {

            var beneficio = db.Beneficios;

            // ref: https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-
            // permite efetuar a pesquisa de um benefício pela descrição ou pela entidade responsável
            if (!String.IsNullOrEmpty(pesquisar)) {
                return View(beneficio.Where(b => b.Descricao.ToUpper().Contains(pesquisar.ToUpper()) || b.EntidRespons.ToUpper().Contains(pesquisar.ToUpper())));
            }
            return View(beneficio.OrderBy(b => b.Descricao).ToList());
        }

        /// <summary>
        /// Mostra a VIEW dos detalhes de um benefício
        /// GET - ex.: Beneficios/Details/5
        /// </summary>
        /// <param name="id"></param>
        public ActionResult Details(int? id) {
            if (id == null) {
                return RedirectToAction("Index");
            }
            Beneficios beneficio = db.Beneficios.Find(id);
            if (beneficio == null) {
                return RedirectToAction("Index");
            }
            return View(beneficio);
        }

        /// <summary>
        /// Mostra a VIEW de criação de um benefício
        /// GET: Beneficios/Create
        /// </summary>
        public ActionResult Create() {
            return View();
        }

        /// <summary>
        /// Verifica se os dados introduzidos para a criação de um benefício são válidos
        /// e, se for o caso, cria esse benefício
        /// POST: Beneficios/Create
        /// </summary>
        /// <param name="beneficio"></param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Descricao,EntidRespons")] Beneficios beneficio) {
            try {
                if (ModelState.IsValid) {
                    db.Beneficios.Add(beneficio);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Não foi possível criar um novo benefício..."));
            }
            return View(beneficio);
        }

        /// <summary>
        /// Mostra a VIEW de edição de um benefício
        /// GET - ex.: Beneficios/Edit/5
        /// </summary>
        /// <param name="id"></param>
        public ActionResult Edit(int? id) {
            if (id == null) {
                return RedirectToAction("Index");
            }
            Beneficios beneficio = db.Beneficios.Find(id);
            if (beneficio == null) {
                return RedirectToAction("Index");
            }
            return View(beneficio);
        }

        /// <summary>
        /// Verifica se os dados introduzidos para a edição de um benefício
        /// são válidos e, se for o caso, edita esse benefício
        /// POST - ex.: Beneficios/Edit/5
        /// </summary>
        /// <param name="beneficio"></param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BeneficioID,Descricao,EntidRespons")] Beneficios beneficio) {
            try {
                if (ModelState.IsValid) {
                    db.Entry(beneficio).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Não foi possível editar este benefício..."));
            }
            return View(beneficio);
        }

        /// <summary>
        /// Mostra a VIEW de eliminação de um benefício
        /// GET - ex.: Beneficios/Delete/5
        /// </summary>
        /// <param name="id"></param>
        public ActionResult Delete(int? id) {
            if (id == null) {
                return RedirectToAction("Index");
            }
            Beneficios beneficio = db.Beneficios.Find(id);
            if (beneficio == null) {
                return RedirectToAction("Index");
            }
            return View(beneficio);
        }

        /// <summary>
        /// Verifica se é possível a eliminação de um benefício
        /// e, se for o caso, elimina esse benefício
        /// POST - ex.: Beneficios/Delete/5
        /// </summary>
        /// <param name="id"></param>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Beneficios beneficio = db.Beneficios.Find(id);
            try {
                db.Beneficios.Remove(beneficio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Não foi possível eliminar este benefício..."));
            }
            return View(beneficio);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
