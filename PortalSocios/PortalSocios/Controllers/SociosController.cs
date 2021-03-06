﻿using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PortalSocios.Models;
using System;
using System.Web;
using System.IO;
using System.Collections.Generic;

namespace PortalSocios.Controllers {

    [Authorize(Roles = "Administrador, Funcionario, Socio")]
    public class SociosController : Controller {

        // cria um novo objeto que representa a BD
        private SociosBD db = new SociosBD();

        /// <summary>
        /// Mostra a VIEW da lista de sócios
        /// GET: Socios
        /// </summary>
        /// <param name="ordenar"></param>
        /// <param name="pesquisar"></param>
        public ActionResult Index(string ordenar, string pesquisar) {

            // inclui os dados das categorias na view Socios
            var socios = db.Socios.Include(s => s.Categoria);
            // caso o utilizador seja funcionário
            if (User.IsInRole("Administrador") || User.IsInRole("Funcionario")) {

                // ref: https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-
                ViewBag.OrdNum = String.IsNullOrEmpty(ordenar) ? "numDesc" : "";
                ViewBag.OrdNome = ordenar == "nomeAsc" ? "nomeDesc" : "nomeAsc";
                ViewBag.OrdInsc = ordenar == "InscAsc" ? "InscDesc" : "InscAsc";
                ViewBag.OrdCateg = ordenar == "categAsc" ? "categDesc" : "categAsc";

                // permite efetuar a pesquisa de um sócio pelo nome
                if (!String.IsNullOrEmpty(pesquisar)) {
                    return View(socios.Where(s => s.Nome.ToUpper().Contains(pesquisar.ToUpper())));
                }

                // ordena a lista de sócios de forma ascendente ou descendente, por coluna
                switch (ordenar) {
                    case "numDesc":
                        return View(socios.OrderByDescending(s => s.NumSocio).ToList());
                    case "nomeDesc":
                        return View(socios.OrderByDescending(s => s.Nome).ToList());
                    case "nomeAsc":
                        return View(socios.OrderBy(s => s.Nome).ToList());
                    case "categDesc":
                        return View(socios.OrderByDescending(s => s.CategoriaFK).ToList());
                    case "categAsc":
                        return View(socios.OrderBy(s => s.CategoriaFK).ToList());
                    default:
                        return View(socios.OrderBy(s => s.NumSocio).ToList());
                }
            }
            // lista apenas os dados do sócio que se autenticou
            return View(db.Socios.Where(s => s.UserName.Equals(User.Identity.Name)).ToList());
        }

        /// <summary>
        /// Mostra a VIEW dos detalhes de um sócio
        /// GET - ex.: Socios/Details/5
        /// </summary>
        /// <param name="id"></param>
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

        /// <summary>
        /// Mostra a VIEW de criação de um sócio
        /// GET: Socios/Create
        /// </summary>
        [Authorize(Roles = "Administrador, Funcionario")]
        public ActionResult Create() {
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome");
            return View();
        }

