using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models
{
    public class ProductosBodega
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProductoBodega { get; set; }
        [ForeignKey("Bodegas")]
        public int IdBodega { get; set; }
        public Bodegas Bodegas { get; set; }

        [ForeignKey("Productos")]
        public int IdProducto { get; set; }
        public Productos Productos { get; set; }
        [Required(ErrorMessage = "La Fecha de ingreso es requerida.")]
        public DateTime FechaIngreso { get; set; }

        public DateTime FechaVencimiento { get; set; }

        [Required(ErrorMessage = "La cantidad del producto en la factura es requerida.")]
        public int Cantidad { get; set; }
       
    }
}
