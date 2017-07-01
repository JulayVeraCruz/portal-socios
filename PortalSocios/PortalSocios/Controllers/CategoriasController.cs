using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PortalSocios.Models;

namespace PortalSocios.Controllers {
    public class CategoriasController : Controller
    {
        private SociosBD db = new SociosBD();

        // GET: Categorias
        public ActionResult Index() {
            // ordena a lista de categorias pelo valor mensal
            return View(db.Categorias.OrderBy(c => c.ValorMensal).ToList());
        }

        // GET: Categorias/Details/5
        public ActionResult Details(int? id) {
            // caso não seja indicado o id
            if (id == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            Categorias categoria = db.Categorias.Find(id);
            // caso não exista o id indicado
            if (categoria == null) {
                // redireciona para o Index
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
        public ActionResult Create([Bind(Include = "CategoriaID,Nome,FaixaEtaria,NumQuotasAnuais,ValorMensal")] Categorias categoria) {
            if (ModelState.IsValid) {
                db.Categorias.Add(categoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public ActionResult Edit(int? id) {
            // caso não seja indicado o id
            if (id == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            Categorias categoria = db.Categorias.Find(id);
            // caso não exista o id indicado
            if (categoria == null) {
                // redireciona para o Index
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
            if (ModelState.IsValid) {
                db.Entry(categoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(int? id) {
            // caso não seja indicado o id
            if (id == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            Categorias categoria = db.Categorias.Find(id);
            // caso não exista o id indicado
            if (categoria == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Categorias categoria = db.Categorias.Find(id);
            db.Categorias.Remove(categoria);
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
