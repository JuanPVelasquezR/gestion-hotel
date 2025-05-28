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
    public class ReservaRepository : IReservaRepository
    {
        private readonly DbConnection dbConnection;

        public ReservaRepository()
        {
            dbConnection = new DbConnection();
        }

        public void Add(Reserva reserva)
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
                        
                        // Verificar disponibilidad de la habitación para las fechas seleccionadas
                        // Mejorar la consulta para evitar falsos positivos
                        // Usar los nombres correctos de las columnas en la base de datos
                        command.CommandText = @"
                            SELECT COUNT(*) FROM Reserva 
                            WHERE IdHabitacion = @idHabitacion 
                            AND Estado NOT IN ('Cancelada') 
                            AND (
                                (FechaEntrada <= @fechaSalida AND FechaSalida >= @fechaEntrada)
                            )";
                        
                        command.Parameters.Add("@idHabitacion", SqlDbType.Int).Value = reserva.IdHabitacion;
                        command.Parameters.Add("@fechaEntrada", SqlDbType.DateTime).Value = reserva.FechaEntrada;
                        command.Parameters.Add("@fechaSalida", SqlDbType.DateTime).Value = reserva.FechaSalida;
                        
                        int reservasExistentes = (int)command.ExecuteScalar();
                        if (reservasExistentes > 0)
                        {
                            throw new Exception("La habitación no está disponible para las fechas seleccionadas.");
                        }
                        
                        // Limpiar parámetros para la inserción
                        command.Parameters.Clear();
                        
                        // Insertar la nueva reserva
                        // Usar los nombres correctos de las columnas en la base de datos
                        command.CommandText = @"
                            INSERT INTO Reserva (IdHuesped, IdHabitacion, FechaEntrada, FechaSalida, 
                            PrecioTotal, Estado, Observaciones, FechaCreacion) 
                            VALUES (@idHuesped, @idHabitacion, @fechaEntrada, @fechaSalida, 
                            @precioTotal, @estado, @observaciones, @fechaCreacion);
                            SELECT SCOPE_IDENTITY();";

                        command.Parameters.Add("@idHuesped", SqlDbType.Int).Value = reserva.IdHuesped;
                        command.Parameters.Add("@idHabitacion", SqlDbType.Int).Value = reserva.IdHabitacion;
                        command.Parameters.Add("@fechaEntrada", SqlDbType.DateTime).Value = reserva.FechaEntrada;
                        command.Parameters.Add("@fechaSalida", SqlDbType.DateTime).Value = reserva.FechaSalida;
                        command.Parameters.Add("@precioTotal", SqlDbType.Decimal).Value = reserva.PrecioTotal;
                        command.Parameters.Add("@estado", SqlDbType.NVarChar).Value = reserva.Estado;
                        command.Parameters.Add("@observaciones", SqlDbType.NVarChar).Value = 
                            reserva.Observaciones ?? (object)DBNull.Value;
                        command.Parameters.Add("@fechaCreacion", SqlDbType.DateTime).Value = DateTime.Now;

                        // Obtener el ID de la reserva insertada
                        reserva.IdReserva = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Manejar errores específicos de SQL Server
                string errorMessage;
                
                switch (sqlEx.Number)
                {
                    case 2627:  // Violación de clave única
                        errorMessage = "Ya existe una reserva con estos datos.";
                        break;
                    case 547:   // Restricción de clave foránea
                        errorMessage = "La habitación o el huésped seleccionado no existe en la base de datos.";
                        break;
                    case 8152:  // Error de truncamiento de cadena
                        errorMessage = "Uno de los campos contiene demasiados caracteres.";
                        break;
                    case 4060:  // Base de datos inaccesible
                    case 4064:
                    case 18456: // Error de login
                        errorMessage = "No se pudo conectar a la base de datos. Verifique la conexión.";
                        break;
                    default:
                        errorMessage = "Error en la base de datos: " + sqlEx.Message;
                        break;
                }
                
                throw new Exception(errorMessage);
            }
            catch (Exception ex)
            {
                // Para otros tipos de excepciones
                if (ex.Message.Contains("no está disponible"))
                {
                    throw; // Reenviar la excepción de disponibilidad sin modificar
                }
                else
                {
                    throw new Exception("Error al agregar la reserva: " + ex.Message);
                }
            }
        }

        public void Edit(Reserva reserva)
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
                        
                        // Verificar disponibilidad de la habitación para las fechas seleccionadas (excluyendo la reserva actual)
                        command.CommandText = @"
                            SELECT COUNT(*) FROM Reserva 
                            WHERE id_habitacion = @idHabitacion 
                            AND id_reserva != @idReserva
                            AND estado NOT IN ('Cancelada') 
                            AND ((fecha_entrada BETWEEN @fechaEntrada AND @fechaSalida) 
                            OR (fecha_salida BETWEEN @fechaEntrada AND @fechaSalida)
                            OR (@fechaEntrada BETWEEN fecha_entrada AND fecha_salida))";
                        
                        command.Parameters.Add("@idReserva", SqlDbType.Int).Value = reserva.IdReserva;
                        command.Parameters.Add("@idHabitacion", SqlDbType.Int).Value = reserva.IdHabitacion;
                        command.Parameters.Add("@fechaEntrada", SqlDbType.DateTime).Value = reserva.FechaEntrada;
                        command.Parameters.Add("@fechaSalida", SqlDbType.DateTime).Value = reserva.FechaSalida;
                        
                        int reservasExistentes = (int)command.ExecuteScalar();
                        if (reservasExistentes > 0)
                        {
                            throw new Exception("La habitación no está disponible para las fechas seleccionadas.");
                        }
                        
                        // Limpiar parámetros para la actualización
                        command.Parameters.Clear();
                        
                        // Actualizar la reserva
                        command.CommandText = @"
                            UPDATE Reserva SET 
                            id_huesped = @idHuesped, 
                            id_habitacion = @idHabitacion, 
                            fecha_entrada = @fechaEntrada, 
                            fecha_salida = @fechaSalida, 
                            precio_total = @precioTotal, 
                            estado = @estado, 
                            observaciones = @observaciones
                            WHERE id_reserva = @idReserva";

                        command.Parameters.Add("@idReserva", SqlDbType.Int).Value = reserva.IdReserva;
                        command.Parameters.Add("@idHuesped", SqlDbType.Int).Value = reserva.IdHuesped;
                        command.Parameters.Add("@idHabitacion", SqlDbType.Int).Value = reserva.IdHabitacion;
                        command.Parameters.Add("@fechaEntrada", SqlDbType.DateTime).Value = reserva.FechaEntrada;
                        command.Parameters.Add("@fechaSalida", SqlDbType.DateTime).Value = reserva.FechaSalida;
                        command.Parameters.Add("@precioTotal", SqlDbType.Decimal).Value = reserva.PrecioTotal;
                        command.Parameters.Add("@estado", SqlDbType.NVarChar).Value = reserva.Estado;
                        command.Parameters.Add("@observaciones", SqlDbType.NVarChar).Value = 
                            reserva.Observaciones ?? (object)DBNull.Value;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar la reserva: " + ex.Message);
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
                        command.CommandText = "DELETE FROM Reserva WHERE id_reserva = @id";
                        command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la reserva: " + ex.Message);
            }
        }

        public IEnumerable<Reserva> GetAll()
        {
            var reservaList = new List<Reserva>();
            try
            {
                using (var connection = dbConnection.GetConnection() as SqlConnection)
                {
                    if (connection == null)
                    {
                        return reservaList;
                    }

                    using (var command = new SqlCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = @"
                            SELECT r.IdReserva, r.IdHuesped, r.IdHabitacion, r.FechaEntrada, 
                            r.FechaSalida, r.PrecioTotal, r.Estado, r.Observaciones, r.FechaCreacion,
                            h.Nombre + ' ' + h.Apellido AS nombre_huesped,
                            hab.Numero AS numero_habitacion,
                            hab.Tipo AS tipo_habitacion,
                            hab.PrecioNoche AS precio_noche
                            FROM Reserva r
                            INNER JOIN Huesped h ON r.IdHuesped = h.IdHuesped
                            INNER JOIN Habitacion hab ON r.IdHabitacion = hab.IdHabitacion
                            ORDER BY r.FechaEntrada DESC";

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var reserva = MapReservaFromReader(reader);
                                reservaList.Add(reserva);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las reservas: {ex.Message}");
            }
            return reservaList;
        }

        public Reserva GetById(int id)
        {
            Reserva reserva = null;
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
                        command.CommandText = @"
                            SELECT r.IdReserva, r.IdHuesped, r.IdHabitacion, r.FechaEntrada, 
                            r.FechaSalida, r.PrecioTotal, r.Estado, r.Observaciones, r.FechaCreacion,
                            h.Nombre + ' ' + h.Apellido AS nombre_huesped,
                            hab.Numero AS numero_habitacion,
                            hab.Tipo AS tipo_habitacion,
                            hab.PrecioNoche AS precio_noche
                            FROM Reserva r
                            INNER JOIN Huesped h ON r.IdHuesped = h.IdHuesped
                            INNER JOIN Habitacion hab ON r.IdHabitacion = hab.IdHabitacion
                            WHERE r.IdReserva = @idReserva";

                        command.Parameters.Add("@idReserva", SqlDbType.Int).Value = id;

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                reserva = MapReservaFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la reserva por ID: {ex.Message}");
            }
            return reserva;
        }

        public IEnumerable<Reserva> GetByValue(string value)
        {
            var reservaList = new List<Reserva>();
            try
            {
                using (var connection = dbConnection.GetConnection() as SqlConnection)
                {
                    if (connection == null)
                    {
                        return reservaList;
                    }

                    using (var command = new SqlCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = @"
                            SELECT r.IdReserva, r.IdHuesped, r.IdHabitacion, r.FechaEntrada, 
                            r.FechaSalida, r.PrecioTotal, r.Estado, r.Observaciones, r.FechaCreacion,
                            h.Nombre + ' ' + h.Apellido AS nombre_huesped,
                            hab.Numero AS numero_habitacion,
                            hab.Tipo AS tipo_habitacion,
                            hab.PrecioNoche AS precio_noche
                            FROM Reserva r
                            INNER JOIN Huesped h ON r.IdHuesped = h.IdHuesped
                            INNER JOIN Habitacion hab ON r.IdHabitacion = hab.IdHabitacion
                            WHERE h.Nombre + ' ' + h.Apellido LIKE @searchValue OR
                            hab.Numero LIKE @searchValue OR
                            r.Estado LIKE @searchValue
                            ORDER BY r.FechaEntrada DESC";

                        command.Parameters.Add("@searchValue", SqlDbType.NVarChar).Value = "%" + value + "%";

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var reserva = MapReservaFromReader(reader);
                                reservaList.Add(reserva);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar reservas: {ex.Message}");
            }
            return reservaList;
        }

        public IEnumerable<Reserva> GetByFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            var reservaList = new List<Reserva>();
            try
            {
                using (var connection = dbConnection.GetConnection() as SqlConnection)
                {
                    if (connection == null)
                    {
                        return reservaList;
                    }

                    using (var command = new SqlCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = @"
                            SELECT r.IdReserva, r.IdHuesped, r.IdHabitacion, r.FechaEntrada, 
                            r.FechaSalida, r.PrecioTotal, r.Estado, r.Observaciones, r.FechaCreacion,
                            h.Nombre + ' ' + h.Apellido AS nombre_huesped,
                            hab.Numero AS numero_habitacion,
                            hab.Tipo AS tipo_habitacion,
                            hab.PrecioNoche AS precio_noche
                            FROM Reserva r
                            INNER JOIN Huesped h ON r.IdHuesped = h.IdHuesped
                            INNER JOIN Habitacion hab ON r.IdHabitacion = hab.IdHabitacion
                            WHERE r.FechaEntrada >= @fechaInicio AND r.FechaEntrada <= @fechaFin
                            ORDER BY r.FechaEntrada DESC";

                        command.Parameters.Add("@fechaInicio", SqlDbType.DateTime).Value = fechaInicio;
                        command.Parameters.Add("@fechaFin", SqlDbType.DateTime).Value = fechaFin;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var reserva = MapReservaFromReader(reader);
                                reservaList.Add(reserva);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar reservas por fechas: {ex.Message}");
            }
            return reservaList;
        }

        public IEnumerable<Reserva> GetByHuesped(int idHuesped)
        {
            var reservaList = new List<Reserva>();
            try
            {
                using (var connection = dbConnection.GetConnection() as SqlConnection)
                {
                    if (connection == null)
                    {
                        return reservaList;
                    }

                    using (var command = new SqlCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = @"
                            SELECT r.IdReserva, r.IdHuesped, r.IdHabitacion, r.FechaEntrada, 
                            r.FechaSalida, r.PrecioTotal, r.Estado, r.Observaciones, r.FechaCreacion,
                            h.Nombre + ' ' + h.Apellido AS nombre_huesped,
                            hab.Numero AS numero_habitacion,
                            hab.Tipo AS tipo_habitacion,
                            hab.PrecioNoche AS precio_noche
                            FROM Reserva r
                            INNER JOIN Huesped h ON r.IdHuesped = h.IdHuesped
                            INNER JOIN Habitacion hab ON r.IdHabitacion = hab.IdHabitacion
                            WHERE r.IdHuesped = @idHuesped
                            ORDER BY r.FechaEntrada DESC";

                        command.Parameters.Add("@idHuesped", SqlDbType.Int).Value = idHuesped;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var reserva = MapReservaFromReader(reader);
                                reservaList.Add(reserva);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar reservas por huésped: {ex.Message}");
            }
            return reservaList;
        }

        public IEnumerable<Reserva> GetByHabitacion(int idHabitacion)
        {
            var reservaList = new List<Reserva>();
            try
            {
                using (var connection = dbConnection.GetConnection() as SqlConnection)
                {
                    if (connection == null)
                    {
                        return reservaList;
                    }

                    using (var command = new SqlCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = @"
                            SELECT r.IdReserva, r.IdHuesped, r.IdHabitacion, r.FechaEntrada, 
                            r.FechaSalida, r.PrecioTotal, r.Estado, r.Observaciones, r.FechaCreacion,
                            h.Nombre + ' ' + h.Apellido AS nombre_huesped,
                            hab.Numero AS numero_habitacion,
                            hab.Tipo AS tipo_habitacion,
                            hab.PrecioNoche AS precio_noche
                            FROM Reserva r
                            INNER JOIN Huesped h ON r.IdHuesped = h.IdHuesped
                            INNER JOIN Habitacion hab ON r.IdHabitacion = hab.IdHabitacion
                            WHERE r.IdHabitacion = @idHabitacion
                            ORDER BY r.FechaEntrada DESC";

                        command.Parameters.Add("@idHabitacion", SqlDbType.Int).Value = idHabitacion;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var reserva = MapReservaFromReader(reader);
                                reservaList.Add(reserva);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar reservas por habitación: {ex.Message}");
            }
            return reservaList;
        }

        public IEnumerable<Reserva> GetByEstado(string estado)
        {
            var reservaList = new List<Reserva>();
            try
            {
                using (var connection = dbConnection.GetConnection() as SqlConnection)
                {
                    if (connection == null)
                    {
                        return reservaList;
                    }

                    using (var command = new SqlCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = @"
                            SELECT r.IdReserva, r.IdHuesped, r.IdHabitacion, r.FechaEntrada, 
                            r.FechaSalida, r.PrecioTotal, r.Estado, r.Observaciones, r.FechaCreacion,
                            h.Nombre + ' ' + h.Apellido AS nombre_huesped,
                            hab.Numero AS numero_habitacion,
                            hab.Tipo AS tipo_habitacion,
                            hab.PrecioNoche AS precio_noche
                            FROM Reserva r
                            INNER JOIN Huesped h ON r.IdHuesped = h.IdHuesped
                            INNER JOIN Habitacion hab ON r.IdHabitacion = hab.IdHabitacion
                            WHERE r.Estado = @estado
                            ORDER BY r.FechaEntrada DESC";

                        command.Parameters.Add("@estado", SqlDbType.NVarChar).Value = estado;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var reserva = MapReservaFromReader(reader);
                                reservaList.Add(reserva);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar reservas por estado: {ex.Message}");
            }
            return reservaList;
        }

        private Reserva MapReservaFromReader(SqlDataReader reader)
        {
            return new Reserva
            {
                IdReserva = Convert.ToInt32(reader["IdReserva"]),
                IdHuesped = Convert.ToInt32(reader["IdHuesped"]),
                IdHabitacion = Convert.ToInt32(reader["IdHabitacion"]),
                FechaEntrada = Convert.ToDateTime(reader["FechaEntrada"]),
                FechaSalida = Convert.ToDateTime(reader["FechaSalida"]),
                PrecioTotal = Convert.ToDecimal(reader["PrecioTotal"]),
                Estado = reader["Estado"].ToString(),
                Observaciones = reader["Observaciones"] == DBNull.Value ? null : reader["Observaciones"].ToString(),
                FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                NombreHuesped = reader["nombre_huesped"].ToString(),
                NumeroHabitacion = reader["numero_habitacion"].ToString(),
                TipoHabitacion = reader["tipo_habitacion"].ToString(),
                PrecioNoche = Convert.ToDecimal(reader["precio_noche"])
            };
        }
    }
}
