using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_Lumel.Interfaces;
using Proyecto_Lumel.Models;

namespace Proyecto_Lumel.Data
{
    public class HabitacionRepository : IHabitacionRepository
    {
        private readonly DbConnection dbConnection;

        public HabitacionRepository()
        {
            dbConnection = new DbConnection();
        }

        public void Add(Habitacion habitacion)
        {
            try
            {
                using (var connection = dbConnection.GetConnection() as SqlConnection)
                {
                    if (connection == null)
                    {
                        throw new Exception("No se pudo establecer conexión con la base de datos.");
                    }

                    using (var command = new SqlCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        
                        // Verificar si ya existe una habitación con el mismo número
                        command.CommandText = "SELECT COUNT(*) FROM Habitacion WHERE numero = @numero";
                        command.Parameters.Add("@numero", SqlDbType.VarChar).Value = habitacion.Numero;
                        
                        int count = (int)command.ExecuteScalar();
                        if (count > 0)
                        {
                            throw new Exception($"Ya existe una habitación con el número {habitacion.Numero}.");
                        }
                        
                        // Limpiar parámetros para la inserción
                        command.Parameters.Clear();
                        
                        // Insertar la nueva habitación
                        command.CommandText = "INSERT INTO Habitacion (numero, tipo, descripcion, capacidad, precio_noche, estado) " +
                                             "VALUES (@numero, @tipo, @descripcion, @capacidad, @precio_noche, @estado)";

                        command.Parameters.Add("@numero", SqlDbType.VarChar).Value = habitacion.Numero;
                        command.Parameters.Add("@tipo", SqlDbType.NVarChar).Value = habitacion.Tipo;
                        command.Parameters.Add("@descripcion", SqlDbType.NVarChar).Value = habitacion.Descripcion ?? (object)DBNull.Value;
                        command.Parameters.Add("@capacidad", SqlDbType.Int).Value = habitacion.Capacidad;
                        command.Parameters.Add("@precio_noche", SqlDbType.Decimal).Value = habitacion.PrecioNoche;
                        command.Parameters.Add("@estado", SqlDbType.NVarChar).Value = habitacion.Estado;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                // Manejar errores específicos de SQL Server
                if (ex.Number == 2627 || ex.Number == 2601) // Violación de clave única o índice
                {
                    throw new Exception($"Ya existe una habitación con el número {habitacion.Numero}.");
                }
                else
                {
                    throw new Exception("Error en la base de datos: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la habitación: " + ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (var connection = dbConnection.GetConnection() as SqlConnection)
                {
                    if (connection == null)
                    {
                        throw new Exception("No se pudo establecer conexión con la base de datos.");
                    }

                    using (var command = new SqlCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = "DELETE FROM Habitacion WHERE id_habitacion = @id";
                        command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos al eliminar la habitación: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la habitación: " + ex.Message);
            }
        }

        public void Edit(Habitacion habitacion)
        {
            try
            {
                using (var connection = dbConnection.GetConnection() as SqlConnection)
                {
                    if (connection == null)
                    {
                        throw new Exception("No se pudo establecer conexión con la base de datos.");
                    }

                    using (var command = new SqlCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        
                        // Verificar si ya existe otra habitación con el mismo número (excepto la actual)
                        command.CommandText = "SELECT COUNT(*) FROM Habitacion WHERE numero = @numero AND id_habitacion <> @id";
                        command.Parameters.Add("@numero", SqlDbType.VarChar).Value = habitacion.Numero;
                        command.Parameters.Add("@id", SqlDbType.Int).Value = habitacion.IdHabitacion;
                        
                        int count = (int)command.ExecuteScalar();
                        if (count > 0)
                        {
                            throw new Exception($"Ya existe otra habitación con el número {habitacion.Numero}.");
                        }
                        
                        // Limpiar parámetros para la actualización
                        command.Parameters.Clear();
                        
                        // Actualizar la habitación
                        command.CommandText = "UPDATE Habitacion SET numero = @numero, tipo = @tipo, " +
                                             "descripcion = @descripcion, capacidad = @capacidad, " +
                                             "precio_noche = @precio_noche, estado = @estado " +
                                             "WHERE id_habitacion = @id";

                        command.Parameters.Add("@numero", SqlDbType.VarChar).Value = habitacion.Numero;
                        command.Parameters.Add("@tipo", SqlDbType.NVarChar).Value = habitacion.Tipo;
                        command.Parameters.Add("@descripcion", SqlDbType.NVarChar).Value = habitacion.Descripcion ?? (object)DBNull.Value;
                        command.Parameters.Add("@capacidad", SqlDbType.Int).Value = habitacion.Capacidad;
                        command.Parameters.Add("@precio_noche", SqlDbType.Decimal).Value = habitacion.PrecioNoche;
                        command.Parameters.Add("@estado", SqlDbType.NVarChar).Value = habitacion.Estado;
                        command.Parameters.Add("@id", SqlDbType.Int).Value = habitacion.IdHabitacion;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                // Manejar errores específicos de SQL Server
                if (ex.Number == 2627 || ex.Number == 2601) // Violación de clave única o índice
                {
                    throw new Exception($"Ya existe otra habitación con el número {habitacion.Numero}.");
                }
                else
                {
                    throw new Exception("Error en la base de datos: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la habitación: " + ex.Message);
            }
        }

        public IEnumerable<Habitacion> GetAll()
        {
            var habitacionList = new List<Habitacion>();
            try
            {
                using (var connection = dbConnection.GetConnection() as SqlConnection)
                {
                    if (connection == null)
                    {
                        return habitacionList;
                    }

                    using (var command = new SqlCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = "SELECT id_habitacion, numero, tipo, descripcion, " +
                                             "capacidad, precio_noche, estado FROM Habitacion " +
                                             "ORDER BY numero";

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var habitacion = new Habitacion
                                {
                                    IdHabitacion = Convert.ToInt32(reader["id_habitacion"]),
                                    Numero = reader["numero"].ToString(),
                                    Tipo = reader["tipo"].ToString(),
                                    Descripcion = reader["descripcion"] == DBNull.Value ? null : reader["descripcion"].ToString(),
                                    Capacidad = Convert.ToInt32(reader["capacidad"]),
                                    PrecioNoche = Convert.ToDecimal(reader["precio_noche"]),
                                    Estado = reader["estado"].ToString()
                                };
                                habitacionList.Add(habitacion);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las habitaciones: {ex.Message}");
            }
            return habitacionList;
        }

        public Habitacion GetById(int id)
        {
            Habitacion habitacion = null;
            try
            {
                using (var connection = dbConnection.GetConnection() as SqlConnection)
                {
                    if (connection == null)
                    {
                        return null;
                    }

                    using (var command = new SqlCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = "SELECT id_habitacion, numero, tipo, descripcion, " +
                                             "capacidad, precio_noche, estado FROM Habitacion WHERE id_habitacion = @id";

                        command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                habitacion = new Habitacion
                                {
                                    IdHabitacion = Convert.ToInt32(reader["id_habitacion"]),
                                    Numero = reader["numero"].ToString(),
                                    Tipo = reader["tipo"].ToString(),
                                    Descripcion = reader["descripcion"] == DBNull.Value ? null : reader["descripcion"].ToString(),
                                    Capacidad = Convert.ToInt32(reader["capacidad"]),
                                    PrecioNoche = Convert.ToDecimal(reader["precio_noche"]),
                                    Estado = reader["estado"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la habitación por ID: {ex.Message}");
            }
            return habitacion;
        }

        public IEnumerable<Habitacion> GetByValue(string value)
        {
            var habitacionList = new List<Habitacion>();
            try
            {
                using (var connection = dbConnection.GetConnection() as SqlConnection)
                {
                    if (connection == null)
                    {
                        return habitacionList;
                    }

                    using (var command = new SqlCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = "SELECT id_habitacion, numero, tipo, descripcion, " +
                                             "capacidad, precio_noche, estado FROM Habitacion " +
                                             "WHERE numero LIKE @filter OR tipo LIKE @filter OR " +
                                             "descripcion LIKE @filter OR estado LIKE @filter " +
                                             "ORDER BY numero";

                        command.Parameters.Add("@filter", SqlDbType.NVarChar).Value = "%" + value + "%";

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var habitacion = new Habitacion
                                {
                                    IdHabitacion = Convert.ToInt32(reader["id_habitacion"]),
                                    Numero = reader["numero"].ToString(),
                                    Tipo = reader["tipo"].ToString(),
                                    Descripcion = reader["descripcion"] == DBNull.Value ? null : reader["descripcion"].ToString(),
                                    Capacidad = Convert.ToInt32(reader["capacidad"]),
                                    PrecioNoche = Convert.ToDecimal(reader["precio_noche"]),
                                    Estado = reader["estado"].ToString()
                                };
                                habitacionList.Add(habitacion);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar habitaciones: {ex.Message}");
            }
            return habitacionList;
        }
    }
}
