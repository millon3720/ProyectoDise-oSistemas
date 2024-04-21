namespace ProyectoDiseñoSistemas.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Proveedor
    {
        [Key]
        public int IdProveedores { get; set; }

        [Required(ErrorMessage = "La cédula del proveedor es requerida.")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El correo del proveedor es requerido.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El nombre del proveedor es requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El teléfono del proveedor es requerido.")]
        public string Telefono { get; set; }

        public int IdProvincia { get; set; }
        public Provincia Provincia { get; set; }

        public int IdCanton { get; set; }
        public Canton Canton { get; set; }

        public int IdDistrito { get; set; }
        public Distrito Distrito { get; set; }

        [Required(ErrorMessage = "La dirección exacta del proveedor es requerida.")]
        public string DireccionExacta { get; set; }
    }

}
