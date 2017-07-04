using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;
using PortalSocios.Models;

namespace PortalSocios {
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
            // inicializa os ROLES e os utilizadores na BD
            inicApp();
        }

        // cria, caso não existam, as ROLES de suporte à aplicação, Socios e Funcionarios e os respetivos utilizadores
        private void inicApp() {

            SociosBD db = new SociosBD();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // cria a ROLE 'Socio'
            // caso a ROLE não exista
            if (!roleManager.RoleExists("Socio")) {
                // cria a ROLE
                var role = new IdentityRole();
                role.Name = "Socio";
                roleManager.Create(role);

                // cria um utilizador 'Socio'
                var user = new ApplicationUser();
                user.UserName = "smodesto@gmail.com";
                user.Email = "smodesto@gmail.com";
                string userPWD = "123qwe#";
                var chkUser = userManager.Create(user, userPWD);
                
                // adiciona o utilizador à respetiva ROLE 'Socio'
                if (chkUser.Succeeded) {
                    var result = userManager.AddToRole(user.Id, "Socio");
                }
            }

            // cria a ROLE 'Funcionario'
            if (!roleManager.RoleExists("Funcionario")) {
                var role = new IdentityRole();
                role.Name = "Funcionario";
                roleManager.Create(role);

                // cria um utilizador 'Funcionario'
                var user = new ApplicationUser();
                user.UserName = "dalves@cdcb.pt";
                user.Email = "dalves@cdcb.pt";
                string userPWD = "123qwe#";
                var chkUser = userManager.Create(user, userPWD);

                // adiciona o utilizador à respetiva ROLE 'Funcionario'
                if (chkUser.Succeeded) {
                    var result = userManager.AddToRole(user.Id, "Funcionario");
                }
            }
        }
    }
}
