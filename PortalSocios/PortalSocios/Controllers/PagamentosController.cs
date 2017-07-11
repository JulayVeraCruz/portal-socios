using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PortalSocios.Models;
using System;

namespace PortalSocios.Controllers {
    [Authorize(Roles = "Administrador, Funcionario, Socio")]
    public class PagamentosController : Controller {

        // cria um novo objeto que representa a BD
        private SociosBD db = new SociosBD();

        /// <summary>
        /// Mostra a VIEW da lista de pagamentos
        /// GET: Pagamentos
        /// </summary>
        /// <param name="pesquisar"></param>
        public ActionResult Index(string pesquisar) {
            var pagamentos = db.Pagamentos.Include(p => p.Funcionario).Include(p => p.Quota).Include(p => p.Socio);

            if (User.IsInRole("Administrador") || User.IsInRole("Funcionario")) {
                // ref: https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-
                // permite efetuar a pesquisa de um pagamento pela referência multibanco
                if (!String.IsNullOrEmpty(pesquisar)) {
                    return View(pagamentos.Where(p => p.RefMultibanco.Contains(pesquisar)));
                }
                return View(db.Pagamentos.OrderBy(p => p.RefMultibanco).ToList());
            }
            // lista apenas os pagamentos relativos ao sócio que se autenticou
            return View(db.Pagamentos.Where(p => p.UserName.Equals(User.Identity.Name)).OrderBy(p => p.DataPrevPagam).ToList());
        }

        /// <summary>
        /// Mostra a VIEW dos detalhes de um pagamento
        /// GET - ex.: Pagamentos/Details/5
        /// </summary>
        /// <param name="id"></param>
        public ActionResult Details(int? id) {
            if (id == null) {
                return RedirectToAction("Index");
            }
            Pagamentos pagamento = db.Pagamentos.Find(id);
            if (pagamento == null) {
                return RedirectToAction("Index");
            }
            return View(pagamento);
        }

        /// <summary>
        /// Mostra a VIEW de criação de um pagamento
        /// GET: Pagamentos/Create
        /// </summary>
        [Authorize(Roles = "Administrador, Funcionario")]
        public ActionResult Create() {
            ViewBag.FuncionarioFK = new SelectList(db.Funcionarios, "FuncionarioID", "Nome");
            ViewBag.QuotaFK = new SelectList(db.Quotas, "QuotaID", "Referencia");
            ViewBag.SocioFK = new SelectList(db.Socios, "SocioID", "Nome");
            return View();
        }

        /// <summary>
        /// Verifica se os dados introduzidos para a criação de um pagamento são válidos
        /// e, se for o caso, cria esse pagamento
        /// POST: Pagamentos/Create
        /// </summary>
        /// <param name="pagamento"></param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Funcionario")]
        public ActionResult Create([Bind(Include = "RefMultibanco,AuxMontante,DataPagam,DataPrevPagam,AuxMulta,QuotaFK,SocioFK,UserName,FuncionarioFK")] Pagamentos pagamento) {
            try {
                // recuperar, converter e atribuir o valor do montante e da multa do pagamento
                pagamento.Montante = Convert.ToDecimal(pagamento.AuxMontante);
                pagamento.Multa = Convert.ToDecimal(pagamento.AuxMulta);

                if (ModelState.IsValid) {
                    db.Pagamentos.Add(pagamento);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Não foi possível criar um novo pagamento...A referência multibanco já poderá existir."));
            }
            ViewBag.FuncionarioFK = new SelectList(db.Funcionarios, "FuncionarioID", "Nome", pagamento.FuncionarioFK);
            ViewBag.QuotaFK = new SelectList(db.Quotas, "QuotaID", "Referencia", pagamento.QuotaFK);
            ViewBag.SocioFK = new SelectList(db.Socios, "SocioID", "Nome", pagamento.SocioFK);
            return View(pagamento);
        }

        /// <summary>
        /// Mostra a VIEW de edição de um pagamento
        /// GET - ex.: Pagamentos/Edit/5
        /// </summary>
        /// <param name="id"></param>
        [Authorize(Roles = "Administrador, Funcionario")]
        public ActionResult Edit(int? id) {
            if (id == null) {
                return RedirectToAction("Index");
            }
            Pagamentos pagamento = db.Pagamentos.Find(id);
            if (pagamento == null) {
                return RedirectToAction("Index");
            }
            ViewBag.FuncionarioFK = new SelectList(db.Funcionarios, "FuncionarioID", "Nome", pagamento.FuncionarioFK);
            ViewBag.QuotaFK = new SelectList(db.Quotas, "QuotaID", "Referencia", pagamento.QuotaFK);
            ViewBag.SocioFK = new SelectList(db.Socios, "SocioID", "Nome", pagamento.SocioFK);
            return View(pagamento);
        }

        /// <summary>
        /// Verifica se os dados introduzidos para a edição de um pagamento
        /// são válidos e, se for o caso, edita esse pagamento
        /// POST: Pagamentos/Edit/5
        /// </summary>
        /// <param name="pagamento"></param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Funcionario")]
        public ActionResult Edit([Bind(Include = "PagamentoID,RefMultibanco,AuxMontante,DataPagam,DataPrevPagam,AuxMulta,QuotaFK,SocioFK,UserName,FuncionarioFK")] Pagamentos pagamento) {
            try {
                // recuperar, converter e atribuir o valor do montante e da multa do pagamento
                pagamento.Montante = Convert.ToDecimal(pagamento.AuxMontante);
                pagamento.Multa = Convert.ToDecimal(pagamento.AuxMulta);

                if (ModelState.IsValid) {
                    db.Entry(pagamento).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Não foi possível editar este pagamento...A referência multibanco já poderá existir."));
            }
            ViewBag.FuncionarioFK = new SelectList(db.Funcionarios, "FuncionarioID", "Nome", pagamento.FuncionarioFK);
            ViewBag.QuotaFK = new SelectList(db.Quotas, "QuotaID", "Referencia", pagamento.QuotaFK);
            ViewBag.SocioFK = new SelectList(db.Socios, "SocioID", "Nome", pagamento.SocioFK);
            return View(pagamento);
        }

        /// <summary>
        /// Mostra a VIEW de eliminação de um pagamento
        /// GET - ex.: Pagamentos/Delete/5
        /// </summary>
        /// <param name="id"></param>
        [Authorize(Roles = "Administrador, Funcionario")]
        public ActionResult Delete(int? id) {
            if (id == null) {
                return RedirectToAction("Index");
            }
            Pagamentos pagamento = db.Pagamentos.Find(id);
            if (pagamento == null) {
                return RedirectToAction("Index");
            }
            return View(pagamento);
        }

        /// <summary>
        /// Verifica se é possível a eliminação de um pagamento
        /// e, se for o caso, elimina esse pagamento
        /// POST: Pagamentos/Delete/5
        /// </summary>
        /// <param name="id"></param>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Funcionario")]
        public ActionResult DeleteConfirmed(int id) {
            Pagamentos pagamento = db.Pagamentos.Find(id);
            try {
                db.Pagamentos.Remove(pagamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Não foi possível eliminar este pagamento..."));
            }
            return View(pagamento);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
