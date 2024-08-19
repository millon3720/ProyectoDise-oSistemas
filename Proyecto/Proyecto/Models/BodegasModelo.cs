namespace Proyecto.Models
{
    public class BodegasModelo
    {
        public Bodegas Bodegas { get; set; }
        public IEnumerable<Provincia> Provincias { get; set; }
        public IEnumerable<Canton> Cantones { get; set; }
        public IEnumerable<Distrito> Distritos { get; set; }
        public IEnumerable<Productos> Productos { get; set; }
        public IEnumerable<ProductosBodega> ProductosBodegas { get; set; }

        // Usa int? para permitir valores nulos
        public int? IdProvincia { get; set; }
        public int? IdCanton { get; set; }
        public int? IdDistrito { get; set; }
        public int[] SelectedProductIds { get; set; }
    }
}
