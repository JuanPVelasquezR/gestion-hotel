using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using Proyecto_Lumel.Data;
using Proyecto_Lumel.Interfaces;
using Proyecto_Lumel.Models;

namespace Proyecto_Lumel.Forms
{
    public partial class FormLogin : Form
    {
        private readonly IUsuarioRepository _usuarioRepository;
        
        public FormLogin()
        {
            InitializeComponent();
            _usuarioRepository = new UsuarioRepository();
            
            // Configurar el botón para mostrar/ocultar contraseña
            btnMostrarContrasena.IconChar = IconChar.Eye;
            btnMostrarContrasena.IconColor = Color.FromArgb(31, 30, 68);
            btnMostrarContrasena.FlatStyle = FlatStyle.Flat;
            btnMostrarContrasena.FlatAppearance.BorderSize = 0;
            
            // Configurar el TextBox de contraseña
            txtContrasena.UseSystemPasswordChar = true;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string correo = txtCorreo.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();
            
            // Validar que se hayan ingresado datos
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Por favor, ingrese su correo y contraseña.", 
                    "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                // Autenticar usuario
                var usuario = _usuarioRepository.Authenticate(correo, contrasena);
                
                if (usuario != null)
                {
                    try
                    {
                        // Guardar el usuario en la clase estática UsuarioActual
                        Models.UsuarioActual.Usuario = usuario;
                        
                        // Abrir el menú principal pasando el usuario
                        var mainMenu = new Proyecto_Lumel.FormMenu(usuario);
                        this.Hide();
                        mainMenu.ShowDialog();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al iniciar sesión: {ex.Message}", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Correo o contraseña incorrectos.", 
                        "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al intentar iniciar sesión: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMostrarContrasena_Click(object sender, EventArgs e)
        {
            // Cambiar la visibilidad de la contraseña
            txtContrasena.UseSystemPasswordChar = !txtContrasena.UseSystemPasswordChar;
            
            // Cambiar el ícono según la visibilidad
            btnMostrarContrasena.IconChar = txtContrasena.UseSystemPasswordChar ? 
                IconChar.Eye : IconChar.EyeSlash;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
