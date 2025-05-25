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
                        command.CommandText = @"
                            SELECT COUNT(*) FROM Reserva 
                            WHERE id_habitacion = @idHabitacion 
                            AND estado NOT IN ('Cancelada') 
                            AND ((fecha_entrada BETWEEN @fechaEntrada AND @fechaSalida) 
                            OR (fecha_salida BETWEEN @fechaEntrada AND @fechaSalida)
                            OR (@fechaEntrada BETWEEN fecha_entrada AND fecha_salida))";
                        
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
                        command.CommandText = @"
                            INSERT INTO Reserva (id_huesped, id_habitacion, fecha_entrada, fecha_salida, 
                            precio_total, estado, observaciones, fecha_creacion) 
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
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la reserva: " + ex.Message);
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
                            SELECT r.id_reserva, r.id_huesped, r.id_habitacion, r.fecha_entrada, 
                            r.fecha_salida, r.precio_total, r.estado, r.observaciones, r.fecha_creacion,
                            h.nombre + ' ' + h.apellido AS nombre_huesped,
                            hab.numero AS numero_habitacion,
                            hab.tipo AS tipo_habitacion,
                            hab.precio_noche
                            FROM Reserva r
                            INNER JOIN Huesped h ON r.id_huesped = h.id_huesped
                            INNER JOIN Habitacion hab ON r.id_habitacion = hab.id_habitacion
                            ORDER BY r.fecha_entrada DESC";

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
                            SELECT r.id_reserva, r.id_huesped, r.id_habitacion, r.fecha_entrada, 
                            r.fecha_salida, r.precio_total, r.estado, r.observaciones, r.fecha_creacion,
                            h.nombre + ' ' + h.apellido AS nombre_huesped,
                            hab.numero AS numero_habitacion,
                            hab.tipo AS tipo_habitacion,
                            hab.precio_noche
                            FROM Reserva r
                            INNER JOIN Huesped h ON r.id_huesped = h.id_huesped
                            INNER JOIN Habitacion hab ON r.id_habitacion = hab.id_habitacion
                            WHERE r.id_reserva = @id";

                        command.Parameters.Add("@id", SqlDbType.Int).Value = id;

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
                            SELECT r.id_reserva, r.id_huesped, r.id_habitacion, r.fecha_entrada, 
                            r.fecha_salida, r.precio_total, r.estado, r.observaciones, r.fecha_creacion,
                            h.nombre + ' ' + h.apellido AS nombre_huesped,
                            hab.numero AS numero_habitacion,
                            hab.tipo AS tipo_habitacion,
                            hab.precio_noche
                            FROM Reserva r
                            INNER JOIN Huesped h ON r.id_huesped = h.id_huesped
                            INNER JOIN Habitacion hab ON r.id_habitacion = hab.id_habitacion
                            WHERE h.nombre LIKE @filter OR h.apellido LIKE @filter OR
                            hab.numero LIKE @filter OR r.estado LIKE @filter
                            ORDER BY r.fecha_entrada DESC";

                        command.Parameters.Add("@filter", SqlDbType.NVarChar).Value = "%" + value + "%";

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
                            SELECT r.id_reserva, r.id_huesped, r.id_habitacion, r.fecha_entrada, 
                            r.fecha_salida, r.precio_total, r.estado, r.observaciones, r.fecha_creacion,
                            h.nombre + ' ' + h.apellido AS nombre_huesped,
                            hab.numero AS numero_habitacion,
                            hab.tipo AS tipo_habitacion,
                            hab.precio_noche
                            FROM Reserva r
                            INNER JOIN Huesped h ON r.id_huesped = h.id_huesped
                            INNER JOIN Habitacion hab ON r.id_habitacion = hab.id_habitacion
                            WHERE (r.fecha_entrada BETWEEN @fechaInicio AND @fechaFin)
                            OR (r.fecha_salida BETWEEN @fechaInicio AND @fechaFin)
                            OR (@fechaInicio BETWEEN r.fecha_entrada AND r.fecha_salida)
                            ORDER BY r.fecha_entrada";

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
                            SELECT r.id_reserva, r.id_huesped, r.id_habitacion, r.fecha_entrada, 
                            r.fecha_salida, r.precio_total, r.estado, r.observaciones, r.fecha_creacion,
                            h.nombre + ' ' + h.apellido AS nombre_huesped,
                            hab.numero AS numero_habitacion,
                            hab.tipo AS tipo_habitacion,
                            hab.precio_noche
                            FROM Reserva r
                            INNER JOIN Huesped h ON r.id_huesped = h.id_huesped
                            INNER JOIN Habitacion hab ON r.id_habitacion = hab.id_habitacion
                            WHERE r.id_huesped = @idHuesped
                            ORDER BY r.fecha_entrada DESC";

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
                            SELECT r.id_reserva, r.id_huesped, r.id_habitacion, r.fecha_entrada, 
                            r.fecha_salida, r.precio_total, r.estado, r.observaciones, r.fecha_creacion,
                            h.nombre + ' ' + h.apellido AS nombre_huesped,
                            hab.numero AS numero_habitacion,
                            hab.tipo AS tipo_habitacion,
                            hab.precio_noche
                            FROM Reserva r
                            INNER JOIN Huesped h ON r.id_huesped = h.id_huesped
                            INNER JOIN Habitacion hab ON r.id_habitacion = hab.id_habitacion
                            WHERE r.id_habitacion = @idHabitacion
                            ORDER BY r.fecha_entrada DESC";

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
                            SELECT r.id_reserva, r.id_huesped, r.id_habitacion, r.fecha_entrada, 
                            r.fecha_salida, r.precio_total, r.estado, r.observaciones, r.fecha_creacion,
                            h.nombre + ' ' + h.apellido AS nombre_huesped,
                            hab.numero AS numero_habitacion,
                            hab.tipo AS tipo_habitacion,
                            hab.precio_noche
                            FROM Reserva r
                            INNER JOIN Huesped h ON r.id_huesped = h.id_huesped
                            INNER JOIN Habitacion hab ON r.id_habitacion = hab.id_habitacion
                            WHERE r.estado = @estado
                            ORDER BY r.fecha_entrada DESC";

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
                IdReserva = Convert.ToInt32(reader["id_reserva"]),
                IdHuesped = Convert.ToInt32(reader["id_huesped"]),
                IdHabitacion = Convert.ToInt32(reader["id_habitacion"]),
                FechaEntrada = Convert.ToDateTime(reader["fecha_entrada"]),
                FechaSalida = Convert.ToDateTime(reader["fecha_salida"]),
                PrecioTotal = Convert.ToDecimal(reader["precio_total"]),
                Estado = reader["estado"].ToString(),
                Observaciones = reader["observaciones"] == DBNull.Value ? null : reader["observaciones"].ToString(),
                FechaCreacion = Convert.ToDateTime(reader["fecha_creacion"]),
                NombreHuesped = reader["nombre_huesped"].ToString(),
                NumeroHabitacion = reader["numero_habitacion"].ToString(),
                TipoHabitacion = reader["tipo_habitacion"].ToString(),
                PrecioNoche = Convert.ToDecimal(reader["precio_noche"])
            };
        }
    }
}
