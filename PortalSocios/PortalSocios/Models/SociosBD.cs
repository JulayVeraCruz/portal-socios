using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PortalSocios.Models {
    public class SociosBD : DbContext {

        // representação das tabelas a criar na BD
        public virtual DbSet<Socios> Socios { get; set; }
        public virtual DbSet<Pagamentos> Pagamentos { get; set; }
        public virtual DbSet<Categorias> Categorias { get; set; }
        public virtual DbSet<Quotas> Quotas { get; set; }
        public virtual DbSet<Beneficios> Beneficios { get; set; }
        public virtual DbSet<Funcionarios> Funcionarios { get; set; }

        // especificação de onde será criada a BD
        public SociosBD() : base("LocalizacaoBD") { }

        // remove os 'on delete cascade' associados às chaves forasteiras
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
