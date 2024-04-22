using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class ProductosPedidos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProductosPedidos { get; set; }
       
        [ForeignKey("Pedido")]
        public int IdPedidos { get; set; }   
        public Pedidos Pedido { get; set; }
       
        [ForeignKey("Producto")]
        public int IdProducto { get; set; }
        public Productos Producto { get; set; }

        [Required(ErrorMessage = "La cantidad del producto es requerida.")]
        public int Cantidad { get; set; }
    }
}
