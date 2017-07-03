using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PortalSocios.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager) {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class SociosBD : IdentityDbContext<ApplicationUser> {
        public SociosBD()
            : base("AppBD", throwIfV1Schema: false) {
        }

        static SociosBD() {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<SociosBD>(new ApplicationDbInitializer());
        }

        public static SociosBD Create() {
            return new SociosBD();
        }

        // Incorporação das tabelas do Portal dos Sócios

        // representação das tabelas a criar na BD
        public virtual DbSet<Socios> Socios { get; set; }
        public virtual DbSet<Pagamentos> Pagamentos { get; set; }
        public virtual DbSet<Categorias> Categorias { get; set; }
        public virtual DbSet<Quotas> Quotas { get; set; }
        public virtual DbSet<Beneficios> Beneficios { get; set; }
        public virtual DbSet<Funcionarios> Funcionarios { get; set; }

        // remove os 'on delete cascade' associados às chaves forasteiras
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}