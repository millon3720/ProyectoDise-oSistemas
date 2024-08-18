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

        public List<Canton> Cantones { get; set; } = new List<Canton>();
        public List<Bodegas> Bodegas { get; set; } = new List<Bodegas>();
        public List<Proveedores> Proveedores { get; set; } = new List<Proveedores>();
    }

}
