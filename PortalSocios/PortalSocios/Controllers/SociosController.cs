using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PortalSocios.Models;
using System;
using System.Web;
using System.IO;

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
        public ActionResult Create([Bind(Include = "Nome,BI,NIF,DataNasc,Email,Telemovel,Morada,CodPostal,Fotografia,DataInscr,CategoriaFK")] Socios socio, HttpPostedFileBase foto2) {
            // caso o utilizador seja funcionário
            if (User.IsInRole("Funcionario")) {

                // atribui um username, uma categoria e uma data de inscrição a um novo sócio
                socio.UserName = socio.Email;
                socio.DataInscr = DateTime.Today;

                // determina o número de sócio a atribuir a um novo sócio
                int novoNumSocio = 0;
                try {
                    novoNumSocio = db.Socios.Max(s => s.NumSocio) + 1;
                }
                catch (Exception) {
                    // caso não existam dados na BD (null)
                    novoNumSocio = 1;
                }
                // atribui um número de sócio a um novo sócio
                socio.NumSocio = novoNumSocio;
                
                // tentativa de registar um novo sócio
                try {

                    // caso não haja um ficheiro selecionado
                    if (foto2 == null) {
                        ModelState.AddModelError("Fotografia", "Nenhum ficheiro selecionado!");
                    }

                    // caso os dados introduzidos estejam consistentes com o model
                    if (ModelState.IsValid) {
                        // http://haacked.com/archive/2010/07/16/uploading-files-with-aspnetmvc.aspx/
                        // caso haja um ficheiro selecionado e o tamanho seja superior a 0
                        if (foto2 != null && foto2.ContentLength > 0) {
                            // obtém o nome do ficheiro
                            var fileName = "n" + Convert.ToString(socio.NumSocio) + "_" + Path.GetFileName(foto2.FileName);
                            // guarda o ficheiro na pasta indicada
                            var path = Path.Combine(Server.MapPath("~/App_Data/Fotos"), fileName);
                            foto2.SaveAs(path);
                            // atribui o nome do ficheiro a um novo sócio
                            socio.Fotografia = fileName;
                        }

                        // adiciona um novo sócio
                        db.Socios.Add(socio);
                        // guarda as alterações na BD
                        db.SaveChanges();
                        // redireciona para a lista de sócios
                        return RedirectToAction("Index");                        
                    }
                }
                catch (Exception) {
                    // cria uma mensagem de erro a ser apresentada ao utilizador
                    ModelState.AddModelError("", string.Format("Não foi possível criar um novo sócio. Verifique o BI/CC, o NIF e/ou o E-mail."));
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
        public ActionResult Edit([Bind(Include = "SocioID,NumSocio,Nome,BI,NIF,DataNasc,Email,Telemovel,Morada,CodPostal,Fotografia,DataInscr,CategoriaFK,UserName")] Socios socio, HttpPostedFileBase foto3) {
            // caso o utilizador seja funcionário
            if (User.IsInRole("Funcionario")) {
                try {

                    // caso os dados introduzidos estejam consistentes com o model
                    if (ModelState.IsValid) {
                        // http://haacked.com/archive/2010/07/16/uploading-files-with-aspnetmvc.aspx/
                        // caso haja um ficheiro selecionado e o tamanho seja superior a 0
                        if (foto3 != null && foto3.ContentLength > 0) {
                            // obtém o nome do ficheiro
                            var fileName = "n" + Convert.ToString(socio.NumSocio) + "_" + Path.GetFileName(foto3.FileName);
                            // guarda o ficheiro na pasta indicada
                            var path = Path.Combine(Server.MapPath("~/App_Data/Fotos"), fileName);
                            foto3.SaveAs(path);
                            // atribui o nome do ficheiro a um novo sócio
                            socio.Fotografia = fileName;
                        }

                        db.Entry(socio).State = EntityState.Modified;
                        // guarda as alterações na BD
                        db.SaveChanges();
                        // redireciona para a lista de sócios
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception) {
                    // casos os dados introduzidos não estejam consistentes com o model, apresenta uma mensagem ao utilizador
                    ModelState.AddModelError("", string.Format("Não foi possível editar o sócio. Verifique o BI/CC, o NIF e/ou o E-mail."));
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
