using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PortalSocios.Models;

namespace PortalSocios.Controllers
{
    public class QuotasController : Controller
    {
        private SociosBD db = new SociosBD();

        // GET: Quotas
        public ActionResult Index()
        {
            var quotas = db.Quotas.Include(q => q.Categoria);
            return View(quotas.ToList());
        }

        // GET: Quotas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotas quotas = db.Quotas.Find(id);
            if (quotas == null)
            {
                return HttpNotFound();
            }
            return View(quotas);
        }

        // GET: Quotas/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome");
            return View();
        }

        // POST: Quotas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuotaID,Montante,Ano,Periodicidade,CategoriaFK")] Quotas quotas)
        {
            if (ModelState.IsValid)
            {
                db.Quotas.Add(quotas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", quotas.CategoriaFK);
            return View(quotas);
        }

        // GET: Quotas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotas quotas = db.Quotas.Find(id);
            if (quotas == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", quotas.CategoriaFK);
            return View(quotas);
        }

        // POST: Quotas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuotaID,Montante,Ano,Periodicidade,CategoriaFK")] Quotas quotas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quotas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", quotas.CategoriaFK);
            return View(quotas);
        }

        // GET: Quotas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotas quotas = db.Quotas.Find(id);
            if (quotas == null)
            {
                return HttpNotFound();
            }
            return View(quotas);
        }

        // POST: Quotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quotas quotas = db.Quotas.Find(id);
            db.Quotas.Remove(quotas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
