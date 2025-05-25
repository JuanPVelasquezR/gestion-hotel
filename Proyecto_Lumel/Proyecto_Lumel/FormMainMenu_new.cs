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
        
        public FormMenu()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            
            // Configurar permisos según el rol del usuario
            ConfigurarPermisos();
            
            // Mostrar información del usuario en la barra de título
            MostrarInfoUsuario();
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
            OpenChildForm(new FormHistorial());
        }

        private void btnHabitaciones_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            // Pasar el rol del usuario actual para configurar permisos en el formulario
            string rol = Models.Session.IsAdmin() ? "administrador" : "empleado";
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
            if (Models.Session.IsAdmin())
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
            Application.Exit();
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
            // Si es empleado, deshabilitar el botón de usuarios
            if (!Models.Session.IsAdmin())
            {
                btnUsuarios.Visible = false;
            }
        }
        
        /// <summary>
        /// Muestra la información del usuario actual en la barra de título
        /// </summary>
        private void MostrarInfoUsuario()
        {
            if (Models.Session.CurrentUser != null)
            {
                string rolText = Models.Session.IsAdmin() ? "Administrador" : "Empleado";
                lblTitleChildForm.Text = $"Bienvenido, {Models.Session.CurrentUser.NombreCompleto} - {rolText}";
            }
        }
        
        /// <summary>
        /// Cierra la sesión actual y vuelve al formulario de login
        /// </summary>
        private void CerrarSesion()
        {
            Models.Session.Logout();
            Forms.FormLogin login = new Forms.FormLogin();
            this.Hide();
            login.ShowDialog();
            this.Close();
        }
    }
}
