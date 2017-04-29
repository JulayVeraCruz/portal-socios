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

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(9)]
        public string NIF { get; set; }

        [Required]
        [StringLength(80)]
        public string Morada { get; set; }

        [Required]
        [StringLength(30)]
        public string CodPostal { get; set; }
        
        [Required]
        [StringLength(9)]
        public string Telemovel { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime DataEntrClube { get; set; }

        // um funcionário tem uma coleção de pagamentos
        public virtual ICollection<Pagamentos> ListaPagamentos { get; set; }
    }
}
