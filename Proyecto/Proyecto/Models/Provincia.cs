using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class Provincia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int IdProvincia { get; set; }

        [Required(ErrorMessage = "El nombre de la provincia es requerido.")]
        public string Nombre { get; set; }
        
        public List<Canton> Canton { get; set; }
        
        public List<Bodegas> Bodegas { get; set; }
        
        public List<Proveedores> Proveedores { get; set; }
    }
}
