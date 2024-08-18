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

        [Required(ErrorMessage = "El correo del usuario es requerido.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El usuario del usuario es requerido.")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "la Contraseña del usuario es requerida.")]
        public string Clave { get; set; }

        [Required(ErrorMessage = "El puesto del usuario es requerido.")]
        public string Puesto { get; set; }

        public int IdBodega { get; set; }
        public Bodegas Bodegas { get; set; }
        public List<Facturas> Facturas { get; set; }
    }

}
