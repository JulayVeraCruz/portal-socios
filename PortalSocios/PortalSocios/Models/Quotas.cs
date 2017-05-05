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

        [Required]
        public float Montante { get; set; }
        
        [Required]
        public int Ano { get; set; }

        [Required]
        [StringLength(15)]
        public string Periodicidade { get; set; }

        // uma quota tem uma coleção de pagamentos
        public virtual ICollection<Pagamentos> ListaPagamentos { get; set; }

        // criação da chave forasteira
        [ForeignKey("Categoria")]
        public int CategoriaFK { get; set; }
        public virtual Categorias Categoria { get; set; }
    }
}
