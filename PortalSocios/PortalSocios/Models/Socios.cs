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

        [Index(IsUnique = true)]
        [Display(Name = "N.º de Sócio")]
        public int? NumSocio { get; set; }
        
        [StringLength(50)]
        [RegularExpression("[A-ZÁÂÉÍÓÚ][a-záàâãäèéêëìíîïòóôõöùúûüç]+((-| )((da|de|do|das|dos) )?[A-ZÁÂÉÍÓÚ][a-záàâãäèéêëìíîïòóôõöùúûüç]+)+", ErrorMessage = "Introduza apenas letras. O {0} começa obrigatoriamente por uma maiúscula.")]
        [Required(ErrorMessage = "O {0} é obrigatório!")]
        [Display(Name = "Nome Completo")]
        public string Nome { get; set; }
        
        [StringLength(8)]
        [Index(IsUnique = true)]
        [RegularExpression("[0-9]{8}", ErrorMessage = "Introduza 8 caracteres numéricos.")]        
        [Required(ErrorMessage = "O {0} é obrigatório!")]
        [Display(Name = "BI / CC")]
        public string BI { get; set; }

        [StringLength(9)]
        [Index(IsUnique = true)]
        [RegularExpression("[0-9]{9}", ErrorMessage = "Introduza 9 caracteres numéricos.")]        
        [Required(ErrorMessage = "O {0} é obrigatório!")]
        public string NIF { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "A {0} é obrigatória!")]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNasc { get; set; }

        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Introduza um {0} válido!")]
        [Required(ErrorMessage = "O {0} é obrigatório!")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [StringLength(9)]
        [RegularExpression("[0-9]{9}", ErrorMessage = "Introduza 9 caracteres numéricos.")]
        [Required(ErrorMessage = "O {0} é obrigatório!")]
        [Display(Name = "Telemóvel")]
        public string Telemovel { get; set; }

        [StringLength(80)]
        [Required(ErrorMessage = "A {0} é obrigatória!")]
        public string Morada { get; set; }

        [StringLength(50)]
        [RegularExpression("[0-9]{4}-[0-9]{3}( [A-ZÁÂÃÉÊÍÎÓÔÕÚÛÇ.-]+)+", ErrorMessage = "Introduza o {0} no formato 0000-000 LOCALIDADE.")]
        [Required(ErrorMessage = "O {0} é obrigatório!")]
        [Display(Name = "Código Postal")]
        public string CodPostal { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "A {0} é obrigatória!")]
        public string Fotografia { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
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
