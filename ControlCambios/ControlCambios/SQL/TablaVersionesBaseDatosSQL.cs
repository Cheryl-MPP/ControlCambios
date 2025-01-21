using ControlCambios.Models;
using Microsoft.Data.SqlClient;
using System;

namespace ControlCambios.SQL
{
    public class TablaVersionesBaseDatosSQL
    {
        private readonly IConfiguration _configuration;
        private string connectionString;

        public TablaVersionesBaseDatosSQL(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("connectionString");
        }

        public void AgregarVersionBaseDatos(TablaVersionesBaseDatos version)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO TablaVersionesBaseDatos (IdBaseDatos, NombreVersion, CambiosSolicitados)
                         VALUES (@IdBaseDatos, @NombreVersion, @CambiosSolicitados)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdBaseDatos", version.ObjetoBaseDatos.IdBaseDatos);
                command.Parameters.AddWithValue("@NombreVersion", version.NombreVersion);
                command.Parameters.AddWithValue("@CambiosSolicitados", version.CambiosSolicitados);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<TablaVersionesBaseDatos> ObtenerVersionesBaseDatos()
        {
            List<TablaVersionesBaseDatos> versiones = new List<TablaVersionesBaseDatos>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT V.NombreVersion, V.CambiosSolicitados, B.IdBaseDatos, B.Nombre AS NombreBaseDatos
                             FROM TablaVersionesBaseDatos V
                             INNER JOIN TablaBaseDatos B ON V.IdBaseDatos = B.IdBaseDatos order by B.IdBaseDatos desc, V.NombreVersion Desc";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TablaVersionesBaseDatos version = new TablaVersionesBaseDatos();
                    version.NombreVersion = reader["NombreVersion"].ToString();
                    version.CambiosSolicitados = reader["CambiosSolicitados"].ToString();

                    TablaBaseDatos baseDatos = new TablaBaseDatos();
                    baseDatos.IdBaseDatos = Convert.ToInt32(reader["IdBaseDatos"]);
                    baseDatos.Nombre = reader["NombreBaseDatos"].ToString();

                    version.ObjetoBaseDatos = baseDatos;
                    versiones.Add(version);
                }
            }

            return versiones;
        }

        public List<TablaVersionesBaseDatos> ObtenerVersionesBaseDatos(int IdBaseDatos)
        {
            List<TablaVersionesBaseDatos> versiones = new List<TablaVersionesBaseDatos>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT V.NombreVersion, V.CambiosSolicitados, B.IdBaseDatos, B.Nombre AS NombreBaseDatos
                             FROM TablaVersionesBaseDatos V
                             INNER JOIN TablaBaseDatos B ON V.IdBaseDatos = B.IdBaseDatos where V.IdBaseDatos = @IdBaseDatos order by B.IdBaseDatos desc, V.NombreVersion Desc";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdBaseDatos", IdBaseDatos);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TablaVersionesBaseDatos version = new TablaVersionesBaseDatos();
                    version.NombreVersion = reader["NombreVersion"].ToString();
                    version.CambiosSolicitados = reader["CambiosSolicitados"].ToString();

                    TablaBaseDatos baseDatos = new TablaBaseDatos();
                    baseDatos.IdBaseDatos = Convert.ToInt32(reader["IdBaseDatos"]);
                    baseDatos.Nombre = reader["NombreBaseDatos"].ToString();

                    version.ObjetoBaseDatos = baseDatos;
                    versiones.Add(version);
                }
            }

            return versiones;
        }

        public void ActualizarVersionBaseDatos(TablaVersionesBaseDatos version)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"UPDATE TablaVersionesBaseDatos 
                             SET CambiosSolicitados = @CambiosSolicitados
                             WHERE IdBaseDatos = @IdBaseDatos AND NombreVersion = @NombreVersion";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdBaseDatos", version.ObjetoBaseDatos.IdBaseDatos);
                command.Parameters.AddWithValue("@NombreVersion", version.NombreVersion);
                command.Parameters.AddWithValue("@CambiosSolicitados", version.CambiosSolicitados);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void EliminarVersionBaseDatos(int IdBaseDatos, string NombreVersion)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM TablaVersionesBaseDatos WHERE IdBaseDatos = @IdBaseDatos AND NombreVersion = @NombreVersion";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdBaseDatos", IdBaseDatos);
                command.Parameters.AddWithValue("@NombreVersion", NombreVersion);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public TablaVersionesBaseDatos ObtenerVersionBaseDatosPorId(int IdBaseDatos, string NombreVersion)
        {
            TablaVersionesBaseDatos version = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT V.NombreVersion, V.CambiosSolicitados, B.IdBaseDatos, B.Nombre AS NombreBaseDatos
                             FROM TablaVersionesBaseDatos V
                             INNER JOIN TablaBaseDatos B ON V.IdBaseDatos = B.IdBaseDatos
                             WHERE V.IdBaseDatos = @IdBaseDatos AND V.NombreVersion = @NombreVersion";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdBaseDatos", IdBaseDatos);
                command.Parameters.AddWithValue("@NombreVersion", NombreVersion);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    version = new TablaVersionesBaseDatos();
                    version.NombreVersion = reader["NombreVersion"].ToString();
                    version.CambiosSolicitados = reader["CambiosSolicitados"].ToString();

                    TablaBaseDatos baseDatos = new TablaBaseDatos();
                    baseDatos.IdBaseDatos = Convert.ToInt32(reader["IdBaseDatos"]);
                    baseDatos.Nombre = reader["NombreBaseDatos"].ToString();

                    version.ObjetoBaseDatos = baseDatos;
                }
            }

            return version;
        }
    }
}