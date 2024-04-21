namespace ProyectoDiseñoSistemas.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Canton
    {
        public int IdCanton { get; set; }

        [Required(ErrorMessage = "El nombre del cantón es requerido.")]
        public string Nombre { get; set; }

        public int IdProvincia { get; set; }
        public Provincia Provincia { get; set; }
    }
}
