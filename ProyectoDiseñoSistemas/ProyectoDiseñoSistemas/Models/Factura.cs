namespace ProyectoDiseñoSistemas.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Factura
    {
        [Key]
        public int IdFactura { get; set; }

        public int IdProveedor { get; set; }
        public Proveedor Proveedor { get; set; }

        public int IdUsuario { get; set; }
        public Usuarios Usuario { get; set; }

        [Required(ErrorMessage = "La fecha de la factura es requerida.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El estado de la factura es requerido.")]
        public bool Pendiente { get; set; }

        [Required(ErrorMessage = "El monto total de la factura es requerido.")]
        public decimal MontoTotal { get; set; }

        public decimal? MontoDeuda { get; set; }
    }

}
