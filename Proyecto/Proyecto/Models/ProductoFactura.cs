using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class ProductoFactura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProductoFactura { get; set; }
        
        [ForeignKey("Facturas")]
        public int IdFactura { get; set; }
        public Facturas Factura { get; set; }
        [ForeignKey("Productos")]
        public int IdProducto { get; set; }
        public Productos Producto { get; set; }

        [Required(ErrorMessage = "La cantidad del producto en la factura es requerida.")]
        public int Cantidad { get; set; }
    }
}
