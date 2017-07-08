using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;
using PortalSocios.Models;

namespace PortalSocios {
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
            // inicializa os roles e os utilizadores na BD
            inicApp();
        }

        // cria, caso não existam, os roles de suporte à aplicação, Socios e Funcionarios e os respetivos utilizadores
        private void inicApp() {

            SociosBD db = new SociosBD();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // cria o role 'Funcionario'
            // caso o role não exista
            if (!roleManager.RoleExists("Funcionario")) {
                // cria o role
                var role = new IdentityRole();
                role.Name = "Funcionario";
                roleManager.Create(role);

                // cria um utilizador 'Funcionario'
                var user = new ApplicationUser();
                user.UserName = "dalves@coxos.pt";
                user.Email = "dalves@coxos.pt";
                string userPWD = "123qwe#";
                var chkUser = userManager.Create(user, userPWD);

                // adiciona o utilizador à respetiva role 'Funcionario'
                if (chkUser.Succeeded) {
                    var result = userManager.AddToRole(user.Id, "Funcionario");
                }
            }

            // cria o role 'Socio'
            // caso o role não exista
            if (!roleManager.RoleExists("Socio")) {
                // cria o role
                var role = new IdentityRole();
                role.Name = "Socio";
                roleManager.Create(role);

                // cria um utilizador 'Socio'
                var user = new ApplicationUser();
                user.UserName = "smodesto@gmail.com";
                user.Email = "smodesto@gmail.com";
                string userPWD = "456qwe#";
                var chkUser = userManager.Create(user, userPWD);
                
                // adiciona o utilizador à respetiva role 'Socio'
                if (chkUser.Succeeded) {
                    var result = userManager.AddToRole(user.Id, "Socio");
                }
            }

        }
    }
}
