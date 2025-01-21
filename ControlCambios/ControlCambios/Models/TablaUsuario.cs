using System.ComponentModel;

namespace ControlCambios.Models
{
    public class TablaUsuario
    {
        [DisplayName("ID")]
        public string IdUsuario { get; set; }
        [DisplayName("Usuario")]
        public string NombreUsuario { get; set; }
        [DisplayName("Teléfono")]
        public string Telefono { get; set; }
        [DisplayName("Correo Electrónico")]
        public string CorreoElectronico { get; set; }
        [DisplayName("Contraseña")]
        public string Contrasenia { get; set; }
        [DisplayName("Rol")]
        public string Rol { get; set; }
        [DisplayName("Autorizado")]
        public bool Autorizado { get; set; }
    }
}
