using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        [StringLength(30)]
        [Display(Name = "Categoria")]
        public string Nome { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Faixa Etária")]
        public string FaixaEtaria { get; set; }

        [Required]
        [Display(Name = "N.º Quotas Anuais")]
        public int NumQuotasAnuais { get; set; }

        [Required]
        [Display(Name = "Valor Mensal")]
        public float ValorMensal { get; set; }

        // uma categoria tem uma coleção de sócios
        public virtual ICollection<Socios> ListaSocios { get; set; }
        // uma categoria tem uma coleção de quotas
        public virtual ICollection<Quotas> ListaQuotas { get; set; }
        // uma categoria tem uma coleção de beneficios
        public virtual ICollection<Beneficios> ListaBeneficios { get; set; }
    }
}
