namespace ProyectoDiseñoSistemas.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;
    public class ProductoFactura
    {
        public int IdProductoFactura { get; set; }

        public int IdFactura { get; set; }
        public Factura Factura { get; set; }

        public int IdProducto { get; set; }
        public Productos Producto { get; set; }

        [Required(ErrorMessage = "La cantidad del producto en la factura es requerida.")]
        public int Cantidad { get; set; }
    }

}
