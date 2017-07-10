using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;
using PortalSocios.Models;
using System.IO;

namespace PortalSocios {
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
            inicApp();
        }

        // inicializa os roles e os utilizadores na BD
        private void inicApp() {

            SociosBD db = new SociosBD();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // cria o role 'Administrador' caso não exista
            if (!roleManager.RoleExists("Administrador")) {
                var role = new IdentityRole();
                role.Name = "Administrador";
                roleManager.Create(role);

                // cria um utilizador 'Administrador'
                var user = new ApplicationUser();
                user.UserName = "dalves@coxos.pt";
                user.Email = "dalves@coxos.pt";
                string userPWD = "123Qwe#";
                var chkUser = userManager.Create(user, userPWD);

                // adiciona o utilizador ao role 'Administrador'
                if (chkUser.Succeeded) {
                    var result = userManager.AddToRole(user.Id, "Administrador");
                }
            }

            // cria o role 'Funcionario' caso não exista
            if (!roleManager.RoleExists("Funcionario")) {
                var role = new IdentityRole();
                role.Name = "Funcionario";
                roleManager.Create(role);

                // cria um utilizador 'Funcionario'
                var user = new ApplicationUser();
                user.UserName = "aazevedo@coxos.pt";
                user.Email = "aazevedo@coxos.pt";
                string userPWD = "456Qwe#";
                var chkUser = userManager.Create(user, userPWD);

                // adiciona o utilizador ao role 'Funcionario'
                if (chkUser.Succeeded) {
                    var result = userManager.AddToRole(user.Id, "Funcionario");
                }
            }

            // cria o role 'Socio' caso não exista
            if (!roleManager.RoleExists("Socio")) {
                // cria o role
                var role = new IdentityRole();
                role.Name = "Socio";
                roleManager.Create(role);

                // cria um utilizador 'Socio'
                var user = new ApplicationUser();
                user.UserName = "smodesto@gmail.com";
                user.Email = "smodesto@gmail.com";
                string userPWD = "789Qwe#";
                var chkUser = userManager.Create(user, userPWD);
                
                // adiciona o utilizador à respetiva role 'Socio'
                if (chkUser.Succeeded) {
                    var result = userManager.AddToRole(user.Id, "Socio");
                }
            }

            // cria a pasta das fotos dos utilizadores se esta não existir
            var pasta = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/FotosSocios");
            Directory.CreateDirectory(pasta);
        }
    }
}
