using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalSocios.Models {
    public class Pagamentos {

        [Key]
        public int PagamentoID { get; set; }

        [StringLength(9)]
        [Index(IsUnique = true)]
        [RegularExpression("[0-9]{9}", ErrorMessage = "Introduza 9 caracteres numéricos.")]
        [Required(ErrorMessage = "A {0} é obrigatória!")]
        [Display(Name = "Referência Multibanco")]
        public string RefMultibanco { get; set; }

        public decimal Montante { get; set; }

        // atributo auxiliar
        [NotMapped]
        [RegularExpression("[0-9]+(,[0-9]{1,2})?", ErrorMessage = "Introduza um valor inteiro ou decimal, no formato 0,00")]
        [Required(ErrorMessage = "O {0} é obrigatório!")]
        [Display(Name = "Montante")]
        public string AuxMontante { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data Pagamento")]
        public DateTime? DataPagam { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "A {0} é obrigatória!")]
        [Display(Name = "Data Prev. Pagamento")]
        public DateTime DataPrevPagam { get; set; }

        public decimal? Multa { get; set; }

        // atributo auxiliar
        [NotMapped]
        [RegularExpression("[0-9]+(,[0-9]{1,2})?", ErrorMessage = "Introduza um valor inteiro ou decimal, no formato 0,00")]
        [Display(Name = "Multa")]
        public string AuxMulta { get; set; }

        // criação de um atributo para ligar a tabela 'Pagamentos' à tabela de utilizadores
        [Display(Name = "Username")]
        [EmailAddress(ErrorMessage = "Introduza um {0} válido!")]
        [Required(ErrorMessage = "O {0} é obrigatório!")]
        public string UserName { get; set; }

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
