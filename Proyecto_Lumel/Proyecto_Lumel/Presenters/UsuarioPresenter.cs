using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_Lumel.Interfaces;
using Proyecto_Lumel.Models;

namespace Proyecto_Lumel.Presenters
{
    public class UsuarioPresenter
    {
        private IUsuarioView view;
        private IUsuarioRepository repository;
        private BindingSource usuarioBindingSource;
        private IEnumerable<Usuario> usuarioList;

        public UsuarioPresenter(IUsuarioView view, IUsuarioRepository repository)
        {
            this.usuarioBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;

            // Suscribirse a los eventos de la vista
            this.view.SearchEvent += SearchUsuario;
            this.view.AddNewEvent += AddNewUsuario;
            this.view.EditEvent += LoadSelectedUsuarioToEdit;
            this.view.DeleteEvent += DeleteSelectedUsuario;
            this.view.SaveEvent += SaveUsuario;
            this.view.CancelEvent += CancelAction;

            // Establecer el binding source
            this.view.SetUsuarioListBindingSource(usuarioBindingSource);

            // Cargar la lista de usuarios
            LoadAllUsuarios();
        }

        private void LoadAllUsuarios()
        {
            try
            {
                usuarioList = repository.GetAll();
                usuarioBindingSource.DataSource = usuarioList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la lista de usuarios: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchUsuario(object sender, EventArgs e)
        {
            try
            {
                bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
                if (emptyValue)
                    usuarioList = repository.GetAll();
                else
                    usuarioList = repository.GetByFilter(this.view.SearchValue);

                usuarioBindingSource.DataSource = usuarioList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar usuarios: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddNewUsuario(object sender, EventArgs e)
        {
            view.IsEdit = false;
            view.IdUsuario = "0";
            view.Nombre = "";
            view.Apellido = "";
            view.Cargo = "Empleado";
            view.Telefono = "";
            view.Correo = "";
            view.Contraseña = "";
        }

        private void LoadSelectedUsuarioToEdit(object sender, EventArgs e)
        {
            try
            {
                // Verificar si hay elementos en la lista y si hay un elemento actual seleccionado
                if (usuarioBindingSource != null && usuarioBindingSource.Current != null)
                {
                    var usuario = (Usuario)usuarioBindingSource.Current;
                    
                    // Verificar que el usuario tenga un ID válido
                    if (usuario.IdUsuario <= 0)
                    {
                        MessageBox.Show("Por favor, seleccione un usuario válido para editar.", "Advertencia", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    view.IdUsuario = usuario.IdUsuario.ToString();
                    view.Nombre = usuario.Nombre ?? string.Empty;
                    view.Apellido = usuario.Apellido ?? string.Empty;
                    view.Cargo = usuario.Cargo ?? string.Empty;
                    view.Telefono = usuario.Telefono ?? string.Empty;
                    view.Correo = usuario.Correo ?? string.Empty;
                    view.Contraseña = usuario.Contraseña ?? string.Empty;
                    view.IsEdit = true;
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un usuario para editar.", "Advertencia", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Se produjo un error al cargar los datos del usuario: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveUsuario(object sender, EventArgs e)
        {
            try
            {
                // Validar que los datos básicos estén presentes
                if (string.IsNullOrWhiteSpace(view.Nombre) || 
                    string.IsNullOrWhiteSpace(view.Apellido) || 
                    string.IsNullOrWhiteSpace(view.Cargo) ||
                    string.IsNullOrWhiteSpace(view.Contraseña))
                {
                    view.IsSuccessful = false;
                    view.Message = "Los campos Nombre, Apellido, Cargo y Contraseña son obligatorios.";
                    return;
                }

                // Validar el formato del correo electrónico
                if (!string.IsNullOrWhiteSpace(view.Correo) && !IsValidEmail(view.Correo))
                {
                    view.IsSuccessful = false;
                    view.Message = "El formato del correo electrónico no es válido.";
                    return;
                }
                
                // Validar el formato del ID
                if (!int.TryParse(view.IdUsuario, out int idUsuario))
                {
                    view.IsSuccessful = false;
                    view.Message = "El ID del usuario no es válido.";
                    return;
                }
                
                var usuario = new Usuario
                {
                    IdUsuario = idUsuario,
                    Nombre = view.Nombre,
                    Apellido = view.Apellido,
                    Cargo = view.Cargo,
                    Telefono = string.IsNullOrWhiteSpace(view.Telefono) ? null : view.Telefono,
                    Correo = string.IsNullOrWhiteSpace(view.Correo) ? null : view.Correo,
                    Contraseña = view.Contraseña
                };

                if (view.IsEdit) // Editar usuario
                {
                    // Verificar que exista el usuario a editar
                    if (idUsuario <= 0)
                    {
                        view.IsSuccessful = false;
                        view.Message = "No se puede editar un usuario sin ID válido.";
                        return;
                    }
                    
                    repository.Edit(usuario);
                    view.Message = "Usuario actualizado correctamente";
                }
                else // Nuevo usuario
                {
                    repository.Add(usuario);
                    view.Message = "Usuario agregado correctamente";
                }
                
                view.IsSuccessful = true;
                LoadAllUsuarios();
                CleanViewFields();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = $"Error al guardar el usuario: {ex.Message}";
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void CleanViewFields()
        {
            view.IdUsuario = "0";
            view.Nombre = "";
            view.Apellido = "";
            view.Cargo = "Empleado";
            view.Telefono = "";
            view.Correo = "";
            view.Contraseña = "";
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void DeleteSelectedUsuario(object sender, EventArgs e)
        {
            try
            {
                // Verificar si hay elementos en la lista y si hay un elemento actual seleccionado
                if (usuarioBindingSource != null && usuarioBindingSource.Current != null)
                {
                    var usuario = (Usuario)usuarioBindingSource.Current;
                    
                    // Verificar que el usuario tenga un ID válido
                    if (usuario.IdUsuario <= 0)
                    {
                        view.IsSuccessful = false;
                        view.Message = "Por favor, seleccione un usuario válido para eliminar.";
                        return;
                    }
                    
                    repository.Delete(usuario.IdUsuario);
                    view.IsSuccessful = true;
                    view.Message = "Usuario eliminado correctamente";
                    LoadAllUsuarios();
                    CleanViewFields();
                }
                else
                {
                    view.IsSuccessful = false;
                    view.Message = "Por favor, seleccione un usuario para eliminar.";
                }
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = $"Error al eliminar el usuario: {ex.Message}";
            }
        }
    }
}
