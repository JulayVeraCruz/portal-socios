using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalSocios.Models {
    public class Pagamentos {

        [Key]
        public int PagamentoID { get; set; }

        [StringLength(9)]        
        [RegularExpression("[0-9]{9}", ErrorMessage = "Introduza 9 caracteres numéricos.")]
        [Required(ErrorMessage = "A {0} é obrigatória!")]
        [Display(Name = "Referência Multibanco")]
        public string RefMultibanco { get; set; }

        [DataType(DataType.Currency)]
        [RegularExpression("[0-9]+(,[0-9]{1,2})?", ErrorMessage = "Introduza um valor inteiro ou decimal.")]
        [Required(ErrorMessage = "O {0} é obrigatório!")]
        public decimal Montante { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de Pagamento")]
        public DateTime? DataPagam { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "A {0} é obrigatória!")]
        [Display(Name = "Data Prevista de Pagamento")]
        public DateTime DataPrevPagam { get; set; }

        [DataType(DataType.Currency)]
        [RegularExpression("[0-9]+(,[0-9]{1,2})?", ErrorMessage = "Introduza um valor inteiro ou decimal.")]
        public decimal? Multa { get; set; }

        // criação das chaves forasteiras
        [ForeignKey("Quota")]
        public int QuotaFK { get; set; }
        public virtual Quotas Quota { get; set; }
        
        [ForeignKey("Socio")]
        public int SocioFK { get; set; }
        public virtual Socios Socio { get; set; }

        [ForeignKey("Funcionario")]
        public int FuncionarioFK { get; set; }
        public virtual Funcionarios Funcionario { get; set; }        
    }
}
