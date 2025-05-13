using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Lumel.Data
{
    public class DbConnection
    {
        private readonly string connectionString;

        public DbConnection()
        {
            connectionString = "Data Source=.;Initial Catalog=HotelReservas;Integrated Security=True";
        }

        public IDbConnection GetConnection()
        {
            IDbConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                return connection;
            }
            catch (Exception ex)
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                MessageBox.Show($"Error al conectar a la base de datos: {ex.Message}", 
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
} 