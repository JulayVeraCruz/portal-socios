using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalSocios.Models {
    public class Quotas {

        public Quotas() {
            // inicialização da lista de pagamentos de uma quota
            ListaPagamentos = new HashSet<Pagamentos>();
        }

        [Key]
        public int QuotaID { get; set; }

       // [Index(IsUnique = true)]
        [Required(ErrorMessage = "A {0} é obrigatória!")]
        [Display(Name = "Referência")]
        public string Referencia { get; set; }

      //  [DataType(DataType.Currency)]
       // [RegularExpression("[0-9]+(,[0-9]{1,2})?", ErrorMessage = "Introduza um valor inteiro ou decimal, no formato 0,00.")]
        [Required(ErrorMessage = "O {0} é obrigatório!")]
        public decimal Montante { get; set; }

        [NotMapped]
        [RegularExpression("[0-9]+(,[0-9]{1,2})?", ErrorMessage = "Introduza um valor inteiro ou decimal, no formato 0,00")]
        [Required(ErrorMessage = "O {0} é obrigatório!")]
        [Display(Name = "Montante")]
        public string AuxMontante { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório!")]
        public int Ano { get; set; }

        [StringLength(15)]
        [RegularExpression("[A-Z][a-z]+", ErrorMessage = "Introduza apenas letras. A {0} começa obrigatoriamente por uma maiúscula.")]
        [Required(ErrorMessage = "A {0} é obrigatória!")]        
        public string Periodicidade { get; set; }

        // uma quota tem uma coleção de pagamentos
        public virtual ICollection<Pagamentos> ListaPagamentos { get; set; }

        // criação da chave forasteira
        [ForeignKey("Categoria")]
        public int CategoriaFK { get; set; }
        public virtual Categorias Categoria { get; set; }
    }
}
