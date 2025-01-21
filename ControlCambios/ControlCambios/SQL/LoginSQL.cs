using ControlCambios.Models;
using Microsoft.Data.SqlClient;
using NuGet.Protocol.Plugins;

namespace ControlCambios.SQL
{
    public class LoginSQL
    {
        private readonly IConfiguration _configuration;
        private string connectionString;

        public LoginSQL(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("connectionString");
        }

        public bool AccederRol(Login login, String Rol)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT count(*) FROM TablaUsuario WHERE NombreUsuario = @NombreUsuario AND Contrasenia = @Contrasenia AND Rol = @Rol AND Autorizado = 1";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreUsuario", login.Usuario);
                    command.Parameters.AddWithValue("@Contrasenia", login.Contrasenia);
                    command.Parameters.AddWithValue("@Rol", Rol);

                    bool resultado = ((int)command.ExecuteScalar()) == 1;

                    return resultado;
                }

            }
        }

        public string IdUsuario(Login login)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "select IdUsuario from TablaUsuario where NombreUsuario = @NombreUsuario";

                using (SqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreUsuario", login.Usuario);
                    string resultado = (string)command.ExecuteScalar();

                    return resultado;
                }

            }
        }
    }
}
