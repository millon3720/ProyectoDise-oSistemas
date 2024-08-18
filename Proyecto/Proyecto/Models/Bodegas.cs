using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class Bodegas
    {
        [Key]
        public int IdBodegas { get; set; }

        [Required(ErrorMessage = "La dirección exacta de la bodega es requerida.")]
        public string DireccionExacta { get; set; }
        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Nombre { get; set; }
        // Claves Foráneas
        public int IdProvincia { get; set; }
        public Provincia Provincia { get; set; }

        public int IdCanton { get; set; }
        public Canton Canton { get; set; }

        public int IdDistrito { get; set; }
        public Distrito Distrito { get; set; }

        public List<ProductosBodega> ProductosBodega { get; set; }
        public List<Usuarios> Usuarios { get; set; }
    }

}
