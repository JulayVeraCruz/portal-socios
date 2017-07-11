using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PortalSocios.Models;
using System;

namespace PortalSocios.Controllers {
    [Authorize(Roles = "Administrador, Funcionario")]
    public class CategoriasController : Controller {

        // cria um novo objeto que representa a BD
        private SociosBD db = new SociosBD();

        /// <summary>
        /// Mostra a VIEW da lista de categorias
        /// GET: Categorias
        /// </summary>
        /// <param name="pesquisar"></param>
        public ActionResult Index(string pesquisar) {

            var categorias = db.Categorias;

            // ref: https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-
            // permite efetuar a pesquisa de uma categoria pelo nome
            if (!String.IsNullOrEmpty(pesquisar)) {
                return View(categorias.Where(c => c.Nome.ToUpper().Contains(pesquisar.ToUpper())));
            }
            return View(categorias.OrderBy(c => c.Nome).ToList());
        }

        /// <summary>
        /// Mostra a VIEW dos detalhes de uma categoria
        /// GET - ex.: Categorias/Details/5
        /// </summary>
        /// <param name="id"></param>
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

        /// <summary>
        /// Mostra a VIEW de criação de uma categoria
        /// GET: Categorias/Create
        /// </summary>
        public ActionResult Create() {
            return View();
        }

        /// <summary>
        /// Verifica se os dados para a criação de uma categoria são válidos
        /// e, se for o caso, cria essa categoria
        /// POST: Categorias/Create
        /// </summary>
        /// <param name="categoria"></param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,FaixaEtaria,NumQuotasAnuais,AuxValorMensal")] Categorias categoria) {
            try {
                // recuperar, converter e atribuir o valor mensal da categoria
                categoria.ValorMensal = Convert.ToDecimal(categoria.AuxValorMensal);

                if (ModelState.IsValid) {
                    db.Categorias.Add(categoria);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Não foi possível criar uma nova categoria..."));
            }
            return View(categoria);
        }

        /// <summary>
        /// Mostra a VIEW de edição de uma categoria
        /// GET - ex.: Categorias/Edit/5
        /// </summary>
        /// <param name="id"></param>
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

        /// <summary>
        /// Verifica se os dados introduzidos para a edição de uma categoria
        /// são válidos e, se for o caso, edita essa categoria
        /// POST - ex.: Categorias/Edit/5
        /// </summary>
        /// <param name="categoria"></param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoriaID,Nome,FaixaEtaria,NumQuotasAnuais,AuxValorMensal")] Categorias categoria) {
            try {
                // recuperar, converter e atribuir o valor mensal da categoria
                categoria.ValorMensal = Convert.ToDecimal(categoria.AuxValorMensal);

                if (ModelState.IsValid) {
                    db.Entry(categoria).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Não foi possível editar esta categoria..."));
            }
            return View(categoria);
        }

        /// <summary>
        /// Mostra a VIEW de eliminação de uma categoria
        /// GET - ex.: Categorias/Delete/5
        /// </summary>
        /// <param name="id"></param>
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

        /// <summary>
        /// Verifica se é possível a eliminação de uma categoria
        /// e, se for o caso, elimina essa categoria
        /// POST: Categorias/Delete/5
        /// </summary>
        /// <param name="id"></param>
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
                ModelState.AddModelError("", string.Format("Não foi possível eliminar esta categoria..."));
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
