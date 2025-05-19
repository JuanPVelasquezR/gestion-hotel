using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_Lumel.Data;
using Proyecto_Lumel.Interfaces;
using Proyecto_Lumel.Models;
using Proyecto_Lumel.Presenters;

namespace Proyecto_Lumel.Forms
{
    public partial class FormUsuarios : Form, IUsuarioView
    {
        private bool isEdit;
        private bool isSuccessful;
        private string message;
        private UsuarioPresenter presenter;

        // Constructor
        public FormUsuarios()
        {
            InitializeComponent();
            AsociarEventos();
            
            // Inicializar el presenter
            presenter = new UsuarioPresenter(this, new UsuarioRepository());
        }

        private void AsociarEventos()
        {
            // Asociar eventos a los controles
            btnBuscar.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            btnNuevo.Click += delegate { AddNewEvent?.Invoke(this, EventArgs.Empty); };
            btnEditar.Click += delegate { EditEvent?.Invoke(this, EventArgs.Empty); };
            
            // Cambiar para que primero valide y luego dispare el evento
            btnEliminar.Click += btnEliminar_Click;
            btnGuardar.Click += btnGuardar_Click;
            
            btnCancelar.Click += delegate { CancelEvent?.Invoke(this, EventArgs.Empty); };
            txtBuscar.KeyDown += (s, e) => 
            { 
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty); 
            };

            // Evento para cargar datos cuando se hace doble clic en un usuario del DataGridView
            dgvUsuarios.DoubleClick += delegate { EditEvent?.Invoke(this, EventArgs.Empty); };

            // Establecer tooltips para los botones
            toolTip1.SetToolTip(btnBuscar, "Buscar usuario");
            toolTip1.SetToolTip(btnNuevo, "Agregar nuevo usuario");
            toolTip1.SetToolTip(btnEditar, "Editar usuario seleccionado");
            toolTip1.SetToolTip(btnEliminar, "Eliminar usuario seleccionado");
            toolTip1.SetToolTip(btnGuardar, "Guardar cambios");
            toolTip1.SetToolTip(btnCancelar, "Cancelar cambios");
        }

        // Propiedades del Model Binding
        public string IdUsuario 
        { 
            get { return txtId.Text; } 
            set { txtId.Text = value; } 
        }
        
        public string Nombre 
        { 
            get { return txtNombre.Text; } 
            set { txtNombre.Text = value; } 
        }
        
        public string Apellido 
        { 
            get { return txtApellido.Text; } 
            set { txtApellido.Text = value; } 
        }
        
        public string Cargo 
        { 
            get { return cboCargo.Text; } 
            set { cboCargo.Text = value; } 
        }
        
        public string Telefono 
        { 
            get { return txtTelefono.Text; } 
            set { txtTelefono.Text = value; } 
        }
        
        public string Correo 
        { 
            get { return txtCorreo.Text; } 
            set { txtCorreo.Text = value; } 
        }
        
        public string Contraseña 
        { 
            get { return txtContraseña.Text; } 
            set { txtContraseña.Text = value; } 
        }
        
        public string SearchValue 
        { 
            get { return txtBuscar.Text; } 
            set { txtBuscar.Text = value; } 
        }
        
        public bool IsEdit 
        { 
            get { return isEdit; } 
            set { isEdit = value; } 
        }
        
        public bool IsSuccessful 
        { 
            get { return isSuccessful; } 
            set { isSuccessful = value; } 
        }
        
        public string Message 
        { 
            get { return message; } 
            set { message = value; } 
        }

        // Eventos
        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        // Métodos
        public void SetUsuarioListBindingSource(BindingSource usuarioList)
        {
            dgvUsuarios.DataSource = usuarioList;
            // Personalizar DataGridView
            ConfigurarDataGridView();
        }

        private void ConfigurarDataGridView()
        {
            // Configurar el DataGridView
            dgvUsuarios.RowHeadersWidth = 25;
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.AllowUserToAddRows = false;

            // Configurar las columnas
            if (dgvUsuarios.Columns.Contains("IdUsuario"))
                dgvUsuarios.Columns["IdUsuario"].HeaderText = "ID";

            if (dgvUsuarios.Columns.Contains("Nombre"))
                dgvUsuarios.Columns["Nombre"].HeaderText = "Nombre";

            if (dgvUsuarios.Columns.Contains("Apellido"))
                dgvUsuarios.Columns["Apellido"].HeaderText = "Apellido";

            if (dgvUsuarios.Columns.Contains("Cargo"))
                dgvUsuarios.Columns["Cargo"].HeaderText = "Cargo";

            if (dgvUsuarios.Columns.Contains("Telefono"))
                dgvUsuarios.Columns["Telefono"].HeaderText = "Teléfono";

            if (dgvUsuarios.Columns.Contains("Correo"))
                dgvUsuarios.Columns["Correo"].HeaderText = "Correo";

            if (dgvUsuarios.Columns.Contains("Contraseña"))
                dgvUsuarios.Columns["Contraseña"].Visible = false;

            if (dgvUsuarios.Columns.Contains("NombreCompleto"))
                dgvUsuarios.Columns["NombreCompleto"].Visible = false;
        }

        private void FormUsuarios_Load(object sender, EventArgs e)
        {
            // Cargar tipos de cargo en el ComboBox
            cboCargo.Items.Add("Empleado");
            cboCargo.Items.Add("Administrador");
            cboCargo.SelectedIndex = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar datos antes de guardar
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                MessageBox.Show("El nombre es obligatorio", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(Apellido))
            {
                MessageBox.Show("El apellido es obligatorio", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(Contraseña))
            {
                MessageBox.Show("La contraseña es obligatoria", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContraseña.Focus();
                return;
            }

            // Validar formato de correo electrónico
            if (!string.IsNullOrWhiteSpace(Correo) && !IsValidEmail(Correo))
            {
                MessageBox.Show("El formato del correo electrónico no es válido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return;
            }

            // Después de validar, invocar el evento para que el Presenter realice la acción
            SaveEvent?.Invoke(this, EventArgs.Empty);

            // Mostrar mensaje de resultado
            if (IsSuccessful)
            {
                MessageBox.Show(Message, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Confirmar eliminación
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                var resultado = MessageBox.Show("¿Está seguro de eliminar el usuario seleccionado?", "Confirmar eliminación", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    // Invocar el evento para que el Presenter realice la eliminación
                    DeleteEvent?.Invoke(this, EventArgs.Empty);

                    // Mostrar mensaje de resultado
                    if (IsSuccessful)
                    {
                        MessageBox.Show(Message, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un usuario para eliminar", "Advertencia", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
