using System.ComponentModel;

namespace ControlCambios.Models
{
    public class Login
    {
        [DisplayName("Usuario")]
        public string Usuario { get; set; }
        [DisplayName("Clave")]
        public string Contrasenia { get; set; }
        [DisplayName("Rol")]
        public string? Acceso { get; set; }
    }
}
