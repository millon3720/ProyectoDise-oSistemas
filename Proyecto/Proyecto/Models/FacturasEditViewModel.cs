namespace Proyecto.Models
{
    public class FacturasEditViewModel
    {
        public Facturas Factura { get; set; }
        public IEnumerable<Productos> Productos { get; set; }
        public IEnumerable<Productos> ProductosFactura { get; set; }

        public int? IdProveedor { get; set; }
        public int? IdUsuario { get; set; }
        public int[] SelectedProductIds { get; set; }

    }
}
