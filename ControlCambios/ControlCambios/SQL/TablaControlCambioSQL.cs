using ControlCambios.Models;
using Microsoft.Data.SqlClient;

namespace ControlCambios.SQL
{
    public class TablaControlCambioSQL
    {
        private readonly IConfiguration _configuration;
        private string connectionString;

        public TablaControlCambioSQL(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("connectionString");
        }

        public void AgregarControlCambio(TablaControlCambio controlCambio)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO TablaControlCambio (NombreCambio, Categoria, IdBaseDatos, NombreVersion, IdUsuario, Fecha, CambiosRealizados)
                             VALUES (@NombreCambio, @Categoria, @IdBaseDatos, @NombreVersion, @IdUsuario, @Fecha, @CambiosRealizados)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NombreCambio", controlCambio.NombreCambio);
                command.Parameters.AddWithValue("@Categoria", controlCambio.Categoria);
                command.Parameters.AddWithValue("@IdBaseDatos", controlCambio.ObjetoVersionesBaseDatos.ObjetoBaseDatos.IdBaseDatos);
                command.Parameters.AddWithValue("@NombreVersion", controlCambio.ObjetoVersionesBaseDatos.NombreVersion);
                command.Parameters.AddWithValue("@IdUsuario", controlCambio.IdUsuario);
                command.Parameters.AddWithValue("@Fecha", controlCambio.Fecha);
                command.Parameters.AddWithValue("@CambiosRealizados", controlCambio.CambiosRealizados);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<TablaControlCambio> ObtenerControlesCambio()
        {
            List<TablaControlCambio> controlesCambio = new List<TablaControlCambio>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT C.IdControlcambio, C.NombreCambio, C.Categoria, C.IdUsuario, C.Fecha, C.CambiosRealizados,
                             V.NombreVersion, V.CambiosSolicitados,
                             B.IdBaseDatos, B.Nombre AS NombreBaseDatos
                             FROM TablaControlCambio C
                             INNER JOIN TablaVersionesBaseDatos V ON C.NombreVersion = V.NombreVersion AND C.IdBaseDatos = V.IdBaseDatos
                             INNER JOIN TablaBaseDatos B ON V.IdBaseDatos = B.IdBaseDatos order by IdControlCambio desc";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TablaControlCambio controlCambio = new TablaControlCambio();
                    controlCambio.IdControlcambio = Convert.ToInt32(reader["IdControlcambio"]);
                    controlCambio.NombreCambio = reader["NombreCambio"].ToString();
                    controlCambio.Categoria = reader["Categoria"].ToString();
                    controlCambio.IdUsuario = reader["IdUsuario"].ToString();
                    controlCambio.Fecha = Convert.ToDateTime(reader["Fecha"]);
                    controlCambio.CambiosRealizados = reader["CambiosRealizados"].ToString();

                    TablaVersionesBaseDatos version = new TablaVersionesBaseDatos();
                    version.NombreVersion = reader["NombreVersion"].ToString();
                    version.CambiosSolicitados = reader["CambiosSolicitados"].ToString();

                    TablaBaseDatos baseDatos = new TablaBaseDatos();
                    baseDatos.IdBaseDatos = Convert.ToInt32(reader["IdBaseDatos"]);
                    baseDatos.Nombre = reader["NombreBaseDatos"].ToString();

                    version.ObjetoBaseDatos = baseDatos;
                    controlCambio.ObjetoVersionesBaseDatos = version;

                    controlesCambio.Add(controlCambio);
                }
            }

            return controlesCambio;
        }

        public void ActualizarControlCambio(TablaControlCambio controlCambio)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"UPDATE TablaControlCambio 
                             SET NombreCambio = @NombreCambio, Categoria = @Categoria, IdUsuario = @IdUsuario,
                             Fecha = @Fecha, CambiosRealizados = @CambiosRealizados
                             WHERE IdControlcambio = @IdControlcambio";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NombreCambio", controlCambio.NombreCambio);
                command.Parameters.AddWithValue("@Categoria", controlCambio.Categoria);
                command.Parameters.AddWithValue("@Fecha", controlCambio.Fecha);
                command.Parameters.AddWithValue("@CambiosRealizados", controlCambio.CambiosRealizados);
                command.Parameters.AddWithValue("@IdControlcambio", controlCambio.IdControlcambio);
                command.Parameters.AddWithValue("@IdUsuario", controlCambio.IdUsuario);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void EliminarControlCambio(int idControlcambio)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM TablaControlCambio WHERE IdControlcambio = @IdControlcambio";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdControlcambio", idControlcambio);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public TablaControlCambio ObtenerControlCambioPorId(int idControlcambio)
        {
            TablaControlCambio controlCambio = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT C.IdControlcambio, C.NombreCambio, C.Categoria, C.IdUsuario, C.Fecha, C.CambiosRealizados,
                             V.NombreVersion, V.CambiosSolicitados,
                             B.IdBaseDatos, B.Nombre AS NombreBaseDatos
                             FROM TablaControlCambio C
                             INNER JOIN TablaVersionesBaseDatos V ON C.NombreVersion = V.NombreVersion AND C.IdBaseDatos = V.IdBaseDatos
                             INNER JOIN TablaBaseDatos B ON V.IdBaseDatos = B.IdBaseDatos
                             WHERE C.IdControlcambio = @IdControlcambio";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdControlcambio", idControlcambio);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    controlCambio = new TablaControlCambio();
                    controlCambio.IdControlcambio = Convert.ToInt32(reader["IdControlcambio"]);
                    controlCambio.NombreCambio = reader["NombreCambio"].ToString();
                    controlCambio.Categoria = reader["Categoria"].ToString();
                    controlCambio.IdUsuario = reader["IdUsuario"].ToString();
                    controlCambio.Fecha = Convert.ToDateTime(reader["Fecha"]);
                    controlCambio.CambiosRealizados = reader["CambiosRealizados"].ToString();

                    TablaVersionesBaseDatos version = new TablaVersionesBaseDatos();
                    version.NombreVersion = reader["NombreVersion"].ToString();
                    version.CambiosSolicitados = reader["CambiosSolicitados"].ToString();

                    TablaBaseDatos baseDatos = new TablaBaseDatos();
                    baseDatos.IdBaseDatos = Convert.ToInt32(reader["IdBaseDatos"]);
                    baseDatos.Nombre = reader["NombreBaseDatos"].ToString();

                    version.ObjetoBaseDatos = baseDatos;
                    controlCambio.ObjetoVersionesBaseDatos = version;
                }
            }

            return controlCambio;
        }
    }
}

