using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PortalSocios.Models;
using System;

namespace PortalSocios.Controllers {
    [Authorize(Roles = "Administrador, Funcionario")]
    public class QuotasController : Controller {

        // cria um novo objeto que representa a BD
        private SociosBD db = new SociosBD();

        /// <summary>
        /// Mostra a VIEW da lista de quotas
        /// GET: Quotas
        /// </summary>
        /// <param name="ordenar"></param>
        /// <param name="pesquisar"></param>
        public ActionResult Index(string ordenar, string pesquisar) {
            var quotas = db.Quotas.Include(s => s.Categoria);

            // ref: https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-
            ViewBag.OrdRef = String.IsNullOrEmpty(ordenar) ? "refDesc" : "";
            ViewBag.OrdMont = ordenar == "montAsc" ? "montDesc" : "montAsc";
            ViewBag.OrdAno = ordenar == "AnoAsc" ? "AnoDesc" : "AnoAsc";
            ViewBag.OrdPeriod = ordenar == "PeriodAsc" ? "PeriodDesc" : "PeriodAsc";
            ViewBag.OrdCateg = ordenar == "categAsc" ? "categDesc" : "categAsc";

            // permite efetuar a pesquisa de uma quota pela referência
            if (!String.IsNullOrEmpty(pesquisar)) {
                return View(quotas.Where(q => q.Referencia.ToUpper().Contains(pesquisar.ToUpper())));
            }

            // ordena a lista de quotas de forma ascendente ou descendente por coluna
            switch (ordenar) {
                case "refDesc":
                    return View(quotas.OrderByDescending(q => q.Referencia).ToList());
                case "montDesc":
                    return View(quotas.OrderByDescending(q => q.Montante).ToList());
                case "montAsc":
                    return View(quotas.OrderBy(q => q.Montante).ToList());
                case "AnoDesc":
                    return View(quotas.OrderByDescending(q => q.Ano).ToList());
                case "AnoAsc":
                    return View(quotas.OrderBy(q => q.Ano).ToList());
                case "PeriodDesc":
                    return View(quotas.OrderByDescending(q => q.Periodicidade).ToList());
                case "PeriodAsc":
                    return View(quotas.OrderBy(q => q.Periodicidade).ToList());
                case "categDesc":
                    return View(quotas.OrderByDescending(q => q.CategoriaFK).ToList());
                case "categAsc":
                    return View(quotas.OrderBy(q => q.CategoriaFK).ToList());
                default:
                    return View(quotas.OrderBy(q => q.Referencia).ToList());
            }
        }

        /// <summary>
        /// Mostra a VIEW dos detalhes de uma quota
        /// GET - ex.: Quotas/Details/5
        /// </summary>
        /// <param name="id"></param>
        public ActionResult Details(int? id) {
            if (id == null) {
                return RedirectToAction("Index");
            }
            Quotas quota = db.Quotas.Find(id);
            if (quota == null) {
                return RedirectToAction("Index");
            }
            return View(quota);
        }

        /// <summary>
        /// Mostra a VIEW de criação de uma quota
        /// GET: Quotas/Create
        /// </summary>
        public ActionResult Create() {
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome");
            return View();
        }

        /// <summary>
        /// Verifica se os dados introduzidos para a criação de uma quota são válidos
        /// e, se for o caso, cria essa quota
        /// POST: Quotas/Create
        /// </summary>
        /// <param name="quota"></param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Referencia,AuxMontante,Ano,Periodicidade,CategoriaFK")] Quotas quota) {
            try {
                // recuperar, converter e atribuir o valor do montante da quota
                quota.Montante = Convert.ToDecimal(quota.AuxMontante);
                if (ModelState.IsValid) {
                    db.Quotas.Add(quota);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Não foi possível criar uma nova quota...A referência já poderá existir."));
            }
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", quota.CategoriaFK);
            return View(quota);
        }

        /// <summary>
        /// Mostra a VIEW de edição de uma quota
        /// GET - ex.: Quotas/Edit/5
        /// </summary>
        /// <param name="id"></param>
        public ActionResult Edit(int? id) {
            if (id == null) {
                return RedirectToAction("Index");
            }
            Quotas quota = db.Quotas.Find(id);
            if (quota == null) {
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", quota.CategoriaFK);
            return View(quota);
        }

        /// <summary>
        /// Verifica se os dados introduzidos para a edição de uma quota
        /// são válidos e, se for o caso, edita essa quota
        /// POST - ex.: Quotas/Edit/5
        /// </summary>
        /// <param name="quota"></param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuotaID,Referencia,AuxMontante,Ano,Periodicidade,CategoriaFK")] Quotas quota) {
            // recuperar, converter e atribuir o valor do montante da quota
            quota.Montante = Convert.ToDecimal(quota.AuxMontante);
            try {
                if (ModelState.IsValid) {
                    db.Entry(quota).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Não foi possível editar esta quota...A referência já poderá existir."));
            }
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", quota.CategoriaFK);
            return View(quota);
        }

        /// <summary>
        /// Mostra a VIEW de eliminação de uma quota
        /// GET - ex.: Quotas/Delete/5
        /// </summary>
        /// <param name="id"></param>
        public ActionResult Delete(int? id) {
            if (id == null) {
                return RedirectToAction("Index");
            }
            Quotas quota = db.Quotas.Find(id);
            if (quota == null) {
                return RedirectToAction("Index");
            }
            return View(quota);
        }

        /// <summary>
        /// Verifica se é possível a eliminação de uma quota
        /// e, se for o caso, elimina essa quota
        /// POST - ex.: Quotas/Delete/5
        /// </summary>
        /// <param name="id"></param>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Quotas quota = db.Quotas.Find(id);
            try {
                db.Quotas.Remove(quota);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Não foi possível eliminar esta quota..."));
            }
            return View(quota);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
