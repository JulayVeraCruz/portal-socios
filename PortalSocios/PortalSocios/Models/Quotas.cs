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

        [RegularExpression("[0-9]{1,4}(,[0-9]{1,2})?", ErrorMessage = "Introduza um valor inteiro ou decimal.")]
        [Required(ErrorMessage = "O montante é obrigatório!")]
        public float Montante { get; set; }

        [Required(ErrorMessage = "O ano é obrigatório!")]
        public int Ano { get; set; }

        [StringLength(15)]
        [RegularExpression("[A-Z][a-z]+", ErrorMessage = "Introduza apenas letras. A periodicidade começa, obrigatoriamente, por uma maiúscula.")]
        [Required(ErrorMessage = "A periodicidade é obrigatória!")]        
        public string Periodicidade { get; set; }

        // uma quota tem uma coleção de pagamentos
        public virtual ICollection<Pagamentos> ListaPagamentos { get; set; }

        // criação da chave forasteira
        [ForeignKey("Categoria")]
        public int CategoriaFK { get; set; }
        public virtual Categorias Categoria { get; set; }
    }
}
