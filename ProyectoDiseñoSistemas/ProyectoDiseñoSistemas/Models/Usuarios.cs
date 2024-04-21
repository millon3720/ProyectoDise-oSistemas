namespace ProyectoDiseñoSistemas.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Usuarios
    {
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

        public int IdBodega { get; set; }
        public Bodega Bodega { get; set; }
    }

}
