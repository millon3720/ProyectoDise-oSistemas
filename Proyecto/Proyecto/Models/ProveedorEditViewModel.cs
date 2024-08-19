namespace Proyecto.Models
{
    public class ProveedorEditViewModel
    {
        public Proveedores Proveedor { get; set; }
        public IEnumerable<Provincia> Provincias { get; set; }
        public IEnumerable<Canton> Cantones { get; set; }
        public IEnumerable<Distrito> Distritos { get; set; }
        public IEnumerable<Productos> Productos { get; set; }
        public IEnumerable<Productos> ProductosProveedor { get; set; }

        // Usa int? para permitir valores nulos
        public int? IdProvincia { get; set; }
        public int? IdCanton { get; set; }
        public int? IdDistrito { get; set; }
        public int[] SelectedProductIds { get; set; }
    }

}
