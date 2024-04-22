using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class Bodegas
    {
        [Key]
        public int IdBodegas { get; set; }

        [ForeignKey("Provincia")]
        public int IdProvincia { get; set; }
        public Provincia Provincia { get; set; }

        [ForeignKey("Canton")]
        public int IdCanton { get; set; }
        public Canton Canton { get; set; }

        [ForeignKey("Distrito")]
        public int IdDistrito { get; set; }
        public Distrito Distrito { get; set; }


        [Required(ErrorMessage = "La dirección exacta de la bodega es requerida.")]
        public string DireccionExacta { get; set; }
        
        public List<ProductosBodega> ProductosBodega { get; set; }
        
        public List<Usuarios> Usuarios { get; set; }
    }
}
