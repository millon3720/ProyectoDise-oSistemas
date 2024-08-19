namespace Proyecto.Models
{
    public class PedidoEditViewModel
    {
        public Pedidos Pedidos { get; set; }
        public IEnumerable<Proveedores> Proveedores { get; set; }
        public IEnumerable<Usuarios> Usuarios { get; set; }
        public IEnumerable<Productos> Productos { get; set; }
        public IEnumerable<Productos> ProductosPedidos { get; set; }

        // Usa int? para permitir valores nulos
        public int? IdProveedores { get; set; }
        public int? IdUsuario { get; set; }
        public int[] SelectedProductIds { get; set; }
    }
}
