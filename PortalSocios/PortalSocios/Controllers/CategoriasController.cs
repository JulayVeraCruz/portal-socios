using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PortalSocios.Models;
using System;

namespace PortalSocios.Controllers {
    public class CategoriasController : Controller
    {
        private SociosBD db = new SociosBD();

        // GET: Categorias
        public ActionResult Index() {
            return View(db.Categorias.OrderBy(c => c.ValorMensal).ToList());
        }

        // GET: Categorias/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return RedirectToAction("Index");
            }
            Categorias categoria = db.Categorias.Find(id);
            if (categoria == null) {
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // GET: Categorias/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,FaixaEtaria,NumQuotasAnuais,ValorMensal")] Categorias categoria) {
            try {
                // recuperar, converter e atribuir o Valor Mensal da Categoria
                categoria.ValorMensal = Convert.ToDecimal(categoria.AuxValorMensal);

                if (ModelState.IsValid) {
                    db.Categorias.Add(categoria);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Ocorreu um erro na criação de uma nova categoria."));
            }
            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return RedirectToAction("Index");
            }
            Categorias categoria = db.Categorias.Find(id);
            if (categoria == null) {
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoriaID,Nome,FaixaEtaria,NumQuotasAnuais,ValorMensal")] Categorias categoria) {
            try {
                // recuperar, converter e atribuir o Valor Mensal da Categoria
                categoria.ValorMensal = Convert.ToDecimal(categoria.AuxValorMensal);

                if (ModelState.IsValid) {
                    db.Entry(categoria).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Ocorreu um erro na edição da categoria."));
            }
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return RedirectToAction("Index");
            }
            Categorias categoria = db.Categorias.Find(id);
            if (categoria == null) {
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Categorias categoria = db.Categorias.Find(id);
            try {
                db.Categorias.Remove(categoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Ocorreu um erro na eliminação da categoria com ID = {0}.", categoria.CategoriaID));
            }
            return View(categoria);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
