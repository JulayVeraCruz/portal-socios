using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PortalSocios.Models;
using System;

namespace PortalSocios.Controllers {
    [Authorize(Roles = "Administrador, Funcionario, Socio")]
    public class PagamentosController : Controller {
        private SociosBD db = new SociosBD();

        // GET: Pagamentos
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

        // GET: Pagamentos/Details/5
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

        // GET: Pagamentos/Create
        [Authorize(Roles = "Administrador, Funcionario")]
        public ActionResult Create() {
            ViewBag.FuncionarioFK = new SelectList(db.Funcionarios, "FuncionarioID", "Nome");
            ViewBag.QuotaFK = new SelectList(db.Quotas, "QuotaID", "Referencia");
            ViewBag.SocioFK = new SelectList(db.Socios, "SocioID", "Nome");
            return View();
        }

        // POST: Pagamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Pagamentos/Edit/5
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

        // POST: Pagamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Pagamentos/Delete/5
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

        // POST: Pagamentos/Delete/5
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
