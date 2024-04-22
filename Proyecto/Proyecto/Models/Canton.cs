using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class Canton
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCanton { get; set; }

        [Required(ErrorMessage = "El nombre del cantón es requerido.")]
        public string Nombre { get; set; }
        [ForeignKey("Provincia")]
        public int IdProvincia { get; set; }
        public Provincia Provincia { get; set; }

       
        public List<Distrito> Distrito { get; set; }
        
        public List<Bodegas> Bodegas { get; set; }
       
        public List<Proveedores> Proveedores { get; set; }
    }
}
