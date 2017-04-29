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

        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }

        [Required]
        [StringLength(50)]
        public string EntidRespons { get; set; }

        // um beneficio tem uma coleção de categorias
        public virtual ICollection<Categorias> ListaCategorias { get; set; }
    }
}
