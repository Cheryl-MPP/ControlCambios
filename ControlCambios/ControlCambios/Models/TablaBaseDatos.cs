using System.ComponentModel;

namespace ControlCambios.Models
{
    public class TablaBaseDatos
    {
        [DisplayName("ID")]
        public int IdBaseDatos { get; set; }
        [DisplayName("Base de Datos")]
        public string Nombre { get; set; }
        [DisplayName("Activa")]
        public bool Activa { get; set; }

        public override string? ToString()
        {
            return IdBaseDatos + " " + Nombre;
        }
    }
}
