using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PortalSocios.Models;
using System;

namespace PortalSocios.Controllers {
    public class BeneficiosController : Controller
    {
        private SociosBD db = new SociosBD();

        // GET: Beneficios
        public ActionResult Index() {
            return View(db.Beneficios.OrderBy(b => b.Descricao).ToList());
        }

        // GET: Beneficios/Details/5
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

        // GET: Beneficios/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Beneficios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                ModelState.AddModelError("", string.Format("Ocorreu um erro na criação de um novo benefício."));
            }
            return View(beneficio);
        }

        // GET: Beneficios/Edit/5
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

        // POST: Beneficios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                ModelState.AddModelError("", string.Format("Ocorreu um erro na edição do benefício."));
            }
            return View(beneficio);
        }

        // GET: Beneficios/Delete/5
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

        // POST: Beneficios/Delete/5
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
                ModelState.AddModelError("", string.Format("Ocorreu um erro na eliminação do benefício com ID = {0}.", beneficio.BeneficioID));
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
