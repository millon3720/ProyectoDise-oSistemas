using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class ProductoFactura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProductoFactura { get; set; }

        public int IdFactura { get; set; }
        public Facturas Factura { get; set; }

        public int IdProducto { get; set; }
        public Productos Producto { get; set; }

        [Required(ErrorMessage = "La cantidad del producto en la factura es requerida.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio del producto en la factura es requerida.")]
        public decimal Total { get; set; }
    }


}
