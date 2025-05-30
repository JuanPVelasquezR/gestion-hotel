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
    public partial class FormReservas : Form, IReservaView
    {
        // Campos
        private string message;
        private bool isEdit;
        private bool isSuccessful;
        private readonly string rolUsuario;
        private static FormReservas instance;
        private ReservaPresenter presenter;

        // Propiedades - IReservaView
        public string IdReserva { get => txtIdReserva.Text; set => txtIdReserva.Text = value; }
        public string IdHuesped { get => txtIdHuesped.Text; set => txtIdHuesped.Text = value; }
        public string NombreHuesped { get => txtNombreHuesped.Text; set => txtNombreHuesped.Text = value; }
        public string IdHabitacion { get => txtIdHabitacion.Text; set => txtIdHabitacion.Text = value; }
        public string NumeroHabitacion { get => txtNumeroHabitacion.Text; set => txtNumeroHabitacion.Text = value; }
        public string TipoHabitacion { get => txtTipoHabitacion.Text; set => txtTipoHabitacion.Text = value; }
        public DateTime FechaEntrada { get => dtpFechaEntrada.Value; set => dtpFechaEntrada.Value = value; }
        public DateTime FechaSalida { get => dtpFechaSalida.Value; set => dtpFechaSalida.Value = value; }
        public string PrecioNoche { get => txtPrecioNoche.Text; set => txtPrecioNoche.Text = value; }
        public string PrecioTotal { get => txtPrecioTotal.Text; set => txtPrecioTotal.Text = value; }
        public string Estado { get => cmbEstado.Text; set => cmbEstado.Text = value; }
        public string Observaciones { get => txtObservaciones.Text; set => txtObservaciones.Text = value; }
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
        public event EventHandler SeleccionarHuespedEvent;
        public event EventHandler SeleccionarHabitacionEvent;
        public event EventHandler CalcularPrecioEvent;
        public event EventHandler FiltrarPorFechasEvent;
        public event EventHandler FiltrarPorEstadoEvent;
        public event EventHandler FiltrarPorHuespedEvent;
        public event EventHandler FiltrarPorHabitacionEvent;
        public event EventHandler LimpiarFiltrosEvent;

        // Constructor
        public FormReservas(string rolUsuario = "usuario")
        {
            InitializeComponent();
            this.rolUsuario = rolUsuario;
            
            // Configurar el TabControl correctamente desde el inicio
            // Asegurarse de que solo la pestaña de lista esté visible inicialmente
            if (tabControl1.TabPages.Contains(tabPageReservaDetail))
            {
                tabControl1.TabPages.Remove(tabPageReservaDetail);
            }
            
            if (!tabControl1.TabPages.Contains(tabPageReservaList))
            {
                tabControl1.TabPages.Add(tabPageReservaList);
            }
            
            tabControl1.SelectedTab = tabPageReservaList;
            
            AssociateAndRaiseViewEvents();
            ConfigurarControlesPorRol();
            ConfigurarComboBoxes();
            
            // Inicializar el presentador
            presenter = new ReservaPresenter(this, new ReservaRepository());
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

            // Agregar nueva reserva
            btnAdd.Click += delegate
            {
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                
                // En lugar de eliminar y agregar pestañas, simplemente cambiamos la pestaña activa
                // Primero nos aseguramos de que ambas pestañas estén en el TabControl
                if (!tabControl1.TabPages.Contains(tabPageReservaDetail))
                {
                    tabControl1.TabPages.Add(tabPageReservaDetail);
                }
                
                // Seleccionamos la pestaña de detalle
                tabControl1.SelectedTab = tabPageReservaDetail;
                tabPageReservaDetail.Text = "Agregar nueva reserva";
            };

            // Editar reserva
            btnEdit.Click += delegate
            {
                EditEvent?.Invoke(this, EventArgs.Empty);
                
                // En lugar de eliminar y agregar pestañas, simplemente cambiamos la pestaña activa
                if (!tabControl1.TabPages.Contains(tabPageReservaDetail))
                {
                    tabControl1.TabPages.Add(tabPageReservaDetail);
                }
                
                // Seleccionamos la pestaña de detalle
                tabControl1.SelectedTab = tabPageReservaDetail;
                tabPageReservaDetail.Text = "Editar reserva";
            };

            // Guardar
            btnSave.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (isSuccessful)
                {
                    // En lugar de eliminar la pestaña, simplemente cambiamos a la pestaña de lista
                    if (!tabControl1.TabPages.Contains(tabPageReservaList))
                    {
                        tabControl1.TabPages.Add(tabPageReservaList);
                    }
                    
                    // Seleccionamos la pestaña de lista
                    tabControl1.SelectedTab = tabPageReservaList;
                }
                MessageBox.Show(Message);
            };

            // Cancelar
            btnCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                
                // En lugar de eliminar la pestaña, simplemente cambiamos a la pestaña de lista
                if (!tabControl1.TabPages.Contains(tabPageReservaList))
                {
                    tabControl1.TabPages.Add(tabPageReservaList);
                }
                
                // Seleccionamos la pestaña de lista
                tabControl1.SelectedTab = tabPageReservaList;
            };

            // Eliminar
            btnDelete.Click += delegate
            {
                var result = MessageBox.Show("¿Está seguro de que desea eliminar esta reserva?", "Advertencia",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };

            // Seleccionar huésped
            btnSeleccionarHuesped.Click += delegate
            {
                SeleccionarHuespedEvent?.Invoke(this, EventArgs.Empty);
            };

            // Seleccionar habitación
            btnSeleccionarHabitacion.Click += delegate
            {
                SeleccionarHabitacionEvent?.Invoke(this, EventArgs.Empty);
            };

            // Calcular precio
            btnCalcularPrecio.Click += delegate
            {
                CalcularPrecioEvent?.Invoke(this, EventArgs.Empty);
            };

            // Cambio en fechas para cálculo automático
            dtpFechaEntrada.ValueChanged += (s, e) => CalcularPrecioEvent?.Invoke(this, EventArgs.Empty);
            dtpFechaSalida.ValueChanged += (s, e) => CalcularPrecioEvent?.Invoke(this, EventArgs.Empty);

            // Filtros
            btnFiltrarFechas.Click += delegate
            {
                FiltrarPorFechasEvent?.Invoke(this, EventArgs.Empty);
            };

            cmbFiltroEstado.SelectedIndexChanged += (s, e) => FiltrarPorEstadoEvent?.Invoke(s, e);

            btnFiltrarHuesped.Click += delegate
            {
                FiltrarPorHuespedEvent?.Invoke(this, EventArgs.Empty);
            };

            btnFiltrarHabitacion.Click += delegate
            {
                FiltrarPorHabitacionEvent?.Invoke(this, EventArgs.Empty);
            };

            btnLimpiarFiltros.Click += delegate
            {
                LimpiarFiltrosEvent?.Invoke(this, EventArgs.Empty);
            };
        }

        private void ConfigurarControlesPorRol()
        {
            // Si el usuario no es administrador, deshabilitar ciertas funciones
            if (rolUsuario != "administrador")
            {
                btnDelete.Enabled = false;
                btnDelete.Visible = false;
            }
        }

        private void ConfigurarComboBoxes()
        {
            // Configurar el ComboBox de Estado de Reserva
            cmbEstado.Items.Clear();
            cmbEstado.Items.Add("Pendiente");
            cmbEstado.Items.Add("Confirmada");
            cmbEstado.Items.Add("Cancelada");
            cmbEstado.Items.Add("Completada");
            cmbEstado.SelectedIndex = 0;

            // Configurar el ComboBox de Filtro por Estado
            cmbFiltroEstado.Items.Clear();
            cmbFiltroEstado.Items.Add("Todos");
            cmbFiltroEstado.Items.Add("Pendiente");
            cmbFiltroEstado.Items.Add("Confirmada");
            cmbFiltroEstado.Items.Add("Cancelada");
            cmbFiltroEstado.Items.Add("Completada");
            cmbFiltroEstado.SelectedIndex = 0;
        }

        // Métodos
        // Label para mostrar mensaje cuando no hay datos
        private Label _noDataLabel;
        
        // Método para mostrar u ocultar el mensaje de "no hay datos"
        private void MostrarMensajeNoHayDatos(bool mostrar)
        {
            // Si el label no existe, crearlo
            if (_noDataLabel == null)
            {
                _noDataLabel = new Label();
                _noDataLabel.Name = "lblNoData";
                _noDataLabel.Text = "No hay reservas para mostrar. Utilice los filtros o cree una nueva reserva.";
                _noDataLabel.ForeColor = Color.Gray;
                _noDataLabel.Font = new Font(dataGridView1.Font.FontFamily, 10, FontStyle.Italic);
                _noDataLabel.TextAlign = ContentAlignment.MiddleCenter;
                _noDataLabel.Dock = DockStyle.None;
                _noDataLabel.AutoSize = false;
                _noDataLabel.Size = new Size(dataGridView1.Width - 40, 30);
                _noDataLabel.Location = new Point(
                    dataGridView1.Location.X + 20,
                    dataGridView1.Location.Y + (dataGridView1.Height / 2) - 15);
                
                // Agregar el Label al formulario
                tabPageReservaList.Controls.Add(_noDataLabel);
                _noDataLabel.BringToFront();
            }
            
            // Mostrar u ocultar el label según corresponda
            _noDataLabel.Visible = mostrar;
        }
        
        public void SetReservaListBindingSource(BindingSource reservaList)
        {
            dataGridView1.DataSource = reservaList;
            
            // Mostrar u ocultar el mensaje de "no hay datos"
            bool noHayDatos = (dataGridView1.Rows.Count == 0);
            MostrarMensajeNoHayDatos(noHayDatos);
            
            // Configurar las columnas del DataGridView
            if (!noHayDatos)
            {
                try
                {
                    // Ocultar columnas que no queremos mostrar
                    if (dataGridView1.Columns.Count > 0)
                    {
                        dataGridView1.Columns[0].HeaderText = "ID";
                        dataGridView1.Columns[0].Width = 50;
                    }
                    
                    if (dataGridView1.Columns.Count > 1)
                    {
                        dataGridView1.Columns[1].Visible = false; // IdHuesped
                    }
                    
                    if (dataGridView1.Columns.Count > 2)
                    {
                        dataGridView1.Columns[2].Visible = false; // IdHabitacion
                    }
                    
                    if (dataGridView1.Columns.Count > 3)
                    {
                        dataGridView1.Columns[3].HeaderText = "Fecha Entrada";
                        dataGridView1.Columns[3].Width = 120;
                        dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
                    }
                    
                    if (dataGridView1.Columns.Count > 4)
                    {
                        dataGridView1.Columns[4].HeaderText = "Fecha Salida";
                        dataGridView1.Columns[4].Width = 120;
                        dataGridView1.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy";
                    }
                    
                    if (dataGridView1.Columns.Count > 5)
                    {
                        dataGridView1.Columns[5].HeaderText = "Precio Total";
                        dataGridView1.Columns[5].Width = 100;
                        dataGridView1.Columns[5].DefaultCellStyle.Format = "C2";
                    }
                    
                    if (dataGridView1.Columns.Count > 6)
                    {
                        dataGridView1.Columns[6].HeaderText = "Estado";
                        dataGridView1.Columns[6].Width = 100;
                    }
                    
                    if (dataGridView1.Columns.Count > 7)
                    {
                        dataGridView1.Columns[7].HeaderText = "Observaciones";
                        dataGridView1.Columns[7].Width = 200;
                    }
                    
                    if (dataGridView1.Columns.Count > 8)
                    {
                        dataGridView1.Columns[8].HeaderText = "Fecha Creación";
                        dataGridView1.Columns[8].Width = 120;
                        dataGridView1.Columns[8].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                    }
                    
                    if (dataGridView1.Columns.Count > 9)
                    {
                        dataGridView1.Columns[9].HeaderText = "Huésped";
                        dataGridView1.Columns[9].Width = 150;
                    }
                    
                    if (dataGridView1.Columns.Count > 10)
                    {
                        dataGridView1.Columns[10].HeaderText = "Nº Habitación";
                        dataGridView1.Columns[10].Width = 100;
                    }
                    
                    if (dataGridView1.Columns.Count > 11)
                    {
                        dataGridView1.Columns[11].HeaderText = "Tipo Habitación";
                        dataGridView1.Columns[11].Width = 120;
                    }
                    
                    if (dataGridView1.Columns.Count > 12)
                    {
                        dataGridView1.Columns[12].HeaderText = "Precio/Noche";
                        dataGridView1.Columns[12].Width = 100;
                        dataGridView1.Columns[12].DefaultCellStyle.Format = "C2";
                    }

                    // Permitir ordenar por columnas
                    dataGridView1.Sort(dataGridView1.Columns[3], ListSortDirection.Descending); // Ordenar por fecha de entrada por defecto
                    
                    // Mejorar la apariencia del DataGridView para el historial
                    dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                    dataGridView1.EnableHeadersVisualStyles = false;
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 30, 68);
                    dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                    dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    
                    // Colorear las filas según el estado con colores más atractivos
                    dataGridView1.CellFormatting += (s, e) =>
                    {
                        if (e.RowIndex >= 0) // Asegurarse de que no es el encabezado
                        {
                            if (e.ColumnIndex == 6 && e.Value != null) // Columna de Estado
                            {
                                string estado = e.Value.ToString();
                                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                                
                                // Aplicar colores a toda la fila según el estado
                                switch (estado)
                                {
                                    case "Pendiente":
                                        e.CellStyle.BackColor = Color.FromArgb(255, 253, 205); // Amarillo claro
                                        e.CellStyle.ForeColor = Color.FromArgb(150, 125, 0); // Texto oscuro
                                        e.CellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                                        break;
                                    case "Confirmada":
                                        e.CellStyle.BackColor = Color.FromArgb(200, 250, 200); // Verde claro
                                        e.CellStyle.ForeColor = Color.FromArgb(0, 100, 0); // Texto verde oscuro
                                        e.CellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                                        break;
                                    case "Cancelada":
                                        e.CellStyle.BackColor = Color.FromArgb(255, 200, 200); // Rojo claro
                                        e.CellStyle.ForeColor = Color.FromArgb(150, 0, 0); // Texto rojo oscuro
                                        e.CellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                                        break;
                                    case "Completada":
                                        e.CellStyle.BackColor = Color.FromArgb(200, 220, 255); // Azul claro
                                        e.CellStyle.ForeColor = Color.FromArgb(0, 0, 150); // Texto azul oscuro
                                        e.CellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                                        break;
                                }
                            }
                            
                            // Resaltar fechas próximas (entradas en los próximos 3 días)
                            if (e.ColumnIndex == 3 && dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString() == "Confirmada")
                            {
                                if (e.Value != null && e.Value != DBNull.Value)
                                {
                                    DateTime fechaEntrada = Convert.ToDateTime(e.Value);
                                    DateTime hoy = DateTime.Today;
                                    
                                    if (fechaEntrada >= hoy && fechaEntrada <= hoy.AddDays(3))
                                    {
                                        // Resaltar entradas próximas
                                        e.CellStyle.BackColor = Color.FromArgb(255, 200, 100); // Naranja claro
                                        e.CellStyle.ForeColor = Color.FromArgb(150, 75, 0); // Texto naranja oscuro
                                        e.CellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                                    }
                                }
                            }
                        }
                    };
                    
                    // Agregar tooltip para mostrar información detallada al pasar el mouse
                    ToolTip toolTip = new ToolTip();
                    dataGridView1.CellMouseEnter += (s, e) =>
                    {
                        if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                        {
                            DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                            if (cell.Value != null && !string.IsNullOrEmpty(cell.Value.ToString()))
                            {
                                // Para columnas específicas, mostrar información adicional
                                if (e.ColumnIndex == 6) // Estado
                                {
                                    string estado = cell.Value.ToString();
                                    string mensaje = "";
                                    
                                    switch (estado)
                                    {
                                        case "Pendiente":
                                            mensaje = "Reserva pendiente de confirmación";
                                            break;
                                        case "Confirmada":
                                            mensaje = "Reserva confirmada y lista para check-in";
                                            break;
                                        case "Cancelada":
                                            mensaje = "Reserva cancelada";
                                            break;
                                        case "Completada":
                                            mensaje = "Reserva finalizada con éxito";
                                            break;
                                    }
                                    
                                    toolTip.SetToolTip(dataGridView1, mensaje);
                                }
                                else
                                {
                                    toolTip.SetToolTip(dataGridView1, cell.Value.ToString());
                                }
                            }
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
                Console.WriteLine("No hay datos para mostrar en la lista de reservas.");
                // El mensaje visual se maneja en MostrarMensajeNoHayDatos
            }
        }

        public static FormReservas GetInstance(Form parentContainer, string rolUsuario = "usuario")
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new FormReservas(rolUsuario);
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
