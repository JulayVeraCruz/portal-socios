﻿using PortalSocios.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System;

namespace PortalSocios.Controllers {

    [Authorize(Roles = "Administrador")]
    public class UsersAdminController : Controller {
        public UsersAdminController() {
        }

        public UsersAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager) {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager {
            get {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set {
                _roleManager = value;
            }
        }

        /// <summary>
        /// Mostra a VIEW da lista de utilizadores
        /// GET: /Users/
        /// </summary>
        public async Task<ActionResult> Index() {
            return View(await UserManager.Users.ToListAsync());
        }

        /// <summary>
        /// Mostra a VIEW dos detalhes de um utilizador
        /// GET - ex.: /Users/Details/5
        /// </summary>
        /// <param name="id"></param>
        public async Task<ActionResult> Details(string id) {
            if (id == null) {
                return RedirectToAction("Index");
            }
            var user = await UserManager.FindByIdAsync(id);

            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);

            return View(user);
        }

        /// <summary>
        /// Mostra a VIEW de criação de um utilizador
        /// GET: /Users/Create
        /// </summary>
        public async Task<ActionResult> Create() {
            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        /// <summary>
        /// Verifica se os dados introduzidos para a criação de um utilizador são válidos
        /// e, se for o caso, cria esse utilizador
        /// POST: /Users/Create
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <param name="selectedRoles"></param>
        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, params string[] selectedRoles) {
            try {
                if (ModelState.IsValid) {
                    var user = new ApplicationUser { UserName = userViewModel.Email, Email = userViewModel.Email };
                    var adminresult = await UserManager.CreateAsync(user, userViewModel.Password);

                    //Add User to the selected Roles 
                    if (adminresult.Succeeded) {
                        if (selectedRoles != null) {
                            var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles);
                            if (!result.Succeeded) {
                                ModelState.AddModelError("", result.Errors.First());
                                ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
                                return View();
                            }
                        }
                    }
                    else {
                        ModelState.AddModelError("", adminresult.Errors.First());
                        ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
                        return View();
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Não foi possível criar um novo utilizador..."));
            }
            ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
            return View();
        }

        /// <summary>
        /// Mostra a VIEW de edição de um utilizador
        /// GET - ex.: /Users/Edit/1
        /// </summary>
        /// <param name="id"></param>
        public async Task<ActionResult> Edit(string id) {
            if (id == null) {
                return RedirectToAction("Index");
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null) {
                return RedirectToAction("Index");
            }

            var userRoles = await UserManager.GetRolesAsync(user.Id);

            return View(new EditUserViewModel() {
                Id = user.Id,
                Email = user.Email,
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem() {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        /// <summary>
        /// Verifica se os dados introduzidos para a edição de um utilizador
        /// são válidos e, se for o caso, edita esse utilizador
        /// POST - ex.: /Users/Edit/5
        /// </summary>
        /// <param name="editUser"></param>
        /// <param name="selectedRole"></param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Email,Id")] EditUserViewModel editUser, params string[] selectedRole) {
            try {
                if (ModelState.IsValid) {
                    var user = await UserManager.FindByIdAsync(editUser.Id);
                    if (user == null) {
                        return RedirectToAction("Index");
                    }

                    user.UserName = editUser.Email;
                    user.Email = editUser.Email;

                    var userRoles = await UserManager.GetRolesAsync(user.Id);

                    selectedRole = selectedRole ?? new string[] { };

                    var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray<string>());

                    if (!result.Succeeded) {
                        ModelState.AddModelError("", result.Errors.First());
                        return View();
                    }
                    result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray<string>());

                    if (!result.Succeeded) {
                        ModelState.AddModelError("", result.Errors.First());
                        return View();
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Não foi possível editar este utilizador..."));
            }
            return View();
        }

        /// <summary>
        /// Mostra a VIEW de eliminação de um utilizador
        /// GET - ex.: /Users/Delete/5
        /// </summary>
        /// <param name="id"></param>
        public async Task<ActionResult> Delete(string id) {
            if (id == null) {
                return RedirectToAction("Index");
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null) {
                return RedirectToAction("Index");
            }
            return View(user);
        }

        /// <summary>
        /// Verifica se é possível a eliminação de um utilizador
        /// e, se for o caso, elimina esse utilizador
        /// POST - ex.: /Users/Delete/5
        /// </summary>
        /// <param name="id"></param>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id) {
            try {
                if (ModelState.IsValid) {
                    if (id == null) {
                        return RedirectToAction("Index");
                    }

                    var user = await UserManager.FindByIdAsync(id);
                    if (user == null) {
                        return RedirectToAction("Index");
                    }
                    var result = await UserManager.DeleteAsync(user);
                    if (!result.Succeeded) {
                        ModelState.AddModelError("", result.Errors.First());
                        return View();
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Não foi possível eliminar este utilizador..."));
            }
            return View();
        }
    }
}
