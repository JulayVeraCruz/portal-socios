using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PortalSocios.Models;
using System;

namespace PortalSocios.Controllers {
    [Authorize(Roles = "Administrador")]
    public class FuncionariosController : Controller {

        // cria um novo objeto que representa a BD
        private SociosBD db = new SociosBD();

        /// <summary>
        /// Mostra a VIEW da lista de funcionários
        /// GET: Funcionarios
        /// </summary>
        /// <param name="ordenar"></param>
        /// <param name="pesquisar"></param>
        public ActionResult Index(string ordenar, string pesquisar) {

            var funcionario = db.Funcionarios;

            // ref: https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-
            ViewBag.OrdNome = String.IsNullOrEmpty(ordenar) ? "nomeDesc" : "";
            ViewBag.OrdEntr = ordenar == "entrAsc" ? "entrDesc" : "entrAsc";

            // permite efetuar a pesquisa de um funcionário pelo nome
            if (!String.IsNullOrEmpty(pesquisar)) {
                return View(funcionario.Where(f => f.Nome.ToUpper().Contains(pesquisar.ToUpper())));
            }

            // ordena a lista de funcionários de forma ascendente ou descendente por coluna
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

        /// <summary>
        /// Mostra a VIEW dos detalhes de um funcionário
        /// GET - ex.: Funcionarios/Details/5
        /// </summary>
        /// <param name="id"></param>
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

        /// <summary>
        /// Mostra a VIEW de criação de um funcionário
        /// GET: Funcionarios/Create
        /// </summary>
        public ActionResult Create() {
            return View();
        }

        /// <summary>
        /// Verifica se os dados para a criação de um funcionário são válidos
        /// e, se for o caso, cria esse funcionário
        /// POST: Funcionarios/Create
        /// </summary>
        /// <param name="funcionario"></param>
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
                ModelState.AddModelError("", string.Format("Não foi possível criar um novo funcionário...O NIF já poderá existir."));
            }
            return View(funcionario);
        }

        /// <summary>
        /// Mostra a VIEW de edição de um funcionário
        /// GET - ex.: Funcionarios/Edit/5
        /// </summary>
        /// <param name="id"></param>
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

        /// <summary>
        /// Verifica se os dados introduzidos para a edição de um funcionário
        /// são válidos e, se for o caso, edita esse funcionário
        /// POST - ex.: Funcionarios/Edit/5
        /// </summary>
        /// <param name="funcionario"></param>
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
                ModelState.AddModelError("", string.Format("Não foi possível editar este funcionário...O NIF já poderá existir."));
            }
            return View(funcionario);
        }

        /// <summary>
        /// Mostra a VIEW de eliminação de um funcionário
        /// GET - ex.: Funcionarios/Delete/5
        /// </summary>
        /// <param name="id"></param>
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

        /// <summary>
        /// Verifica se é possível a eliminação de um funcionário
        /// e, se for o caso, elimina esse funcionário
        /// POST: Funcionarios/Delete/5
        /// </summary>
        /// <param name="id"></param>
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
                ModelState.AddModelError("", string.Format("Não foi possível eliminar este funcionário..."));
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
