using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Lumel.Interfaces;
using Proyecto_Lumel.Models;

namespace Proyecto_Lumel.Data
{
    public class HuespedRepository : IHuespedRepository
    {
        private readonly DbConnection dbConnection;

        public HuespedRepository()
        {
            dbConnection = new DbConnection();
        }

        public void Add(Huesped huesped)
        {
            using (var connection = dbConnection.GetConnection() as SqlConnection)
            {
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO Huesped (nombre, apellido, tipo_documento, numero_documento, telefono, correo, direccion) " +
                                         "VALUES (@nombre, @apellido, @tipoDocumento, @numeroDocumento, @telefono, @correo, @direccion)";

                    command.Parameters.Add("@nombre", SqlDbType.NVarChar).Value = huesped.Nombre;
                    command.Parameters.Add("@apellido", SqlDbType.NVarChar).Value = huesped.Apellido;
                    command.Parameters.Add("@tipoDocumento", SqlDbType.NVarChar).Value = huesped.TipoDocumento;
                    command.Parameters.Add("@numeroDocumento", SqlDbType.VarChar).Value = huesped.NumeroDocumento;
                    command.Parameters.Add("@telefono", SqlDbType.VarChar).Value = huesped.Telefono ?? (object)DBNull.Value;
                    command.Parameters.Add("@correo", SqlDbType.NVarChar).Value = huesped.Correo ?? (object)DBNull.Value;
                    command.Parameters.Add("@direccion", SqlDbType.NVarChar).Value = huesped.Direccion ?? (object)DBNull.Value;

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = dbConnection.GetConnection() as SqlConnection)
            {
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM Huesped WHERE id_huesped = @id";

                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Edit(Huesped huesped)
        {
            using (var connection = dbConnection.GetConnection() as SqlConnection)
            {
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "UPDATE Huesped SET nombre = @nombre, apellido = @apellido, " +
                                         "tipo_documento = @tipoDocumento, numero_documento = @numeroDocumento, " +
                                         "telefono = @telefono, correo = @correo, direccion = @direccion " +
                                         "WHERE id_huesped = @id";

                    command.Parameters.Add("@nombre", SqlDbType.NVarChar).Value = huesped.Nombre;
                    command.Parameters.Add("@apellido", SqlDbType.NVarChar).Value = huesped.Apellido;
                    command.Parameters.Add("@tipoDocumento", SqlDbType.NVarChar).Value = huesped.TipoDocumento;
                    command.Parameters.Add("@numeroDocumento", SqlDbType.VarChar).Value = huesped.NumeroDocumento;
                    command.Parameters.Add("@telefono", SqlDbType.VarChar).Value = huesped.Telefono ?? (object)DBNull.Value;
                    command.Parameters.Add("@correo", SqlDbType.NVarChar).Value = huesped.Correo ?? (object)DBNull.Value;
                    command.Parameters.Add("@direccion", SqlDbType.NVarChar).Value = huesped.Direccion ?? (object)DBNull.Value;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = huesped.IdHuesped;

                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Huesped> GetAll()
        {
            var huespedList = new List<Huesped>();
            using (var connection = dbConnection.GetConnection() as SqlConnection)
            {
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT id_huesped, nombre, apellido, tipo_documento, numero_documento, " +
                                         "telefono, correo, direccion FROM Huesped ORDER BY apellido, nombre";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var huesped = new Huesped
                            {
                                IdHuesped = Convert.ToInt32(reader["id_huesped"]),
                                Nombre = reader["nombre"].ToString(),
                                Apellido = reader["apellido"].ToString(),
                                TipoDocumento = reader["tipo_documento"].ToString(),
                                NumeroDocumento = reader["numero_documento"].ToString(),
                                Telefono = reader["telefono"] == DBNull.Value ? null : reader["telefono"].ToString(),
                                Correo = reader["correo"] == DBNull.Value ? null : reader["correo"].ToString(),
                                Direccion = reader["direccion"] == DBNull.Value ? null : reader["direccion"].ToString()
                            };
                            huespedList.Add(huesped);
                        }
                    }
                }
            }
            return huespedList;
        }

        public Huesped GetById(int id)
        {
            Huesped huesped = null;
            using (var connection = dbConnection.GetConnection() as SqlConnection)
            {
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT id_huesped, nombre, apellido, tipo_documento, numero_documento, " +
                                         "telefono, correo, direccion FROM Huesped WHERE id_huesped = @id";

                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            huesped = new Huesped
                            {
                                IdHuesped = Convert.ToInt32(reader["id_huesped"]),
                                Nombre = reader["nombre"].ToString(),
                                Apellido = reader["apellido"].ToString(),
                                TipoDocumento = reader["tipo_documento"].ToString(),
                                NumeroDocumento = reader["numero_documento"].ToString(),
                                Telefono = reader["telefono"] == DBNull.Value ? null : reader["telefono"].ToString(),
                                Correo = reader["correo"] == DBNull.Value ? null : reader["correo"].ToString(),
                                Direccion = reader["direccion"] == DBNull.Value ? null : reader["direccion"].ToString()
                            };
                        }
                    }
                }
            }
            return huesped;
        }

        public IEnumerable<Huesped> GetByFilter(string filter)
        {
            var huespedList = new List<Huesped>();
            using (var connection = dbConnection.GetConnection() as SqlConnection)
            {
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT id_huesped, nombre, apellido, tipo_documento, numero_documento, " +
                                         "telefono, correo, direccion FROM Huesped " +
                                         "WHERE nombre LIKE @filter OR apellido LIKE @filter OR " +
                                         "numero_documento LIKE @filter " +
                                         "ORDER BY apellido, nombre";

                    command.Parameters.Add("@filter", SqlDbType.NVarChar).Value = "%" + filter + "%";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var huesped = new Huesped
                            {
                                IdHuesped = Convert.ToInt32(reader["id_huesped"]),
                                Nombre = reader["nombre"].ToString(),
                                Apellido = reader["apellido"].ToString(),
                                TipoDocumento = reader["tipo_documento"].ToString(),
                                NumeroDocumento = reader["numero_documento"].ToString(),
                                Telefono = reader["telefono"] == DBNull.Value ? null : reader["telefono"].ToString(),
                                Correo = reader["correo"] == DBNull.Value ? null : reader["correo"].ToString(),
                                Direccion = reader["direccion"] == DBNull.Value ? null : reader["direccion"].ToString()
                            };
                            huespedList.Add(huesped);
                        }
                    }
                }
            }
            return huespedList;
        }
    }
} 