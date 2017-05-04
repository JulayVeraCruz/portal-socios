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

            // ############################################################
            // adiciona Categorias
            var categorias = new List<Categorias> {
                new Categorias {CategoriaID = 1, Nome = "Sénior", FaixaEtaria = "Maiores de 18", NumQuotasAnuais = 12, ValorMensal = 12},
                new Categorias {CategoriaID = 2, Nome = "Juvenil", FaixaEtaria = "Entre 12 e 17", NumQuotasAnuais = 12, ValorMensal = 6},
                new Categorias {CategoriaID = 3, Nome = "Infantil", FaixaEtaria = "Entre 0 e 11", NumQuotasAnuais = 12, ValorMensal = 3},
             };

            categorias.ForEach(cc => context.Categorias.AddOrUpdate(c => c.Nome, cc));
            context.SaveChanges();

            // ############################################################
            // adiciona Socios
            var socios = new List<Socios> {
                new Socios {SocioID = 1, NumSocio = 1, Nome = "Simão Alvito Modesto", NIF = "172596295", Morada = "Rua dos Armazéns N.º 40", CodPostal = "5743-247 Torres Novas", DataNasc = new DateTime(1962,2,6), Telemovel = "913567285", Email = "simao_modesto@gmail.com", Fotografia = "samodesto.jpg", DataInscr = new DateTime(2016,10,4), CategoriaFK = 1},
                new Socios {SocioID = 2, NumSocio = 2, Nome = "Maria Gonçalves Sousa", NIF = "498953925", Morada = "Rua 1º de Dezembro N.º 2 - 3 Esq.", CodPostal = "2485-632 Tomar", DataNasc = new DateTime(1991,8,24), Telemovel = "916258358", Email = "maria_sousa@gmail.com", Fotografia = "mgsousa.jpg", DataInscr = new DateTime(2016,10,16), CategoriaFK = 1},
                new Socios {SocioID = 3, NumSocio = 3, Nome = "Miguel Alves Almeida", NIF = "632158329", Morada = "Rua Vale Miguel N.º 10", CodPostal = "2485-215 Tomar", DataNasc = new DateTime(2002,10,30), Telemovel = "914752861", Email = "miguelaalmeida@outlook.com", Fotografia = "maalmeida.jpg", DataInscr = new DateTime(2016,11,2), CategoriaFK = 2},
                new Socios {SocioID = 4, NumSocio = 4, Nome = "Manuela Silva Rocha", NIF = "628102144", Morada = "Travessa do Parque N.º 88", CodPostal = "4257-156 Lisboa", DataNasc = new DateTime(2006,5,13), Telemovel = "961471852", Email = "manuela_rocha@hotmail.com", Fotografia = "msrocha.jpg", DataInscr = new DateTime(2016,11,26), CategoriaFK = 3},
                new Socios {SocioID = 5, NumSocio = 5, Nome = "André Filipe Melo Barbosa", NIF = "294953732", Morada = "Rua do Pelourinho N.º 1 - 2 Dir.", CodPostal = "4257-742 Lisboa", DataNasc = new DateTime(1982,12,12), Telemovel = "962148215", Email = "andrefm.barbosa@gmail.com", Fotografia = "afmbarbosa.jpg", DataInscr = new DateTime(2017,1,8), CategoriaFK = 1},
                new Socios {SocioID = 6, NumSocio = 6, Nome = "Ana Lima Rocha Fernandes", NIF = "613103285", Morada = "Rua dos Combatentes N.º 17", CodPostal = "6759-022 Entroncamento", DataNasc = new DateTime(2004,5,28), Telemovel = "915372758", Email = "alrfernandes@outlook.com", Fotografia = "alrfernandes.jpg", DataInscr = new DateTime(2017,1,28), CategoriaFK = 2}
            };

            socios.ForEach(ss => context.Socios.AddOrUpdate(s => s.NIF, ss));
            context.SaveChanges();

            // ############################################################
            // adiciona Quotas
            var quotas = new List<Quotas> {
                new Quotas {QuotaID = 1, Montante = 12, RefMultibanco = "620832411", Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 2, Montante = 12, RefMultibanco = "620832412", Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 3, Montante = 12, RefMultibanco = "620832413", Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 4, Montante = 12, RefMultibanco = "620832414", Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 5, Montante = 12, RefMultibanco = "620832415", Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 6, Montante = 12, RefMultibanco = "620832416", Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 7, Montante = 12, RefMultibanco = "620832417", Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 8, Montante = 12, RefMultibanco = "620832418", Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 9, Montante = 12, RefMultibanco = "620832419", Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 10, Montante = 12, RefMultibanco = "620832420", Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 11, Montante = 12, RefMultibanco = "620832421", Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 12, Montante = 12, RefMultibanco = "620832422", Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
            };

            quotas.ForEach(qq => context.Quotas.AddOrUpdate(q => q.RefMultibanco, qq));
            context.SaveChanges();

            // ############################################################
            // adiciona Funcionarios
            var funcionarios = new List<Funcionarios> {
                new Funcionarios {FuncionarioID = 1, Nome = "David Alves", NIF = "628438592", Morada = "Rua das Oliveirinhas N.º 32", CodPostal = "2835-852 Ourém", Telemovel = "916732583", DataEntrClube = new DateTime(2016,8,12)},
                new Funcionarios {FuncionarioID = 2, Nome = "Alice Azevedo", NIF = "832652957", Morada = "Avenida da Praia N.º 4 - 3 Esq.", CodPostal = "2485-024 Tomar", Telemovel = "915285673", DataEntrClube = new DateTime(2016,10,25)},
                new Funcionarios {FuncionarioID = 3, Nome = "Rebeca Ferreira", NIF = "721864159", Morada = "Rua da Tradição N.º 12", CodPostal = "2485-145 Tomar", Telemovel = "919528259", DataEntrClube = new DateTime(2017,1,18)}
            };

            funcionarios.ForEach(ff => context.Funcionarios.AddOrUpdate(f => f.NIF, ff));
            context.SaveChanges();

            // ############################################################
            // adiciona Pagamentos
            var pagamentos = new List<Pagamentos> {
                new Pagamentos {PagamentoID = 1, Montante = 12, DataPagam = new DateTime(2017,1,18), DataPrevPagam = new DateTime(2017,1,24), QuotaFK = 1, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 2, Montante = 12, DataPagam = new DateTime(2017,2,15), DataPrevPagam = new DateTime(2017,2,24), QuotaFK = 2, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 3, Montante = 12, DataPagam = new DateTime(2017,3,23), DataPrevPagam = new DateTime(2017,3,24), QuotaFK = 3, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 4, Montante = 12, DataPagam = new DateTime(2017,4,22), DataPrevPagam = new DateTime(2017,4,24), QuotaFK = 4, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 5, Montante = 12, DataPrevPagam = new DateTime(2017,5,24), QuotaFK = 5, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 6, Montante = 12, DataPrevPagam = new DateTime(2017,6,24), QuotaFK = 6, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 7, Montante = 12, DataPrevPagam = new DateTime(2017,7,24), QuotaFK = 7, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 8, Montante = 12, DataPrevPagam = new DateTime(2017,8,24), QuotaFK = 8, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 9, Montante = 12, DataPrevPagam = new DateTime(2017,9,24), QuotaFK = 9, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 10, Montante = 12, DataPrevPagam = new DateTime(2017,10,24), QuotaFK = 10, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 11, Montante = 12, DataPrevPagam = new DateTime(2017,11,24), QuotaFK = 11, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 12, Montante = 12, DataPrevPagam = new DateTime(2017,12,24), QuotaFK = 12, SocioFK = 1, FuncionarioFK = 1}
            };

            //pagamentos.ForEach(pp => context.Pagamentos.AddOrUpdate(p => new { p.PagamentoID, p.Montante }, pp));
            pagamentos.ForEach(pp => context.Pagamentos.Add(pp));
            context.SaveChanges();

            // ############################################################
            // adiciona Beneficios
            var beneficios = new List<Beneficios> {
                new Beneficios {BeneficioID = 1, Descricao = "Desconto imediato de 10% na loja do clube", EntidRespons = "Clube", ListaCategorias = new List<Categorias> {categorias[0], categorias[1], categorias[2]}},
                new Beneficios {BeneficioID = 2, Descricao = "Oferta de visita ao estádio", EntidRespons = "Clube", ListaCategorias = new List<Categorias> {categorias[0], categorias[1], categorias[2]}},
                new Beneficios {BeneficioID = 3, Descricao = "Descontos até 5% no Gás Natural e 6% na electricidade", EntidRespons = "Empresa de eletricidade", ListaCategorias = new List<Categorias> {categorias[0]}},
                new Beneficios {BeneficioID = 4, Descricao = "Descontos na bilhética nos jogos realizados no estádio do clube", EntidRespons = "Clube", ListaCategorias = new List<Categorias> {categorias[0], categorias[1], categorias[2]}},
                new Beneficios {BeneficioID = 5, Descricao = "Desconto 5% em todos os produtos da loja de desporto", EntidRespons = "Loja de desporto", ListaCategorias = new List<Categorias> {categorias[0], categorias[1], categorias[2]}},
                new Beneficios {BeneficioID = 6, Descricao = "Oferta da revista do clube", EntidRespons = "Clube", ListaCategorias = new List<Categorias> {categorias[0], categorias[1]}}
            };

            beneficios.ForEach(bb => context.Beneficios.AddOrUpdate(b => b.Descricao, bb));
            context.SaveChanges();
        }
    }
}
