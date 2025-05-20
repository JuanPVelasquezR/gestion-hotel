using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_Lumel.Interfaces;
using Proyecto_Lumel.Models;
using Proyecto_Lumel.Presenters;
using Proyecto_Lumel.Data;

namespace Proyecto_Lumel.Forms
{
    public partial class FormHabitacionesNuevo : Form, IHabitacionView
    {
        // Campos
        private string message;
        private bool isEdit;
        private bool isSuccessful;
        private readonly string rolUsuario;
        private static FormHabitacionesNuevo instance;
        private HabitacionPresenter presenter;

        // Propiedades - IHabitacionView
        public string IdHabitacion { get => txtIdHabitacion.Text; set => txtIdHabitacion.Text = value; }
        public string Numero { get => txtNumero.Text; set => txtNumero.Text = value; }
        public string Tipo { get => cmbTipo.Text; set => cmbTipo.Text = value; }
        public string Descripcion { get => txtDescripcion.Text; set => txtDescripcion.Text = value; }
        public string Capacidad { get => txtCapacidad.Text; set => txtCapacidad.Text = value; }
        public string PrecioNoche { get => txtPrecioNoche.Text; set => txtPrecioNoche.Text = value; }
        public string Estado { get => cmbEstado.Text; set => cmbEstado.Text = value; }
        public string SearchValue { get => txtSearch.Text; set => txtSearch.Text = value; }
        public bool IsEdit { get => isEdit; set => isEdit = value; }
        public bool IsSuccessful { get => isSuccessful; set => isSuccessful = value; }
        public string Message { get => message; set => message = value; }

        // Eventos
        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;
        public event EventHandler LoadAllEvent;

        // Constructor
        public FormHabitacionesNuevo(string rolUsuario = "usuario")
        {
            InitializeComponent();
            this.rolUsuario = rolUsuario;
            AssociateAndRaiseViewEvents();
            ConfigurarControlesPorRol();
            ConfigurarComboBoxes();
            tabControl1.TabPages.Remove(tabPageHabitacionDetail);
            
            // Inicializar el presentador
            presenter = new HabitacionPresenter(this, new HabitacionRepository());
        }

        private void AssociateAndRaiseViewEvents()
        {
            // Buscar
            btnSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };

            // Agregar nuevo
            btnAdd.Click += delegate
            {
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPageHabitacionList);
                tabControl1.TabPages.Add(tabPageHabitacionDetail);
                tabPageHabitacionDetail.Text = "Agregar nueva habitación";
            };

