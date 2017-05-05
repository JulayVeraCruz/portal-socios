using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalSocios.Models {
    public class Pagamentos {

        [Key]
        public int PagamentoID { get; set; }

        [Required]
        [StringLength(9)]
        public string RefMultibanco { get; set; }

        [Required]
        public float Montante { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataPagam { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime DataPrevPagam { get; set; }

        public float? Multa { get; set; }

        // criação das chaves forasteiras
        [ForeignKey("Quota")]
        public int QuotaFK { get; set; }
        public Quotas Quota { get; set; }
        
        [ForeignKey("Socio")]
        public int SocioFK { get; set; }
        public Socios Socio { get; set; }
        
        [ForeignKey("Funcionario")]
        public int FuncionarioFK { get; set; }
        public Funcionarios Funcionario { get; set; }        
    }
}
