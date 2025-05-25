using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using FontAwesome.Sharp;
using Proyecto_Lumel.Forms;
using Proyecto_Lumel.Data;
using Proyecto_Lumel.Interfaces;
using Proyecto_Lumel.Models;
using Proyecto_Lumel.Presenters;

namespace Proyecto_Lumel
{
    public partial class FormMenu : Form
    {
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        private Models.Usuario usuarioActual;
        
        public FormMenu(Models.Usuario usuario)
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            
            // Si el usuario es null, mostrar el diálogo de login
            if (usuario == null)
            {
                // Mostrar el diálogo de login
                usuario = MostrarDialogoLogin();
                
                // Si el usuario sigue siendo null después del login, usar un usuario temporal
                if (usuario == null)
                {
                    usuario = new Models.Usuario
                    {
                        IdUsuario = 1,
                        Nombre = "Admin",
                        Apellido = "Temporal",
                        Cargo = "Administrador",
                        Correo = "admin@hotel.com"
                    };
                }
            }
            
            // Guardar el usuario actual
            usuarioActual = usuario;
            
            // No es necesario guardar el usuario en una clase estática
            // ya que lo estamos manejando directamente en esta clase
            
            // Configurar permisos según el rol del usuario
            ConfigurarPermisos();
            
            // Mostrar información del usuario en la barra de título
            MostrarInfoUsuario();
        }
        
        /// <summary>
        /// Muestra un diálogo de login y devuelve el usuario autenticado
        /// </summary>
        /// <returns>Usuario autenticado o null si el login falla</returns>
        private Models.Usuario MostrarDialogoLogin()
        {
            // Crear un formulario de login personalizado
            using (var loginForm = new Form())
            {
                loginForm.Text = "Iniciar Sesión";
                loginForm.Size = new Size(400, 250);
                loginForm.StartPosition = FormStartPosition.CenterScreen;
                loginForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                loginForm.MaximizeBox = false;
                loginForm.MinimizeBox = false;
                
                // Crear controles para el formulario
                var lblUsuario = new Label { Text = "Usuario:", Location = new Point(50, 30), AutoSize = true };
                var txtUsuario = new TextBox { Location = new Point(150, 30), Size = new Size(200, 20) };
                
                var lblPassword = new Label { Text = "Contraseña:", Location = new Point(50, 70), AutoSize = true };
                var txtPassword = new TextBox { Location = new Point(150, 70), Size = new Size(200, 20), UseSystemPasswordChar = true };
                
                var btnLogin = new Button { Text = "Ingresar", Location = new Point(150, 120), Size = new Size(100, 30) };
                var btnCancel = new Button { Text = "Cancelar", Location = new Point(260, 120), Size = new Size(100, 30) };
                
                // Variable para almacenar el resultado del login
                Models.Usuario usuarioAutenticado = null;
                
                // Manejar el evento de clic del botón de login
                btnLogin.Click += (sender, e) =>
                {
                    string correo = txtUsuario.Text.Trim();
                    string contrasena = txtPassword.Text.Trim();
                    
                    // Validar que se hayan ingresado datos
                    if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
                    {
                        MessageBox.Show("Por favor, ingrese su correo y contraseña.", 
                            "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    try
                    {
                        // Crear una instancia del repositorio de usuarios
                        var usuarioRepository = new Data.UsuarioRepository();
                        
                        // Autenticar usuario
                        var usuario = usuarioRepository.Authenticate(correo, contrasena);
                        
                        if (usuario != null)
                        {
                            usuarioAutenticado = usuario;
                            loginForm.DialogResult = DialogResult.OK;
                            loginForm.Close();
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
                };
                
                // Manejar el evento de clic del botón de cancelar
                btnCancel.Click += (sender, e) =>
                {
                    loginForm.DialogResult = DialogResult.Cancel;
                    loginForm.Close();
                };
                
                // Agregar los controles al formulario
                loginForm.Controls.Add(lblUsuario);
                loginForm.Controls.Add(txtUsuario);
                loginForm.Controls.Add(lblPassword);
                loginForm.Controls.Add(txtPassword);
                loginForm.Controls.Add(btnLogin);
                loginForm.Controls.Add(btnCancel);
                
                // Mostrar el formulario como diálogo
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    return usuarioAutenticado;
                }
                
                return null;
            }
        }


        private struct RGBColors
        {
            public static System.Drawing.Color color1 = System.Drawing.Color.FromArgb(172, 126, 241);
            public static System.Drawing.Color color2 = System.Drawing.Color.FromArgb(249, 118, 176);
            public static System.Drawing.Color color3 = System.Drawing.Color.FromArgb(253, 138, 114);
            public static System.Drawing.Color color4 = System.Drawing.Color.FromArgb(95, 77, 221);
            public static System.Drawing.Color color5 = System.Drawing.Color.FromArgb(249, 88, 155);
            public static System.Drawing.Color color6 = System.Drawing.Color.FromArgb(24, 161, 251);
        }



        private void ActivateButton(object senderBtn, System.Drawing.Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                //Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = System.Drawing.Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //Left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
                iconCurrentChildForm.IconChar = currentBtn.IconChar;
                iconCurrentChildForm.IconColor = color;
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = System.Drawing.Color.FromArgb(31, 30, 68);
                currentBtn.ForeColor = System.Drawing.Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = System.Drawing.Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void OpenChildForm(Form childForm)
        {
            //open only form
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            //End
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitleChildForm.Text = childForm.Text;
        }

        private void btnReservas_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new FormReservas());
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color2);
            // Pasar el rol del usuario actual para configurar permisos en el formulario
            string rol = usuarioActual.Cargo.ToLower() == "administrador" ? "administrador" : "empleado";
            // Abrir el formulario de historial
            // Nota: En una implementación futura, se debería modificar FormHistorial para que respete los permisos
            OpenChildForm(new FormHistorial());
        }

        private void btnHabitaciones_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            // Pasar el rol del usuario actual para configurar permisos en el formulario
            string rol = usuarioActual.Cargo.ToLower() == "administrador" ? "administrador" : "empleado";
            OpenChildForm(new FormHabitacionesNuevo(rol));
        }

