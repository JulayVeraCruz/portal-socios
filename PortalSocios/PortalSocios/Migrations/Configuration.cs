namespace PortalSocios.Migrations {
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<PortalSocios.Models.SociosBD>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PortalSocios.Models.SociosBD context) {

            // Configuração --- SEED
            //=============================================================

            // ############################################################################################
            // adiciona Categorias
            var categorias = new List<Categorias> {
                new Categorias {CategoriaID = 1, Nome = "Sénior", FaixaEtaria = "Maiores de 18", NumQuotasAnuais = 12, ValorMensal = 12},
                new Categorias {CategoriaID = 2, Nome = "Juvenil", FaixaEtaria = "Entre 12 e 17", NumQuotasAnuais = 12, ValorMensal = 6},
                new Categorias {CategoriaID = 3, Nome = "Infantil", FaixaEtaria = "Entre 0 e 11", NumQuotasAnuais = 12, ValorMensal = 3}
            };

            categorias.ForEach(cc => context.Categorias.AddOrUpdate(c => c.Nome, cc));
            context.SaveChanges();

            // ############################################################################################
            // adiciona Socios
            var socios = new List<Socios> {
                new Socios {SocioID = 1, NumSocio = 1, Nome = "Simão Alvito Modesto", NIF = "172596295", Morada = "Rua dos Armazéns N.º 40", CodPostal = "5743-247 TORRES NOVAS", DataNasc = new DateTime(1962,2,6), Telemovel = "913567285", Email = "simao_modesto@gmail.com", Fotografia = "samodesto.jpg", DataInscr = new DateTime(2017,1,6), CategoriaFK = 1},
                new Socios {SocioID = 2, NumSocio = 2, Nome = "Maria Gonçalves Sousa", NIF = "498953925", Morada = "Rua 1º de Dezembro N.º 2 - 3 Esq.", CodPostal = "2485-632 TOMAR", DataNasc = new DateTime(1991,8,24), Telemovel = "916258358", Email = "maria_sousa@gmail.com", Fotografia = "mgsousa.jpg", DataInscr = new DateTime(2017,1,10), CategoriaFK = 1},
                new Socios {SocioID = 3, NumSocio = 3, Nome = "Miguel Alves Almeida", NIF = "632158329", Morada = "Rua Vale Miguel N.º 10", CodPostal = "2485-215 TOMAR", DataNasc = new DateTime(2002,10,30), Telemovel = "914752861", Email = "miguelaalmeida@outlook.com", Fotografia = "maalmeida.jpg", DataInscr = new DateTime(2017,1,19), CategoriaFK = 2},
                new Socios {SocioID = 4, NumSocio = 4, Nome = "Manuela Silva Rocha", NIF = "628102144", Morada = "Travessa do Parque N.º 88", CodPostal = "4257-156 LISBOA", DataNasc = new DateTime(2006,5,13), Telemovel = "961471852", Email = "manuela_rocha@hotmail.com", Fotografia = "msrocha.jpg", DataInscr = new DateTime(2017,1,28), CategoriaFK = 3},
                new Socios {SocioID = 5, NumSocio = 5, Nome = "André Filipe Melo Barbosa", NIF = "294953732", Morada = "Rua do Pelourinho N.º 1 - 2 Dir.", CodPostal = "4257-742 LISBOA", DataNasc = new DateTime(1982,12,12), Telemovel = "962148215", Email = "andrefm.barbosa@gmail.com", Fotografia = "afmbarbosa.jpg", DataInscr = new DateTime(2017,2,3), CategoriaFK = 1},
                new Socios {SocioID = 6, NumSocio = 6, Nome = "Ana Lima Rocha Fernandes", NIF = "613103285", Morada = "Rua dos Combatentes N.º 17", CodPostal = "6759-022 ENTRONCAMENTO", DataNasc = new DateTime(2004,5,28), Telemovel = "915372758", Email = "analrfernandes@outlook.com", Fotografia = "alrfernandes.jpg", DataInscr = new DateTime(2017,2,8), CategoriaFK = 2}
            };

            socios.ForEach(ss => context.Socios.AddOrUpdate(s => s.NIF, ss));
            context.SaveChanges();

            // ############################################################################################
            // adiciona Quotas
            var quotas = new List<Quotas> {
                new Quotas {QuotaID = 1, Montante = 12, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 2, Montante = 12, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 3, Montante = 12, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 4, Montante = 12, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 5, Montante = 12, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 6, Montante = 12, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 7, Montante = 12, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 8, Montante = 12, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 9, Montante = 12, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 10, Montante = 12, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 11, Montante = 12, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 12, Montante = 12, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 13, Montante = 6, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 2},
                new Quotas {QuotaID = 14, Montante = 6, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 2},
                new Quotas {QuotaID = 15, Montante = 6, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 2},
                new Quotas {QuotaID = 16, Montante = 6, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 2},
                new Quotas {QuotaID = 17, Montante = 6, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 2},
                new Quotas {QuotaID = 18, Montante = 6, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 2},
                new Quotas {QuotaID = 19, Montante = 6, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 2},
                new Quotas {QuotaID = 20, Montante = 6, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 2},
                new Quotas {QuotaID = 21, Montante = 6, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 2},
                new Quotas {QuotaID = 22, Montante = 6, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 2},
                new Quotas {QuotaID = 23, Montante = 6, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 2},
                new Quotas {QuotaID = 24, Montante = 6, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 2},
                new Quotas {QuotaID = 25, Montante = 3, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 3},
                new Quotas {QuotaID = 26, Montante = 3, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 3},
                new Quotas {QuotaID = 27, Montante = 3, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 3},
                new Quotas {QuotaID = 28, Montante = 3, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 3},
                new Quotas {QuotaID = 29, Montante = 3, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 3},
                new Quotas {QuotaID = 30, Montante = 3, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 3},
                new Quotas {QuotaID = 31, Montante = 3, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 3},
                new Quotas {QuotaID = 32, Montante = 3, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 3},
                new Quotas {QuotaID = 33, Montante = 3, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 3},
                new Quotas {QuotaID = 34, Montante = 3, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 3},
                new Quotas {QuotaID = 35, Montante = 3, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 3},
                new Quotas {QuotaID = 36, Montante = 3, Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 3}
            };

            quotas.ForEach(qq => context.Quotas.AddOrUpdate(q => q.QuotaID, qq));
            context.SaveChanges();

            // ############################################################################################
            // adiciona Funcionarios
            var funcionarios = new List<Funcionarios> {
                new Funcionarios {FuncionarioID = 1, Nome = "David Alves", NIF = "628438592", Morada = "Rua das Oliveirinhas N.º 32", CodPostal = "2835-852 OURÉM", Telemovel = "916732583", DataEntrClube = new DateTime(2016,11,26)},
                new Funcionarios {FuncionarioID = 2, Nome = "Alice Azevedo", NIF = "832652957", Morada = "Avenida da Praia N.º 4 - 3 Esq.", CodPostal = "2485-024 TOMAR", Telemovel = "915285673", DataEntrClube = new DateTime(2016,12,10)},
                new Funcionarios {FuncionarioID = 3, Nome = "Rebeca Ferreira", NIF = "721864159", Morada = "Rua da Tradição N.º 12", CodPostal = "2485-145 TOMAR", Telemovel = "919528259", DataEntrClube = new DateTime(2016,12,17)}
            };

            funcionarios.ForEach(ff => context.Funcionarios.AddOrUpdate(f => f.NIF, ff));
            context.SaveChanges();

            // ############################################################################################
            // adiciona Pagamentos
            var pagamentos = new List<Pagamentos> {
                new Pagamentos {PagamentoID = 1, RefMultibanco = "620832401", Montante = 12, DataPagam = new DateTime(2017,1,18), DataPrevPagam = new DateTime(2017,1,24), QuotaFK = 1, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 2, RefMultibanco = "620832402", Montante = 12, DataPagam = new DateTime(2017,2,15), DataPrevPagam = new DateTime(2017,2,24), QuotaFK = 2, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 3, RefMultibanco = "620832403", Montante = 12, DataPagam = new DateTime(2017,3,23), DataPrevPagam = new DateTime(2017,3,24), QuotaFK = 3, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 4, RefMultibanco = "620832404", Montante = 12, DataPagam = new DateTime(2017,4,22), DataPrevPagam = new DateTime(2017,4,24), QuotaFK = 4, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 5, RefMultibanco = "620832405", Montante = 12, DataPrevPagam = new DateTime(2017,5,24), QuotaFK = 5, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 6, RefMultibanco = "620832406", Montante = 12, DataPrevPagam = new DateTime(2017,6,24), QuotaFK = 6, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 7, RefMultibanco = "620832407", Montante = 12, DataPrevPagam = new DateTime(2017,7,24), QuotaFK = 7, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 8, RefMultibanco = "620832408", Montante = 12, DataPrevPagam = new DateTime(2017,8,24), QuotaFK = 8, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 9, RefMultibanco = "620832409", Montante = 12, DataPrevPagam = new DateTime(2017,9,24), QuotaFK = 9, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 10, RefMultibanco = "620832410", Montante = 12, DataPrevPagam = new DateTime(2017,10,24), QuotaFK = 10, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 11, RefMultibanco = "620832411", Montante = 12, DataPrevPagam = new DateTime(2017,11,24), QuotaFK = 11, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 12, RefMultibanco = "620832412", Montante = 12, DataPrevPagam = new DateTime(2017,12,24), QuotaFK = 12, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 13, RefMultibanco = "620832413", Montante = 12, DataPagam = new DateTime(2017,1,21), DataPrevPagam = new DateTime(2017,1,24), QuotaFK = 1, SocioFK = 2, FuncionarioFK = 2},
                new Pagamentos {PagamentoID = 14, RefMultibanco = "620832414", Montante = 12, DataPagam = new DateTime(2017,2,20), DataPrevPagam = new DateTime(2017,2,24), QuotaFK = 2, SocioFK = 2, FuncionarioFK = 2},
                new Pagamentos {PagamentoID = 15, RefMultibanco = "620832415", Montante = 12, DataPagam = new DateTime(2017,3,21), DataPrevPagam = new DateTime(2017,3,24), QuotaFK = 3, SocioFK = 2, FuncionarioFK = 2},
                new Pagamentos {PagamentoID = 16, RefMultibanco = "620832416", Montante = 12, DataPrevPagam = new DateTime(2017,4,24), Multa = 5, QuotaFK = 4, SocioFK = 2, FuncionarioFK = 2},
                new Pagamentos {PagamentoID = 17, RefMultibanco = "620832417", Montante = 12, DataPrevPagam = new DateTime(2017,5,24), QuotaFK = 5, SocioFK = 2, FuncionarioFK = 2},
                new Pagamentos {PagamentoID = 18, RefMultibanco = "620832418", Montante = 12, DataPrevPagam = new DateTime(2017,6,24), QuotaFK = 6, SocioFK = 2, FuncionarioFK = 2},
                new Pagamentos {PagamentoID = 19, RefMultibanco = "620832419", Montante = 12, DataPrevPagam = new DateTime(2017,7,24), QuotaFK = 7, SocioFK = 2, FuncionarioFK = 2},
                new Pagamentos {PagamentoID = 20, RefMultibanco = "620832420", Montante = 12, DataPrevPagam = new DateTime(2017,8,24), QuotaFK = 8, SocioFK = 2, FuncionarioFK = 2},
                new Pagamentos {PagamentoID = 21, RefMultibanco = "620832421", Montante = 12, DataPrevPagam = new DateTime(2017,9,24), QuotaFK = 9, SocioFK = 2, FuncionarioFK = 2},
                new Pagamentos {PagamentoID = 22, RefMultibanco = "620832422", Montante = 12, DataPrevPagam = new DateTime(2017,10,24), QuotaFK = 10, SocioFK = 2, FuncionarioFK = 2},
                new Pagamentos {PagamentoID = 23, RefMultibanco = "620832423", Montante = 12, DataPrevPagam = new DateTime(2017,11,24), QuotaFK = 11, SocioFK = 2, FuncionarioFK = 2},
                new Pagamentos {PagamentoID = 24, RefMultibanco = "620832424", Montante = 12, DataPrevPagam = new DateTime(2017,12,24), QuotaFK = 12, SocioFK = 2, FuncionarioFK = 2}
            };

            pagamentos.ForEach(pp => context.Pagamentos.AddOrUpdate(p => p.RefMultibanco, pp));
            context.SaveChanges();

            // ############################################################################################
            // adiciona Beneficios
            var beneficios = new List<Beneficios> {
                new Beneficios {BeneficioID = 1, Descricao = "Desconto imediato de 10% em todos os artigos da loja do clube", EntidRespons = "Clube", ListaCategorias = new List<Categorias> {categorias[0], categorias[1], categorias[2]}},
                new Beneficios {BeneficioID = 2, Descricao = "Oferta de visita ao estádio", EntidRespons = "Clube", ListaCategorias = new List<Categorias> {categorias[0], categorias[1], categorias[2]}},
                new Beneficios {BeneficioID = 3, Descricao = "Desconto de 5% na eletricidade", EntidRespons = "Empresa X", ListaCategorias = new List<Categorias> {categorias[0]}},
                new Beneficios {BeneficioID = 4, Descricao = "Desconto nos bilhetes para os jogos realizados em casa", EntidRespons = "Clube", ListaCategorias = new List<Categorias> {categorias[0], categorias[1], categorias[2]}},
                new Beneficios {BeneficioID = 5, Descricao = "Desconto de 6 cêntimos/litro em combustível", EntidRespons = "Empresa Y", ListaCategorias = new List<Categorias> {categorias[0]}},
                new Beneficios {BeneficioID = 6, Descricao = "Oferta da revista do clube", EntidRespons = "Clube", ListaCategorias = new List<Categorias> {categorias[0], categorias[1]}}
            };

            beneficios.ForEach(bb => context.Beneficios.AddOrUpdate(b => b.Descricao, bb));
            context.SaveChanges();
        }
    }
}