            // Editar
            btnEdit.Click += delegate
            {
                EditEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPageHabitacionList);
                tabControl1.TabPages.Add(tabPageHabitacionDetail);
                tabPageHabitacionDetail.Text = "Editar habitación";
            };

            // Guardar
            btnSave.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (isSuccessful)
                {
                    tabControl1.TabPages.Remove(tabPageHabitacionDetail);
                    tabControl1.TabPages.Add(tabPageHabitacionList);
                    // Solo mostrar mensaje de éxito si es realmente necesario
                    // MessageBox.Show(Message);
                }
                else if (!string.IsNullOrEmpty(Message))
                {
                    // Mostrar mensaje de error solo si hay un mensaje que mostrar
                    MessageBox.Show(Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            // Cancelar
            btnCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPageHabitacionDetail);
                tabControl1.TabPages.Add(tabPageHabitacionList);
            };

            // Eliminar
            btnDelete.Click += delegate
            {
                var result = MessageBox.Show("¿Está seguro de eliminar la habitación seleccionada?", "Advertencia",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };

            // Cargar todos al iniciar el formulario
            this.Load += delegate { LoadAllEvent?.Invoke(this, EventArgs.Empty); };
        }

        private void ConfigurarControlesPorRol()
        {
            // Configurar controles según el rol del usuario
            if (rolUsuario.ToLower() == "administrador")
            {
                // El administrador tiene acceso completo
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnSave.Enabled = true;
            }
            else if (rolUsuario.ToLower() == "recepcionista")
            {
                // El recepcionista puede agregar y editar, pero no eliminar
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = false;
                btnSave.Enabled = true;
            }
            else
            {
                // Otros usuarios solo pueden ver
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Enabled = false;
            }
        }

        private void ConfigurarComboBoxes()
        {
            // Configurar el ComboBox de Tipo de Habitación
            cmbTipo.Items.Clear();
            cmbTipo.Items.AddRange(new string[] {
                "Individual",
                "Doble",
                "Suite",
                "Familiar",
                "Ejecutiva"
            });

            // Configurar el ComboBox de Estado
            cmbEstado.Items.Clear();
            cmbEstado.Items.AddRange(new string[] {
                "Disponible",
                "Ocupada",
                "Reservada",
                "En Mantenimiento",
                "Fuera de Servicio"
            });

            // Establecer valores predeterminados
            cmbTipo.SelectedIndex = 0;
            cmbEstado.SelectedIndex = 0;
        }

        // Métodos
        public void SetHabitacionListBindingSource(BindingSource habitacionList)
        {
            dataGridView1.DataSource = habitacionList;
            
            // Verificar que el DataGridView tenga columnas antes de configurarlas
            if (dataGridView1.Columns.Count > 0)
            {
                try
                {
                    // Configurar las columnas del DataGridView
                    if (dataGridView1.Columns.Count > 0)
                    {
                        dataGridView1.Columns[0].HeaderText = "ID";
                        dataGridView1.Columns[0].Visible = false;
                    }
                    
                    if (dataGridView1.Columns.Count > 1)
                    {
                        dataGridView1.Columns[1].HeaderText = "Número";
                        dataGridView1.Columns[1].Width = 80;
                    }
                    
                    if (dataGridView1.Columns.Count > 2)
                    {
                        dataGridView1.Columns[2].HeaderText = "Tipo";
                        dataGridView1.Columns[2].Width = 100;
                    }
                    
                    if (dataGridView1.Columns.Count > 3)
                    {
                        dataGridView1.Columns[3].HeaderText = "Descripción";
                        dataGridView1.Columns[3].Width = 200;
                    }
                    
                    if (dataGridView1.Columns.Count > 4)
                    {
                        dataGridView1.Columns[4].HeaderText = "Capacidad";
                        dataGridView1.Columns[4].Width = 80;
                    }
                    
                    if (dataGridView1.Columns.Count > 5)
                    {
                        dataGridView1.Columns[5].HeaderText = "Precio/Noche";
                        dataGridView1.Columns[5].Width = 100;
                    }
                    
                    if (dataGridView1.Columns.Count > 6)
                    {
                        dataGridView1.Columns[6].HeaderText = "Estado";
                        dataGridView1.Columns[6].Width = 100;
                    }

                    // Colorear las filas según el estado
                    dataGridView1.CellFormatting += (s, e) =>
                    {
                        if (e.ColumnIndex == 6 && e.Value != null) // Columna de Estado
                        {
                            string estado = e.Value.ToString();
                            if (estado == "Disponible")
                                e.CellStyle.BackColor = Color.LightGreen;
                            else if (estado == "Ocupada")
                                e.CellStyle.BackColor = Color.LightCoral;
                            else if (estado == "Reservada")
                                e.CellStyle.BackColor = Color.LightBlue;
                            else if (estado == "En Mantenimiento")
                                e.CellStyle.BackColor = Color.Orange;
                            else if (estado == "Fuera de Servicio")
                                e.CellStyle.BackColor = Color.Gray;
                        }
                    };
                }
                catch (Exception ex)
                {
                    // Manejo silencioso de errores
                    Console.WriteLine("Error al configurar las columnas: " + ex.Message);
                }
            }
            else
            {
                // No mostrar mensaje cuando no hay datos
                Console.WriteLine("No hay datos para mostrar en la lista de habitaciones.");
            }
        }

        public static FormHabitacionesNuevo GetInstance(Form parentContainer, string rolUsuario = "usuario")
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new FormHabitacionesNuevo(rolUsuario);
                instance.MdiParent = parentContainer;
                instance.FormBorderStyle = FormBorderStyle.None;
                instance.Dock = DockStyle.Fill;
            }
            else
            {
                if (instance.WindowState == FormWindowState.Minimized)
                    instance.WindowState = FormWindowState.Normal;
                instance.BringToFront();
            }
            return instance;
        }
    }
}
