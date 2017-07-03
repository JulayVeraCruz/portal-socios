using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PortalSocios.Models;
using System;

namespace PortalSocios.Controllers {
    public class QuotasController : Controller {
        private SociosBD db = new SociosBD();

        // GET: Quotas
        public ActionResult Index() {
            // permite incluir os dados das categorias na View dos 'Quotas'
            var quotas = db.Quotas.Include(s => s.Categoria);
            // ordena a lista de sócios pelo número de sócio
            return View(quotas.OrderBy(q => q.Referencia).ToList());
        }

        // GET: Quotas/Details/5
        public ActionResult Details(int? id) {
            // caso não seja indicado o id
            if (id == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            Quotas quota = db.Quotas.Find(id);
            // caso não exista o id indicado
            if (quota == null) {
                // redireciona para o Index
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
        public ActionResult Create([Bind(Include = "QuotaID,Referencia,Montante,Ano,Periodicidade,CategoriaFK")] Quotas quota) {
            try {
                if (ModelState.IsValid) {
                    db.Quotas.Add(quota);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else {
                    //
                    if (ModelState["Montante"].Errors.Count == 1 && ModelState["Montante"].Errors[0].ErrorMessage.Contains("is not valid for")) {
                        //
                        ModelState["Montante"].Errors.Clear();
                        //
                        ModelState["Montante"].Errors.Add("Introduza um valor inteiro ou decimal, no formato 0,00.");
                    }
                }
            }
            catch (Exception ex) {
                // cria uma mensagem de erro a ser apresentada ao utilizador
                ModelState.AddModelError("", string.Format("Ocorreu um erro na criação de uma nova quota."));
            }
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", quota.CategoriaFK);
            return View(quota);
        }

        // GET: Quotas/Edit/5
        public ActionResult Edit(int? id) {
            // caso não seja indicado o id
            if (id == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            Quotas quota = db.Quotas.Find(id);
            // caso não exista o id indicado
            if (quota == null) {
                // redireciona para o Index
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
        public ActionResult Edit([Bind(Include = "QuotaID,Referencia,Montante,Ano,Periodicidade,CategoriaFK")] Quotas quota) {
            if (ModelState.IsValid) {
                db.Entry(quota).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            } else {
                //
                if (ModelState["Montante"].Errors.Count == 1 && ModelState["Montante"].Errors[0].ErrorMessage.Contains("is not valid for")) {
                    //
                    ModelState["Montante"].Errors.Clear();
                    //
                    ModelState["Montante"].Errors.Add("Introduza um valor inteiro ou decimal, no formato 0,00.");
                }
            }

            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", quota.CategoriaFK);
            return View(quota);
        }

        // GET: Quotas/Delete/5
        public ActionResult Delete(int? id) {
            // caso não seja indicado o id
            if (id == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            Quotas quota = db.Quotas.Find(id);
            // caso não exista o id indicado
            if (quota == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            return View(quota);
        }

        // POST: Quotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Quotas quota = db.Quotas.Find(id);
            db.Quotas.Remove(quota);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
