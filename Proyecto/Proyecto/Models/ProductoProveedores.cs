using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models
{
    public class ProductoProveedores
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProductoProveedores { get; set; }
        [ForeignKey("Proveedores")]
        public int IdProveedor { get; set; }
        public Proveedores Proveedores { get; set; }
        
        [ForeignKey("Productos")]
        public int IdProducto { get; set; }
        public Productos Productos { get; set; }

        [Required(ErrorMessage = "El Precio Es Requerido")]
        public decimal Precio { get; set; }
        
    }
}
