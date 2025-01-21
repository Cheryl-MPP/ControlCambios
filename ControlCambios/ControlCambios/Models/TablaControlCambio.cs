using System.ComponentModel;

namespace ControlCambios.Models
{
    public class TablaControlCambio
    {
        [DisplayName("ID")]
        public int IdControlcambio { get; set; }
        [DisplayName("Cambio")]
        public string NombreCambio { get; set; }
        [DisplayName("Categoría")]
        public string Categoria { get; set; }

        [DisplayName("Base Datos")]
        public TablaVersionesBaseDatos ObjetoVersionesBaseDatos { get; set; } = new TablaVersionesBaseDatos();

        [DisplayName("Cambios Realizados")]
        public string CambiosRealizados { get; set; }
        [DisplayName("Usuario")]
        public string IdUsuario { get; set; }
        [DisplayName("Fecha")]
        public DateTime Fecha { get; set; }

    }
}