        /// <summary>
        /// Verifica se os dados introduzidos para a criação de um sócio são válidos
        /// e, se for o caso, cria esse sócio
        /// POST: Socios/Create
        /// </summary>
        /// <param name="socio"></param>
        /// <param name="foto2"></param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Funcionario")]
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
                    // ref: http://haacked.com/archive/2010/07/16/uploading-files-with-aspnetmvc.aspx/
                    // caso haja um ficheiro selecionado e o tamanho seja superior a 0
                    if (foto2 != null && foto2.ContentLength > 0) {
                        // obtém o nome do ficheiro
                        var fileName = "n" + Convert.ToString(socio.NumSocio) + "_" + Path.GetFileName(foto2.FileName);
                        // atribui o nome do ficheiro a um novo sócio
                        socio.Fotografia = fileName;
                        // adiciona um novo sócio
                        db.Socios.Add(socio);
                        // guarda as alterações na BD
                        db.SaveChanges();                        
                        // guarda o ficheiro na pasta indicada
                        var path = Path.Combine(Server.MapPath("~/App_Data/FotosSocios"), fileName);
                        foto2.SaveAs(path);                        
                    }
                    // redireciona para a lista de sócios
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) {
                // cria uma mensagem de erro a ser apresentada ao utilizador
                ModelState.AddModelError("", string.Format("Não foi possível criar um novo sócio...O e-mail, o BI/CC e/ou o NIF já poderão existir."));
            }
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", socio.CategoriaFK);
            // volta para a view da criação do sócio
            return View(socio);
        }

        /// <summary>
        /// Mostra a VIEW de edição de um sócio
        /// GET - ex.: Socios/Edit/5
        /// </summary>
        /// <param name="id"></param>
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

            // armazena temporariamente o e-mail antigo
            TempData["socioUsername"] = socio.UserName;
            TempData.Keep("socioUsername");

            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", socio.CategoriaFK);
            // vai para a view da edição do sócio
            return View(socio);
        }

        /// <summary>
        /// Verifica se os dados introduzidos para a edição de um sócio
        /// são válidos e, se for o caso, edita esse sócio
        /// POST - ex.: Socios/Edit/5
        /// </summary>
        /// <param name="socio"></param>
        /// <param name="foto3"></param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SocioID,NumSocio,Nome,BI,NIF,DataNasc,Email,Telemovel,Morada,CodPostal,Fotografia,DataInscr,CategoriaFK,UserName")] Socios socio, HttpPostedFileBase foto3) {
            try {

                // atribui o e-mail antigo a uma string
                string endEmail = TempData["socioUsername"].ToString();
                
                // procura o registo com o e-mail antigo                
                ApplicationUser app = db.Users.Where(x => x.UserName.Equals(endEmail)).SingleOrDefault();

                // caso o registo exista
                if (app != null) {
                    // atualiza o novo username de um sócio na tabela dos dados da conta
                    app.UserName = socio.Email;
                }                

                // edita o username de um sócio na tabela dos sócios
                socio.UserName = socio.Email;

                // caso não haja um ficheiro selecionado
                if (foto3 == null && socio.Fotografia == null) {
                    ModelState.AddModelError("Fotografia", "Nenhum ficheiro selecionado!");
                }

                // caso os dados introduzidos estejam consistentes com o model
                if (ModelState.IsValid) {
                    // ref: http://haacked.com/archive/2010/07/16/uploading-files-with-aspnetmvc.aspx/
                    // caso haja um ficheiro selecionado e o tamanho seja superior a 0
                    if (foto3 != null && foto3.ContentLength > 0) {
                        // obtém o nome do ficheiro
                        var fileName = "n" + Convert.ToString(socio.NumSocio) + "_" + Path.GetFileName(foto3.FileName);
                        // atribui o nome do ficheiro a um novo sócio
                        socio.Fotografia = fileName;
                        db.Entry(socio).State = EntityState.Modified;
                        // guarda as alterações na BD
                        db.SaveChanges();
                        // guarda o ficheiro na pasta indicada
                        var path = Path.Combine(Server.MapPath("~/App_Data/FotosSocios"), fileName);
                        foto3.SaveAs(path);
                    } else if (socio.Fotografia != null) {
                        db.Entry(socio).State = EntityState.Modified;
                        // guarda as alterações na BD
                        db.SaveChanges();
                    }
                    // redireciona para a lista de sócios
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) {
                // casos os dados introduzidos não estejam consistentes com o model, apresenta uma mensagem ao utilizador
                ModelState.AddModelError("", string.Format("Não foi possível editar este sócio...O e-mail, o BI/CC e/ou o NIF já poderão existir."));
            }
            ViewBag.CategoriaFK = new SelectList(db.Categorias, "CategoriaID", "Nome", socio.CategoriaFK);
            // volta para a VIEW da edição do sócio
            return View(socio);
        }

        /// <summary>
        /// Mostra a VIEW de eliminação de um sócio
        /// GET: Socios/Delete/5
        /// </summary>
        /// <param name="id"></param>
        [Authorize(Roles = "Administrador, Funcionario")]
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

        /// <summary>
        /// Verifica se é possível a eliminação de um sócio
        /// e, se for o caso, elimina esse sócio
        /// POST - ex.: Socios/Delete/5
        /// </summary>
        /// <param name="id"></param>
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrador, Funcionario")]
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
                ModelState.AddModelError("", string.Format("Não foi possível eliminar este sócio..."));
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
