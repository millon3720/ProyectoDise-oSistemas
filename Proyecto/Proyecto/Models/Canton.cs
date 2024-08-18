using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class Canton
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCanton { get; set; }

        [Required(ErrorMessage = "El nombre del cantón es requerido.")]
        public string Nombre { get; set; }

        [ForeignKey("Provincia")]
        public int IdProvincia { get; set; }
        public Provincia Provincia { get; set; }

        public List<Distrito> Distritos { get; set; } = new List<Distrito>();
        public List<Bodegas> Bodegas { get; set; } = new List<Bodegas>();
        public List<Proveedores> Proveedores { get; set; } = new List<Proveedores>();
    }

}
