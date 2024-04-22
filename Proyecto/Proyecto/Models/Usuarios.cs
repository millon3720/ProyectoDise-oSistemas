using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class Usuarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "La cédula del usuario es requerida.")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El nombre del usuario es requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Los apellidos del usuario son requeridos.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El teléfono del usuario es requerido.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El puesto del usuario es requerido.")]
        public string Puesto { get; set; }
        [ForeignKey("Bodegas")]
        public int IdBodegas { get; set; }
        public Bodegas Bodegas { get; set; }

        
        public List<Facturas> Facturas { get; set; }
    }
}
