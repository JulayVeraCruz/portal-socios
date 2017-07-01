using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PortalSocios.Models;

namespace PortalSocios.Controllers {
    public class SociosController : Controller {
        private SociosBD db = new SociosBD();

        // GET: Socios
        public ActionResult Index() {
            // ordena a lista de sócios pelo número de sócio
            return View(db.Socios.OrderBy(s => s.NumSocio).ToList());
        }

        // GET: Socios/Details/5
        public ActionResult Details(int? id) {
            // caso não seja indicado o id
            if (id == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            Socios socio = db.Socios.Find(id);
            // caso não exista o id indicado
            if (socio == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            return View(socio);
        }

        // GET: Socios/Create
        public ActionResult Create() {
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome");
            return View();
        }

        // POST: Socios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SocioID,NumSocio,Nome,BI,NIF,DataNasc,Email,Telemovel,Morada,CodPostal,Fotografia,DataInscr,CategoriaFK")] Socios socio) {
            if (ModelState.IsValid) {
                db.Socios.Add(socio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", socio.CategoriaFK);
            return View(socio);
        }

        // GET: Socios/Edit/5
        public ActionResult Edit(int? id) {
            // caso não seja indicado o id
            if (id == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            Socios socio = db.Socios.Find(id);
            // caso não exista o id indicado
            if (socio == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", socio.CategoriaFK);
            return View(socio);
        }

        // POST: Socios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SocioID,NumSocio,Nome,BI,NIF,DataNasc,Email,Telemovel,Morada,CodPostal,Fotografia,DataInscr,CategoriaFK")] Socios socio) {
            if (ModelState.IsValid) {
                db.Entry(socio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", socio.CategoriaFK);
            return View(socio);
        }

        // GET: Socios/Delete/5
        public ActionResult Delete(int? id) {
            // caso não seja indicado o id
            if (id == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            Socios socio = db.Socios.Find(id);
            // caso não exista o id indicado
            if (socio == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            return View(socio);
        }

        // POST: Socios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Socios socio = db.Socios.Find(id);
            db.Socios.Remove(socio);
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
