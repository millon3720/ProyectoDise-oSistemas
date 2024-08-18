using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class Proveedores
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProveedores { get; set; }

        [Required(ErrorMessage = "El nombre del proveedor es requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo del proveedor es requerido.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El teléfono del proveedor es requerido.")]
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }

        [Required(ErrorMessage = "La dirección exacta del proveedor es requerida.")]
        public string DireccionExacta { get; set; }

        public int IdProvincia { get; set; }
        public Provincia Provincia { get; set; }

        public int IdCanton { get; set; }
        public Canton Canton { get; set; }

        public int IdDistrito { get; set; }
        public Distrito Distrito { get; set; }

        // Lista de facturas asociadas al proveedor (relación inversa de Facturas)
        public List<Facturas> Facturas { get; set; }
    }


}
