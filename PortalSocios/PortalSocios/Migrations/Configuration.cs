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

            // Seed para as tabelas Socios, Pagamentos, Quotas, Categorias,
            // Beneficios, Funcionarios e CategoriasBeneficios

            // ###########################################################
            // adiciona Categorias
            var categorias = new List<Categorias> {
                new Categorias {CategoriaID = 1, Nome = "S�nior", FaixaEtaria = "Maiores de 18", NumQuotasAnuais = 12, ValorMensal = 12},
                new Categorias {CategoriaID = 2, Nome = "Juvenil", FaixaEtaria = "Entre 12 e 17", NumQuotasAnuais = 12, ValorMensal = 6},
                new Categorias {CategoriaID = 3, Nome = "Infantil", FaixaEtaria = "Entre 0 e 11", NumQuotasAnuais = 12, ValorMensal = 3},
            };

            categorias.ForEach(cc => context.Categorias.AddOrUpdate(c => c.Nome, cc));
            context.SaveChanges();


            // ###########################################################
            // adiciona Socios
            var socios = new List<Socios> {
                new Socios {SocioID = 1, NumSocio = 1, Nome = "Sim�o Alvito Modesto", NIF = "172596295", Morada = "Rua dos Armaz�ns N.� 40", CodPostal = "5743-247 Torres Novas", DataNasc = new DateTime(1962,2,6), Telemovel = "913567285", Email = "simao_modesto@gmail.com", Fotografia = "./fotos/samodesto.jpg", DataInscr = new DateTime(2016,10,4), CategoriaFK = 1},
                new Socios {SocioID = 2, NumSocio = 2, Nome = "Maria Gon�alves Sousa", NIF = "498953925", Morada = "Rua 1� de Dezembro N.� 2 - 3 Esq.", CodPostal = "2485-632 Tomar", DataNasc = new DateTime(1991,8,24), Telemovel = "916258358", Email = "maria_sousa@gmail.com", Fotografia = "./fotos/mgsousa.jpg", DataInscr = new DateTime(2016,10,16), CategoriaFK = 1},
                new Socios {SocioID = 3, NumSocio = 3, Nome = "Miguel Alves Almeida", NIF = "632158329", Morada = "Rua Vale Miguel N.� 10", CodPostal = "2485-215 Tomar", DataNasc = new DateTime(2002,10,30), Telemovel = "914752861", Email = "miguelaalmeida@outlook.com", Fotografia = "./fotos/maalmeida.jpg", DataInscr = new DateTime(2016,11,2), CategoriaFK = 2},
                new Socios {SocioID = 4, NumSocio = 4, Nome = "Manuela Silva Rocha", NIF = "628102144", Morada = "Travessa do Parque N.� 88", CodPostal = "4257-156 Lisboa", DataNasc = new DateTime(2006,5,13), Telemovel = "961471852", Email = "manuela_rocha@hotmail.com", Fotografia = "./fotos/msrocha.jpg", DataInscr = new DateTime(2016,11,26), CategoriaFK = 3},
                new Socios {SocioID = 5, NumSocio = 5, Nome = "Andr� Filipe Melo Barbosa", NIF = "294953732", Morada = "Rua do Pelourinho N.� 1 - 2 Dir.", CodPostal = "4257-742 Lisboa", DataNasc = new DateTime(1982,12,12), Telemovel = "962148215", Email = "andrefm.barbosa@gmail.com", Fotografia = "./fotos/andrefmbarbosa.jpg", DataInscr = new DateTime(2017,1,8), CategoriaFK = 1},
                new Socios {SocioID = 6, NumSocio = 6, Nome = "Ana Lima Rocha Fernandes", NIF = "613103285", Morada = "Rua dos Combatentes N.� 17", CodPostal = "6759-022 Entroncamento", DataNasc = new DateTime(2004,5,28), Telemovel = "915372758", Email = "analrfernandes@outlook.com", Fotografia = "./fotos/analrfernandes.jpg", DataInscr = new DateTime(2017,1,28), CategoriaFK = 2}
            };

            socios.ForEach(ss => context.Socios.AddOrUpdate(s => s.Nome, ss));
            context.SaveChanges();
            

            // ###########################################################
            // adiciona Funcionarios
            var funcionarios = new List<Funcionarios> {
                new Funcionarios {FuncionarioID = 1, Nome = "David Alves", NIF = "628438592", Morada = "Rua das Oliveirinhas N.� 32", CodPostal = "2835-852 Our�m", Telemovel = "916732583", DataEntrClube = new DateTime(2016,8,12)},
                new Funcionarios {FuncionarioID = 2, Nome = "Alice Azevedo", NIF = "832652957", Morada = "Avenida da Praia N.� 4 - 3 Esq.", CodPostal = "2485-024 Tomar", Telemovel = "915285673", DataEntrClube = new DateTime(2016,10,25)},
                new Funcionarios {FuncionarioID = 3, Nome = "Rebeca Ferreira", NIF = "721864159", Morada = "Rua da Tradi��o N.� 12", CodPostal = "2485-145 Tomar", Telemovel = "919528259", DataEntrClube = new DateTime(2017,1,18)}
            };

            funcionarios.ForEach(ff => context.Funcionarios.AddOrUpdate(f => f.Nome, ff));
            context.SaveChanges();
        }
    }
}
