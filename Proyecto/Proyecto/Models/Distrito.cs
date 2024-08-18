using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class Distrito
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDistrito { get; set; }

        [Required(ErrorMessage = "El nombre del distrito es requerido.")]
        public string Nombre { get; set; }

        [ForeignKey("Canton")]
        public int IdCanton { get; set; }
        public Canton Canton { get; set; }

        public List<Bodegas> Bodegas { get; set; } = new List<Bodegas>();
        public List<Proveedores> Proveedores { get; set; } = new List<Proveedores>();
    }

}
