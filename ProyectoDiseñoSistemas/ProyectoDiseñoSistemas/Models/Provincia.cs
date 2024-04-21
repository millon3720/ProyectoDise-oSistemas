﻿namespace ProyectoDiseñoSistemas.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Provincia
    {
        public int IdProvincia { get; set; }

        [Required(ErrorMessage = "El nombre de la provincia es requerido.")]
        public string Nombre { get; set; }
    }
}
