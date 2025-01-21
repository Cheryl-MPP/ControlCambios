using ControlCambios.Models;
using Microsoft.Data.SqlClient;

namespace ControlCambios.SQL
{
    public class TablaBaseDatosSQL
    {
        private readonly IConfiguration _configuration;
        private string connectionString;

        public TablaBaseDatosSQL(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("connectionString");
        }


        public void AgregarBaseDatos(TablaBaseDatos baseDatos)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO TablaBaseDatos (Nombre, Activa) VALUES (@Nombre, @Activa)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nombre", baseDatos.Nombre);
                command.Parameters.AddWithValue("@Activa", baseDatos.Activa);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<TablaBaseDatos> ObtenerBasesDeDatosTodas()
        {
            List<TablaBaseDatos> basesDeDatos = new List<TablaBaseDatos>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM TablaBaseDatos order by IdBaseDatos desc";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TablaBaseDatos baseDatos = new TablaBaseDatos();
                    baseDatos.IdBaseDatos = Convert.ToInt32(reader["IdBaseDatos"]);
                    baseDatos.Nombre = reader["Nombre"].ToString();
                    baseDatos.Activa = Convert.ToBoolean(reader["Activa"]);
                    basesDeDatos.Add(baseDatos);
                }
            }

            return basesDeDatos;
        }

        public List<TablaBaseDatos> ObtenerBasesDeDatosActivas()
        {
            List<TablaBaseDatos> basesDeDatos = new List<TablaBaseDatos>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM TablaBaseDatos where Activa = 1 order by IdBaseDatos desc";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TablaBaseDatos baseDatos = new TablaBaseDatos();
                    baseDatos.IdBaseDatos = Convert.ToInt32(reader["IdBaseDatos"]);
                    baseDatos.Nombre = reader["Nombre"].ToString();
                    baseDatos.Activa = Convert.ToBoolean(reader["Activa"]);
                    basesDeDatos.Add(baseDatos);
                }
            }

            return basesDeDatos;
        }

        public void ActualizarBaseDatos(TablaBaseDatos baseDatos)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE TablaBaseDatos SET Nombre = @Nombre, Activa = @Activa WHERE IdBaseDatos = @IdBaseDatos";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nombre", baseDatos.Nombre);
                command.Parameters.AddWithValue("@IdBaseDatos", baseDatos.IdBaseDatos);
                command.Parameters.AddWithValue("@Activa", baseDatos.Activa);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void EliminarBaseDatos(int idBaseDatos)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM TablaBaseDatos WHERE IdBaseDatos = @IdBaseDatos";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdBaseDatos", idBaseDatos);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public TablaBaseDatos ObtenerBaseDatosPorId(int idBaseDatos)
        {
            TablaBaseDatos baseDatos = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM TablaBaseDatos WHERE IdBaseDatos = @IdBaseDatos";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdBaseDatos", idBaseDatos);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    baseDatos = new TablaBaseDatos();
                    baseDatos.IdBaseDatos = Convert.ToInt32(reader["IdBaseDatos"]);
                    baseDatos.Nombre = reader["Nombre"].ToString();
                    baseDatos.Activa = Convert.ToBoolean(reader["Activa"]);
                }
            }

            return baseDatos;
        }
    }
}
