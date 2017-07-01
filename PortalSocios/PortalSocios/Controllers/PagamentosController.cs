using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PortalSocios.Models;

namespace PortalSocios.Controllers {
    public class PagamentosController : Controller
    {
        private SociosBD db = new SociosBD();

        // GET: Pagamentos
        public ActionResult Index() {
            // permite incluir os dados dos funcionários, das quotas e dos sócios na View dos 'Pagamentos'
            var pagamentos = db.Pagamentos.Include(p => p.Funcionario).Include(p => p.Quota).Include(p => p.Socio);
            // ordena a lista dos pagamentos pelo nome do sócio e depois pela data prevista de pagamento
            return View(pagamentos.OrderBy(p => p.Socio.Nome).ThenBy(p => p.DataPrevPagam).ToList());
        }

        // GET: Pagamentos/Details/5
        public ActionResult Details(int? id) {
            // caso não seja indicado o id
            if (id == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            Pagamentos pagamento = db.Pagamentos.Find(id);
            // caso não exista o id indicado
            if (pagamento == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            return View(pagamento);
        }

        // GET: Pagamentos/Create
        public ActionResult Create() {
            ViewBag.FuncionarioFK = new SelectList(db.Funcionarios, "FuncionarioID", "Nome");
            ViewBag.QuotaFK = new SelectList(db.Quotas, "QuotaID", "Periodicidade");
            ViewBag.SocioFK = new SelectList(db.Socios, "SocioID", "Nome");
            return View();
        }

        // POST: Pagamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PagamentoID,RefMultibanco,Montante,DataPagam,DataPrevPagam,Multa,QuotaFK,SocioFK,FuncionarioFK")] Pagamentos pagamento) {
            if (ModelState.IsValid) {
                db.Pagamentos.Add(pagamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FuncionarioFK = new SelectList(db.Funcionarios, "FuncionarioID", "Nome", pagamento.FuncionarioFK);
            ViewBag.QuotaFK = new SelectList(db.Quotas, "QuotaID", "Periodicidade", pagamento.QuotaFK);
            ViewBag.SocioFK = new SelectList(db.Socios, "SocioID", "Nome", pagamento.SocioFK);
            return View(pagamento);
        }

        // GET: Pagamentos/Edit/5
        public ActionResult Edit(int? id) {
            // caso não seja indicado o id
            if (id == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            Pagamentos pagamento = db.Pagamentos.Find(id);
            // caso não exista o id indicado
            if (pagamento == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            ViewBag.FuncionarioFK = new SelectList(db.Funcionarios, "FuncionarioID", "Nome", pagamento.FuncionarioFK);
            ViewBag.QuotaFK = new SelectList(db.Quotas, "QuotaID", "Periodicidade", pagamento.QuotaFK);
            ViewBag.SocioFK = new SelectList(db.Socios, "SocioID", "Nome", pagamento.SocioFK);
            return View(pagamento);
        }

        // POST: Pagamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PagamentoID,RefMultibanco,Montante,DataPagam,DataPrevPagam,Multa,QuotaFK,SocioFK,FuncionarioFK")] Pagamentos pagamento) {
            if (ModelState.IsValid) {
                db.Entry(pagamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FuncionarioFK = new SelectList(db.Funcionarios, "FuncionarioID", "Nome", pagamento.FuncionarioFK);
            ViewBag.QuotaFK = new SelectList(db.Quotas, "QuotaID", "Periodicidade", pagamento.QuotaFK);
            ViewBag.SocioFK = new SelectList(db.Socios, "SocioID", "Nome", pagamento.SocioFK);
            return View(pagamento);
        }

        // GET: Pagamentos/Delete/5
        public ActionResult Delete(int? id) {
            // caso não seja indicado o id
            if (id == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            Pagamentos pagamento = db.Pagamentos.Find(id);
            // caso não exista o id indicado
            if (pagamento == null) {
                // redireciona para o Index
                return RedirectToAction("Index");
            }
            return View(pagamento);
        }

        // POST: Pagamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Pagamentos pagamento = db.Pagamentos.Find(id);
            db.Pagamentos.Remove(pagamento);
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
