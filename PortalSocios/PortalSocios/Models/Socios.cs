using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalSocios.Models {
    public class Socios {

        public Socios() {
            // inicialização da lista de pagamentos de um sócio 
            ListaPagamentos = new HashSet<Pagamentos>();
        }

        [Key]
        public int SocioID { get; set; }

        [Required]
        public int NumSocio { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(8)]
        public string BI { get; set; }

        [Required]
        [StringLength(9)]
        public string NIF { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime DataNasc { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(9)]
        public string Telemovel { get; set; }

        [Required]
        [StringLength(80)]
        public string Morada { get; set; }

        [Required]
        [StringLength(30)]
        public string CodPostal { get; set; }

        [Required]
        [StringLength(255)]
        public string Fotografia { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime DataInscr { get; set; }

        // um sócio tem uma coleção de pagamentos
        public virtual ICollection<Pagamentos> ListaPagamentos { get; set; }

        // criação da chave forasteira
        [ForeignKey("Categoria")]
        public int CategoriaFK { get; set; }
        public virtual Categorias Categoria { get; set; }        
    }
}
