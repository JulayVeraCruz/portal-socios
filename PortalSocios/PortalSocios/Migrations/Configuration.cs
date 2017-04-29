namespace PortalSocios.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PortalSocios.Models.SociosBD>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PortalSocios.Models.SociosBD context) {

            // Seed para as classes Socios, Pagamentos, Quotas, Categorias,
            // Beneficios, Funcionarios e CategoriasBeneficios

            // ###########################################################
            // adiciona Funcionarios
            var funcionarios = new List<Funcionarios> {
               new Funcionarios {FuncionarioID = 1, Nome = "David Alves", NIF = "628438592", Morada = "Rua das Oliveirinhas N.º 32", CodPostal = "2835-852 Ourém", Telemovel = "916732583", DataEntrClube = new DateTime(2016,8,12)},
               new Funcionarios {FuncionarioID = 2, Nome = "Alice Azevedo", NIF = "832652957", Morada = "Avenida da Praia N.º 4 - 3 Esq.", CodPostal = "2485-024 Tomar", Telemovel = "915285673", DataEntrClube = new DateTime(2016,10,25)},
               new Funcionarios {FuncionarioID = 3, Nome = "Rebeca Ferreira", NIF = "721864159", Morada = "Rua da Tradição N.º 12", CodPostal = "2485-145 Tomar", Telemovel = "919528259", DataEntrClube = new DateTime(2017,1,18)}
            };

            funcionarios.ForEach(ff => context.Funcionarios.AddOrUpdate(f => f.Nome, ff));
            context.SaveChanges();
        }
    }
}
