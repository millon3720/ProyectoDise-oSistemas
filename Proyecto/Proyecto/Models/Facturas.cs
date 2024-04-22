using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class Facturas
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFactura { get; set; }
        [ForeignKey("Proveedores")]
        public int IdProveedor { get; set; }
        public Proveedores Proveedor { get; set; }
        [ForeignKey("Usuarios")]
        public int IdUsuario { get; set; }
        public Usuarios Usuario { get; set; }

        [Required(ErrorMessage = "La fecha de la factura es requerida.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El estado de la factura es requerido.")]
        public bool Pendiente { get; set; }

        [Required(ErrorMessage = "El monto total de la factura es requerido.")]
        public decimal MontoTotal { get; set; }

        public decimal? MontoDeuda { get; set; }

        
        public List<ProductoFactura> ProductoFactura { get; set; }
    }
}
