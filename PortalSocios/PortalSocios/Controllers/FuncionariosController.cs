using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PortalSocios.Models;

namespace PortalSocios.Controllers {
    public class FuncionariosController : Controller
    {
        private SociosBD db = new SociosBD();

        // GET: Funcionarios
        public ActionResult Index() {
            // ordena a lista de funcionários pelo nome do funcionário
            return View(db.Funcionarios.OrderBy(f => f.Nome).ToList());
        }

        // GET: Funcionarios/Details/5
        public ActionResult Details(int? id) {
            // caso não seja indicado o id
            if (id == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            Funcionarios funcionario = db.Funcionarios.Find(id);
            // caso não exista o id indicado
            if (funcionario == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FuncionarioID,Nome,NIF,Telemovel,Morada,CodPostal,DataEntrClube")] Funcionarios funcionario) {
            if (ModelState.IsValid) {
                db.Funcionarios.Add(funcionario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        public ActionResult Edit(int? id) {
            // caso não seja indicado o id
            if (id == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            Funcionarios funcionario = db.Funcionarios.Find(id);
            // caso não exista o id indicado
            if (funcionario == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FuncionarioID,Nome,NIF,Telemovel,Morada,CodPostal,DataEntrClube")] Funcionarios funcionario) {
            if (ModelState.IsValid) {
                db.Entry(funcionario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        public ActionResult Delete(int? id) {
            // caso não seja indicado o id
            if (id == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            Funcionarios funcionario = db.Funcionarios.Find(id);
            // caso não exista o id indicado
            if (funcionario == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Funcionarios funcionario = db.Funcionarios.Find(id);
            db.Funcionarios.Remove(funcionario);
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
