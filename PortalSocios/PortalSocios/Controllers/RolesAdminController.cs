using PortalSocios.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System;

namespace PortalSocios.Controllers {
    [Authorize(Roles = "Administrador")]
    public class RolesAdminController : Controller {
        public RolesAdminController() {
        }

        public RolesAdminController(ApplicationUserManager userManager,
            ApplicationRoleManager roleManager) {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set {
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
        /// Mostra a VIEW da lista de funções
        /// GET: /Roles/
        /// </summary>
        public ActionResult Index() {
            return View(RoleManager.Roles);
        }

        /// <summary>
        /// Mostra a VIEW dos detalhes de uma função
        /// GET - ex.: /Roles/Details/5
        /// </summary>
        /// <param name="id"></param>
        public async Task<ActionResult> Details(string id) {
            if (id == null) {
                return RedirectToAction("Index");
            }
            var role = await RoleManager.FindByIdAsync(id);
            // Get the list of Users in this Role
            var users = new List<ApplicationUser>();

            // Get the list of Users in this Role
            foreach (var user in UserManager.Users.ToList()) {
                if (await UserManager.IsInRoleAsync(user.Id, role.Name)) {
                    users.Add(user);
                }
            }

            ViewBag.Users = users;
            ViewBag.UserCount = users.Count();
            return View(role);
        }

        /// <summary>
        /// Mostra a VIEW de criação de uma função
        /// GET: /Roles/Create
        /// </summary>
        public ActionResult Create() {
            return View();
        }

        /// <summary>
        /// Verifica se os dados introduzidos para a criação de uma função são válidos
        /// e, se for o caso, cria essa função
        /// POST: /Roles/Create
        /// </summary>
        /// <param name="roleViewModel"></param>
        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel roleViewModel) {
            try {
                if (ModelState.IsValid) {
                    var role = new IdentityRole(roleViewModel.Name);
                    var roleresult = await RoleManager.CreateAsync(role);
                    if (!roleresult.Succeeded) {
                        ModelState.AddModelError("", roleresult.Errors.First());
                        return View();
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Não foi possível criar uma nova função..."));
            }
            return View();
        }

        /// <summary>
        /// Mostra a VIEW de edição de uma função
        /// GET: /Roles/Edit/Admin
        /// </summary>
        /// <param name="id"></param>
        public async Task<ActionResult> Edit(string id) {
            if (id == null) {
                return RedirectToAction("Index");
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null) {
                return RedirectToAction("Index");
            }
            RoleViewModel roleModel = new RoleViewModel { Id = role.Id, Name = role.Name };
            return View(roleModel);
        }

        /// <summary>
        /// Verifica se os dados introduzidos para a edição de uma função
        /// são válidos e, se for o caso, edita essa função
        /// POST - ex.: /Roles/Edit/5
        /// </summary>
        /// <param name="roleModel"></param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Name,Id")] RoleViewModel roleModel) {
            try {
                if (ModelState.IsValid) {
                    var role = await RoleManager.FindByIdAsync(roleModel.Id);
                    role.Name = roleModel.Name;
                    await RoleManager.UpdateAsync(role);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Não foi possível editar esta função..."));
            }
            return View();
        }

        /// <summary>
        /// Mostra a VIEW de eliminação de uma função
        /// GET - ex.: /Roles/Delete/5
        /// </summary>
        /// <param name="id"></param>
        public async Task<ActionResult> Delete(string id) {
            if (id == null) {
                return RedirectToAction("Index");
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null) {
                return RedirectToAction("Index");
            }
            return View(role);
        }

        /// <summary>
        /// Verifica se é possível a eliminação de uma função
        /// e, se for o caso, elimina essa função
        /// POST - ex.: /Roles/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deleteUser"></param>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id, string deleteUser) {
            try {
                if (ModelState.IsValid) {
                    if (id == null) {
                        return RedirectToAction("Index");
                    }
                    var role = await RoleManager.FindByIdAsync(id);
                    if (role == null) {
                        return RedirectToAction("Index");
                    }
                    IdentityResult result;
                    if (deleteUser != null) {
                        result = await RoleManager.DeleteAsync(role);
                    }
                    else {
                        result = await RoleManager.DeleteAsync(role);
                    }
                    if (!result.Succeeded) {
                        ModelState.AddModelError("", result.Errors.First());
                        return View();
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) {
                ModelState.AddModelError("", string.Format("Não foi possível eliminar esta função..."));
            }
            return View();
        }
    }
}
