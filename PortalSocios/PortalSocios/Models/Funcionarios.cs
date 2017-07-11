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
        [RegularExpression("[A-ZÁÂÉÍÓÚ][a-záàâãäèéêëìíîïòóôõöùúûüç]+(-| )((da|de|do|das|dos) )?[A-ZÁÂÉÍÓÚ][a-záàâãäèéêëìíîïòóôõöùúûüç]+", ErrorMessage = "O {0} é constituído apenas por letras e começa obrigatoriamente por uma maiúscula.")]
        [Required(ErrorMessage = "O {0} é obrigatório!")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [StringLength(9)]
        [Index(IsUnique = true)]
        [RegularExpression("[0-9]{9}", ErrorMessage = "O {0} deve ter 9 caracteres numéricos.")]
        [Required(ErrorMessage = "O {0} é obrigatório!")]
        public string NIF { get; set; }

        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Introduza um {0} válido!")]
        [RegularExpression("[a-z]+@coxos.pt", ErrorMessage = "O {0} deve ter o formato nome@coxos.pt")]
        [Required(ErrorMessage = "O {0} é obrigatório!")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [StringLength(9)]
        [RegularExpression("[0-9]{9}", ErrorMessage = "O {0} deve ter 9 caracteres numéricos.")]
        [Required(ErrorMessage = "O {0} é obrigatório!")]
        [Display(Name = "Telemóvel")]
        public string Telemovel { get; set; }

        [StringLength(80)]
        [Required(ErrorMessage = "A {0} é obrigatória!")]
        public string Morada { get; set; }

        [StringLength(50)]
        [RegularExpression("[0-9]{4}-[0-9]{3}( [A-ZÁÂÃÉÊÍÎÓÔÕÚÛÇ.-]+)+", ErrorMessage = "O {0} deve ter o formato 0000-000 LOCALIDADE.")]
        [Required(ErrorMessage = "O {0} é obrigatório!")]
        [Display(Name = "Código Postal")]
        public string CodPostal { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "A {0} é obrigatória!")]
        [Display(Name = "Data Entrada")]
        public DateTime DataEntrClube { get; set; }

        // um funcionário tem uma coleção de pagamentos
        public virtual ICollection<Pagamentos> ListaPagamentos { get; set; }
    }
}
