namespace ProyectoDiseñoSistemas.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Distrito
    {
        [Key]
        public int IdDistrito { get; set; }

        [Required(ErrorMessage = "El nombre del distrito es requerido.")]
        public string Nombre { get; set; }

        public int IdCanton { get; set; }
        public Canton Canton { get; set; }
    }

}
