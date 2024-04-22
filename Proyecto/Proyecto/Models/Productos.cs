using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class Productos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProductos { get; set; }

        [Required(ErrorMessage = "El nombre del producto es requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción del producto es requerida.")]
        public string Descripcion { get; set; }

        public string Presentacion { get; set; }

        public int CantidadPresentacion { get; set; }

        public string TipoEmpaque { get; set; }

        public int CantidadEmpaque { get; set; }

        [Required(ErrorMessage = "El precio del producto es requerido.")]
        public decimal Precio { get; set; }

        
        public List<ProductoFactura> ProductoFactura { get; set; }
       
        public List<ProductosPedidos> ProductosPedidos { get; set; }
        
       
        public List<ProductoProveedores> ProductoProveedores { get; set; }
        
        
        public List<ProductosBodega> ProductosBodega { get; set; }

    }
}
