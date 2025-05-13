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
    public partial class FormHuéspedes : Form, IHuespedView
    {
        private bool isEdit;
        private bool isSuccessful;
        private string message;
        private HuespedPresenter presenter;

        // Constructor
        public FormHuéspedes()
        {
            InitializeComponent();
            AsociarEventos();
            
            // Inicializar el presenter
            presenter = new HuespedPresenter(this, new HuespedRepository());
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

            // Evento para cargar datos cuando se hace doble clic en un huésped del DataGridView
            dgvHuespedes.DoubleClick += delegate { EditEvent?.Invoke(this, EventArgs.Empty); };

            // Establecer tooltips para los botones
            toolTip1.SetToolTip(btnBuscar, "Buscar huésped");
            toolTip1.SetToolTip(btnNuevo, "Agregar nuevo huésped");
            toolTip1.SetToolTip(btnEditar, "Editar huésped seleccionado");
            toolTip1.SetToolTip(btnEliminar, "Eliminar huésped seleccionado");
            toolTip1.SetToolTip(btnGuardar, "Guardar cambios");
            toolTip1.SetToolTip(btnCancelar, "Cancelar cambios");
        }

        // Propiedades del Model Binding
        public string IdHuesped 
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
        
        public string TipoDocumento 
        { 
            get { return cboTipoDocumento.Text; } 
            set { cboTipoDocumento.Text = value; } 
        }
        
        public string NumeroDocumento 
        { 
            get { return txtNumeroDocumento.Text; } 
            set { txtNumeroDocumento.Text = value; } 
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
        
        public string Direccion 
        { 
            get { return txtDireccion.Text; } 
            set { txtDireccion.Text = value; } 
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
        public void SetHuespedListBindingSource(BindingSource huespedList)
        {
            dgvHuespedes.DataSource = huespedList;
            // Personalizar DataGridView
            ConfigurarDataGridView();
        }

        private void ConfigurarDataGridView()
        {
            // Configurar el DataGridView
            dgvHuespedes.RowHeadersWidth = 25;
            dgvHuespedes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHuespedes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHuespedes.MultiSelect = false;
            dgvHuespedes.ReadOnly = true;
            dgvHuespedes.AllowUserToAddRows = false;

            // Configurar las columnas
            if (dgvHuespedes.Columns.Contains("IdHuesped"))
                dgvHuespedes.Columns["IdHuesped"].HeaderText = "ID";

            if (dgvHuespedes.Columns.Contains("Nombre"))
                dgvHuespedes.Columns["Nombre"].HeaderText = "Nombre";

            if (dgvHuespedes.Columns.Contains("Apellido"))
                dgvHuespedes.Columns["Apellido"].HeaderText = "Apellido";

            if (dgvHuespedes.Columns.Contains("TipoDocumento"))
                dgvHuespedes.Columns["TipoDocumento"].HeaderText = "Tipo Doc.";

            if (dgvHuespedes.Columns.Contains("NumeroDocumento"))
                dgvHuespedes.Columns["NumeroDocumento"].HeaderText = "Número Doc.";

            if (dgvHuespedes.Columns.Contains("Telefono"))
                dgvHuespedes.Columns["Telefono"].HeaderText = "Teléfono";

            if (dgvHuespedes.Columns.Contains("Correo"))
                dgvHuespedes.Columns["Correo"].HeaderText = "Correo";

            if (dgvHuespedes.Columns.Contains("Direccion"))
                dgvHuespedes.Columns["Direccion"].HeaderText = "Dirección";

            if (dgvHuespedes.Columns.Contains("NombreCompleto"))
                dgvHuespedes.Columns["NombreCompleto"].Visible = false;
        }

        private void FormHuéspedes_Load(object sender, EventArgs e)
        {
            // Cargar tipos de documento en el ComboBox
            cboTipoDocumento.Items.Add("DNI");
            cboTipoDocumento.Items.Add("Pasaporte");
            cboTipoDocumento.Items.Add("Cédula");
            cboTipoDocumento.SelectedIndex = 0;
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

            if (string.IsNullOrWhiteSpace(NumeroDocumento))
            {
                MessageBox.Show("El número de documento es obligatorio", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumeroDocumento.Focus();
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Confirmar eliminación
            if (dgvHuespedes.SelectedRows.Count > 0)
            {
                var resultado = MessageBox.Show("¿Está seguro de eliminar el huésped seleccionado?", "Confirmar eliminación", 
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
                MessageBox.Show("Debe seleccionar un huésped para eliminar", "Advertencia", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
