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

        [Display(Name = "N.º de Sócio")]
        public int? NumSocio { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [Display(Name = "Nome Completo")]
        public string Nome { get; set; }

        [Index(IsUnique = true)]
        [RegularExpression("[0-9]{8}", ErrorMessage = "Introduza 8 caracteres numéricos.")]
        [StringLength(8)]
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [Display(Name = "BI / CC")]
        public string BI { get; set; }

        [Index(IsUnique = true)]
        [RegularExpression("[0-9]{9}", ErrorMessage = "Introduza 9 caracteres numéricos.")]
        [StringLength(9)]
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        public string NIF { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNasc { get; set; }

        [StringLength(50)]
        [EmailAddress(ErrorMessage = "E-mail inválido!")]
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [StringLength(9)]
        [RegularExpression("[0-9]{9}", ErrorMessage = "Introduza 9 caracteres numéricos.")]
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [Display(Name = "Telemóvel")]
        public string Telemovel { get; set; }

        [StringLength(80)]
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        public string Morada { get; set; }

        [StringLength(30)]
        [RegularExpression("[0-9]{4}-[0-9]{3} [A-ZÁÀÂÃÄÉÈÊËÍÌÎÏÓÒÔÕÖÚÙÛÜÇ]+((-| )?[A-ZÁÀÂÃÄÉÈÊËÍÌÎÏÓÒÔÕÖÚÙÛÜÇ]+)*", ErrorMessage = "Introduza o código postal no formato 0000-000 XXXXX.")]
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [Display(Name = "Código Postal")]
        public string CodPostal { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        public string Fotografia { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de Inscrição")]
        public DateTime? DataInscr { get; set; }

        // um sócio tem uma coleção de pagamentos
        public virtual ICollection<Pagamentos> ListaPagamentos { get; set; }

        // criação da chave forasteira
        [ForeignKey("Categoria")]
        public int CategoriaFK { get; set; }
        public virtual Categorias Categoria { get; set; }        
    }
}
