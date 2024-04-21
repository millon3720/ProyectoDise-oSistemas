namespace ProyectoDiseñoSistemas.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Bodega
    {
        public int IdBodegas { get; set; }

        public int IdProvincia { get; set; }
        public Provincia Provincia { get; set; }

        public int IdCanton { get; set; }
        public Canton Canton { get; set; }

        public int IdDistrito { get; set; }
        public Distrito Distrito { get; set; }

        [Required(ErrorMessage = "La dirección exacta de la bodega es requerida.")]
        public string DireccionExacta { get; set; }
    }

}
