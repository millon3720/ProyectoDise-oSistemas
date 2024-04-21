namespace ProyectoDiseñoSistemas.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Pedidos
    {
        [Key]
        public int IdPedidos { get; set; }

        public int IdProveedor { get; set; }
        public Proveedor Proveedor { get; set; }

        [Required(ErrorMessage = "La fecha del pedido es requerida.")]
        public DateTime Fecha { get; set; }

        public int IdUsuario { get; set; }
        public Usuarios Usuario { get; set; }
    }

}
