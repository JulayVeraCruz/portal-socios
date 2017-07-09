using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PortalSocios.Models;
using System;

namespace PortalSocios.Controllers {
    [Authorize(Roles = "Funcionario")]
    public class QuotasController : Controller {

        private SociosBD db = new SociosBD();

        // GET: Quotas
        public ActionResult Index() {
            var quotas = db.Quotas.Include(s => s.Categoria);
            return View(quotas.OrderBy(q => q.Referencia).ToList());
        }

        // GET: Quotas/Details/5
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

        // GET: Quotas/Create
        public ActionResult Create() {
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome");
            return View();
        }

        // POST: Quotas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                ModelState.AddModelError("", string.Format("Não foi possível criar uma nova quota. Verifique a referência..."));
            }
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", quota.CategoriaFK);
            return View(quota);
        }

        // GET: Quotas/Edit/5
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

        // POST: Quotas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                ModelState.AddModelError("", string.Format("Não foi possível editar esta quota. Verifique a referência..."));
            }
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", quota.CategoriaFK);
            return View(quota);
        }

        // GET: Quotas/Delete/5
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

        // POST: Quotas/Delete/5
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
                ModelState.AddModelError("", string.Format("Não foi possível eliminar esta quota."));
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
