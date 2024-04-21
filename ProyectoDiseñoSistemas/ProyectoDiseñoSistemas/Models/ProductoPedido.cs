namespace ProyectoDiseñoSistemas.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ProductoPedido
    {
        [Key]
        public int IdProductosPedidos { get; set; }

        public int IdPedidos { get; set; }
        public Pedidos Pedido { get; set; }

        public int IdProducto { get; set; }
        public Productos Producto { get; set; }

        [Required(ErrorMessage = "La cantidad del producto es requerida.")]
        public int Cantidad { get; set; }
    }

}