        private void btnHuéspedes_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            OpenChildForm(new FormHuéspedes());
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            // Solo los administradores pueden acceder a la gestión de usuarios
            if (usuarioActual.Cargo.ToLower() == "administrador")
            {
                ActivateButton(sender, RGBColors.color5);
                OpenChildForm(new FormUsuarios());
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a esta sección.", 
                    "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconCurrentChildForm.IconChar = IconChar.Home;  
            iconCurrentChildForm.IconColor = System.Drawing.Color.MediumPurple;
            lblTitleChildForm.Text = "Inicio";

        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            Reset();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            // Preguntar si desea cerrar sesión o salir de la aplicación
            DialogResult result = MessageBox.Show("¿Desea cerrar sesión o salir de la aplicación?", 
                "Cerrar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes) // Cerrar sesión
            {
                CerrarSesion();
            }
            else if (result == DialogResult.No) // Salir de la aplicación
            {
                Application.Exit();
            }
            // Si es Cancel, no hacer nada
        }
        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        
        /// <summary>
        /// Configura los permisos de acceso según el rol del usuario
        /// </summary>
        private void ConfigurarPermisos()
        {
            // Verificar que el usuario no sea null antes de configurar permisos
            if (usuarioActual != null)
            {
                // Si es empleado, deshabilitar el botón de usuarios
                if (usuarioActual.Cargo.ToLower() != "administrador")
                {
                    btnUsuarios.Visible = false;
                }
                
                // Configurar permisos adicionales para empleados
                if (usuarioActual.Cargo.ToLower() == "empleado")
                {
                    // Los empleados solo tienen permisos de lectura en habitaciones e historial
                    // Esta funcionalidad se implementará en los formularios correspondientes
                }
            }
        }
        
        /// <summary>
        /// Muestra la información del usuario actual en la barra de título
        /// </summary>
        private void MostrarInfoUsuario()
        {
            if (usuarioActual != null)
            {
                string rolText = usuarioActual.Cargo.ToLower() == "administrador" ? "Administrador" : "Empleado";
                lblTitleChildForm.Text = $"Bienvenido, {usuarioActual.NombreCompleto} - {rolText}";
            }
            else
            {
                lblTitleChildForm.Text = "Sistema de Gestión Hotelera";
            }
        }
        
        /// <summary>
        /// Cierra la sesión actual y muestra el diálogo de login
        /// </summary>
        private void CerrarSesion()
        {
            // Informar al usuario que se cerró la sesión
            MessageBox.Show("Se ha cerrado la sesión correctamente.", "Sesión cerrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            // Limpiar la referencia al usuario actual
            usuarioActual = null;
            
            // Mostrar el diálogo de login
            var nuevoUsuario = MostrarDialogoLogin();
            
            if (nuevoUsuario != null)
            {
                // Guardar el nuevo usuario
                usuarioActual = nuevoUsuario;
                
                // Configurar permisos según el rol del usuario
                ConfigurarPermisos();
                
                // Mostrar información del usuario en la barra de título
                MostrarInfoUsuario();
                
                // Cerrar cualquier formulario hijo abierto
                if (currentChildForm != null)
                {
                    currentChildForm.Close();
                    Reset();
                }
            }
            else
            {
                // Si el usuario canceló el login, cerrar la aplicación
                Application.Exit();
            }
        }
    }
}
