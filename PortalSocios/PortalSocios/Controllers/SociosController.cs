using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PortalSocios.Models;
using System;

namespace PortalSocios.Controllers {
    public class SociosController : Controller {

        // cria um novo objeto que representa a BD
        private SociosBD db = new SociosBD();

        // GET: Socios
        public ActionResult Index() {
            // caso o utilizador seja funcionário
            if (User.IsInRole("Funcionario")) {
                // inclui os dados das categorias na VIEW dos 'Socios'
                var socios = db.Socios.Include(s => s.Categoria);
                // ordena a lista de sócios pelo número de sócio
                return View(socios.OrderBy(s => s.NumSocio).ToList());
            }
            // redireciona para a página inicial da aplicação
            return RedirectToAction("Shared", "NotFound");
        }

        // GET: Socios/Details/5
        public ActionResult Details(int? id) {
            // caso o utilizador seja funcionário
            if (User.IsInRole("Funcionario")) {
                // caso não se indique um id
                if (id == null) {
                // redireciona para a lista de sócios
                    return RedirectToAction("Index");
                }
                // procura o sócio com o id indicado
                Socios socio = db.Socios.Find(id);
                // caso não exista o sócio
                if (socio == null) {
                    // redireciona para a lista de sócios
                    return RedirectToAction("Index");
                }
                // vai para a VIEW dos detalhes do sócio
                return View(socio);
            }
            // redireciona para a página inicial da aplicação
            return RedirectToAction("Shared", "NotFound");
        }

        // GET: Socios/Create
        public ActionResult Create() {
            // caso o utilizador seja funcionário
            if (User.IsInRole("Funcionario")) {
                ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome");
                return View();
            }
            // redireciona para a página inicial da aplicação
            return RedirectToAction("Index", "Home");
        }

        // POST: Socios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,BI,NIF,DataNasc,Email,Telemovel,Morada,CodPostal,Fotografia,DataInscr,CategoriaFK")] Socios socio) {
            // caso o utilizador seja funcionário
            if (User.IsInRole("Funcionario")) {
                // determinar o NumSocio a atribuir ao novo 'Socio'
                int novoNumSocio = 0;
                try {
                    novoNumSocio = db.Socios.Max(s => s.NumSocio) + 1;
                }
                catch (Exception) {
                    // caso não existam dados na BD (NULL)
                    novoNumSocio = 1;
                }
                // atribui um número de sócio 
                socio.NumSocio = novoNumSocio;
                // atribui a data corrente à data de inscrição
                socio.DataInscr = DateTime.Today;
                // atribui o e-mail ao username do sócio
                socio.UserName = socio.Email;
                try {                
                    // caso os dados introduzidos estejam consistentes com o MODEL
                    if (ModelState.IsValid) {
                        // adiciona um novo sócio
                        db.Socios.Add(socio);
                        // guarda as alterações
                        db.SaveChanges();
                        // redireciona para a lista de sócios
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception) {
                    // cria uma mensagem de erro a ser apresentada ao utilizador
                    ModelState.AddModelError("", string.Format("Ocorreu um erro na criação de um novo sócio. Verifique o BI/CC e o NIF."));
                }
                ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", socio.CategoriaFK);
                // volta para a VIEW da criação do sócio
                return View(socio);
            }
            // redireciona para a página inicial da aplicação
            return RedirectToAction("Index", "Home");
        }

        // GET: Socios/Edit/5
        public ActionResult Edit(int? id) {
            // caso o utilizador seja funcionário
            if (User.IsInRole("Funcionario")) {
                // caso não se indique um id
                if (id == null) {
                    // redireciona para a lista de sócios
                    return RedirectToAction("Index");
                }
                // procura o sócio com o id indicado
                Socios socio = db.Socios.Find(id);
                // caso não exista o sócio
                if (socio == null) {
                    // redireciona para a lista de sócios
                    return RedirectToAction("Index");
                }
                ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", socio.CategoriaFK);
                // vai para a VIEW da edição do sócio
                return View(socio);
            }
            // redireciona para a página inicial da aplicação
            return RedirectToAction("Index", "Home");
        }

        // POST: Socios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SocioID,NumSocio,Nome,BI,NIF,DataNasc,Email,Telemovel,Morada,CodPostal,Fotografia,DataInscr,CategoriaFK,UserName")] Socios socio) {
            // caso o utilizador seja funcionário
            if (User.IsInRole("Funcionario")) {
                try {
                    // caso os dados introduzidos estejam consistentes com o MODEL
                    if (ModelState.IsValid) {
                        // altera os dados do sócio
                        db.Entry(socio).State = EntityState.Modified;
                        // guarda as alterações
                        db.SaveChanges();
                        // redireciona para a lista de sócios
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex) {
                    // cria uma mensagem de erro a ser apresentada ao utilizador
                    String innerMessage = (ex.InnerException != null)
                      ? ex.InnerException.Message
                      : "";
                    ModelState.AddModelError("", innerMessage);
                }
                ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", socio.CategoriaFK);
                // volta para a VIEW da edição do sócio
                return View(socio);
            }
            // redireciona para a página inicial da aplicação
            return RedirectToAction("Index", "Home");
        }

        // GET: Socios/Delete/5
        public ActionResult Delete(int? id) {
            // caso o utilizador seja funcionário
            if (User.IsInRole("Funcionario")) {
                // caso não se indique um id
                if (id == null) {
                    // redireciona para a lista de sócios
                    return RedirectToAction("Index");
                }
                // procura o sócio com o id indicado
                Socios socio = db.Socios.Find(id);
                // caso não exista o sócio
                if (socio == null) {
                    // redireciona para a lista de sócios
                    return RedirectToAction("Index");
                }
                // vai para a VIEW de eliminação do sócio
                return View(socio);
            }
            // redireciona para a página inicial da aplicação
            return RedirectToAction("Index", "Home");
        }

        // POST: Socios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            // caso o utilizador seja funcionário
            if (User.IsInRole("Funcionario")) {
                // procura o sócio com o id indicado
                Socios socio = db.Socios.Find(id);
                try {
                    // remove o sócio
                    db.Socios.Remove(socio);
                    // guarda as alterações
                    db.SaveChanges();
                    // redireciona para a lista de sócios
                    return RedirectToAction("Index");
                }
                catch (Exception) {
                    // cria uma mensagem de erro a ser apresentada ao utilizador
                    ModelState.AddModelError("", string.Format("Não é possível eliminar este sócio."));
                }
                // volta para a VIEW de eliminação do sócio
                return View(socio);
            }
            // redireciona para a página inicial da aplicação
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
