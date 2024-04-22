using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class Pedidos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPedidos { get; set; }

        [ForeignKey("Proveedor")] // Nombre de la navegación, no de la tabla
        public int IdProveedor { get; set; }
        public Proveedores Proveedor { get; set; }

        [Required(ErrorMessage = "La fecha del pedido es requerida.")]
        public DateTime Fecha { get; set; }

        [ForeignKey("Usuario")] // Nombre de la navegación, no de la tabla
        public int IdUsuario { get; set; }
        public Usuarios Usuario { get; set; }

        public List<ProductosPedidos> ProductosPedidos { get; set; }
    }

}
