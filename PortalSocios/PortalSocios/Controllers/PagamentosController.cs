using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PortalSocios.Models;
using System;

namespace PortalSocios.Controllers {
    public class PagamentosController : Controller
    {
        private SociosBD db = new SociosBD();

        // GET: Pagamentos
        public ActionResult Index() {
            var pagamentos = db.Pagamentos.Include(p => p.Funcionario).Include(p => p.Quota).Include(p => p.Socio);
            return View(pagamentos.OrderBy(p => p.Socio.Nome).ThenBy(p => p.DataPrevPagam).ToList());
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
        public ActionResult Create([Bind(Include = "RefMultibanco,Montante,DataPagam,DataPrevPagam,Multa,QuotaFK,SocioFK,FuncionarioFK")] Pagamentos pagamento) {
            try {
                // recuperar, converter e atribuir o valor do Montante e da Multa do Pagamento
                pagamento.Montante = Convert.ToDecimal(pagamento.AuxMontante);
                pagamento.Multa = Convert.ToDecimal(pagamento.AuxMulta);

                if (ModelState.IsValid) {
                    db.Pagamentos.Add(pagamento);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Ocorreu um erro na criação de um novo pagamento. Verifique a referência multibanco."));
            }
            ViewBag.FuncionarioFK = new SelectList(db.Funcionarios, "FuncionarioID", "Nome", pagamento.FuncionarioFK);
            ViewBag.QuotaFK = new SelectList(db.Quotas, "QuotaID", "Referencia", pagamento.QuotaFK);
            ViewBag.SocioFK = new SelectList(db.Socios, "SocioID", "Nome", pagamento.SocioFK);
            return View(pagamento);
        }

        // GET: Pagamentos/Edit/5
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
        public ActionResult Edit([Bind(Include = "PagamentoID,RefMultibanco,Montante,DataPagam,DataPrevPagam,Multa,QuotaFK,SocioFK,FuncionarioFK")] Pagamentos pagamento) {
            try {
                // recuperar, converter e atribuir o valor do Montante e da Multa do Pagamento
                pagamento.Montante = Convert.ToDecimal(pagamento.AuxMontante);
                pagamento.Multa = Convert.ToDecimal(pagamento.AuxMulta);

                if (ModelState.IsValid) {
                    db.Entry(pagamento).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Ocorreu um erro na edição do pagamento. Verifique a referência multibanco."));
            }
            ViewBag.FuncionarioFK = new SelectList(db.Funcionarios, "FuncionarioID", "Nome", pagamento.FuncionarioFK);
            ViewBag.QuotaFK = new SelectList(db.Quotas, "QuotaID", "Referencia", pagamento.QuotaFK);
            ViewBag.SocioFK = new SelectList(db.Socios, "SocioID", "Nome", pagamento.SocioFK);
            return View(pagamento);
        }

        // GET: Pagamentos/Delete/5
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
        public ActionResult DeleteConfirmed(int id) {
            Pagamentos pagamento = db.Pagamentos.Find(id);
            try {
                db.Pagamentos.Remove(pagamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Ocorreu um erro na eliminação do pagamento com Referência Multibanco = {0}.", pagamento.RefMultibanco));
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
