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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DbConnection dbConnection;

        public UsuarioRepository()
        {
            dbConnection = new DbConnection();
        }

        public void Add(Usuario usuario)
        {
            using (var connection = dbConnection.GetConnection() as SqlConnection)
            {
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO Usuario (nombre, apellido, cargo, telefono, correo, contrasena) " +
                                         "VALUES (@nombre, @apellido, @cargo, @telefono, @correo, @contrasena)";

                    command.Parameters.Add("@nombre", SqlDbType.NVarChar).Value = usuario.Nombre;
                    command.Parameters.Add("@apellido", SqlDbType.NVarChar).Value = usuario.Apellido;
                    command.Parameters.Add("@cargo", SqlDbType.NVarChar).Value = usuario.Cargo;
                    command.Parameters.Add("@telefono", SqlDbType.VarChar).Value = usuario.Telefono ?? (object)DBNull.Value;
                    command.Parameters.Add("@correo", SqlDbType.NVarChar).Value = usuario.Correo ?? (object)DBNull.Value;
                    command.Parameters.Add("@contrasena", SqlDbType.NVarChar).Value = usuario.Contraseña;

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
                    command.CommandText = "DELETE FROM Usuario WHERE id_usuario = @id";

                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Edit(Usuario usuario)
        {
            using (var connection = dbConnection.GetConnection() as SqlConnection)
            {
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "UPDATE Usuario SET nombre = @nombre, apellido = @apellido, " +
                                         "cargo = @cargo, telefono = @telefono, correo = @correo, " +
                                         "contrasena = @contrasena " +
                                         "WHERE id_usuario = @id";

                    command.Parameters.Add("@nombre", SqlDbType.NVarChar).Value = usuario.Nombre;
                    command.Parameters.Add("@apellido", SqlDbType.NVarChar).Value = usuario.Apellido;
                    command.Parameters.Add("@cargo", SqlDbType.NVarChar).Value = usuario.Cargo;
                    command.Parameters.Add("@telefono", SqlDbType.VarChar).Value = usuario.Telefono ?? (object)DBNull.Value;
                    command.Parameters.Add("@correo", SqlDbType.NVarChar).Value = usuario.Correo ?? (object)DBNull.Value;
                    command.Parameters.Add("@contrasena", SqlDbType.NVarChar).Value = usuario.Contraseña;
                    command.Parameters.Add("@id", SqlDbType.Int).Value = usuario.IdUsuario;

                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Usuario> GetAll()
        {
            var usuarioList = new List<Usuario>();
            using (var connection = dbConnection.GetConnection() as SqlConnection)
            {
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT id_usuario, nombre, apellido, cargo, " +
                                         "telefono, correo, contrasena FROM Usuario ORDER BY apellido, nombre";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var usuario = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                                Nombre = reader["nombre"].ToString(),
                                Apellido = reader["apellido"].ToString(),
                                Cargo = reader["cargo"].ToString(),
                                Telefono = reader["telefono"] == DBNull.Value ? null : reader["telefono"].ToString(),
                                Correo = reader["correo"] == DBNull.Value ? null : reader["correo"].ToString(),
                                Contraseña = reader["contrasena"].ToString()
                            };
                            usuarioList.Add(usuario);
                        }
                    }
                }
            }
            return usuarioList;
        }

        public Usuario GetById(int id)
        {
            Usuario usuario = null;
            using (var connection = dbConnection.GetConnection() as SqlConnection)
            {
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT id_usuario, nombre, apellido, cargo, " +
                                         "telefono, correo, contrasena FROM Usuario WHERE id_usuario = @id";

                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                                Nombre = reader["nombre"].ToString(),
                                Apellido = reader["apellido"].ToString(),
                                Cargo = reader["cargo"].ToString(),
                                Telefono = reader["telefono"] == DBNull.Value ? null : reader["telefono"].ToString(),
                                Correo = reader["correo"] == DBNull.Value ? null : reader["correo"].ToString(),
                                Contraseña = reader["contrasena"].ToString()
                            };
                        }
                    }
                }
            }
            return usuario;
        }

        public IEnumerable<Usuario> GetByFilter(string filter)
        {
            var usuarioList = new List<Usuario>();
            using (var connection = dbConnection.GetConnection() as SqlConnection)
            {
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT id_usuario, nombre, apellido, cargo, " +
                                         "telefono, correo, contrasena FROM Usuario " +
                                         "WHERE nombre LIKE @filter OR apellido LIKE @filter OR " +
                                         "correo LIKE @filter OR cargo LIKE @filter " +
                                         "ORDER BY apellido, nombre";

                    command.Parameters.Add("@filter", SqlDbType.NVarChar).Value = "%" + filter + "%";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var usuario = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                                Nombre = reader["nombre"].ToString(),
                                Apellido = reader["apellido"].ToString(),
                                Cargo = reader["cargo"].ToString(),
                                Telefono = reader["telefono"] == DBNull.Value ? null : reader["telefono"].ToString(),
                                Correo = reader["correo"] == DBNull.Value ? null : reader["correo"].ToString(),
                                Contraseña = reader["contrasena"].ToString()
                            };
                            usuarioList.Add(usuario);
                        }
                    }
                }
            }
            return usuarioList;
        }
    }
}
