﻿using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PortalSocios.Models;
using System;
using System.Web;
using System.IO;

namespace PortalSocios.Controllers {

    [Authorize(Roles = "Funcionario, Socio")]
    public class SociosController : Controller {

        // cria um novo objeto que representa a BD
        private SociosBD db = new SociosBD();

        // GET: Socios
        public ActionResult Index() {
            // inclui os dados das categorias na view Socios
            var socios = db.Socios.Include(s => s.Categoria);
            // caso o utilizador seja funcionário
            if (User.IsInRole("Funcionario")) {
                // ordena a lista de sócios pelo número de sócio
                return View(socios.OrderBy(s => s.NumSocio).ToList());
            }
            // lista apenas os dados do sócio que se autenticou
            return View(db.Socios.Where(s => s.UserName.Equals(User.Identity.Name)).ToList());
        }
        
        // GET: Socios/Details/5
        public ActionResult Details(int? id) {
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
            // caso o username do sócio a visualizar não seja igual ao username do utilizador autenticado
            // e caso o utilizador autenticado esteja no role 'Socio'
            if (!socio.UserName.Equals(User.Identity.Name) && User.IsInRole("Socio")) {
                return RedirectToAction("Restrito", "Erros");
            }
            // vai para a view dos detalhes do sócio
            return View(socio);
        }

        // GET: Socios/Create
        [Authorize(Roles = "Funcionario")]
        public ActionResult Create() {
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome");
            return View();
        }

        // POST: Socios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario")]
        public ActionResult Create([Bind(Include = "Nome,BI,NIF,DataNasc,Email,Telemovel,Morada,CodPostal,Fotografia,DataInscr,CategoriaFK")] Socios socio, HttpPostedFileBase foto2) {
            // atribui um username e uma data de inscrição a um novo sócio
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
                ModelState.AddModelError("", string.Format("Não foi possível criar um novo sócio. Verifique o E-mail, o BI/CC e/ou o NIF..."));
            }
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", socio.CategoriaFK);
            // volta para a view da criação do sócio
            return View(socio);
        }

        // GET: Socios/Edit/5
        public ActionResult Edit(int? id) {
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
            // caso o username do sócio a visualizar não seja igual ao username do utilizador autenticado
            // e caso o utilizador autenticado esteja no role 'Socio'
            if (!socio.UserName.Equals(User.Identity.Name) && User.IsInRole("Socio")) {
                return RedirectToAction("Restrito", "Erros");
            }
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", socio.CategoriaFK);
            // vai para a view da edição do sócio
            return View(socio);
        }

        // POST: Socios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SocioID,NumSocio,Nome,BI,NIF,DataNasc,Email,Telemovel,Morada,CodPostal,Fotografia,DataInscr,CategoriaFK,UserName")] Socios socio, HttpPostedFileBase foto3) {
            try {
                // edita o username de um sócio
                socio.UserName = socio.Email;

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
                ModelState.AddModelError("", string.Format("Não foi possível editar este sócio. Verifique o E-mail, o BI/CC e/ou o NIF..."));
            }
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", socio.CategoriaFK);
            // volta para a VIEW da edição do sócio
            return View(socio);
        }

        // GET: Socios/Delete/5
        [Authorize(Roles = "Funcionario")]
        public ActionResult Delete(int? id) {
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
            // vai para a view de eliminação do sócio
            return View(socio);
        }

        // POST: Socios/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Funcionario")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
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
                ModelState.AddModelError("", string.Format("Não foi possível eliminar este sócio."));
            }
            // volta para a view de eliminação do sócio
            return View(socio);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
