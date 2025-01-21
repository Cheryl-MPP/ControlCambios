using ControlCambios.Models;
using Microsoft.Data.SqlClient;

namespace ControlCambios.SQL
{
    public class TablaUsuarioSQL
    {
        private readonly IConfiguration _configuration;
        private string connectionString;

        public TablaUsuarioSQL(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("connectionString");
        }

        public void AgregarUsuario(TablaUsuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO TablaUsuario (IdUsuario, NombreUsuario, Telefono, CorreoElectronico, Contrasenia, Rol, Autorizado)
                             VALUES (@IdUsuario, @NombreUsuario, @Telefono, @CorreoElectronico, @Contrasenia, @Rol, @Autorizado)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                command.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                command.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                command.Parameters.AddWithValue("@CorreoElectronico", usuario.CorreoElectronico);
                command.Parameters.AddWithValue("@Contrasenia", usuario.Contrasenia);
                command.Parameters.AddWithValue("@Rol", usuario.Rol);
                command.Parameters.AddWithValue("@Autorizado", usuario.Autorizado);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<TablaUsuario> ObtenerUsuarios()
        {
            List<TablaUsuario> usuarios = new List<TablaUsuario>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM TablaUsuario";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TablaUsuario usuario = new TablaUsuario();
                    usuario.IdUsuario = reader["IdUsuario"].ToString();
                    usuario.NombreUsuario = reader["NombreUsuario"].ToString();
                    usuario.Telefono = reader["Telefono"].ToString();
                    usuario.CorreoElectronico = reader["CorreoElectronico"].ToString();
                    usuario.Contrasenia = reader["Contrasenia"].ToString();
                    usuario.Rol = reader["Rol"].ToString();
                    usuario.Autorizado = Convert.ToBoolean(reader["Autorizado"]);
                    usuarios.Add(usuario);
                }
            }

            return usuarios;
        }

        public void ActualizarUsuario(TablaUsuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"UPDATE TablaUsuario 
                             SET NombreUsuario = @NombreUsuario, Telefono = @Telefono, CorreoElectronico = @CorreoElectronico, 
                                 Contrasenia = @Contrasenia, Rol = @Rol, Autorizado = @Autorizado
                             WHERE IdUsuario = @IdUsuario";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                command.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                command.Parameters.AddWithValue("@CorreoElectronico", usuario.CorreoElectronico);
                command.Parameters.AddWithValue("@Contrasenia", usuario.Contrasenia);
                command.Parameters.AddWithValue("@Rol", usuario.Rol);
                command.Parameters.AddWithValue("@Autorizado", usuario.Autorizado);
                command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void EliminarUsuario(string idUsuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM TablaUsuario WHERE IdUsuario = @IdUsuario";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdUsuario", idUsuario);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public TablaUsuario ObtenerUsuarioPorId(string idUsuario)
        {
            TablaUsuario usuario = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM TablaUsuario WHERE IdUsuario = @IdUsuario";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdUsuario", idUsuario);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    usuario = new TablaUsuario();
                    usuario.IdUsuario = reader["IdUsuario"].ToString();
                    usuario.NombreUsuario = reader["NombreUsuario"].ToString();
                    usuario.Telefono = reader["Telefono"].ToString();
                    usuario.CorreoElectronico = reader["CorreoElectronico"].ToString();
                    usuario.Contrasenia = reader["Contrasenia"].ToString();
                    usuario.Rol = reader["Rol"].ToString();
                    usuario.Autorizado = Convert.ToBoolean(reader["Autorizado"]);
                }
            }

            return usuario;
        }
    }

}
