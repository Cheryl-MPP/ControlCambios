using System.ComponentModel;

namespace ControlCambios.Models
{
    public class TablaVersionesBaseDatos
    {
        [DisplayName("Base Datos")]
        public TablaBaseDatos ObjetoBaseDatos { get; set; } = new TablaBaseDatos();
        [DisplayName("Versión")]
        public string NombreVersion { get; set; }
        [DisplayName("Cambios Solicitados")]
        public string CambiosSolicitados { get; set; }

        public override string? ToString()
        {
            return NombreVersion.ToString();
        }
    }
}
