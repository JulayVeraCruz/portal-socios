using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PortalSocios.Models;
using System;

namespace PortalSocios.Controllers {
    [Authorize(Roles = "Administrador")]
    public class FuncionariosController : Controller {
        private SociosBD db = new SociosBD();

        // GET: Funcionarios
        public ActionResult Index(string ordenar, string pesquisar) {

            var funcionario = db.Funcionarios;

            // ref: https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-
            ViewBag.OrdNome = String.IsNullOrEmpty(ordenar) ? "nomeDesc" : "";
            ViewBag.OrdEntr = ordenar == "entrAsc" ? "entrDesc" : "entrAsc";

            // permite efetuar a pesquisa de um funcionário pelo nome
            if (!String.IsNullOrEmpty(pesquisar)) {
                return View(funcionario.Where(f => f.Nome.ToUpper().Contains(pesquisar.ToUpper())));
            }

            // ordena a lista de funcionários de forma ascendente ou descendente, pelo atributo escolhido
            switch (ordenar) {
                case "nomeDesc":
                    return View(funcionario.OrderByDescending(f => f.Nome).ToList());
                case "entrDesc":
                    return View(funcionario.OrderByDescending(f => f.DataEntrClube).ToList());
                case "entrAsc":
                    return View(funcionario.OrderBy(f => f.DataEntrClube).ToList());
                default:
                    return View(funcionario.OrderBy(f => f.Nome).ToList());
            }
        }

        // GET: Funcionarios/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return RedirectToAction("Index");
            }
            Funcionarios funcionario = db.Funcionarios.Find(id);
            if (funcionario == null) {
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
        public ActionResult Create([Bind(Include = "Nome,NIF,Email,Telemovel,Morada,CodPostal,DataEntrClube")] Funcionarios funcionario) {
            try {
                if (ModelState.IsValid) {
                    db.Funcionarios.Add(funcionario);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Não foi possível criar um novo funcionário. Verifique o NIF..."));
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return RedirectToAction("Index");
            }
            Funcionarios funcionario = db.Funcionarios.Find(id);
            if (funcionario == null) {
                return RedirectToAction("Index");
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FuncionarioID,Nome,NIF,Email,Telemovel,Morada,CodPostal,DataEntrClube")] Funcionarios funcionario) {
            try {
                if (ModelState.IsValid) {
                    db.Entry(funcionario).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Não foi possível editar este funcionário. Verifique o NIF..."));
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return RedirectToAction("Index");
            }
            Funcionarios funcionario = db.Funcionarios.Find(id);
            if (funcionario == null) {
                return RedirectToAction("Index");
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Funcionarios funcionario = db.Funcionarios.Find(id);
            try {
                db.Funcionarios.Remove(funcionario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Não foi possível eliminar este funcionário."));
            }
            return View(funcionario);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
