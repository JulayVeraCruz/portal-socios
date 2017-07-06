using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalSocios.Models {
    public class Funcionarios {

        public Funcionarios() {
            // inicialização da lista de pagamentos de um funcionário
            ListaPagamentos = new HashSet<Pagamentos>();
        }

        [Key]
        public int FuncionarioID { get; set; }

        [StringLength(50)]
        [RegularExpression("[A-ZÁÂÉÍÓÚ][a-záàâãäèéêëìíîïòóôõöùúûüç]+(-| )((da|de|do|das|dos) )?[A-ZÁÂÉÍÓÚ][a-záàâãäèéêëìíîïòóôõöùúûüç]+", ErrorMessage = "Introduza apenas letras. O {0} começa obrigatoriamente por uma maiúscula.")]
        [Required(ErrorMessage = "O {0} é obrigatório!")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [StringLength(9)]
        [Index(IsUnique = true)]
        [RegularExpression("[0-9]{9}", ErrorMessage = "Introduza 9 caracteres numéricos.")]
        [Required(ErrorMessage = "O {0} é obrigatório!")]
        public string NIF { get; set; }

        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Introduza um {0} válido!")]
        [RegularExpression("[a-z]+@cdcb.pt", ErrorMessage = "Introduza o {0} no formato nome@cdcb.pt")]
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

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "A {0} é obrigatória!")]
        [Display(Name = "Data de Entrada")]
        public DateTime DataEntrClube { get; set; }

        // criação de um atributo para ligar este atributo à BD de autenticação
        public string UserName { get; set; } // corresponde ao LOGIN

        // um funcionário tem uma coleção de pagamentos
        public virtual ICollection<Pagamentos> ListaPagamentos { get; set; }
    }
}
