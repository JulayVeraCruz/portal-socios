using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PortalSocios.Models;
using System;

namespace PortalSocios.Controllers {
    public class SociosController : Controller {
        private SociosBD db = new SociosBD();

        // GET: Socios
        public ActionResult Index() {
            // permite incluir os dados das categorias na View dos 'Socios'
            var socios = db.Socios.Include(s => s.Categoria);
            // ordena a lista de sócios pelo número de sócio
            return View(socios.OrderBy(s => s.NumSocio).ToList());
        }

        // GET: Socios/Details/5
        public ActionResult Details(int? id) {
            // caso não seja indicado o id
            if (id == null) {
                // redireciona para a página inicial
                return RedirectToAction("Index");
            }
            Socios socio = db.Socios.Find(id);
            // caso não exista o id indicado
            if (socio == null) {
                // redireciona para a página inicial
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
            try {
                // caso os dados a ser introduzidos estejam consistentes com o Model
                if (ModelState.IsValid) {
                    // adiciona um novo sócio
                    db.Socios.Add(socio);
                    // guarda as alterações
                    db.SaveChanges();
                    // redireciona para a página inicial
                    return RedirectToAction("Index");
                }
                else {
                    // ref: http://www.iminfo.in/post/change-message-the-value-is-not-valid-for-number-mvc-workarond
                    // caso a 'DataNasc' tenha um erro com o conteúdo indicado
                    if (ModelState["DataNasc"].Errors.Count == 1 && ModelState["DataNasc"].Errors[0].ErrorMessage.Contains("is not valid for")) {
                        // limpa as mensagens de erro
                        ModelState["DataNasc"].Errors.Clear();
                        // adiciona uma nova mensagem de erro
                        ModelState["DataNasc"].Errors.Add("Introduza uma data de nascimento válida!");
                    }
                    if (ModelState["DataInscr"].Errors.Count == 1 && ModelState["DataInscr"].Errors[0].ErrorMessage.Contains("is not valid for")) {
                        ModelState["DataInscr"].Errors.Clear();
                        ModelState["DataInscr"].Errors.Add("Introduza uma data de inscrição válida!");
                    }

                }
            }
            catch (Exception) {
                // cria uma mensagem de erro a ser apresentada ao utilizador
                ModelState.AddModelError("", string.Format("Ocorreu um erro na criação do novo sócio. Verifique o N.º de Sócio, o BI / CC e o NIF.", socio.NumSocio));
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
            try {
                if (ModelState.IsValid) {
                    db.Entry(socio).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else {
                    // ref: http://www.iminfo.in/post/change-message-the-value-is-not-valid-for-number-mvc-workarond
                    if (ModelState["DataNasc"].Errors.Count == 1 && ModelState["DataNasc"].Errors[0].ErrorMessage.Contains("is not valid for")) {
                        ModelState["DataNasc"].Errors.Clear();
                        ModelState["DataNasc"].Errors.Add("Introduza uma data de nascimento válida!");
                    }
                    if (ModelState["DataInscr"].Errors.Count == 1 && ModelState["DataInscr"].Errors[0].ErrorMessage.Contains("is not valid for")) {
                        ModelState["DataInscr"].Errors.Clear();
                        ModelState["DataInscr"].Errors.Add("Introduza uma data de inscrição válida!");
                    }
                }
            }
            catch (Exception) {
                // cria uma mensagem de erro a ser apresentada ao utilizador
                ModelState.AddModelError("", string.Format("Ocorreu um erro na edição do sócio. Verifique o N.º de Sócio, o BI / CC e o NIF.", socio.NumSocio));
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
            try {                
                db.Socios.Remove(socio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception) {
                // cria uma mensagem de erro a ser apresentada ao utilizador
                ModelState.AddModelError("", string.Format("Ocorreu um erro na eliminação do sócio com N.º de Sócio = {0}.", socio.NumSocio));
                return View(socio);
            }            
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
