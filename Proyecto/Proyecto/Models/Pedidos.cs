using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class Pedidos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPedidos { get; set; }

        [ForeignKey("Proveedores")] // Nombre de la navegación, no de la tabla
        public int IdProveedores { get; set; }
        public Proveedores Proveedores { get; set; }

        [Required(ErrorMessage = "La fecha del pedido es requerida.")]
        public DateTime Fecha { get; set; }

        [ForeignKey("Usuario")] // Nombre de la navegación, no de la tabla
        public int IdUsuario { get; set; }
        public Usuarios Usuario { get; set; }

        public List<ProductosPedidos> ProductosPedidos { get; set; }
    }

}
