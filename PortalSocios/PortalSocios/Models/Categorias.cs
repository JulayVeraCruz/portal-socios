using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalSocios.Models {
    public class Categorias {

        public Categorias() {
            // inicialização da lista de socios de uma categoria
            ListaSocios = new HashSet<Socios>();
            // inicialização da lista de quotas de uma categoria
            ListaQuotas = new HashSet<Quotas>();
            // inicialização da lista de beneficios de uma categoria
            ListaBeneficios = new HashSet<Beneficios>();
        }

        [Key]
        public int CategoriaID { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "O {0} é obrigatório!")]
        public string Nome { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "A {0} é obrigatória!")]
        [Display(Name = "Faixa Etária")]
        public string FaixaEtaria { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório!")]
        [Display(Name = "N.º Quotas Anuais")]
        public int NumQuotasAnuais { get; set; }

        [Display(Name = "Valor Mensal")]
        public decimal ValorMensal { get; set; }

        // atributo auxiliar
        [NotMapped]
        [RegularExpression("[0-9]+(,[0-9]{1,2})?", ErrorMessage = "Introduza um valor inteiro ou decimal, no formato 0,00")]
        [Required(ErrorMessage = "O {0} é obrigatório!")]
        [Display(Name = "Valor Mensal")]
        public string AuxValorMensal { get; set; }

        // uma categoria tem uma coleção de sócios
        public virtual ICollection<Socios> ListaSocios { get; set; }
        // uma categoria tem uma coleção de quotas
        public virtual ICollection<Quotas> ListaQuotas { get; set; }
        // uma categoria tem uma coleção de beneficios
        public virtual ICollection<Beneficios> ListaBeneficios { get; set; }
    }
}
