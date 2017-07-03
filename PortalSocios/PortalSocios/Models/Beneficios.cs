using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortalSocios.Models {
    public class Beneficios {

        public Beneficios() {
            // inicialização da lista de categorias de um beneficio
            ListaCategorias = new HashSet<Categorias>();
        }

        [Key]
        public int BeneficioID { get; set; }
        
        [StringLength(100)]        
        [Required(ErrorMessage = "A {0} é obrigatória!")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "A {0} é obrigatória!")]
        [Display(Name = "Entidade Responsável")]
        public string EntidRespons { get; set; }

        // um beneficio tem uma coleção de categorias
        public virtual ICollection<Categorias> ListaCategorias { get; set; }
    }
}
