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
                new Categorias {CategoriaID = 1, Nome = "Sénior", FaixaEtaria = "Maiores de 18", NumQuotasAnuais = 12, ValorMensal = 12, AuxValorMensal = "0"},
                new Categorias {CategoriaID = 2, Nome = "Juvenil", FaixaEtaria = "Entre 12 e 17", NumQuotasAnuais = 12, ValorMensal = 6, AuxValorMensal = "0"},
                new Categorias {CategoriaID = 3, Nome = "Infantil", FaixaEtaria = "Entre 0 e 11", NumQuotasAnuais = 12, ValorMensal = 3, AuxValorMensal = "0"}
            };

            categorias.ForEach(cc => context.Categorias.AddOrUpdate(c => c.Nome, cc));
            context.SaveChanges();

            // ############################################################################################
            // adiciona Socios
            var socios = new List<Socios> {
                new Socios {SocioID = 1, NumSocio = 1, Nome = "Simão Alvito Modesto", BI = "10325782", NIF = "172596295", DataNasc = new DateTime(1962,2,6), Email = "smodesto@gmail.com", Telemovel = "913567285", Morada = "Rua dos Armazéns N.º 40", CodPostal = "5743-247 TORRES NOVAS", Fotografia = "samodesto.jpg", DataInscr = new DateTime(2017,1,6), CategoriaFK = 1, UserName = "smodesto@gmail.com"},
                new Socios {SocioID = 2, NumSocio = 2, Nome = "Maria Gonçalves Sousa", BI = "13728586", NIF = "498953925", DataNasc = new DateTime(1991,8,24), Email = "msousa@gmail.com", Telemovel = "916258358", Morada = "Rua 1º de Dezembro N.º 2 - 3 Esq.", CodPostal = "2485-632 TOMAR", Fotografia = "mgsousa.jpg", DataInscr = new DateTime(2017,1,10), CategoriaFK = 1, UserName = "msousa@gmail.com"},
                new Socios {SocioID = 3, NumSocio = 3, Nome = "Miguel Alves Almeida", BI = "16259232", NIF = "632158329", DataNasc = new DateTime(2002,10,30), Email = "malmeida@outlook.com", Telemovel = "914752861", Morada = "Rua Vale Miguel N.º 10", CodPostal = "2485-215 TOMAR", Fotografia = "maalmeida.jpg", DataInscr = new DateTime(2017,1,19), CategoriaFK = 2, UserName = "malmeida@outlook.com"},
                new Socios {SocioID = 4, NumSocio = 4, Nome = "Manuela Silva Rocha", BI = "18325025", NIF = "628102144", DataNasc = new DateTime(2006,5,13), Email = "mrocha@hotmail.com", Telemovel = "961471852", Morada = "Travessa do Parque N.º 88", CodPostal = "4257-156 LISBOA", Fotografia = "msrocha.jpg", DataInscr = new DateTime(2017,1,28), CategoriaFK = 3, UserName = "mrocha@hotmail.com"},
                new Socios {SocioID = 5, NumSocio = 5, Nome = "André Filipe Melo Barbosa", BI = "11823582", NIF = "294953732", DataNasc = new DateTime(1982,12,12), Email = "abarbosa@gmail.com", Telemovel = "962148215", Morada = "Rua do Pelourinho N.º 1 - 2 Dir.", CodPostal = "4257-742 LISBOA", Fotografia = "afmbarbosa.jpg", DataInscr = new DateTime(2017,2,3), CategoriaFK = 1, UserName = "abarbosa@gmail.com"},
                new Socios {SocioID = 6, NumSocio = 6, Nome = "Ana Lima Rocha Fernandes", BI = "17549303", NIF = "613103285", DataNasc = new DateTime(2004,5,28), Email = "afernandes@outlook.com", Telemovel = "915372758", Morada = "Rua dos Combatentes N.º 17", CodPostal = "6759-022 ENTRONCAMENTO", Fotografia = "alrfernandes.jpg", DataInscr = new DateTime(2017,2,8), CategoriaFK = 2, UserName = "afernandes@outlook.com"}
            };

            socios.ForEach(ss => context.Socios.AddOrUpdate(s => s.BI, ss));
            context.SaveChanges();

            // ############################################################################################
            // adiciona Quotas
            var quotas = new List<Quotas> {
                new Quotas {QuotaID = 1, Referencia = "Sénior-2017-Mensal",   Montante = 12,  AuxMontante = "0", Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 1},
                new Quotas {QuotaID = 2, Referencia = "Sénior-2017-Anual",    Montante = 132, AuxMontante = "0", Ano = 2017, Periodicidade = "Anual",  CategoriaFK = 1},
                new Quotas {QuotaID = 3, Referencia = "Juvenil-2017-Mensal",  Montante = 6,   AuxMontante = "0", Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 2},
                new Quotas {QuotaID = 4, Referencia = "Juvenil-2017-Anual",   Montante = 66,  AuxMontante = "0", Ano = 2017, Periodicidade = "Anual",  CategoriaFK = 2},
                new Quotas {QuotaID = 5, Referencia = "Infantil-2017-Mensal", Montante = 3,   AuxMontante = "0", Ano = 2017, Periodicidade = "Mensal", CategoriaFK = 3},
                new Quotas {QuotaID = 6, Referencia = "Infantil-2017-Anual",  Montante = 33,  AuxMontante = "0", Ano = 2017, Periodicidade = "Anual",  CategoriaFK = 3}
            };

            quotas.ForEach(qq => context.Quotas.AddOrUpdate(q => q.QuotaID, qq));
            context.SaveChanges();

            // ############################################################################################
            // adiciona Funcionarios
            var funcionarios = new List<Funcionarios> {
                new Funcionarios {FuncionarioID = 1, Nome = "David Alves", NIF = "628438592", Email = "dalves@cdcb.pt", Telemovel = "916732583", Morada = "Rua das Oliveirinhas N.º 32", CodPostal = "2835-852 OURÉM", DataEntrClube = new DateTime(2016,11,26), UserName = "dalves@cdcb.pt"},
                new Funcionarios {FuncionarioID = 2, Nome = "Alice Azevedo", NIF = "832652957", Email = "aazevedo@cdcb.pt", Telemovel = "915285673", Morada = "Avenida da Praia N.º 4 - 3 Esq.", CodPostal = "2485-024 TOMAR", DataEntrClube = new DateTime(2016,12,10), UserName = "aazevedo@cdcb.pt"},
                new Funcionarios {FuncionarioID = 3, Nome = "Rebeca Ferreira", NIF = "721864159", Email = "rferreira@cdcb.pt", Telemovel = "919528259", Morada = "Rua da Tradição N.º 12", CodPostal = "2485-145 TOMAR", DataEntrClube = new DateTime(2016,12,17), UserName = "rferreira@cdcb.pt"}
            };

            funcionarios.ForEach(ff => context.Funcionarios.AddOrUpdate(f => f.NIF, ff));
            context.SaveChanges();

            // ############################################################################################
            // adiciona Pagamentos
            var pagamentos = new List<Pagamentos> {
                new Pagamentos {PagamentoID = 1, RefMultibanco = "620832401", Montante = 12, AuxMontante = "0", DataPagam = new DateTime(2017,1,18), DataPrevPagam = new DateTime(2017,1,24), QuotaFK = 1, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 2, RefMultibanco = "620832402", Montante = 12, AuxMontante = "0", DataPagam = new DateTime(2017,2,15), DataPrevPagam = new DateTime(2017,2,24), QuotaFK = 1, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 3, RefMultibanco = "620832403", Montante = 12, AuxMontante = "0", DataPagam = new DateTime(2017,3,23), DataPrevPagam = new DateTime(2017,3,24), QuotaFK = 1, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 4, RefMultibanco = "620832404", Montante = 12, AuxMontante = "0", DataPagam = new DateTime(2017,4,22), DataPrevPagam = new DateTime(2017,4,24), QuotaFK = 1, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 5, RefMultibanco = "620832405", Montante = 12, AuxMontante = "0", DataPrevPagam = new DateTime(2017,5,24), QuotaFK = 1, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 6, RefMultibanco = "620832406", Montante = 12, AuxMontante = "0", DataPrevPagam = new DateTime(2017,6,24), QuotaFK = 1, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 7, RefMultibanco = "620832407", Montante = 12, AuxMontante = "0", DataPrevPagam = new DateTime(2017,7,24), QuotaFK = 1, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 8, RefMultibanco = "620832408", Montante = 12, AuxMontante = "0", DataPrevPagam = new DateTime(2017,8,24), QuotaFK = 1, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 9, RefMultibanco = "620832409", Montante = 12, AuxMontante = "0", DataPrevPagam = new DateTime(2017,9,24), QuotaFK = 1, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 10, RefMultibanco = "620832410", Montante = 12, AuxMontante = "0", DataPrevPagam = new DateTime(2017,10,24), QuotaFK = 1, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 11, RefMultibanco = "620832411", Montante = 12, AuxMontante = "0", DataPrevPagam = new DateTime(2017,11,24), QuotaFK = 1, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 12, RefMultibanco = "620832412", Montante = 12, AuxMontante = "0", DataPrevPagam = new DateTime(2017,12,24), QuotaFK = 1, SocioFK = 1, FuncionarioFK = 1},
                new Pagamentos {PagamentoID = 13, RefMultibanco = "620832413", Montante = 12, AuxMontante = "0", DataPagam = new DateTime(2017,1,21), DataPrevPagam = new DateTime(2017,1,24), QuotaFK = 1, SocioFK = 2, FuncionarioFK = 2},
                new Pagamentos {PagamentoID = 14, RefMultibanco = "620832414", Montante = 12, AuxMontante = "0", DataPagam = new DateTime(2017,2,20), DataPrevPagam = new DateTime(2017,2,24), QuotaFK = 1, SocioFK = 2, FuncionarioFK = 2},
                new Pagamentos {PagamentoID = 15, RefMultibanco = "620832415", Montante = 12, AuxMontante = "0", DataPagam = new DateTime(2017,3,21), DataPrevPagam = new DateTime(2017,3,24), QuotaFK = 1, SocioFK = 2, FuncionarioFK = 2},
                new Pagamentos {PagamentoID = 16, RefMultibanco = "620832416", Montante = 12, AuxMontante = "0", DataPrevPagam = new DateTime(2017,4,24), Multa = 5, AuxMulta = "0", QuotaFK = 1, SocioFK = 2, FuncionarioFK = 2},
                new Pagamentos {PagamentoID = 17, RefMultibanco = "620832417", Montante = 12, AuxMontante = "0", DataPrevPagam = new DateTime(2017,5,24), QuotaFK = 1, SocioFK = 2, FuncionarioFK = 2},
                new Pagamentos {PagamentoID = 18, RefMultibanco = "620832418", Montante = 12, AuxMontante = "0", DataPrevPagam = new DateTime(2017,6,24), QuotaFK = 1, SocioFK = 2, FuncionarioFK = 2},
                new Pagamentos {PagamentoID = 19, RefMultibanco = "620832419", Montante = 12, AuxMontante = "0", DataPrevPagam = new DateTime(2017,7,24), QuotaFK = 1, SocioFK = 2, FuncionarioFK = 2},
                new Pagamentos {PagamentoID = 20, RefMultibanco = "620832420", Montante = 12, AuxMontante = "0", DataPrevPagam = new DateTime(2017,8,24), QuotaFK = 1, SocioFK = 2, FuncionarioFK = 2},
                new Pagamentos {PagamentoID = 21, RefMultibanco = "620832421", Montante = 12, AuxMontante = "0", DataPrevPagam = new DateTime(2017,9,24), QuotaFK = 1, SocioFK = 2, FuncionarioFK = 2},
                new Pagamentos {PagamentoID = 22, RefMultibanco = "620832422", Montante = 12, AuxMontante = "0", DataPrevPagam = new DateTime(2017,10,24), QuotaFK = 1, SocioFK = 2, FuncionarioFK = 2},
                new Pagamentos {PagamentoID = 23, RefMultibanco = "620832423", Montante = 12, AuxMontante = "0", DataPrevPagam = new DateTime(2017,11,24), QuotaFK = 1, SocioFK = 2, FuncionarioFK = 2},
                new Pagamentos {PagamentoID = 24, RefMultibanco = "620832424", Montante = 12, AuxMontante = "0", DataPrevPagam = new DateTime(2017,12,24), QuotaFK = 1, SocioFK = 2, FuncionarioFK = 2}
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
