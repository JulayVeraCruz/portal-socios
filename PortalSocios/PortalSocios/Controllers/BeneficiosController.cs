using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PortalSocios.Models;

namespace PortalSocios.Controllers {
    public class BeneficiosController : Controller
    {
        private SociosBD db = new SociosBD();

        // GET: Beneficios
        public ActionResult Index() {
            // ordena a lista de benefícios pela descrição
            return View(db.Beneficios.OrderBy(b => b.Descricao).ToList());
        }

        // GET: Beneficios/Details/5
        public ActionResult Details(int? id) {
            // caso não seja indicado o id
            if (id == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            Beneficios beneficio = db.Beneficios.Find(id);
            // caso não exista o id indicado
            if (beneficio == null) {
                // redireciona para o Index
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
        public ActionResult Create([Bind(Include = "BeneficioID,Descricao,EntidRespons")] Beneficios beneficio) {
            if (ModelState.IsValid) {
                db.Beneficios.Add(beneficio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(beneficio);
        }

        // GET: Beneficios/Edit/5
        public ActionResult Edit(int? id) {
            // caso não seja indicado o id
            if (id == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            Beneficios beneficio = db.Beneficios.Find(id);
            // caso não exista o id indicado
            if (beneficio == null) {
                // redireciona para o Index
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
            if (ModelState.IsValid) {
                db.Entry(beneficio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(beneficio);
        }

        // GET: Beneficios/Delete/5
        public ActionResult Delete(int? id) {
            // caso não seja indicado o id
            if (id == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            Beneficios beneficio = db.Beneficios.Find(id);
            // caso não exista o id indicado
            if (beneficio == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            return View(beneficio);
        }

        // POST: Beneficios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Beneficios beneficio = db.Beneficios.Find(id);
            db.Beneficios.Remove(beneficio);
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
